using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace FilterImg
{
    public partial class FolderRead : Form
    {
        public FolderRead()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private void FolderRead_Load(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                FolderPath.Text = folderBrowserDialog1.SelectedPath;
                
                listBox1.Items.Clear();
                string[] imgFiles = Directory.GetFiles(FolderPath.Text, "*jpg", SearchOption.TopDirectoryOnly);

                foreach (string jpgFile in imgFiles)
                {
                    listBox1.Items.Add(jpgFile);
                }
                //for (int i = 0; i < imgFiles.Length; i++)
                //{
                //    listBox1.Items.Add(imgFiles[i]);
                //}
                if (listBox1.Items.Count > 0)
                {
                    listBox1.SetSelected(0, true);
                    pictureBox1.Image = new Bitmap(listBox1.SelectedItem.ToString());
                }
                else
                {
                    MessageBox.Show("JPG Files Not Available");
                }
            }
        }

        private void FolderPath_TextChanged(object sender, EventArgs e)
        {
            string Path = FolderPath.Text;
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                // Get the currently selected item in the ListBox.
                pictureBox1.Image = new Bitmap(listBox1.SelectedItem.ToString());
            }
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex+1 < listBox1.Items.Count)
            {
                listBox1.SetSelected(listBox1.SelectedIndex + 1, true);
            }
            //else
            //{
               
            //    Next.Enabled = false;
            //}
        }

        private void Prev_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex - 1 >= 0)
            {
                listBox1.SetSelected(listBox1.SelectedIndex - 1, true);
            }
        }

        private void Select_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                string destPath = FolderPath.Text + @"\SelectedItems\";
                if (Directory.Exists(destPath))
                {
                    File.Copy(listBox1.SelectedItem.ToString(), $"{ destPath}{ Path.GetFileName(listBox1.SelectedItem.ToString())}");

                    if (listBox1.Items.Count == 1)
                    {
                        listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    }

                    if (listBox1.SelectedIndex + 1 < listBox1.Items.Count)
                    {
                        listBox1.SetSelected(listBox1.SelectedIndex + 1, true);

                        listBox1.Items.RemoveAt(listBox1.SelectedIndex - 1);
                    }
                    else if(listBox1.SelectedIndex-1 < listBox1.Items.Count && listBox1.SelectedIndex-1 >= 0)
                    {
                       
                        listBox1.SetSelected(listBox1.SelectedIndex - 1, true);

                        listBox1.Items.RemoveAt(listBox1.SelectedIndex + 1);
                    }
                }
                else
                {
                    Directory.CreateDirectory(FolderPath.Text + @"\SelectedItems");
                    File.Copy(listBox1.SelectedItem.ToString(), $"{ destPath}{ Path.GetFileName(listBox1.SelectedItem.ToString())}");

                    if (listBox1.SelectedIndex + 1 < listBox1.Items.Count)
                    {
                        listBox1.SetSelected(listBox1.SelectedIndex + 1, true);

                        listBox1.Items.RemoveAt(listBox1.SelectedIndex - 1);
                    }
                    else
                    {
                        listBox1.SetSelected(listBox1.SelectedIndex - 1, true);

                        listBox1.Items.RemoveAt(listBox1.SelectedIndex + 1);
                    }
                }
            }
        }

        private void Next_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.KeyCode.ToString());
            //if(e.KeyCode==Keys.Enter)
            //{
            //    MessageBox.Show("D Pressed");
            //    //if (listBox1.SelectedIndex + 1 < listBox1.Items.Count)
            //    //{
            //    //    listBox1.SetSelected(listBox1.SelectedIndex + 1, true);
            //    //}
            //}
        }

        private void Next_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void Next_KeyPress(object sender, KeyPressEventArgs e)
        {
            //MessageBox.Show(e.KeyChar.ToString());
            //if(e.KeyChar==(char)Keys.A)
            //{
            //    if (listBox1.SelectedIndex - 1 >= 0)
            //    {
            //        listBox1.SetSelected(listBox1.SelectedIndex - 1, true);
            //    }
            //}
        }

        private void FolderRead_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.D)
            {
                //MessageBox.Show("A");
                Next.PerformClick();
            }
            if(e.KeyCode==Keys.A)
            {
                Prev.PerformClick();
            }
            if (e.KeyCode == Keys.W)
            {
                Select.PerformClick();
            }
        }
    }
}
