using System;
using System.Data;
using System.Drawing;
using DVLD_BusinessTier;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_ViewTier.Tests
{
    public partial class TestAppointments : Form
    {
        //test types ids : 1-vision , 2-writing , 3- street (practical)
        DataTable appointments;
        int TestTypesID;
        int ApplicationID;
        public TestAppointments(int TestTypeID, int applicationID)
        {
            this.TestTypesID = TestTypeID;
            InitializeComponent();
            ApplicationID = applicationID;
            appointments = ApplicationController.GetTestAppointments(TestTypeID.ToString(),ApplicationID.ToString());
        }

        private void TestAppointments_Load(object sender, EventArgs e)
        {
            string title = "";
            switch (TestTypesID)
            {
                case 1:
                    title = "Vision Test Appointments";
                    break;
                case 2:
                    title = "Writing Test Appointments";
                    break;
                case 3:
                    title = "Street Test Appointments";
                    break;
            }
            Label lblMyLabel = new Label();
            lblMyLabel.Text = title;
            lblMyLabel.Location = new Point(210, 30);
            lblMyLabel.Size = new Size(150, 20); 
            lblMyLabel.AutoSize = true;
            lblMyLabel.Font = new Font("Tahoma", 20);
            lblMyLabel.ForeColor = Color.Red;
            this.Controls.Add(lblMyLabel);

           dgvTestAppointments.DataSource = appointments;

            dgvTestAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            dgvTestAppointments.DataSource = appointments;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddAppointment addAppointment =  new AddAppointment(TestTypesID,ApplicationID);
            addAppointment.ShowDialog();

        }
    }
}
