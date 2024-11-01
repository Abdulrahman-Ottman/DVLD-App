using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessTier
{
    public class clsCountry
    {
        public static Dictionary<string, string> GetAllCountries()
        {
            Dictionary<string, string> results = new Dictionary<string, string>();
            string query = "select * from Countries";
            SqlCommand command = new SqlCommand(query, clsSettings.connection);
            try
            {
                clsSettings.connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(reader["CountryID"].ToString(), reader["CountryName"].ToString());
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
        public static string GetCountryNameByID(string countryID)
        {
            string result = null;
            string query = "select * from Countries where CountryID = @countryID";
            SqlCommand command = new SqlCommand(query, clsSettings.connection);
            command.Parameters.AddWithValue("@countryID" , countryID);
            try
            {
                clsSettings.connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    result = reader["CountryName"].ToString();
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


            return result;
        }
    }
}
