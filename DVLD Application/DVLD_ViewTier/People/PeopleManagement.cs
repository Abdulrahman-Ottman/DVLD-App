using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DVLD_BusinessTier;

namespace DVLD_ViewTier.People
{
    public partial class PeopleManagement : Form
    {
        DataTable peopleData = PersonController.GetAllPeople();
        static DataColumn[] keyColumns = new DataColumn[1];
        public PeopleManagement()
        {
            InitializeComponent();
        }

        private void LoadDataToGridView()
        {

            keyColumns[0] = peopleData.Columns["Id"];
            peopleData.PrimaryKey = keyColumns;
            // Set up the DataGridView and bind the DataTable
            dgvViewPeople.DataSource = peopleData;

            dgvViewPeople.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //// Optional: Customize the columns further if needed
            dgvViewPeople.Columns[0].Width = 30;  // Set checkbox column width
            //dgvViewPeople.Columns[1].Width = 40;  // Set checkbox column width
            //dgvViewPeople.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy"; // Format End Date
            dgvViewPeople.Columns[6].DefaultCellStyle.Format = "dd/MM/yyyy"; // Format Created At        }

            //dgvViewPeople.Columns["ID"].DefaultCellStyle.ForeColor = Color.Black;
            //dgvViewPeople.Columns["Title"].DefaultCellStyle.ForeColor = Color.Black;
            //dgvViewPeople.Columns["Description"].DefaultCellStyle.ForeColor = Color.Black;
            //dgvViewPeople.Columns["End Date"].DefaultCellStyle.ForeColor = Color.Black;
            //dgvViewPeople.Columns["Created At"].DefaultCellStyle.ForeColor = Color.Black;
        }

        private void PeopleManagement_Load(object sender, System.EventArgs e)
        {
            LoadDataToGridView();
            cmbFilters.SelectedIndex = 0;
        }

        private void btnAddNewPerson_Click(object sender, System.EventArgs e)
        {
            AddPerson addPerson = new AddPerson();
            addPerson.ShowDialog();
        }
    }
}
