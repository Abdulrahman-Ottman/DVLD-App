using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessTier
{
    public class clsApplicationType
    {

        public static DataTable GetAllTypes()
        {
            DataTable results;
            string query = "Select * from ApplicationTypes";
            SqlCommand command = new SqlCommand(query, clsSettings.connection);
            results = clsHelpers.ApplicationsTypesCommandExecuter(command);

            return results;
        }
        public static Dictionary<string, string> FindTypeByID(int id)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            string query = "select * from ApplicationTypes where ApplicationTypeID = @id";
            SqlCommand command = new SqlCommand(query, clsSettings.connection);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                clsSettings.connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data.Add("Id", reader["ApplicationTypeID"].ToString());
                    data.Add("title", reader["ApplicationTypeTitle"].ToString());
                    data.Add("fees", reader["ApplicationFees"].ToString());
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
            return data;
        }
        public static bool UpdateType(int id , string title , string fees)
        {
            string query = @"UPDATE ApplicationTypes
                 SET ApplicationTypeTitle = @title,
                     ApplicationFees = @fees      
                 WHERE ApplicationTypeID = @Id";

            SqlCommand command = new SqlCommand(@query, clsSettings.connection);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@fees", fees);
            command.Parameters.AddWithValue("@Id", id);
            return clsHelpers.NonQueryCommandExecuter(command);
        }
    }
}