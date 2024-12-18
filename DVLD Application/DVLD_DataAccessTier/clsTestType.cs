using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessTier
{
    public class clsTestType
    {
        public static DataTable GetAllTypes()
        {
            string query = "select * from TestTypes";
            SqlCommand command = new SqlCommand(query , clsSettings.connection);
            return clsHelpers.TestsTypesCommandExecuter(command);
        }

        public static Dictionary<string,string> FindTestByID(int id) { 
            Dictionary<string,string> data = new Dictionary<string,string>();
            string query = "select * from TestTypes where TestTypeID = @id";
            SqlCommand command = new SqlCommand(query, clsSettings.connection);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                clsSettings.connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data.Add("Id", reader["TestTypeID"].ToString());
                    data.Add("title", reader["TestTypeTitle"].ToString());
                    data.Add("description", reader["TestTypeDescription"].ToString());
                    data.Add("fees", reader["TestTypeFees"].ToString());
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

        public static bool UpdateTest(int id , string title , string description , string fees)
        {
            string query = @"UPDATE TestTypes
                 SET TestTypeTitle = @title,
                     TestTypeDescription = @description,     
                     TestTypeFees = @fees      
                 WHERE TestTypeID = @Id";

            SqlCommand command = new SqlCommand(@query, clsSettings.connection);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@fees", fees);
            command.Parameters.AddWithValue("@Id", id);
            return clsHelpers.NonQueryCommandExecuter(command);
        }

    }
}
