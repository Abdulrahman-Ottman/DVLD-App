using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DVLD_BusinessTier;

namespace DVLD_ViewTier.People
{
    public partial class PeopleManagement : Form
    {
        DataTable peopleData = PersonController.GetAllPeople();
        static DataColumn[] keyColumns = new DataColumn[1];

        Control currentFilterControl = null;
        public PeopleManagement()
        {
            InitializeComponent();
        }

        private void ReFetchDataFromDB()
        {
            peopleData = PersonController.GetAllPeople();
            LoadDataToGridView();
        }
        private void AddRowToDataTable(int id,
                string nationalID, string firstName, string secondName,
                string thirdName, string lastName, DateTime dateOfBirth,
                int gender, string address, string phone, string email,
                string nationalityCountryID, string imagePath, int createdBy)
        {
            DataRow newRow = peopleData.NewRow();

            newRow["Id"] = id;
            newRow["NationalNumber"] = nationalID;
            newRow["FirstName"] = firstName;
            newRow["SecondName"] = secondName;
            newRow["ThirdName"] = thirdName;
            newRow["LastName"] = lastName;
            newRow["DateOfBirth"] = dateOfBirth;
            newRow["Gender"] = gender;
            newRow["Address"] = address;
            newRow["Phone"] = phone;
            newRow["Email"] = email;
            newRow["NationalityCountryID"] = nationalityCountryID;
            newRow["ImagePath"] = imagePath;
            newRow["Created_by"] = createdBy;

            peopleData.Rows.Add(newRow);
        }

        private void UpdateRowInDataTable(int id,
        string nationalID, string firstName, string secondName,
        string thirdName, string lastName, DateTime dateOfBirth,
        int gender, string address, string phone, string email,
        string nationalityCountryID, string imagePath, int createdBy)
        {
           
            DataRow row = peopleData.Rows.Find(id);
            row["NationalNumber"] = nationalID;
            row["FirstName"] = firstName;
            row["SecondName"] = secondName;
            row["ThirdName"] = thirdName;
            row["LastName"] = lastName;
            row["DateOfBirth"] = dateOfBirth;
            row["Gender"] = Helpers.ConvertToGenderName(gender);
            row["Address"] = address;
            row["Phone"] = phone;
            row["Email"] = email;
            row["NationalityCountry"] = Helpers.GetCountryNameByID(nationalityCountryID);
            row["ImagePath"] = imagePath;

        }
        private void LoadDataToGridView()
        {
            dgvViewPeople.DataSource = peopleData;

            dgvViewPeople.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvViewPeople.Columns[0].Width = 30;  // Set checkbox column width
            dgvViewPeople.Columns[6].DefaultCellStyle.Format = "dd/MM/yyyy"; // Format Created At        }

        }

        private void PeopleManagement_Load(object sender, System.EventArgs e)
        {
            cmbFilters.SelectedIndex = 0;
            keyColumns[0] = peopleData.Columns["Id"];
            peopleData.PrimaryKey = keyColumns;
            LoadDataToGridView();
        }

        private void btnAddNewPerson_Click(object sender, System.EventArgs e)
        {
            AddEditPerson addPerson = new AddEditPerson();
            addPerson.DataStatusChanged += AddRowToDataTable;
            addPerson.ShowDialog();
        }

        private void cmbFilters_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if(currentFilterControl != null)
            {
                this.Controls.Remove(currentFilterControl);
            }

            Control inputControl = null;
            string fieldName = cmbFilters.Text;

