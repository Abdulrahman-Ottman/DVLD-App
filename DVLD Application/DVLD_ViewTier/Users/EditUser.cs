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
    public partial class EditUser : Form
    {
        Dictionary<string, string> userData;
        int UserIdToEdit = -1;
        private enum enMode
        {
            Add = 0,
            Update = 1,
        }
        private enMode mode = enMode.Add;
        public EditUser(int id)
        {
            InitializeComponent();
            UserIdToEdit = id;
            userData = UserController.FindUserByID(id);
            tbNewUserName.Text = userData["UserName"];
            cbIsActive.Checked = bool.Parse(userData["IsActive"]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool validationResult = (mode == enMode.Update) ? ValidateForm("UserID" , UserIdToEdit) : ValidateForm();
                if (validationResult)
                {
                    if (UserController.UpdateUser(int.Parse(userData["UserID"]), tbNewUserName.Text, userData["Password"], cbIsActive.Checked))
                    {
                        MessageBox.Show("User Updated Successfully");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update user");
                    }
                }
        }
        private bool ValidateForm(string primaryKeyColumn = null , int? id = null)
        {
            bool isValid = true;

            isValid &= Helpers.ValidateRequiredTextBox(tbNewUserName, "New UserName is required");

            if (!Helpers.ValidateUnique("Users", "UserName", tbNewUserName.Text, primaryKeyColumn, id)) {
                isValid = false;
                Helpers.errorProvider.SetError(tbNewUserName , "User Name must be unique");
            }
        
            return isValid;
        }


    }
       
    }

