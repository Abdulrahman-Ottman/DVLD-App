using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DVLD_BusinessTier;

namespace DVLD_ViewTier
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            btnClose.FlatAppearance.BorderSize = 0; // Remove border
            btnClose.TabStop = false; // Disable tab focus
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
            if (LoginController.AttempLogin(userName, password))
            {
                MessageBox.Show("Loged In");
            }
            else
            {
                MessageBox.Show("falid to Loged In");
            }
        }
    }
}
