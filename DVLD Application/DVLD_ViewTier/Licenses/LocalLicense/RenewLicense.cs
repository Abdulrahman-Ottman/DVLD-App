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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_ViewTier.Licenses.LocalLicense
{
    public partial class RenewLicense : Form
    {
        bool personSelected = false;    
        public RenewLicense()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(tbLocalLicenseID.Text != null && tbLocalLicenseID.Text.Length > 0)
            {
                Dictionary<string, string> data = LicenseController.getLicenseInfoByLicenseID(int.Parse(tbLocalLicenseID.Text),null,true);
                if (data.Count > 0 && DateTime.Parse(data["ExpirationDate"]) < DateTime.Now)
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
                }
            }
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (personSelected) {
                if (LicenseController.renewLicense(int.Parse(tbLocalLicenseID.Text), int.Parse(lbDriverID.Text))){
                    MessageBox.Show("License Has been renewed successfully");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to renew the license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
