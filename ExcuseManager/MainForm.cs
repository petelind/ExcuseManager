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
        bool isChanged = false;

        public MainForm()
        {
            InitializeComponent();
            foreach (Control control in this.Controls)
            {
                control.Enabled = false;
            }
            buttonChooseFolder.Enabled = true;
            CurrentExcuse = new Excuse();
            CurrentExcuse.LastUsed = dateTimePicker1.Value;

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
            if (!isChanged)
            {
                MessageBox.Show("Nothing to save, enter or edit data!");
                return;
            }

            saveFileDialog1.Title = "Save an excuse";
            saveFileDialog1.Filter = "Excuse files | *.excuse ";
            saveFileDialog1.DefaultExt = "excuse";
            saveFileDialog1.InitialDirectory = currentFolder;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = CurrentExcuse.Save(saveFileDialog1.FileName, DateTime.Now);
                isChanged = false;
                this.Text = "Excuse Manager";
                MessageBox.Show("Excuse Successfully Saved!");
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            isChanged = true;
            this.Text = "Excuse Manager - Unsaved Changes";
            CurrentExcuse.Description = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            isChanged = true;
            this.Text = "Excuse Manager - Unsaved Changes";
            CurrentExcuse.Results = textBox2.Text;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            isChanged = true;
            this.Text = "Excuse Manager - Unsaved Changes";
            CurrentExcuse.LastUsed = dateTimePicker1.Value;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Choose excuse file to load...";
            openFileDialog1.Filter = "Excuse files | *.excuse ";
            openFileDialog1.InitialDirectory = currentFolder;
            openFileDialog1.CheckFileExists = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                {
                    CurrentExcuse.Description = sr.ReadLine();
                    CurrentExcuse.Results = sr.ReadLine();
                    CurrentExcuse.LastUsed = Convert.ToDateTime(sr.ReadLine());

                    textBox1.Text = CurrentExcuse.Description;
                    textBox2.Text = CurrentExcuse.Results;
                    dateTimePicker1.Value = CurrentExcuse.LastUsed;
                    textBox3.Text = Convert.ToString(File.GetLastAccessTime(openFileDialog1.FileName));

                }
            }

        }
    }
}
