using DVLD_BusinessTier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_BusinessTier;
using System.Windows.Forms;
using DVLD_ViewTier.Licenses;

namespace DVLD_ViewTier.Drivers
{
    public partial class ShowDrivers : Form
    {
        DataTable data = DriverController.getAllDrivers();
        public ShowDrivers()
        {
            InitializeComponent();
            dgvDrivers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            LoadDataToGridView();
          
        }
        private void LoadDataToGridView()
        {
            dgvDrivers.DataSource = data;
            lbRecords.Text = dgvDrivers.RowCount.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbNationalNumber_TextChanged(object sender, EventArgs e)
        {
            data = DriverController.getAllDrivers(tbNationalNumber.Text);
            LoadDataToGridView();
        }

        private void dgvDrivers_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Get the row index of the row that was clicked
                var hitTestInfo = dgvDrivers.HitTest(e.X, e.Y);

                // Check if the click was on a row
                if (hitTestInfo.RowIndex >= 0)
                {
                    // Select the clicked row
                    dgvDrivers.ClearSelection();
                    dgvDrivers.Rows[hitTestInfo.RowIndex].Selected = true;

                    // Optionally, set the current cell if needed
                    dgvDrivers.CurrentCell = dgvDrivers.Rows[hitTestInfo.RowIndex].Cells[0];
                }
            }
        }

        private void dgvDrivers_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvDrivers.IsCurrentCellDirty && dgvDrivers.CurrentCell is DataGridViewCheckBoxCell)
            {
                dgvDrivers.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void showLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NationalNumber =  dgvDrivers.CurrentRow.Cells[2].Value.ToString();
            ShowLicenseHistory licenseHistory = new ShowLicenseHistory(NationalNumber);
            licenseHistory.Show();
        }
    }
}
