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

namespace DVLD_ViewTier.Licenses
{
    public partial class DetainLicense : Form
    {
        bool personSelected = false;
        public DetainLicense()
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
                    if (bool.Parse(data["IsDetained"]))
                    {
                        MessageBox.Show("License Is already detained", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnDetain.Enabled = false;
                    }
                    else
                    {
                        btnDetain.Enabled = true;
                    }
                }
                else
                {
                    personSelected = false;
                    MessageBox.Show("License Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDetaine_Click(object sender, EventArgs e)
        {
            if (personSelected && !string.IsNullOrWhiteSpace(tbFineFees.Text)) {
                if (LicenseController.detainLicense(int.Parse(lbLicenseID.Text), tbFineFees.Text))
                {
                    MessageBox.Show("License has been detained successfully");
                }
                else
                {
                    MessageBox.Show("Failed to detain the license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Person or Fess is not selected","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return; // Let it process  
            }

            // Check if the character is a digit or a decimal point  
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.')
            {
                // Allow only one decimal point  
                if (e.KeyChar == '.' && tbFineFees.Text.Contains("."))
                {
                    e.Handled = true; // Ignore further decimal points  
                }
            }
            else
            {
                e.Handled = true; // All other characters are ignored  
            }
        }
    }
}
