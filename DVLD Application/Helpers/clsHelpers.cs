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
            results.Columns.Add("Gender", typeof(int));
            results.Columns.Add("Address", typeof(string));
            results.Columns.Add("Phone", typeof(string));
            results.Columns.Add("Email", typeof(string));
            results.Columns.Add("NationalityCountryID", typeof(string));
            results.Columns.Add("ImagePath", typeof(string));
            results.Columns.Add("Created_by", typeof(int));
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
                    results.Rows.Add(
                          reader["PersonId"],
                          reader["NationalNo"],
                          reader["FirstName"],
                          reader["SecondName"],
                          reader["ThirdName"],
                          reader["LastName"],
                          reader["DateOfBirth"],
                          reader["Gender"],
                          reader["Address"],
                          reader["Phone"],
                          reader["Email"],
                          reader["NationalityCountryID"],
                          reader["ImagePath"],
                          reader["created_by"]
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
        static public Dictionary<string , string> GetAllCountries()
        {
            Dictionary<string , string> results = new Dictionary<string, string>();
            string query = "select * from Countries";
            SqlCommand command = new SqlCommand(query, clsSettings.connection);
            try
            {
                clsSettings.connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(reader["CountryID"].ToString() , reader["CountryName"].ToString());
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



    }
}

