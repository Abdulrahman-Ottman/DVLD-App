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

        static string GetAllPeopleJoinQuery = @"SELECT Countries.CountryName, People.PersonID, People.NationalNo,
                                People.FirstName, People.SecondName, People.ThirdName, 
                            People.LastName, People.DateOfBirth, People.Gender, People.Address,
                            People.Phone, People.NationalityCountryID , People.created_by , People.Email,  
                            People.ImagePath, Users.UserName  
                             FROM  Countries INNER JOIN
                            People ON Countries.CountryID = People.NationalityCountryID INNER JOIN  
                            Users ON People.created_by = Users.UserID";

        public static DataTable GetAllPeople()
        {
            string query = GetAllPeopleJoinQuery;
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
                    query = GetAllPeopleJoinQuery;
                    break;

                case "NationalNumber":
                    query = $"{GetAllPeopleJoinQuery} where NationalNo Like '%' + @NationalNumber + '%'";
                    parameterName = "@NationalNumber";
                    break;

                case "FirstName":
                    query = $"{GetAllPeopleJoinQuery} where FirstName Like '%' + @FirstName + '%'";
                    parameterName = "@FirstName";
                    break;

                case "SecondName":
                    query = $"{GetAllPeopleJoinQuery} where SecondName Like '%' + @SecondName + '%'";
                    parameterName = "@SecondName";
                    break;

                case "ThirdName":
                    query = $"{GetAllPeopleJoinQuery} where ThirdName Like '%' + @ThirdName + '%'";
                    parameterName = "@ThirdName";
                    break;

                case "LastName":
                    query = $"{GetAllPeopleJoinQuery} where LastName Like '%' + @LastName + '%'";
                    parameterName = "@LastName";
                    break;

                case "DateOfBirth":
                    query = $"{GetAllPeopleJoinQuery} where DateOfBirth = @DateOfBirth";
                    parameterName = "@DateOfBirth";
                    break;

                case "Gender":
                    query = $"{GetAllPeopleJoinQuery} where Gender = @Gender";
                    parameterName = "@Gender";
                    break;

                case "Address":
                    query = $"{GetAllPeopleJoinQuery} where Address Like '%' + @Address + '%'";
                    parameterName = "@Address";
                    break;

                case "Phone":
                    query = $"{GetAllPeopleJoinQuery} where Phone Like '%' + @Phone + '%'";
                    parameterName = "@Phone";
                    break;

                case "Email":
                    query = $"{GetAllPeopleJoinQuery} where Email Like '%' + @Email + '%'";
                    parameterName = "@Email";
                    break;

                case "NationalityCountryID":
                    query = $"{GetAllPeopleJoinQuery} where NationalityCountryID = @NationalityCountryID";
                    parameterName = "@NationalityCountryID";
                    break;

                case "Created_by":
                    query = $"{GetAllPeopleJoinQuery} where Created_by = @Created_by";
                    parameterName = "@Created_by";
                    break;

                default:
                    query = GetAllPeopleJoinQuery;
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
                throw new Exception("Null NationalNumber");
            }
            string query = "select * from People where NationalNo = @NationalNumber";
            SqlCommand command = new SqlCommand (query , clsSettings.connection);
            command.Parameters.AddWithValue("@NationalNumber" , NationalNumber);
            return clsHelpers.FindPersonCommandExecuter(command);
        }

        public static clsPerson FindPersonByID(string id)
        {
            if (id == null)
            {
                return null;
            }
            string query = $"{GetAllPeopleJoinQuery} where PersonID = @id";
            SqlCommand command = new SqlCommand(query, clsSettings.connection);
            command.Parameters.AddWithValue("@id", id);
            return clsHelpers.FindPersonCommandExecuter(command);
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
        public static int UpdatePerson(clsPerson person)
        {
            bool result = false;

            if (person != null)
            {
                string query = @"UPDATE People
                 SET NationalNo = @NationalNumber,
                     FirstName = @FirstName,
                     SecondName = @SecondName,
                     ThirdName = @ThirdName,
                     LastName = @LastName,
                     DateOfBirth = @DateOfBirth,
                     Gender = @Gender,
                     Address = @Address,
                     Phone = @Phone,
                     Email = @Email,
                     NationalityCountryID = @NationalityCountryID,
                     ImagePath = @ImagePath,
                     Created_by = @Created_by
                 WHERE PersonID = @Id";

                SqlCommand command = new SqlCommand(query, clsSettings.connection);

                command.Parameters.AddWithValue("@Id", person.Id);
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
                return person.Id;
            }

            return -1;
        }

        public static bool DeletePerson(int personId) {
            string query = "Delete From People where PersonID = @id";
            SqlCommand command = new SqlCommand(query, clsSettings.connection);
            command.Parameters.AddWithValue("@id", personId);
            return clsHelpers.NonQueryCommandExecuter(command);
        }


    }
}
