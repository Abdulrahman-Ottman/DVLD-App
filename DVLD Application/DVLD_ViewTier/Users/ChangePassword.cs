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
    public partial class ChangePassword : Form
    {
        int UserID = -1;

        public ChangePassword(int id)
        {
            UserID = id;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> UserData = UserController.FindUserByID(UserID);

            if (ValidateForm(UserData["Password"]))
            {
                if(UserController.UpdateUser(UserID , UserData["UserName"] , tbNewPassword.Text , bool.Parse(UserData["IsActive"])))
                {
                    MessageBox.Show("Password Updated Successfully");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update password");
                }
            }
        }
        private bool ValidateForm(string currentPassword)
        {

            bool isValid = true;
            isValid &= (UserID != -1);
            isValid &= Helpers.ValidateRequiredTextBox(tbCurrentPassword , "Current Password field is required");
            isValid &= Helpers.ValidateRequiredTextBox(tbNewPassword , "New Password field is required");
            isValid &= Helpers.ValidateRequiredTextBox(tbNewPassword , "Confirm Password field is required");

            if(!(tbCurrentPassword.Text == currentPassword))
            {
                isValid = false;
                Helpers.errorProvider.SetError(tbCurrentPassword , "Incorrect Password");
            }

            if(!(tbNewPassword.Text == tbConfirmPassword.Text))
            {
                isValid = false;
                Helpers.errorProvider.SetError(tbNewPassword, "Password and Confirm Password doesn't match");
                Helpers.errorProvider.SetError(tbConfirmPassword, "Password and Confirm Password doesn't match");
            }
            return isValid;
        }
    }
}
