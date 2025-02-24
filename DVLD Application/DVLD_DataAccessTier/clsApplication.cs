using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessTier
{
    public class clsApplication
    {
        static string  GetAllLocalApplicationsJoinQuery = @"SELECT        LicenseClasses.ClassName, Applications.*, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, LocalDrivingLicenseApplications.LicenseClassID
FROM                     Applications INNER JOIN
                         LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID INNER JOIN
                         LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID INNER JOIN
                         People ON Applications.ApplicantPersonID = People.PersonID WHERE Applications.ApplicationTypeID = 1 ";
        public static int addLocalDrivingLicenseApplication(Dictionary<string,string> data)
        {
            int id = -1;

            //add local license application algorithm :
            //1- get all the person applications with type id 1 (local)
            //applications status : 1-new 2-canceled 3-completed

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
                    if (reader["LicenseClassID"].ToString() == data["LicenseClassID"] && reader["ApplicationStatus"].ToString() != "2")
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
                command2.Parameters.AddWithValue("@ApplicationStatus", 1);
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

        public static DataTable GetAllLocalApplications()
        {
            DataTable results = new DataTable();
            string query = GetAllLocalApplicationsJoinQuery;
        

            SqlCommand command = new SqlCommand(query, clsSettings.connection);
            results = clsHelpers.LocalApplicationsCommandExecuter(command);
            return results;
        }

        public static DataTable GetLocalApplicationsOnFilter(string filter, string value) {
            string query;
            string parameterName = "None";
            switch (filter)
            {
                case "None":
                    query = GetAllLocalApplicationsJoinQuery;
                    break;

                case "ApplicationID":
                    query = $"{GetAllLocalApplicationsJoinQuery} AND Applications.ApplicationID Like '%' + @ApplicationID + '%' ";
                    parameterName = "@ApplicationID";
                    break;
                
                case "NationalNumber":
                    query = $"{GetAllLocalApplicationsJoinQuery} AND NationalNo Like '%' + @NationalNo + '%'";
                    parameterName = "@NationalNo";
                    break;

                case "FullName":
                    query = $"{GetAllLocalApplicationsJoinQuery} AND FirstName LIKE '%' + @FullName + '%' " +
                            $"OR SecondName LIKE '%' + @FullName + '%' " +
                            $"OR ThirdName LIKE '%' + @FullName + '%' " +
                            $"OR LastName LIKE '%' + @FullName + '%'";
                    parameterName = "@FullName";
                    break;
                case "Status":
                    query = $"{GetAllLocalApplicationsJoinQuery} AND ApplicationStatus = @Status ";
                    parameterName = "@Status";
                    break;
              
                default:
                    query = GetAllLocalApplicationsJoinQuery;
                    break;
            }
            SqlCommand command = new SqlCommand(query, clsSettings.connection);
            command.Parameters.AddWithValue(parameterName, value);
            return clsHelpers.LocalApplicationsCommandExecuter(command);

        }

    }
}
