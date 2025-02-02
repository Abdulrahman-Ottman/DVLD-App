using DVLD_BusinessTier;
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

namespace DVLD_ViewTier.Applications.LocalLicenseApplications
{
    public partial class ShowLocalDrivingLicenseApplications : Form
    {
        DataTable applications = ApplicationController.GetAllLocalApplications();


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
    }
    }

