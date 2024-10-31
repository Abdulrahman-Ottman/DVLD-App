using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_DataAccessTier;

namespace DVLD_BusinessTier
{
    public class Helpers
    {
        public static ErrorProvider errorProvider = new ErrorProvider
        {
            BlinkStyle = ErrorBlinkStyle.NeverBlink,
        };
        public Helpers()
        {
        }

        public static bool ValidateRequiredTextBox(TextBox textBox, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                errorProvider.SetError(textBox, errorMessage);
                return false;
            }
            else
            {
                errorProvider.SetError(textBox, "");
                return true;
            }
        }
        public static bool ValidateEmail(TextBox textBox)
        {
            // Basic email format regex
            if (!Regex.IsMatch(textBox.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorProvider.SetError(textBox, "Please enter a valid email address.");
                return false;
            }
            else
            {
                errorProvider.SetError(textBox, "");
                return true;
            }
        }
        public static bool ValidatePhoneNumber(TextBox textBox)
        {
            // Check that phone number is exactly 10 digits
            if (!Regex.IsMatch(textBox.Text, @"^\d{10}$"))
            {
                errorProvider.SetError(textBox, "Phone number must be exactly 10 digits.");
                return false;
            }
            else
            {
                errorProvider.SetError(textBox, "");
                return true;
            }
        }
        public static bool ValidateDateTimePicker(DateTimePicker date)
        {
            bool isValid = true;
            // Example: Ensure the date is not in the future
            if (date.Value > DateTime.Now)
            {
                errorProvider.SetError(date, "Invalid age");
                isValid = false;
            }

            // Example: Ensure the age is greater than 18 years (for a birthdate)
            if ((DateTime.Now.Year - date.Value.Year) < 18)
            {
                errorProvider.SetError(date, "Age must be more than 18");
                isValid = false;
            }

            return isValid; // If the date is valid
        }
        public static Dictionary<string , string> GetPersonDataByID(string id)
        {
            Dictionary<string , string> personData = new Dictionary<string , string>();
            clsPerson person  = clsPerson.FindPersonByID(id);
            if (person != null)
            {
                personData["Id"] = person.Id.ToString();
                personData["NationalNumber"] = person.NationalNumber;
                personData["FirstName"] = person.FirstName;
                personData["SecondName"] = person.SecondName;
                personData["ThirdName"] = person.ThirdName;
                personData["LastName"] = person.LastName;
                personData["DateOfBirth"] = person.DateOfBirth.ToString("yyyy-MM-dd");
                personData["Gender"] = person.Gender.ToString();
                personData["Address"] = person.Address;
                personData["Phone"] = person.Phone;
                personData["Email"] = person.Email;
                personData["NationalityCountryID"] = person.NationalityCountryID;
                personData["ImagePath"] = person.ImagePath;
                personData["Created_by"] = person.Created_by.ToString();
                return personData;
            }
            return null;
        }
        public static Dictionary<string , string> GetPersonDataByNationalNumber(string nationalNumber)
        {
            Dictionary<string , string> personData = new Dictionary<string , string>();
            clsPerson person  = clsPerson.FindPersonByNationalNumber(nationalNumber);
            if (person != null)
            {
                personData["Id"] = person.Id.ToString();
                personData["NationalNumber"] = person.NationalNumber;
                personData["FirstName"] = person.FirstName;
                personData["SecondName"] = person.SecondName;
                personData["ThirdName"] = person.ThirdName;
                personData["LastName"] = person.LastName;
                personData["DateOfBirth"] = person.DateOfBirth.ToString("yyyy-MM-dd");
                personData["Gender"] = person.Gender.ToString();
                personData["Address"] = person.Address;
                personData["Phone"] = person.Phone;
                personData["Email"] = person.Email;
                personData["NationalityCountryID"] = person.NationalityCountryID;
                personData["ImagePath"] = person.ImagePath;
                personData["Created_by"] = person.Created_by.ToString();
                return personData;
            }
            return null;
        }

    }
}
