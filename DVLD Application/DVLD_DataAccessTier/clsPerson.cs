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
        public static DataTable GetPeopleBasedOnFilter(string filter , string value)
        {
            string query;
            string parameterName = "None";

            switch (filter)
            {
                case "None":
                    query = "select * from People";
                    break;

                case "NationalNumber":
                    query = "select * from People where NationalNo Like '%' + @NationalNumber + '%'";
                    parameterName = "@NationalNumber";
                    break;

                case "FirstName":
                    query = "select * from People where FirstName Like '%' + @FirstName + '%'";
                    parameterName = "@FirstName";
                    break;

                case "SecondName":
                    query = "select * from People where SecondName Like '%' + @SecondName + '%'";
                    parameterName = "@SecondName";
                    break;

                case "ThirdName":
                    query = "select * from People where ThirdName Like '%' + @ThirdName + '%'";
                    parameterName = "@ThirdName";
                    break;

                case "LastName":
                    query = "select * from People where LastName Like '%' + @LastName + '%'";
                    parameterName = "@LastName";
                    break;

                case "DateOfBirth":
                    query = "select * from People where DateOfBirth = @DateOfBirth";
                    parameterName = "@DateOfBirth";
                    break;

                case "Gender":
                    query = "select * from People where Gender = @Gender";
                    parameterName = "@Gender";
                    break;

                case "Address":
                    query = "select * from People where Address Like '%' + @Address + '%'";
                    parameterName = "@Address";
                    break;

                case "Phone":
                    query = "select * from People where Phone Like '%' + @Phone + '%'";
                    parameterName = "@Phone";
                    break;

                case "Email":
                    query = "select * from People where Email Like '%' + @Email + '%'";
                    parameterName = "@Email";
                    break;

                case "NationalityCountryID":
                    query = "select * from People where NationalityCountryID = @NationalityCountryID";
                    parameterName = "@NationalityCountryID";
                    break;

                case "Created_by":
                    query = "select * from People where Created_by = @Created_by";
                    parameterName = "@Created_by";
                    break;

                default:
                    query = "select * from People";
                    break;
            }
            SqlCommand command = new SqlCommand(query , clsSettings.connection);
            command.Parameters.AddWithValue(parameterName, value);
            return clsHelpers.PeopleQueryCommandExecuter(command);

        }
        public static clsPerson FindPersonByNationalNumber(string NationalNumber)
        {
            if(NationalNumber == null)
            {
              return null;  
            }
            string query = "select * from People where NationalNo = @NationalNumber";
            SqlCommand command = new SqlCommand (query , clsSettings.connection);
            command.Parameters.AddWithValue("@NationalNumber" , NationalNumber);
            return clsHelpers.FindPersonByID(command);
        }
        public static int AddNewPerson(clsPerson person)
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
            if (result)
            {
                int personId = FindPersonByNationalNumber(person.NationalNumber).Id;
                return personId;
            }

            return -1;
        }

    }
}
