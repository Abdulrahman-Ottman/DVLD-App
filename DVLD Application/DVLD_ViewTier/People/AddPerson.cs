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

namespace DVLD_ViewTier.People
{
    public partial class AddPerson : Form
    {
        private string imagePath = null;

        public AddPerson()
        {
            InitializeComponent();
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

                int gender = -1;
                if (rbMale.Checked)
                {
                    gender = 1;
                }
                else if (rbFemale.Checked)
                {
                    gender = 2;
                }
                bool added = PersonController.AddNewPerson(
                     tbNationalID.Text,
                     tbFirstName.Text,
                     tbSecondName.Text,
                     tbThirdName.Text,
                     tbLastName.Text,
                     dtpDateOfBirth.Value,
                     gender,
                     tbAddress.Text,
                     tbPhone.Text,
                     tbEmail.Text,
                     cbCountry.SelectedValue.ToString(),
                     imagePath,
                     UserController.GetCurrentUserID()
                     );
                if (added)
                {
                    MessageBox.Show("Person Added Successfully");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error : Failed to add the Person");
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

            if(imagePath == null)
            {
                Helpers.errorProvider.SetError(pbPersonaleImage, "Personal Image Required");
                isValid = false;
            }
            if(!rbMale.Checked && !rbFemale.Checked)
            {
                Helpers.errorProvider.SetError(label9, "Gender is required");
            }
            return isValid;
        }

    }
}
