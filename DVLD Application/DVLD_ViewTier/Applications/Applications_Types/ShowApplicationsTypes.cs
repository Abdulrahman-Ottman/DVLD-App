using System;
using System.Data;
using System.Windows.Forms;
using DVLD_BusinessTier;
using DVLD_ViewTier.People;

namespace DVLD_ViewTier.Applications.Applications_Types
{
    public partial class ShowApplicationsTypes : Form
    {
        DataTable ApplicationsTypes = ApplicationsTypesController.GetAllTypes();
        public ShowApplicationsTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LoadDataToGridView()
        {
            dgvApplicationsTypes.DataSource = ApplicationsTypes;
            dgvApplicationsTypes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvApplicationsTypes.Columns[0].Width = 60;

        }
        private void ShowApplicationsTypes_Load(object sender, EventArgs e)
        {
            LoadDataToGridView();
            lbRecords.Text = $"# Records : {ApplicationsTypes.Rows.Count}";
        }

        private void dgvApplicationsTypes_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Get the row index of the row that was clicked
                var hitTestInfo = dgvApplicationsTypes.HitTest(e.X, e.Y);

                // Check if the click was on a row
                if (hitTestInfo.RowIndex >= 0)
                {
                    // Select the clicked row
                    dgvApplicationsTypes.ClearSelection();
                    dgvApplicationsTypes.Rows[hitTestInfo.RowIndex].Selected = true;

                    // Optionally, set the current cell if needed
                    dgvApplicationsTypes.CurrentCell = dgvApplicationsTypes.Rows[hitTestInfo.RowIndex].Cells[0];
                }
            }
        }

        private void dgvApplicationsTypes_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvApplicationsTypes.IsCurrentCellDirty && dgvApplicationsTypes.CurrentCell is DataGridViewCheckBoxCell)
            {
                dgvApplicationsTypes.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)(dgvApplicationsTypes.CurrentRow.Cells[0].Value);
                EditApplicationsTypes editApplicationsTypes = new EditApplicationsTypes(id);
                editApplicationsTypes.DataUpdated += UpdateRecord;
                editApplicationsTypes.ShowDialog();
                LoadDataToGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : No Application Selected");
            }
           
        }

        private void UpdateRecord(int id , string title , string fees)
        {
            DataRow row = ApplicationsTypes.Rows.Find(id);
            row["Title"] = title;
            row["Fees"] = fees;
        }
    }
}
