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
    }
}
