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

namespace DVLD_ViewTier.Licenses.LocalLicense
{
    public partial class ShowDrivingLicenseInfo : Form
    {
        int applicationID;
        public ShowDrivingLicenseInfo(int applicationID)
        {
            this.applicationID = applicationID;
            InitializeComponent();
        }



        
   

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowDrivingLicenseInfo_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> data = LicenseController.getLicenseInfo(applicationID);
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
                pictureBox1.Image = Image.FromFile(data["ImagePath"]);
            }
        }
    }
}
