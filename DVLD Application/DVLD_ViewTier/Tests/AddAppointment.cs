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

namespace DVLD_ViewTier.Tests
{
    public partial class AddAppointment : Form
    {
        int TestTypeID;
        int ApplicationID;
        public AddAppointment(int TestTypeID, int ApplicationID)
        {
            this.TestTypeID = TestTypeID;
            this.ApplicationID = ApplicationID;
            InitializeComponent();
        }
      
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int paidFees = 10;
            if (ApplicationController.saveTestAppointment(TestTypeID, dateTimePicker1.Value, ApplicationID, paidFees))
            {
                MessageBox.Show("Appointment Saved Successfully");
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to save the appointment" ,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void AddAppointment_Load(object sender, EventArgs e)
        {
            lbFees.Text = Helpers.getTestTypeFeesByID(TestTypeID).ToString();
        }
    }
}
