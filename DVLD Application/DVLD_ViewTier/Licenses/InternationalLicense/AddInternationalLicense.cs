using DVLD_BusinessTier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_ViewTier.Licenses.InternationalLicense
{
    public partial class AddInternationalLicense : Form
    {
        bool personSelected = false;    
        public AddInternationalLicense()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox1.Text.Length > 0)
            {
                Dictionary<string, string> data = LicenseController.getLicenseInfoByLicenseID(int.Parse(textBox1.Text));
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
                }
            }
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if(personSelected)
            {
                if(DateTime.Parse(lbExpirationDate.Text) < DateTime.Now)
                {
                    MessageBox.Show("This License has bees expired","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                if (LicenseController.issueInternationalLicense(int.Parse(lbLicenseID.Text), int.Parse(lbDriverID.Text)))
                {
                    MessageBox.Show("International License Issued Successfully");
                }
                else
                {
                    MessageBox.Show("Failed to Issue the International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
