﻿using System;
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
        private static DataTable peopleData = PersonController.GetAllPeople();

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
                string nationalityCountry, string imagePath, int createdBy)
        {
            DataRow newRow = peopleData.NewRow();

            newRow["Id"] = id;
            newRow["NationalNumber"] = nationalID;
            newRow["FirstName"] = firstName;
            newRow["SecondName"] = secondName;
            newRow["ThirdName"] = thirdName;
            newRow["LastName"] = lastName;
            newRow["DateOfBirth"] = dateOfBirth;
            newRow["Gender"] = Helpers.ConvertToGenderName(gender);
            newRow["Address"] = address;
            newRow["Phone"] = phone;
            newRow["Email"] = email;
            newRow["NationalityCountry"] = Helpers.GetCountryNameByID(nationalityCountry);
            newRow["ImagePath"] = imagePath;
            newRow["Created_by"] = Helpers.GetUserNameByID(createdBy);

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
  
            LoadDataToGridView();

            lbRecordsCount.Text = $"# Records: {peopleData.Rows.Count}";
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
                    throw new ArgumentException($"Filter '{fieldName}' is not supported.");
            }
            currentFilterControl = inputControl;
            this.Controls.Add(inputControl);
            if (inputControl is DateTimePicker date)
            {
                inputControl_ValueChanged(date , new EventArgs());
                return;
            } else if (inputControl is ComboBox combo)
            {
                inputControl_SelectedIndexChanged(combo , new EventArgs());
                return;
            }
            peopleData = PersonController.GetAllPeople();
            LoadDataToGridView();

        }         
        private void inputControl_TextChanged(object sender , EventArgs e)
        {
            string filterColumn = "";
           switch (((TextBox)sender).Name)
            {
                case "tbNationalNumber":
                    filterColumn = "NationalNumber";
                    break;
                case "tbFirstName":
                    filterColumn = "FirstName";
                    break;
                case "tbSecondName":
                filterColumn = "SecondName";
                    break;
                case "tbThirdName":
                filterColumn = "ThirdName";
                    break;
                case "tbLastName":
                filterColumn = "LastName" ;
                    break;
                case "tbAddress":
                    filterColumn = "Address";
                    break;
                case "tbPhone":
                    filterColumn = "Phone";
                    break;
                case "tbEmail":
                    filterColumn = "Email";
                    break;
                case "tbCreatedBy":
                 filterColumn = "Created_by";
                    break;
                default:
                    filterColumn = "None";
                    break;
            }
            if (currentFilterControl.Text.Trim() == "" || filterColumn == "None")
            {
                peopleData.DefaultView.RowFilter = "";
                lbRecordsCount.Text = $"# Records: {dgvViewPeople.Rows.Count.ToString()}";
                return;
            }
            if (filterColumn == "PersonID")
                //in this case we deal with integer not string.

                peopleData.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, currentFilterControl.Text.Trim());
            else
                peopleData.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filterColumn, currentFilterControl.Text.Trim());
           
            
            LoadDataToGridView();
            lbRecordsCount.Text = $"# Records: {dgvViewPeople.Rows.Count.ToString()}";

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