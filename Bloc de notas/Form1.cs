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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bloc_de_notas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += new DrawItemEventHandler(tabControl1_DrawItem);
            tabControl1.MouseDown += new MouseEventHandler(tabControl1_MouseDown);
            NewTab();
        }

        // Drawing the tab and the close button
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage tabPage = tabControl1.TabPages[e.Index];
            Rectangle tabRect = tabControl1.GetTabRect(e.Index);
            tabRect.Inflate(-2, -2);

            // Draw tab text
            e.Graphics.DrawString(tabPage.Text, e.Font, Brushes.Black, tabRect.X + 2, tabRect.Y + 2);

            // Draw close button
            Rectangle closeButton = new Rectangle(
                tabRect.Right - 15, // Position the close button near the right edge
                tabRect.Top + 4,    // Center vertically
                12, 12);            // Size of the close button

            e.Graphics.DrawRectangle(Pens.Black, closeButton);
            e.Graphics.DrawString("X", e.Font, Brushes.Black, closeButton.X, closeButton.Y - 2);
        }

        // Handling mouse clicks on the close button
        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                Rectangle tabRect = tabControl1.GetTabRect(i);
                Rectangle closeButton = new Rectangle(
                    tabRect.Right - 15,
                    tabRect.Top + 4,
                    12, 12);

                if (closeButton.Contains(e.Location))
                {
                    tabControl1.TabPages.RemoveAt(i);
                    break;
                }
            }
        }

        int tabCounter = 0;
        const string emptySpace = "     ";

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
            richTextBox.TextChanged += new System.EventHandler(textBox_TextChanged);

            tabPage.Controls.Add(richTextBox);
            tabPage.Location = new System.Drawing.Point(4, 22);
            tabPage.Name = "New File " + ++tabCounter;
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(792, 400);
            tabPage.TabIndex = 0;
            tabPage.Text = "New File " + tabCounter + emptySpace;
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
            richTextBox.TextChanged += new System.EventHandler(textBox_TextChanged);

            string fileName = Path.GetFileName(filePath);

            tabPage.Controls.Add(richTextBox);
            tabPage.Location = new System.Drawing.Point(4, 22);
            tabPage.Name = fileName;
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(792, 400);
            tabPage.TabIndex = 0;
            tabPage.Text = fileName + emptySpace;
            tabPage.UseVisualStyleBackColor = true;


            return tabPage;
        }

        public void NewTab ()
        {
            TabPage tabPage = CreateTab();

            tabControl1.Controls.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
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
                tabControl1.Controls.Add(tabPage);
                tabControl1.SelectedTab = tabPage;
            }
        }

        private void textBox_TextChanged (object sender, EventArgs e)
        {
            if (!(sender is RichTextBox)) return;

            RichTextBox tt = (RichTextBox)sender;

            int cursorPosition = tt.SelectionStart;

            // Reemplaza ":)" por el emoji de cara feliz
            tt.Text = tt.Text.Replace(":)", "😊");

            // Restaura la posición del cursor después de los cambios
            tt.SelectionStart = cursorPosition;
        }
    }
}
