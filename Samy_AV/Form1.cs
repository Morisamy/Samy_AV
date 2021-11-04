using System;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Samy_AV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string getMD5FromFile(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var Stram = File.OpenRead(filePath))
                {
                    return BitConverter.ToString(md5.ComputeHash(Stram)).Replace("-", string.Empty).ToLower();
                }
            }
        }
        private void btnScan_Click(object sender, EventArgs e)
        {
            var md5Signs = File.ReadAllLines("MD5Base.txt");
            if (md5Signs.Contains(tbMD5.Text))
            {
                DialogResult = MessageBox.Show("This File is Infected!!", "File Scan Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbMD5.Text = "";
                tbFilepath.Text = "";
            }
            else
            {
                DialogResult = MessageBox.Show("This File is Clean!!", "File Scan Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbMD5.Text = "";
                tbFilepath.Text = "";
            }

            //var md5Signs = File.ReadAllLines("MD5Base.txt"); //var files = Directory.GetFiles(Application.ExecutablePath, "*.txt"); //List<Sample> list = new List<Sample>(); //string[] text = new string[9000]; //foreach (var item in files) //{ //    text = File.ReadAllLines(item); //} //if (text.Contains(tbMD5.Text)) //{ //    DialogResult = MessageBox.Show("This File is Infected!!", "File Scan Result", MessageBoxButtons.OK, MessageBoxIcon.Error); //    tbMD5.Text = ""; //    tbFilepath.Text = ""; //} //else //{ //    DialogResult = MessageBox.Show("This File is Clean!!", "File Scan Result", MessageBoxButtons.OK, MessageBoxIcon.Information); //    tbMD5.Text = ""; //    tbFilepath.Text = ""; //}
        }

        private void browseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "TextFiles | *.txt";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    tbFilepath.Text = ofd.FileName;
                    tbMD5.Text = getMD5FromFile(ofd.FileName);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("This AntiVirus was Created by Samy!", "SamyAV", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    //internal class Sample
    //{
    //}
}
