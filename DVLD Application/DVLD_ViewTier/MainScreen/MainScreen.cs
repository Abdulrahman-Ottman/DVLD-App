using DVLD_BusinessTier;
using DVLD_ViewTier.Applications.Applications_Types;
using DVLD_ViewTier.Applications.LocalLicenseApplications;
using DVLD_ViewTier.Drivers;
using DVLD_ViewTier.Licenses;
using DVLD_ViewTier.Licenses.InternationalLicense;
using DVLD_ViewTier.Licenses.LocalLicense;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_ViewTier.MainScreen
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void tsmPeople_Click(object sender, EventArgs e)
        {
            People.PeopleManagement peopleManagement = new People.PeopleManagement();
            peopleManagement.Show();
        }

        private void tsmUsers_Click(object sender, EventArgs e)
        {
            Users.UsersManagement usersManagement = new Users.UsersManagement();
            usersManagement.Show();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Users.ChangePassword changePassword = new Users.ChangePassword(UserController.GetCurrentUserID());
            changePassword.ShowDialog();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();

            Application.Restart();
        }

        private void manageApplicationsTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Applications.Applications_Types.ShowApplicationsTypes showApplicationsTypes = new ShowApplicationsTypes();
            showApplicationsTypes.Show();
        }

        private void manageTeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tests.ShowTestsTypes showTestsTypes = new Tests.ShowTestsTypes();
            showTestsTypes.Show();
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {

        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLocalLicensApplication addLocalLicensApplication = new AddLocalLicensApplication();
            addLocalLicensApplication.Show();
        }

        private void localDrigingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLocalDrivingLicenseApplications localApplications = new ShowLocalDrivingLicenseApplications();
            localApplications.Show();
        }

        private void tsmDrivers_Click(object sender, EventArgs e)
        {
            ShowDrivers showDrivers = new ShowDrivers();
            showDrivers.Show();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddInternationalLicense addInternational = new AddInternationalLicense();   
            addInternational.ShowDialog();
        }

        private void internationalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowInternationalLicenseApplications internationalLicenseApplications = new ShowInternationalLicenseApplications(); 
            internationalLicenseApplications.Show();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenewLicense renewLicense = new RenewLicense();
            renewLicense.ShowDialog();  
        }

        private void replacmentToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LicenseReplacment licenseReplacment = new LicenseReplacment();
            licenseReplacment.ShowDialog();
        }

        private void detaineLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetainLicense detainLicense = new DetainLicense();
            detainLicense.ShowDialog();
        }

        private void releToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseLicense releaseLicense = new ReleaseLicense();
            releaseLicense.ShowDialog();
        }
    }
}
