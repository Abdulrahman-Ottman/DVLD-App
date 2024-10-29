using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessTier
{
    public class clsPerson
    {
        public int Id { get; set; }
        public string NationalNumber { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string NationalityCountryID { get; set; }
        public string ImagePath { get; set; }
        public int Created_by { get; set; }


        public static DataTable GetAllPeople()
        {
            string query = "Select * from People";
            SqlCommand command = new SqlCommand(query , clsSettings.connection);
            return clsHelpers.PeopleQueryCommandExecuter(command);
        }

        public static bool AddNewPerson(clsPerson person)
        {
            bool result = false;

            if (person != null)
            {
                string query = @"
        INSERT INTO People (NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, 
                            Address, Phone, Email, NationalityCountryID, ImagePath, Created_by) 
        VALUES (@NationalNumber, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, @Gender, 
                @Address, @Phone, @Email, @NationalityCountryID, @ImagePath, @Created_by)";

                SqlCommand command = new SqlCommand(query, clsSettings.connection);

                command.Parameters.AddWithValue("@NationalNumber", person.NationalNumber);
                command.Parameters.AddWithValue("@FirstName", person.FirstName);
                command.Parameters.AddWithValue("@SecondName", person.SecondName);
                command.Parameters.AddWithValue("@ThirdName", person.ThirdName);
                command.Parameters.AddWithValue("@LastName", person.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);
                command.Parameters.AddWithValue("@Gender", person.Gender);
                command.Parameters.AddWithValue("@Address", person.Address);
                command.Parameters.AddWithValue("@Phone", person.Phone);
                command.Parameters.AddWithValue("@Email", person.Email);
                command.Parameters.AddWithValue("@NationalityCountryID", person.NationalityCountryID);
                command.Parameters.AddWithValue("@ImagePath", person.ImagePath);
                command.Parameters.AddWithValue("@Created_by", person.Created_by);

                result = clsHelpers.NonQueryCommandExecuter(command);
            }

            return result;
        }

    }
}
