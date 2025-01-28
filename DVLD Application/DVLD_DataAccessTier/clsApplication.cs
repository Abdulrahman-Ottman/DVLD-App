using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessTier
{
    public class clsApplication
    {

        public static int addLocalDrivingLicenseApplication(Dictionary<string,string> data)
        {
            int id = -1;

            //add local license application algorithm :
            //1- get all the person applications with type id 1 (local)
            string query = @"SELECT  Applications.*, LocalDrivingLicenseApplications.LicenseClassID
                            FROM Applications INNER JOIN LocalDrivingLicenseApplications
                            ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID = @PersonID AND Applications.ApplicationTypeID = 1";
            SqlCommand command = new SqlCommand(query, clsSettings.connection);
            command.Parameters.AddWithValue("@PersonID", data["PersonID"]);
            try
            {
                clsSettings.connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["LicenseClassID"].ToString() == data["LicenseClassID"])
                    {
                        return id;
                    }
                }
                reader.Close();

                string addingQuery = @"INSERT INTO Applications
                                               (ApplicantPersonID
                                               ,ApplicationDate
                                               ,ApplicationTypeID
                                               ,ApplicationStatus
                                               ,LastStatusDate
                                               ,PaidFees
                                               ,CreatedByUserID)
                                         VALUES(
                                                @ApplicantPersonID
                                               ,@ApplicationDate
                                               ,@ApplicationTypeID
                                               ,@ApplicationStatus
                                               ,@LastStatusDate
                                               ,@PaidFees
                                               ,@CreatedByUserID)
                                            SELECT SCOPE_IDENTITY();";
                SqlCommand command2 = new SqlCommand(addingQuery, clsSettings.connection);
                command2.Parameters.AddWithValue("@ApplicantPersonID" , data["PersonID"]);
                command2.Parameters.AddWithValue("@ApplicationDate", data["ApplicationDate"]);
                command2.Parameters.AddWithValue("@ApplicationTypeID", 1);
                command2.Parameters.AddWithValue("@ApplicationStatus", 0);
                command2.Parameters.AddWithValue("@LastStatusDate", data["ApplicationDate"]);
                command2.Parameters.AddWithValue("@PaidFees", 0);
                command2.Parameters.AddWithValue("@CreatedByUserID", data["UserID"]);

                var result = command2.ExecuteScalar();
                id = Convert.ToInt32(result); 
                string addingToLocalTableQuery = @"INSERT INTO LocalDrivingLicenseApplications
                                               (ApplicationID
                                               ,LicenseClassID)
                                         VALUES(
                                                @ApplicationID
                                               ,@LicenseClassID)";

                SqlCommand command3 = new SqlCommand(addingToLocalTableQuery, clsSettings.connection);
                command3.Parameters.AddWithValue("@ApplicationID", id);
                command3.Parameters.AddWithValue("@LicenseClassID", data["LicenseClassID"]);

                command3.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                clsSettings.connection.Close();
            }


            return id;
        }
    }
}
