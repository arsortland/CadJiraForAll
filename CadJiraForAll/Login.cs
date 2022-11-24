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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.label1.Text = CadJira.UserNameInSystem();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //this.label1.Text = CadJira.UserNameInSystem();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CadJira.loginpw = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
