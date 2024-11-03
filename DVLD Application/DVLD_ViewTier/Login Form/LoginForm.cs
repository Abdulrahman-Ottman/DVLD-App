using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DVLD_BusinessTier;

namespace DVLD_ViewTier
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            btnClose.FlatAppearance.BorderSize = 0; // Remove border
            btnClose.TabStop = false; // Disable tab focus
            tbUserName.Text = "Abdulrahman";
            tbPassword.Text = "asdfasdf";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = tbUserName.Text;
            string password = tbPassword.Text;
            if (LoginController.AttemptLogin(userName, password))
            {
                    this.DialogResult = DialogResult.OK; // Sets the result to OK  
                    this.Close(); // Closes the login form  
            }
            else
            {
                MessageBox.Show("Error : Failed to login");
            }
        }
    }
}
