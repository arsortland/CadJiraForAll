using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadJiraForAll
{
    public partial class DescUserInput : Form
    {
        public DescUserInput()
        {
            InitializeComponent();
            richTextBox1.Text = CadJira.richtext;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {
            CadJira.richtext = richTextBox1.Text;
            this.Close();
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
