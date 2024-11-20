using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DVLD_BusinessTier;
using DVLD_DataAccessTier;


namespace DVLD_BusinessTier
{
    public class ApplicationsTypesController
    {
        public static DataTable GetAllTypes()
        {
            return clsApplicationType.GetAllTypes();
        }

        public static Dictionary<string,string> FindTypeByID(int id)
        {
            Dictionary<string,string> data = new Dictionary<string,string>();
            string query = "select * from ApplicationTypes where ApplicationTypeID = @id";
            SqlCommand command = new SqlCommand(query,clsSettings.connection);
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
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            finally
            {
                clsSettings.connection.Close();
            }
            return data;
        }
    }
}
