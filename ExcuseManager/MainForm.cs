using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ExcuseManager
{
    public partial class MainForm : Form
    {
        string currentFolder = Directory.GetCurrentDirectory() + @"\Excuses";
        Excuse CurrentExcuse;

        public MainForm()
        {
            InitializeComponent();
            foreach (Control control in this.Controls)
            {
                control.Enabled = false;
            }
            buttonChooseFolder.Enabled = true;
            CurrentExcuse = new Excuse();

        }

        private void buttonChooseFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.SelectedPath = currentFolder;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (Control control in this.Controls)
                {
                    control.Enabled = true;
                    currentFolder = folderBrowserDialog1.SelectedPath;
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Save an excuse";
            saveFileDialog1.Filter = "Excuse files | *.excuse ";
            saveFileDialog1.DefaultExt = "excuse";
            saveFileDialog1.InitialDirectory = currentFolder;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                CurrentExcuse.Save(saveFileDialog1.FileName, DateTime.Now);
            }

        }
    }
}
