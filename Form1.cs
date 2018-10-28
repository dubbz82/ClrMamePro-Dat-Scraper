using System;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace dat_crc_scraper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            if (d.ShowDialog() == DialogResult.OK)
            {
                
                string path = d.FileName;
                if (path.EndsWith(".dat"))
                {
                    StringBuilder sb = new StringBuilder();
                    XmlDocument xDoc = new XmlDocument();

                    try
                    {
                        xDoc.Load(path);
                        foreach (XmlNode node in xDoc.DocumentElement.ChildNodes)
                        {
                            string gameName = "";
                            string md5 = "";
                            if (node.Name == "game")
                            {
                                gameName = node.Attributes["name"].Value;
                                //check children elements for "rom"
                                foreach (XmlNode rm in node.ChildNodes)
                                {



                                    if (rm.Name == "rom")
                                    {
                                        md5 = rm.Attributes["md5"].Value;
                                    }
                                }
                                sb.AppendLine(gameName + "\n" + md5);
                                sb.AppendLine("");
                            }
                        }
                        if (System.IO.File.Exists("out.txt"))
                        {
                            DialogResult r = MessageBox.Show("File Already Exists!  Do you want to Continue?", "File Exists", MessageBoxButtons.YesNo);
                            if (r == DialogResult.Yes)
                            {
                                System.IO.File.WriteAllText("out.txt", sb.ToString());
                                MessageBox.Show("Success!");
                            }

                        }
                        else
                        {
                            System.IO.File.WriteAllText("out.txt", sb.ToString());
                            MessageBox.Show("Success!");
                        }
                        
                    }
                    catch
                    {
                        MessageBox.Show("Failed to parse XML!");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid File!");
                }
                
            }
        }
    }
}
