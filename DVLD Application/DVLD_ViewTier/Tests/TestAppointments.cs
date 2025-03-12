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
        private void reFetchData()
        {
            appointments = ApplicationController.GetTestAppointments(TestTypesID.ToString(),ApplicationID.ToString());
            LoadDataToGridView();
        }

        private void LoadDataToGridView()
        {
            dgvTestAppointments.DataSource = appointments;
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


            dgvTestAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

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

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)(dgvTestAppointments.CurrentRow.Cells[0].Value);
            TakeTest takeTest = new TakeTest(id);
            takeTest.ShowDialog();
        }
        private void dgvTestAppointments_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Get the row index of the row that was clicked
                var hitTestInfo = dgvTestAppointments.HitTest(e.X, e.Y);

                // Check if the click was on a row
                if (hitTestInfo.RowIndex >= 0)
                {
                    // Select the clicked row
                    dgvTestAppointments.ClearSelection();
                    dgvTestAppointments.Rows[hitTestInfo.RowIndex].Selected = true;

                    // Optionally, set the current cell if needed
                    dgvTestAppointments.CurrentCell = dgvTestAppointments.Rows[hitTestInfo.RowIndex].Cells[0];
                }
            }
         

         
        }
        private void dgvTestAppointments_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvTestAppointments.IsCurrentCellDirty && dgvTestAppointments.CurrentCell is DataGridViewCheckBoxCell)
            {
                dgvTestAppointments.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reFetchData();
        }
    }
}
