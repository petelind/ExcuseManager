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
        public MainForm()
        {
            InitializeComponent();
            foreach (Control control in this.Controls)
            {
                control.Enabled = false;
            }
            buttonChooseFolder.Enabled = true;

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
    }
}
