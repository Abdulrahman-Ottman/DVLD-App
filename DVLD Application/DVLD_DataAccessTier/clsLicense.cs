using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessTier
{
    public class clsLicense
    {
        public static Dictionary<string ,string> getLicenseInfo(int applicationID)
        {
            Dictionary<string,string> data = new Dictionary<string,string>();
            string query = @"SELECT      Applications.ApplicationID,  LicenseClasses.ClassName, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.NationalNo, Licenses.LicenseID, Licenses.IssueDate, Licenses.LicenseClass, Licenses.ExpirationDate, 
                         Licenses.Notes, Licenses.IsActive, Licenses.IssueReason, People.DateOfBirth, People.Gender, Drivers.DriverID, People.ImagePath
FROM            Drivers INNER JOIN
                         Licenses ON Drivers.DriverID = Licenses.DriverID
						INNER JOIN Applications on Applications.ApplicationID = Licenses.ApplicationID
						INNER JOIN
                         LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID INNER JOIN
                         People ON Drivers.PersonID = People.PersonID where Applications.ApplicationID = @applicationID";
            SqlCommand command = new SqlCommand(query,clsSettings.connection);
            command.Parameters.AddWithValue("@applicationID",applicationID);
            try
            {
                clsSettings.connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    data.Add("Class", reader["ClassName"].ToString());
                    data.Add("Name", reader["FirstName"].ToString() + " " + reader["SecondName"].ToString() + " " + reader["ThirdName"].ToString() + " " +reader["LastName"].ToString());
                    data.Add("LicenseID", reader["LicenseID"].ToString());
                    data.Add("NationalNo", reader["NationalNo"].ToString());
                    data.Add("Gender", reader["Gender"].ToString());
                    data.Add("IssueDate", reader["IssueDate"].ToString());
                    data.Add("IssueReason", reader["IssueReason"].ToString());
                    data.Add("Notes", reader["Notes"].ToString());
                    data.Add("IsActive", reader["IsActive"].ToString());
                    data.Add("DateOfBirth", reader["DateOfBirth"].ToString());
                    data.Add("DriverID", reader["DriverID"].ToString());
                    data.Add("ExpirationDate", reader["ExpirationDate"].ToString());
                    //default for now
                    data.Add("IsDetained", "false");
                    data.Add("ImagePath", reader["ImagePath"].ToString());
                }
            }
            catch (Exception ex) { 
                throw ex;
            }
            finally
            {
                 clsSettings.connection.Close();
            }
            return data;
        }
        public static DataTable getLocalLicenseHistory(string NationalNo)
        {
            string query = @"SELECT Licenses.LicenseID , Licenses.ApplicationID , LicenseClasses.ClassName , Licenses.IssueDate,Licenses.ExpirationDate,Licenses.IsActive
                FROM Licenses INNER JOIN LicenseClasses on Licenses.LicenseClass = LicenseClasses.LicenseClassID
				INNER JOIN Drivers on Drivers.DriverID = Licenses.DriverID
				INNER JOIN People on Drivers.PersonID = People.PersonID
Where People.NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(query,clsSettings.connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            DataTable data = new DataTable();
            data.Columns.Add("LicenseID",typeof(int));
            data.Columns.Add("ApplicationID",typeof(int));
            data.Columns.Add("ClassName",typeof(string));
            data.Columns.Add("IssueDate",typeof(DateTime));
            data.Columns.Add("ExpirationDate",typeof(DateTime));
            data.Columns.Add("IsActive",typeof(bool));
            DataColumn[] keyColumns = new DataColumn[1];
            keyColumns[0] = data.Columns["LicenseID"];
            data.PrimaryKey = keyColumns;
            try
            {
                clsSettings.connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data.Rows.Add(
                        reader["LicenseID"],
                        reader["ApplicationID"],
                        reader["ClassName"],
                        reader["IssueDate"],
                        reader["ExpirationDate"],
                        reader["IsActive"]
                        );
                }
            }catch (Exception ex) { throw ex; }
            finally { clsSettings.connection.Close(); }


            return data;

        }
    }
}
