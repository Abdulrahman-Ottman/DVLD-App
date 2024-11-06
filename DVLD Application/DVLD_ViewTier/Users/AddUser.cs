using DVLD_BusinessTier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_ViewTier.Users
{
    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateForm()
        {
            bool isValid = true;
            isValid &= Helpers.ValidateRequiredTextBox(tbUserName , "User Name Field is required");
            isValid &= Helpers.ValidateRequiredTextBox(tbPassword , "Password Field is required");
            isValid &= Helpers.ValidateRequiredTextBox(tbConfirmPassword , "Confirm Password Field is required");

            if(!Helpers.ValidateUnique("Users" , "UserName" , tbUserName.Text))
            {
                isValid = false;
                Helpers.errorProvider.SetError(tbUserName , "User Name must be unique");
            }
            if(tbPassword.Text != tbConfirmPassword.Text)
            {
                isValid = false;
                Helpers.errorProvider.SetError(tbConfirmPassword , "Password and Confirm Password does not match");
                Helpers.errorProvider.SetError(tbPassword , "Password and Confirm Password does not match");
            }
            return isValid;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                if (UserController.AddUser(tbUserName.Text, tbPassword.Text, cbIsActive.Checked) != -1)
                {
                    MessageBox.Show("User Added Successfully");
                }
                else
                {
                    MessageBox.Show("Failed to add the user");
                }
            }
        }


    }
}
