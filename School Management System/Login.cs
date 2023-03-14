using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace School_Management_System
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            txtUserEmail.Text = "";
            txtUserPassword.Text = "";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtUserEmail.Text == "" || txtUserPassword.Text == "")
            {
                MessageBox.Show("Username or Password is empty", "Login",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(txtUserEmail.Text == "Admin" && txtUserPassword.Text=="1234")
            {
                MainMenu mObj = new MainMenu();
                mObj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Username or Password");
                txtUserEmail.Text = "";
                txtUserPassword.Text = "";
            }
        }
    }
}
