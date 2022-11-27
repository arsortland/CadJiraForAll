using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CadJiraForAll
{
    public partial class DescUserInput : Form
    {
        public DescUserInput()
        {
            InitializeComponent();
            richTextBox1.Text = CadJira.richtext;
            toolStripProgressBar1.Visible = false;
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        public async void button1_Click(object sender, EventArgs e)
        {
            CadJira.richtext = richTextBox1.Text;
            toolStripProgressBar1.Visible = true;
            await CadJira.API_Request(CadJira.redm_or_gcs);
            toolStripProgressBar1.Visible = false;
            this.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void DescUserInput_Load(object sender, EventArgs e)
        {

        }

        public void toolStripProgressBar1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
