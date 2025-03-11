using DVLD_BusinessTier;
using DVLD_ViewTier.Licenses.LocalLicense;
using DVLD_ViewTier.People;
using DVLD_ViewTier.Tests;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_ViewTier.Applications.LocalLicenseApplications
{
    public partial class ShowLocalDrivingLicenseApplications : Form
    {
        DataTable applications = ApplicationController.GetAllLocalApplications();
        Control currentFilterControl = null;

        public ShowLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void ShowLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            //cmbFilters.SelectedIndex = 0;

            Image AddApplicationImage = Properties.Resources.New_Application_64;

            int reducedWidth = btnNewLocalApplication.Width - 10;
            int reducedHeight = btnNewLocalApplication.Height - 10;

            // Ensure the dimensions stay positive
            reducedWidth = Math.Max(1, reducedWidth);
            reducedHeight = Math.Max(1, reducedHeight);

            // Create the resized image
            Image resizedImage = new Bitmap(AddApplicationImage, new Size(reducedWidth, reducedHeight));

            btnNewLocalApplication.Image = resizedImage;

            btnNewLocalApplication.ImageAlign = ContentAlignment.MiddleCenter;

            btnNewLocalApplication.Text = "";

            LoadDataToGridView();

            lbRecords.Text = $"{applications.Rows.Count}";


            
        }

        private void LoadDataToGridView()
        {
            dgvApplications.DataSource = applications;

            dgvApplications.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvApplications.Columns[0].Width = 70;
            dgvApplications.Columns[1].Width = 160;
            dgvApplications.Columns[5].Width = 70;
            dgvApplications.Columns[6].Width = 70;

        }

        private void btnNewLocalApplication_Click(object sender, EventArgs e)
        {
            AddLocalLicensApplication addLocal = new AddLocalLicensApplication();
            addLocal.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentFilterControl != null)
            {
                this.Controls.Remove(currentFilterControl);
            }

            Control inputControl = null;
            string fieldName = cmbFilters.Text;

            switch (fieldName)
            {
                case "None":
                    break;
                case "NationalNumber":
                case "FullName":
                case "ApplicationID":
                    inputControl = new TextBox
                    {
                        Name = $"tb{fieldName}",
                        Width = 150,
                        Location = new Point(252, 206)
                    };
                    ((TextBox)inputControl).TextChanged += inputControl_TextChanged;
                    break;
                case "Status":
                    inputControl = new ComboBox
                    {
                        Name = "cmbStatus",
                        Width = 125,
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Location = new Point(252, 206)
                    };
                    ((ComboBox)inputControl).Items.Add(new KeyValuePair<string, int>("New", 1));
                    ((ComboBox)inputControl).Items.Add(new KeyValuePair<string, int>("Canceled", 2));
                    ((ComboBox)inputControl).Items.Add(new KeyValuePair<string, int>("Completed", 3));

                    ((ComboBox)inputControl).DisplayMember = "Key";
                    ((ComboBox)inputControl).ValueMember = "Value";

                    ((ComboBox)inputControl).SelectedIndex = 0;
                    ((ComboBox)inputControl).SelectedIndexChanged += inputControl_SelectedIndexChanged;

                    break;
                default:
                    throw new ArgumentException($"Filter '{fieldName}' is not supported.");
            }


            currentFilterControl = inputControl;
            this.Controls.Add(inputControl);
            if (inputControl is ComboBox combo)
            {
                inputControl_SelectedIndexChanged(combo, new EventArgs());
                return;
            }
            applications = ApplicationController.GetAllLocalApplications();
            LoadDataToGridView();
        }

        private void inputControl_TextChanged(object sender, EventArgs e)
        {
            switch (((TextBox)sender).Name)
            {
                case "tbNationalNumber":
                    applications = ApplicationController.GetLocalApplicationsOnFilter("NationalNumber", ((TextBox)sender).Text);
                    break;
                case "tbFullName":
                    applications = ApplicationController.GetLocalApplicationsOnFilter("FullName", ((TextBox)sender).Text);
                    break;
                case "tbApplicationID":
                    applications = ApplicationController.GetLocalApplicationsOnFilter("ApplicationID", ((TextBox)sender).Text);
                    break;
            }
            LoadDataToGridView();
        }

        private void inputControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedItem is KeyValuePair<string, int> selectedGender)
            {
                applications = ApplicationController.GetLocalApplicationsOnFilter("Status", selectedGender.Value.ToString());
            }
            LoadDataToGridView();
        }

        private void dgvApplications_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Get the row index of the row that was clicked
                var hitTestInfo = dgvApplications.HitTest(e.X, e.Y);

                // Check if the click was on a row
                if (hitTestInfo.RowIndex >= 0)
                {
                    // Select the clicked row
                    dgvApplications.ClearSelection();
                    dgvApplications.Rows[hitTestInfo.RowIndex].Selected = true;

                    // Optionally, set the current cell if needed
                    dgvApplications.CurrentCell = dgvApplications.Rows[hitTestInfo.RowIndex].Cells[0];
                }
            }
            
                string id = (dgvApplications.CurrentRow.Cells[0].Value).ToString();
                Dictionary<int, bool> passedTests = ApplicationController.getApplicationPassedTests(id);
            switch (passedTests.Count)
            {
                case 0:
                    scheduleVisionTestToolStripMenuItem.Enabled = true;
                    scheduleWrittenTestToolStripMenuItem.Enabled = false;
                    scheduleStreetTestToolStripMenuItem.Enabled = false;

                    break;
                case 1:
                    scheduleVisionTestToolStripMenuItem.Enabled = false;
                    scheduleWrittenTestToolStripMenuItem.Enabled = true;
                    scheduleStreetTestToolStripMenuItem.Enabled = false;
                    break;
                case 2:
                    scheduleVisionTestToolStripMenuItem.Enabled = false;
                    scheduleWrittenTestToolStripMenuItem.Enabled = false;
                    scheduleStreetTestToolStripMenuItem.Enabled = true;
                    break;
                default:
                    scheduleVisionTestToolStripMenuItem.Enabled = false;
                    scheduleWrittenTestToolStripMenuItem.Enabled = false;
                    scheduleStreetTestToolStripMenuItem.Enabled = false;
                    break;

            }
        }

        private void dgvApplications_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvApplications.IsCurrentCellDirty && dgvApplications.CurrentCell is DataGridViewCheckBoxCell)
            {
                dgvApplications.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)(dgvApplications.CurrentRow.Cells[0].Value);
            TestAppointments testAppointments = new TestAppointments(1,id);
            testAppointments.Show();
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)(dgvApplications.CurrentRow.Cells[0].Value);
            TestAppointments testAppointments = new TestAppointments(2, id);
            testAppointments.Show();
        }
    }
}

