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
            saveFileDialog1.Filter = "Excuse files | *.excuse";
            saveFileDialog1.DefaultExt = "excuse";
            saveFileDialog1.InitialDirectory = currentFolder;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                isChanged = false;
                UpdateForm();
                MessageBox.Show("Excuse Successfully Saved!");
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CurrentExcuse.Description = textBox1.Text;
            isChanged = true;
            UpdateForm();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            CurrentExcuse.Results = textBox2.Text;
            isChanged = true;
            UpdateForm();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            CurrentExcuse.LastUsed = dateTimePicker1.Value;
            isChanged = true;
            UpdateForm();

        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Choose excuse file to load...";
            openFileDialog1.Filter = "Excuse files | *.excuse";
            openFileDialog1.InitialDirectory = currentFolder;
            openFileDialog1.CheckFileExists = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                    CurrentExcuse.Load(openFileDialog1.FileName);
                // TODO: Refactor: Create UpdateForm() which updates all fields
                UpdateForm();

                
            }

        }

        private void UpdateForm()
        {
            textBox1.Text = CurrentExcuse.Description;
            textBox2.Text = CurrentExcuse.Results;
            dateTimePicker1.Value = CurrentExcuse.LastUsed;
            textBox3.Text = Convert.ToString(File.GetLastAccessTime(openFileDialog1.FileName));
            if (isChanged) this.Text = "Excuse Manager - Unsaved Changes";
            else this.Text = "Excuse Manager";

        }

        private void buttonRandom_Click(object sender, EventArgs e)
        {
            string[] excuseFiles;
            excuseFiles = Directory.GetFiles(currentFolder, "*.excuse", SearchOption.AllDirectories);
            if (excuseFiles.Count() == 0)
            {
                MessageBox.Show("No excuses! Do the thing finally, or create new excuse!");
                return;
            }
            else
            {
                Random random = new Random();
                int numOfExcuseToLoad = random.Next(0, excuseFiles.Count()-1);
                string pathToExcuse = excuseFiles[numOfExcuseToLoad];
                CurrentExcuse.Load(pathToExcuse);
                UpdateForm();

            }


        }
    }
}
