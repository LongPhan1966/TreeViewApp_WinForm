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

namespace TreeViewApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            InitTree();
        }

        private void InitTree()
        {
            string[] dv = Directory.GetLogicalDrives();
            TreeNode node = null;
            foreach(string d in dv)
            {
                node = new TreeNode(d);
                treeView1.Nodes.Add(node);
                node.Nodes.Add("Folder");
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            node.Nodes.Clear();
            node.ImageIndex = 1;
            try
            {
                foreach(string dir in Directory.GetDirectories(node.FullPath))
                {
                    TreeNode n = node.Nodes.Add(Path.GetFileName(dir));
                    n.Nodes.Add("folder");
                }
            }
            catch
            {

            }
        }

        private void treeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.ImageIndex = 0;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                pictureBox1.Image = null;
                string[] arrFiles = Directory.GetFiles(e.Node.FullPath);
                flowLayoutPanel1.Controls.Clear();
                foreach(string file in arrFiles)
                {
                    if(file.ToLower().EndsWith(".pnj")||
                        file.ToLower().EndsWith(".jpg") ||
                        file.ToLower().EndsWith(".webp"))
                    {
                        PictureBox pic = new PictureBox();
                        pic.SizeMode = PictureBoxSizeMode.StretchImage;
                        pic.Image = Image.FromFile(file);
                        
                        pic.Cursor = Cursors.Hand;
                        flowLayoutPanel1.Controls.Add(pic);

                        pic.Click += new EventHandler(picBoxclick);
                    }
                }
            }
            catch
            {

            }
        }
        private void picBoxclick(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            pictureBox1.Image = pic.Image;
        }
    }
}
