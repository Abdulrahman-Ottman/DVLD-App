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
using DVLD_BusinessTier;

namespace DVLD_ViewTier.Licenses.LocalLicense
{
    public partial class IssueLocalDrivingLicense : Form
    {
        int applicationID;
        public IssueLocalDrivingLicense(int applicationID)
        {
            this.applicationID = applicationID;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ApplicationController.IssueLocalDrivingLicense(applicationID, textBox1.Text))
            {
                MessageBox.Show("License has been issued successfully");
            }
            else
            {
                MessageBox.Show("Failed to issue license");
            }
            this.Close();
        }
    }
}
