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

namespace DVLD_ViewTier.Licenses.InternationalLicense
{
    public partial class ShowInternationalLicenseApplications : Form
    {
        DataTable data = ApplicationController.GetAllInternationalApplications();
        public ShowInternationalLicenseApplications()
        {
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            LoadDataToGridView();
        }

        private void LoadDataToGridView()
        {
            dataGridView1.DataSource = data;
            lbRecords.Text = dataGridView1.RowCount.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddInternationalLicense addInternationalLicense = new AddInternationalLicense();    
            addInternationalLicense.ShowDialog();
        }

        private void tbNationalNo_TextChanged(object sender, EventArgs e)
        {
            if (tbDriverID.Text != null && tbDriverID.Text.Length > 0) {
                data = ApplicationController.GetAllInternationalApplications(tbDriverID.Text);
                LoadDataToGridView();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            data = ApplicationController.GetAllInternationalApplications();
            LoadDataToGridView();
        }
    }
}
