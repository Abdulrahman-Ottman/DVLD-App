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
    public partial class TakeTest : Form
    {
        int TestAppointmentID;
        public TakeTest(int TestAppointmentID)
        {
            this.TestAppointmentID = TestAppointmentID;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool result = rbPassed.Checked ? true : false;
            string notes=textBox1.Text;
            if (ApplicationController.TakeTest(TestAppointmentID, result, notes))
            {
                MessageBox.Show("Test Passed Successfully");
            }
            else
            {
                MessageBox.Show("Failed in the Test");
            }
            this.Close();
        }

        private void TakeTest_Load(object sender, EventArgs e)
        {
            rbPassed.Checked = true;
        }
    }
}
