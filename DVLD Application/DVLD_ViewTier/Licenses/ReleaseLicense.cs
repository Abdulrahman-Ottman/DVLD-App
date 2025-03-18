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
    public partial class ReleaseLicense : Form
    {
        bool personSelected  = false;
        public ReleaseLicense()
        {
            InitializeComponent();
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
                    if (!bool.Parse(data["IsDetained"]))
                    {
                        MessageBox.Show("License Is not detained", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnRelease.Enabled = false;
                    }
                    else
                    {
                        btnRelease.Enabled = true;
                    }
                }
                else
                {
                    personSelected = false;
                    MessageBox.Show("License Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (personSelected) {
                if (LicenseController.releaseLicense(int.Parse(lbLicenseID.Text), int.Parse(lbDriverID.Text)))
                {
                    MessageBox.Show("License has been released successfully");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to release the license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("No person selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
