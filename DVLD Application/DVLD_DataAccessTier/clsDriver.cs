using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessTier;
namespace DVLD_DataAccessTier
{
    public class clsDriver
    {
        public static DataTable getAllDrivers(string NationalNumberFilter=null)
        {

            string query = @"SELECT   
    Drivers.DriverID,  
    People.PersonID,  
    People.NationalNo,  
    People.FirstName,  
    People.SecondName,  
    People.ThirdName,  
    People.LastName,  
    Drivers.CreatedDate,  
    COUNT(Licenses.LicenseID) AS ActiveLicenseCount  
FROM   
    Drivers   
INNER JOIN   
    People ON Drivers.PersonID = People.PersonID  
LEFT JOIN   
    Licenses ON Drivers.DriverID = Licenses.DriverID AND Licenses.IsActive = 1  
";
            if (NationalNumberFilter != null){
                query += @" Where NationalNo Like '%' + @NationalNumber + '%' " ;
            }
            query += @" GROUP BY   
    Drivers.DriverID,  
    People.PersonID,  
    People.NationalNo,  
    People.FirstName,  
    People.SecondName,  
    People.ThirdName,  
    People.LastName,  
    Drivers.CreatedDate ";
            SqlCommand command = new SqlCommand(query, clsSettings.connection);
            if (NationalNumberFilter != null)
            {
                command.Parameters.AddWithValue("@NationalNumber",NationalNumberFilter);
            }

                DataTable data = new DataTable();
            data.Columns.Add("Driver ID", typeof(int));
            data.Columns.Add("Person ID", typeof(int));
            data.Columns.Add("National No", typeof(string));
            data.Columns.Add("Name", typeof(string));
            data.Columns.Add("Created Date", typeof(DateTime));
            data.Columns.Add("Active Licenses", typeof(int));


            try
            {
                clsSettings.connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data.Rows.Add(
                        reader["DriverID"],
                        reader["PersonID"],
                        reader["NationalNo"],
                        string.Concat(reader["FirstName"], " ", reader["SecondName"], " ", reader["ThirdName"], " ", reader["LastName"]),
                        reader["CreatedDate"],
                        reader["ActiveLicenseCount"]
                        );
                }
            }
            catch (Exception ex) { throw ex; }
            finally { clsSettings.connection.Close(); }
        
            return data;
        }
    }
}
