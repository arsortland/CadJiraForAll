using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace CadJiraForAll
{
    public partial class FormChoice : Form
    {
        public FormChoice()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void FormChoice_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CadJira.redm_or_gcs = "redm";
            //MessageBox.Show(CadJira.redm_or_gcs);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CadJira.redm_or_gcs = "gcs";
            //MessageBox.Show(CadJira.redm_or_gcs);
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://wiki.nov.com/pages/viewpage.action?pageId=187802228") { UseShellExecute = true });
            linkLabel1.LinkVisited = true;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://wiki.nov.com/pages/viewpage.action?pageId=157108563") { UseShellExecute = true });
            linkLabel2.LinkVisited = true;
        }
    }
}
