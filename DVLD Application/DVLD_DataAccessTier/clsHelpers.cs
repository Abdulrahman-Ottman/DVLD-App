using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessTier;
namespace DVLD_DataAccessTier
{
    public class clsHelpers
    {
        static public DataTable GeneratePeopleDataTable()
        {
            DataTable results = new DataTable();
            results.Columns.Add("Id", typeof(int));
            results.Columns.Add("NationalNumber", typeof(string));
            results.Columns.Add("FirstName", typeof(string));
            results.Columns.Add("SecondName", typeof(string));
            results.Columns.Add("ThirdName", typeof(string));
            results.Columns.Add("LastName", typeof(string));
            results.Columns.Add("DateOfBirth", typeof(DateTime));
            results.Columns.Add("Gender", typeof(string));
            results.Columns.Add("Address", typeof(string));
            results.Columns.Add("Phone", typeof(string));
            results.Columns.Add("Email", typeof(string));
            results.Columns.Add("NationalityCountry", typeof(string));
            results.Columns.Add("ImagePath", typeof(string));
            results.Columns.Add("Created_by", typeof(string));
            return results;
        }
        static public DataTable GenerateUsersDataTable()
        {
            DataTable results = new DataTable();
            results.Columns.Add("UserID", typeof(int));
            results.Columns.Add("UserName", typeof(string));
            results.Columns.Add("IsActive", typeof(bool));
            return results;
        }
        static public DataTable PeopleQueryCommandExecuter(SqlCommand command)
        {
            DataTable results = GeneratePeopleDataTable();
            try
            {
                clsSettings.connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string gender = int.Parse(reader["Gender"].ToString()) == 1 ? "Male" : "FeMale";
                    results.Rows.Add(
                          reader["PersonId"],
                          reader["NationalNo"],
                          reader["FirstName"],
                          reader["SecondName"],
                          reader["ThirdName"],
                          reader["LastName"],
                          reader["DateOfBirth"],
                          gender,
                          reader["Address"],
                          reader["Phone"],
                          reader["Email"],
                          reader["CountryName"],
                          reader["ImagePath"],
                          reader["UserName"]
                      );
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                clsSettings.connection.Close();
            }


            return results;
        }

        static public DataTable UsersQueryCommandExecuter(SqlCommand command)
        {
            DataTable results = GenerateUsersDataTable();
            try
            {
                clsSettings.connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    results.Rows.Add(
                          reader["UserID"],
                          reader["UserName"],
                          reader["IsActive"]
                      );
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                clsSettings.connection.Close();
            }

            return results;
        }
        static public bool NonQueryCommandExecuter(SqlCommand command)
        {
            int rowsAffected = 0;
            try
            {
                clsSettings.connection.Open();

                rowsAffected = command.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                clsSettings.connection.Close();
            }

            return rowsAffected > 0;
        }
        static public clsPerson FindPersonCommandExecuter(SqlCommand command)
        {
            clsPerson person = null;
            try
            {
                clsSettings.connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    person = new clsPerson();
                    person.Id = int.Parse(reader["PersonId"].ToString());
                    person.NationalNumber = reader["NationalNo"].ToString();
                    person.FirstName = reader["FirstName"].ToString();
                    person.SecondName = reader["SecondName"].ToString();
                    person.ThirdName = reader["ThirdName"].ToString();
                    person.LastName = reader["LastName"].ToString();
                    person.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                    person.Gender = int.Parse(reader["Gender"].ToString());
                    person.Address = reader["Address"].ToString();
                    person.Phone = reader["Phone"].ToString();
                    person.Email = reader["Email"].ToString();
                    person.NationalityCountryID = reader["NationalityCountryID"].ToString();
                    person.ImagePath = reader["ImagePath"].ToString();
                    person.Created_by = int.Parse(reader["created_by"].ToString());
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error : couldn't find the person");
            }
            finally
            {
                clsSettings.connection.Close();
            }


            return person;
        }

        static public clsUser FindUserCommandExecuter(SqlCommand command)
        {
            clsUser user = null;
            try
            {
                clsSettings.connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    user = new clsUser();
                    user.UserId = int.Parse(reader["UserID"].ToString());
                    user.UserName = reader["UserName"].ToString();
                    user.IsActive = bool.Parse(reader["IsActive"].ToString());

                }
            }

            catch (Exception ex)
            {
                throw new Exception("Error : couldn't find the user");
            }
            finally
            {
                clsSettings.connection.Close();
            }


            return user;
        }
    }
}