            switch (fieldName)
            {
                case "None":
                    peopleData = PersonController.GetAllPeople();
                    LoadDataToGridView();
                    break;
                case "NationalNumber":
                case "FirstName":
                case "SecondName":
                case "ThirdName":
                case "LastName":
                case "Address":
                case "Phone":
                case "Email":
                    inputControl = new TextBox
                    {
                        Name = $"tb{fieldName}",
                        Width = 150,
                        Location = new Point(220, 193)
                    };
                    ((TextBox)inputControl).TextChanged += inputControl_TextChanged;
                    break;

                case "DateOfBirth":
                    inputControl = new DateTimePicker
                    {
                        Name = "dtpDateOfBirth",
                        Format = DateTimePickerFormat.Short,
                        Width = 150,
                        Location = new Point(220, 193)
                    };
                    ((DateTimePicker)inputControl).ValueChanged += inputControl_ValueChanged;
                    break;

                case "Gender":
                    inputControl = new ComboBox
                    {
                        Name = "cmbGender",
                        Width = 125,
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Location = new Point(220, 193)
                    };
                    ((ComboBox)inputControl).Items.Add(new KeyValuePair<string, int>("Male", 1));
                    ((ComboBox)inputControl).Items.Add(new KeyValuePair<string, int>("Female", 2));

                    // Set the properties to display text and store value
                    ((ComboBox)inputControl).DisplayMember = "Key";
                    ((ComboBox)inputControl).ValueMember = "Value";

                    ((ComboBox)inputControl).SelectedIndex = 0;
                    ((ComboBox)inputControl).SelectedIndexChanged += inputControl_SelectedIndexChanged;

                    break;

                case "NationalityCountryID":
                    inputControl = new ComboBox
                    {
                        Name = "cmbNationalityCountryID",
                        Width = 150,
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Location = new Point(220, 193)
                    };
                    Dictionary<string, string> countries = CountryController.GetAllCountries();
                    ((ComboBox)inputControl).DataSource = new BindingSource(countries, null);
                    ((ComboBox)inputControl).DisplayMember = "Value";
                    ((ComboBox)inputControl).ValueMember = "Key";

                    ((ComboBox)inputControl).SelectedIndexChanged += inputControl_SelectedIndexChanged;
                    break;

                case "Created_by":
                    inputControl = new TextBox
                    {
                        Name = "tbCreatedBy",
                        Width = 150,
                        Location = new Point(220, 193)
                    };
                    ((TextBox)inputControl).TextChanged += inputControl_TextChanged;
                    break;

                default:
                    throw new ArgumentException($"Field '{fieldName}' is not supported.");
            }
            currentFilterControl = inputControl;
            this.Controls.Add(inputControl);
            if (inputControl is DateTimePicker date)
            {
                inputControl_ValueChanged(date , new EventArgs());
            } else if (inputControl is ComboBox combo)
            {
                inputControl_SelectedIndexChanged(combo , new EventArgs());
            }


        }         
        private void inputControl_TextChanged(object sender , EventArgs e)
        {
           switch (((TextBox)sender).Name)
            {
                case "tbNationalNumber":
                    peopleData = PersonController.GetPeopleBasedOnFilter("NationalNumber" , ((TextBox)sender).Text);
                    break;
                case "tbFirstName":
                    peopleData = PersonController.GetPeopleBasedOnFilter("FirstName", ((TextBox)sender).Text);
                    break;
                case "tbSecondName":
                    peopleData = PersonController.GetPeopleBasedOnFilter("SecondName", ((TextBox)sender).Text);
                    break;
                case "tbThirdName":
                    peopleData = PersonController.GetPeopleBasedOnFilter("ThirdName", ((TextBox)sender).Text);
                    break;
                case "tbLastName":
                    peopleData = PersonController.GetPeopleBasedOnFilter("LastName", ((TextBox)sender).Text);
                    break;
                case "tbAddress":
                    peopleData = PersonController.GetPeopleBasedOnFilter("Address", ((TextBox)sender).Text);
                    break;
                case "tbPhone":
                    peopleData = PersonController.GetPeopleBasedOnFilter("Phone", ((TextBox)sender).Text);
                    break;
                case "tbEmail":
                    peopleData = PersonController.GetPeopleBasedOnFilter("Email", ((TextBox)sender).Text);
                    break;
                case "tbCreatedBy":
                    peopleData = PersonController.GetPeopleBasedOnFilter("Created_by", ((TextBox)sender).Text);
                    break;
            }
            LoadDataToGridView();
            

        }
        private void inputControl_ValueChanged(object sender , EventArgs e)
        {
            if(sender is DateTimePicker date && date.Name == "dtpDateOfBirth")
            {
                peopleData = PersonController.GetPeopleBasedOnFilter("DateOfBirth" , date.Value.ToString());
                LoadDataToGridView();
            }
        }
        private void inputControl_SelectedIndexChanged(object sender , EventArgs e)
        {
            switch (((ComboBox)sender).Name)
            {
                case "cmbNationalityCountryID":
                    peopleData = PersonController.GetPeopleBasedOnFilter("NationalityCountryID" , ((ComboBox)sender).SelectedValue.ToString());
                    break;
                case "cmbGender":
                    if (((ComboBox)sender).SelectedItem is KeyValuePair<string, int> selectedGender)
                    {
                        peopleData = PersonController.GetPeopleBasedOnFilter("Gender", selectedGender.Value.ToString());
                    }
                    break;
            }
            LoadDataToGridView();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)(dgvViewPeople.CurrentRow.Cells[0].Value);
                People.AddEditPerson EditScreen = new AddEditPerson(id);
                EditScreen.DataStatusChanged += UpdateRowInDataTable;
                EditScreen.ShowDialog();
                LoadDataToGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : No Person Selected");
            }
        }

        private void dgvViewPeople_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Get the row index of the row that was clicked
                var hitTestInfo = dgvViewPeople.HitTest(e.X, e.Y);

                // Check if the click was on a row
                if (hitTestInfo.RowIndex >= 0)
                {
                    // Select the clicked row
                    dgvViewPeople.ClearSelection();
                    dgvViewPeople.Rows[hitTestInfo.RowIndex].Selected = true;

                    // Optionally, set the current cell if needed
                    dgvViewPeople.CurrentCell = dgvViewPeople.Rows[hitTestInfo.RowIndex].Cells[0];
                }
            }
        }

        private void dgvViewPeople_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvViewPeople.IsCurrentCellDirty && dgvViewPeople.CurrentCell is DataGridViewCheckBoxCell)
            {
                dgvViewPeople.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)(dgvViewPeople.CurrentRow.Cells[0].Value);
                if (PersonController.DeletePerson(id))
                {
                    MessageBox.Show("Person Deleted Successfully");
                    DataRow rowToDelete = peopleData.Select($"Id = {id}").FirstOrDefault();
                    if (rowToDelete != null)
                    {
                        peopleData.Rows.Remove(rowToDelete);
                        peopleData.AcceptChanges();
                        LoadDataToGridView();
                    }
                }
                else
                {
                    MessageBox.Show("Error : Could not delete selected person");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : No Person Selected");
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                int id = (int)(dgvViewPeople.CurrentRow.Cells[0].Value);
                Dictionary<string,string> personData = new Dictionary<string,string>();
                personData = Helpers.GetPersonDataByID(id.ToString());
                ShowPersonInfo showPersonInfo = new ShowPersonInfo(personData);
                showPersonInfo.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : No Person Selected");
            }
  
        }
    }
    }