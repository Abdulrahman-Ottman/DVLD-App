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

namespace DVLD_ViewTier.Applications.Applications_Types
{
    public partial class EditApplicationsTypes : Form
    {
        public delegate void ApplicationTypes_DataUpdated(int id , string title , string fees);
        public event ApplicationTypes_DataUpdated DataUpdated;
        int TypeID = -1;
        public EditApplicationsTypes(int id)
        {
            TypeID = id;
            InitializeComponent();
        }

        private void EditApplicationsTypes_Load(object sender, EventArgs e)
        {
            Dictionary<string , string> data = ApplicationsTypesController.FindTypeByID(TypeID);
            lbID.Text = TypeID.ToString();
            tbTitle.Text = data["title"];
            tbFees.Text = data["fees"];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                if (ApplicationsTypesController.UpdateType(TypeID, tbTitle.Text, tbFees.Text))
                {
                    MessageBox.Show("Type updated successfully");
                    DataUpdated?.Invoke(TypeID , tbTitle.Text , tbFees.Text);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update type");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateForm()
        {
            bool isValid = true;

            isValid &= Helpers.ValidateRequiredTextBox(tbTitle,"Title field is required");
            isValid &= Helpers.ValidateRequiredTextBox(tbFees,"Fees field is required");

            if(!Helpers.ValidateUnique("ApplicationTypes" , "ApplicationTypeTitle" , tbTitle.Text , "ApplicationTypeID" , TypeID)){
                isValid = false;
                Helpers.errorProvider.SetError(tbTitle , "Title must be unique");
            }

            return isValid;
        }
    }
}
