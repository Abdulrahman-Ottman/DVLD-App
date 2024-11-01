using DVLD_BusinessTier;
using System;
using System.Collections.Generic;

using System.Drawing;
using System.Windows.Forms;

namespace DVLD_ViewTier.People
{
    public partial class AddEditPerson : Form
    {
        private string imagePath = null;
        private int personIdToUpdate = -1;
        private string nationalityCountryID = "1";
        private enum enMode
        {
            Add =0 ,
            Update = 1 ,
        }
        private enMode mode = enMode.Add;
        public delegate void AddDataToDataTableEventHandler(int id ,string nationalID, string firstName, string secondName,
                    string thirdName, string lastName, DateTime dateOfBirth,
                    int gender, string address, string phone, string email,
                    string nationalityCountryID, string imagePath, int createdBy);

        public event AddDataToDataTableEventHandler DataStatusChanged;
        public AddEditPerson() 
        {
            InitializeComponent();
        }
        public AddEditPerson(int id)
        {
            personIdToUpdate = id;
            InitializeComponent();
            Dictionary<string , string> personData = Helpers.GetPersonDataByID(id.ToString());

            if (personData != null)
            {
                tbNationalID.Text = personData.ContainsKey("NationalNumber") ? personData["NationalNumber"] : string.Empty;
                tbFirstName.Text = personData.ContainsKey("FirstName") ? personData["FirstName"] : string.Empty;
                tbSecondName.Text = personData.ContainsKey("SecondName") ? personData["SecondName"] : string.Empty;
                tbThirdName.Text = personData.ContainsKey("ThirdName") ? personData["ThirdName"] : string.Empty;
                tbLastName.Text = personData.ContainsKey("LastName") ? personData["LastName"] : string.Empty;

                if (personData.ContainsKey("DateOfBirth") && DateTime.TryParse(personData["DateOfBirth"], out DateTime dateOfBirth))
                {
                    dtpDateOfBirth.Value = dateOfBirth;
                }

                if (personData.ContainsKey("Gender"))
                {
                    if (personData["Gender"] == "1")
                    {
                        rbMale.Checked = true;
                    }
                    else 
                    {
                        rbFemale.Checked = true;
                    }
                }


                tbAddress.Text = personData.ContainsKey("Address") ? personData["Address"] : string.Empty;
                tbPhone.Text = personData.ContainsKey("Phone") ? personData["Phone"] : string.Empty;
                tbEmail.Text = personData.ContainsKey("Email") ? personData["Email"] : string.Empty;

                if (personData.ContainsKey("NationalityCountryID"))
                {
                    nationalityCountryID = personData["NationalityCountryID"];
                }


                pbPersonaleImage.Image = Image.FromFile(personData["ImagePath"]);

                mode = enMode.Update;
            }

        }

        private void btnUploadeImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
                openFileDialog.Title = "Select an Image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = openFileDialog.FileName;
                    // Display the selected file path in the text box
                    pbPersonaleImage.Image = Image.FromFile(imagePath);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                bool statusChanged = false;
                int gender = -1;
                if (rbMale.Checked)
                {
                    gender = 1;
                }
                else if (rbFemale.Checked)
                {
                    gender = 2;
                }
                string nationalID = tbNationalID.Text;
                string firstName = tbFirstName.Text;
                string secondName = tbSecondName.Text;
                string thirdName = tbThirdName.Text;
                string lastName = tbLastName.Text;
                DateTime dateOfBirth = dtpDateOfBirth.Value;
                string address = tbAddress.Text;
                string phone = tbPhone.Text;
                string email = tbEmail.Text;
                string nationalityCountryID = cbCountry.SelectedValue.ToString();
                int createdBy = UserController.GetCurrentUserID();

                int id = -1;
                if (mode == enMode.Add)
                {
                    id = PersonController.AddNewPerson(
                                        nationalID, firstName, secondName, thirdName, lastName,
                                        dateOfBirth, gender, address, phone, email,
                                        nationalityCountryID, imagePath, createdBy);
                    if (id > 0)
                    {
                        MessageBox.Show("Person Added Successfully");
                        statusChanged = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error : Failed to add Person");
                    }
                }
                else if (mode == enMode.Update)
                {

                    id = personIdToUpdate;
                    imagePath = Helpers.GetPersonDataByID(id.ToString())["ImagePath"];



                        PersonController.UpdatePerson(id ,
                                        nationalID, firstName, secondName, thirdName, lastName,
                                        dateOfBirth, gender, address, phone, email,
                                        nationalityCountryID, imagePath, createdBy);

                    if (id > 0)
                    {
                        MessageBox.Show("Person Updated Successfully");
                        statusChanged = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error : Failed to update person info");
                    }
                }

                if (statusChanged)
                {
                    DataStatusChanged?.Invoke(id,
                    nationalID, firstName, secondName, thirdName, lastName,
                    dateOfBirth, gender, address, phone, email,
                    nationalityCountryID, imagePath, createdBy);
                }
            }
        }

        private void LoadCourtiers()
        {
            Dictionary<string , string> countries = CountryController.GetAllCountries();
            cbCountry.DataSource = new BindingSource(countries, null);
            cbCountry.DisplayMember = "Value";  
            cbCountry.ValueMember = "Key";
        }

        private void AddPerson_Load(object sender, EventArgs e)
        {
            LoadCourtiers();
            cbCountry.SelectedValue = nationalityCountryID;
        }

        private bool ValidateForm()
        {
            bool isValid = true;

            isValid &= Helpers.ValidateRequiredTextBox(tbNationalID, "National Number is required.");
            isValid &= Helpers.ValidateRequiredTextBox(tbFirstName, "First Name is required.");
            isValid &= Helpers.ValidateRequiredTextBox(tbSecondName, "Second Name is required.");
            isValid &= Helpers.ValidateRequiredTextBox(tbThirdName, "Third Name is required.");
            isValid &= Helpers.ValidateRequiredTextBox(tbLastName, "Last Name is required.");
            isValid &= Helpers.ValidateRequiredTextBox(tbAddress, "Address is required.");
            isValid &= Helpers.ValidateRequiredTextBox(tbPhone, "Phone number is required.");
            isValid &= Helpers.ValidateRequiredTextBox(tbEmail, "Email is required.");

            isValid &= Helpers.ValidateEmail(tbEmail);
            isValid &= Helpers.ValidatePhoneNumber(tbPhone);
            isValid &= Helpers.ValidateDateTimePicker(dtpDateOfBirth);

            if(imagePath == null && mode == enMode.Add)
            {
                Helpers.errorProvider.SetError(pbPersonaleImage, "Personal Image Required");
                isValid = false;
            }
            if(!rbMale.Checked && !rbFemale.Checked)
            {
                Helpers.errorProvider.SetError(label9, "Gender is required");
                isValid = false;
            }
            return isValid;
        }

    }
}
