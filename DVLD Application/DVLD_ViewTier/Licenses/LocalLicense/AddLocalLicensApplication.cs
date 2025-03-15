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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD_ViewTier.Licenses.LocalLicense
{
    public partial class AddLocalLicensApplication : Form
    {
        public AddLocalLicensApplication()
        {
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void AddLocalLicensApplication_Load(object sender, EventArgs e)
        {
            lbApplicationDate.Text = DateTime.Now.ToShortDateString();
            lbCreatedBy.Text = UserController.GetCurrentUserName();
            lbApplicationFees.Text = Helpers.GetApplicationTypeFeesByID(1);

            DataTable dataTable = LicenseClassesController.getAllLicenseClasses();
         
            cmbLicenseClasses.Items.Clear();
            Dictionary<string, string> classes = new Dictionary<string, string>();

            foreach (DataRow row in dataTable.Rows)
            {
                classes.Add(row["LicenseClassID"].ToString(), row["ClassName"].ToString());
            }
            cmbLicenseClasses.DataSource = new BindingSource(classes, null);
            cmbLicenseClasses.DisplayMember = "Value";
            cmbLicenseClasses.ValueMember = "Key";
            cmbLicenseClasses.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string personID = personInfoViewerWithFilters1.getSelectedPersonID();
            if (string.IsNullOrEmpty(personID))
            {
                MessageBox.Show("no person selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Dictionary<string,string> data = new Dictionary<string,string>();
            data.Add("PersonID",personInfoViewerWithFilters1.getSelectedPersonID());
            data.Add("LicenseClassID",cmbLicenseClasses.SelectedValue.ToString());
            data.Add("ApplicationDate", lbApplicationDate.Text);
            data.Add("UserID", UserController.GetCurrentUserID().ToString());

            int applicationID = ApplicationController.addLocalDrivingLicenseApplication(data);
           
            if (applicationID > 0) {
                lbApplicationID.Text = applicationID.ToString();
                MessageBox.Show("Application placed successfully");
            }
            else
            {
                MessageBox.Show("Failed to place the application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
