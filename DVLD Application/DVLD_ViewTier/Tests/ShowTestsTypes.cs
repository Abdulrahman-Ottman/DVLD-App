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
using DVLD_ViewTier.Applications.Applications_Types;

namespace DVLD_ViewTier.Tests
{
    public partial class ShowTestsTypes : Form
    {
        DataTable TestsTypes = TestsTypesController.GetAllTypes();
        public ShowTestsTypes()
        {
            InitializeComponent();
        }
        private void LoadDataToGridView()
        {
            dgvTestsTypes.DataSource = TestsTypes;
            dgvTestsTypes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTestsTypes.Columns[0].Width = 60;

        }
        private void ShowTestsTypes_Load(object sender, EventArgs e)
        {
            LoadDataToGridView();
            lbRecords.Text = $"# Records : {TestsTypes.Rows.Count}";

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvTestsTypes_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Get the row index of the row that was clicked
                var hitTestInfo = dgvTestsTypes.HitTest(e.X, e.Y);

                // Check if the click was on a row
                if (hitTestInfo.RowIndex >= 0)
                {
                    // Select the clicked row
                    dgvTestsTypes.ClearSelection();
                    dgvTestsTypes.Rows[hitTestInfo.RowIndex].Selected = true;

                    // Optionally, set the current cell if needed
                    dgvTestsTypes.CurrentCell = dgvTestsTypes.Rows[hitTestInfo.RowIndex].Cells[0];
                }
            }
        }

        private void dgvTestsTypes_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvTestsTypes.IsCurrentCellDirty && dgvTestsTypes.CurrentCell is DataGridViewCheckBoxCell)
            {
                dgvTestsTypes.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)(dgvTestsTypes.CurrentRow.Cells[0].Value);
                EditTestsTypes editTestsTypes = new EditTestsTypes(id);
                editTestsTypes.DataUpdated += UpdateRecord;
                editTestsTypes.ShowDialog();
                LoadDataToGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : No Test Selected");
            }
        }
        private void UpdateRecord(int id , string title , string description , string fees)
        {
            DataRow row = TestsTypes.Rows.Find(id);
            row["Title"] = title;
            row["Description"] = description;   
            row["Fees"] = fees;
        }
    }
}
