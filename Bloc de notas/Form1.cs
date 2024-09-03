using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bloc_de_notas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int tabCounter = 0;

        public TabPage CreateTab()
        {
            TabPage tabPage = new TabPage();
            RichTextBox richTextBox = new RichTextBox();

            richTextBox.AcceptsTab = true;
            richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            richTextBox.Location = new System.Drawing.Point(3, 3);
            //richTextBox.Name = "richTextBox1";
            richTextBox.Size = new System.Drawing.Size(786, 394);
            richTextBox.TabIndex = 0;
            richTextBox.Text = "";

            tabPage.Controls.Add(richTextBox);
            tabPage.Location = new System.Drawing.Point(4, 22);
            tabPage.Name = "New File " + ++tabCounter;
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(792, 400);
            tabPage.TabIndex = 0;
            tabPage.Text = "New File " + tabCounter;
            tabPage.UseVisualStyleBackColor = true;

            return tabPage;
        }

        public TabPage CreateTab(string filePath)
        {
            TabPage tabPage = new TabPage();
            RichTextBox richTextBox = new RichTextBox();

            richTextBox.AcceptsTab = true;
            richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            richTextBox.Location = new System.Drawing.Point(3, 3);
            //richTextBox.Name = "richTextBox1";
            richTextBox.Size = new System.Drawing.Size(786, 394);
            richTextBox.TabIndex = 0;
            richTextBox.Text = File.ReadAllText(filePath);

            string fileName = Path.GetFileName(filePath);

            tabPage.Controls.Add(richTextBox);
            tabPage.Location = new System.Drawing.Point(4, 22);
            tabPage.Name = fileName;
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(792, 400);
            tabPage.TabIndex = 0;
            tabPage.Text = fileName;
            tabPage.UseVisualStyleBackColor = true;

            return tabPage;
        }

        public void NewTab ()
        {
            TabPage tabPage = CreateTab();

            this.tabControl1.Controls.Add(tabPage);
        }

        private void nuevoArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewTab();
        }

        private void abrirArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos|*.*";

            if (fileDialog.ShowDialog() != DialogResult.OK) return;


            foreach (string filename in fileDialog.FileNames)
            {
                TabPage tabPage = CreateTab(filename);
                this.tabControl1.Controls.Add(tabPage);
            }
        }
    }
}
