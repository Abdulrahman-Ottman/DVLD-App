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

namespace DVLD_ViewTier.Licenses
{
  
    public partial class LicenseReplacment : Form
    {
        bool personSelected = false;
        public LicenseReplacment()
        {
            InitializeComponent();
            rbDamaged.Checked = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (tbLocalLicenseID.Text != null && tbLocalLicenseID.Text.Length > 0)
            {
                Dictionary<string, string> data = LicenseController.getLicenseInfoByLicenseID(int.Parse(tbLocalLicenseID.Text), null, true);
                if (data.Count > 0)
                {

                    lbClass.Text = data["Class"];
                    lbName.Text = data["Name"];
                    lbLicenseID.Text = data["LicenseID"];
                    lbNationalNo.Text = data["NationalNo"];
                    lbNotes.Text = data["Notes"];
                    lbGender.Text = Helpers.ConvertToGenderName(int.Parse(data["Gender"]));
                    lbIssueDate.Text = DateTime.Parse(data["IssueDate"]).ToShortDateString();
                    lbExpirationDate.Text = DateTime.Parse(data["ExpirationDate"]).ToShortDateString();
                    lbDateOfBirth.Text = DateTime.Parse(data["DateOfBirth"]).ToShortDateString();
                    lbIssueReason.Text = data["IssueReason"];
                    lbIsActive.Text = bool.Parse(data["IsActive"]).ToString();
                    lbDriverID.Text = data["DriverID"];
                    lbIsDetained.Text = bool.Parse(data["IsDetained"]).ToString();
                    pictureBox1.Image = System.Drawing.Image.FromFile(data["ImagePath"]);
                    personSelected = true;
                }
                else
                {
                    personSelected = false;
                    MessageBox.Show("License Not Found");
                }
            }
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (personSelected) {
                bool result=false;
                if (rbDamaged.Checked) {
                    result = LicenseController.replaceLicense(int.Parse(lbLicenseID.Text),int.Parse(lbDriverID.Text),0);
                }
                else
                {
                    result = LicenseController.replaceLicense(int.Parse(lbLicenseID.Text), int.Parse(lbDriverID.Text), 1);
                }

                if (result) {
                    MessageBox.Show("License Replaced successfully");
                }
                else
                {
                    MessageBox.Show("Failed to replace the license","Error",MessageBoxButtons.OK,MessageBoxIcon.Error
                        );
                }
            }
        }
    }
}
