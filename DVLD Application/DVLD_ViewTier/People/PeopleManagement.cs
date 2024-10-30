﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.Remoting.Channels;
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
            AddPerson addPerson = new AddPerson();
            addPerson.DataCreated += AddRowToDataTable;
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
    }
    }