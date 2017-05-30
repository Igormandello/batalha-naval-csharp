using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace batalha_naval
{
    public partial class UserForm : Form
    {
        public String User
        {
            get { return textBox1.Text.Trim(); }
        }

        public UserForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Digite algum usuário!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
