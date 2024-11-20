using DVLD_BusinessTier;
using DVLD_ViewTier.Applications.Applications_Types;
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
    }
}
