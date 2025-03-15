using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
            string checkLicenseExistQuery = @"SELECT   Count(*)
FROM            Applications INNER JOIN
                         Licenses ON Applications.ApplicationID = Licenses.ApplicationID INNER JOIN
                         People ON Applications.ApplicantPersonID = People.PersonID Where PersonID = @personID";
            SqlCommand command1 = new SqlCommand(checkLicenseExistQuery,clsSettings.connection);
            command1.Parameters.AddWithValue("@personID", data["PersonID"]);
            string query = @"SELECT  Applications.*, LocalDrivingLicenseApplications.LicenseClassID
                            FROM Applications INNER JOIN LocalDrivingLicenseApplications
                            ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID = @PersonID AND Applications.ApplicationTypeID = 1";
            SqlCommand command = new SqlCommand(query, clsSettings.connection);
            command.Parameters.AddWithValue("@PersonID", data["PersonID"]);
            try
            {
                clsSettings.connection.Open();
                int numOfResults = (int)command1.ExecuteScalar();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (numOfResults>0&& reader["LicenseClassID"].ToString() == data["LicenseClassID"] && reader["ApplicationStatus"].ToString() != "2")
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
        public static Dictionary<int , bool> getApplicationPassedTests(string id)
        {
            Dictionary<int,bool> passedTests = new Dictionary<int,bool>();
            string query = @"SELECT        LocalDrivingLicenseApplications.ApplicationID, TestAppointments.TestTypeID, TestAppointments.TestAppointmentID, Tests.TestResult, LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                   FROM 
                         LocalDrivingLicenseApplications
                   INNER JOIN
                         TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
                    INNER JOIN
                         Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                    WHERE ApplicationID=@id";
            SqlCommand command = new SqlCommand(query,clsSettings.connection);
            command.Parameters.AddWithValue ("@id", id);
            try
            {
                clsSettings.connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    if ((bool)reader["TestResult"])
                    {
                       
                        if (passedTests.ContainsKey((int)reader["TestTypeID"]))
                        {
                            continue;
                        }
                        passedTests.Add((int)reader["TestTypeID"], (bool)reader["TestResult"]);
                    }
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


            return passedTests;
        }
        public static DataTable GetTestAppointments(string TestTypeID,string applicationID)
        {
            string query = @"SELECT
                       LocalDrivingLicenseApplications.ApplicationID, TestAppointments.TestTypeID, TestAppointments.TestAppointmentID, TestAppointments.AppointmentDate, TestAppointments.PaidFees, TestAppointments.CreatedByUserID, 
                                        TestAppointments.IsLocked
                    FROM
                        LocalDrivingLicenseApplications
                INNER JOIN
                TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
                 WHERE LocalDrivingLicenseApplications.ApplicationID = @id and TestTypeID = @TestTypeID";
            SqlCommand command = new SqlCommand (query,clsSettings.connection);
            command.Parameters.AddWithValue("@id",applicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            DataTable results = new DataTable();
            results.Columns.Add("AppointmentID", typeof(int));
            results.Columns.Add("AppointmentDate", typeof(DateTime));
            results.Columns.Add("PaidFees", typeof(float));
            results.Columns.Add("IsLocked", typeof(bool));

            try
            {
                clsSettings.connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    results.Rows.Add(
                           reader["TestAppointmentID"],
                           reader["AppointmentDate"],
                           reader["PaidFees"],
                           reader["IsLocked"]
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
        public static bool saveTestAppointment(int testTypeID,DateTime date,int applicationID , float paidFees)
        {
            string query1 = @"SELECT        TestAppointments.AppointmentDate, TestAppointments.TestTypeID, TestAppointments.IsLocked, LocalDrivingLicenseApplications.ApplicationID
                FROM 
                    LocalDrivingLicenseApplications
                INNER JOIN
                         TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
                Where ApplicationID = @applicationID";
            //query2 for checking if there passed test from the same type 
            string query2 = @"SELECT   Count(*)   
                        FROM            LocalDrivingLicenseApplications INNER JOIN
                         TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                         Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID Where ApplicationID = @ApplicationID
						 and TestTypeID = @TestTypeID and TestResult = 1";
            using(SqlCommand command1 = new SqlCommand(query1, clsSettings.connection))
            {
                command1.Parameters.AddWithValue("@applicationID",applicationID);
                try
                {
                    clsSettings.connection.Open();
                    SqlDataReader reader = command1.ExecuteReader();
                    while (reader.Read())
                    {
                        if ((((DateTime)reader["AppointmentDate"] == date || (int)reader["TestTypeID"]==testTypeID) && !(bool)reader["IsLocked"]))
                        {
                            return false;
                        }
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
            }
            using (SqlCommand command2 = new SqlCommand(query2, clsSettings.connection))
            {
                command2.Parameters.AddWithValue("@TestTypeID",testTypeID);
                command2.Parameters.AddWithValue("@ApplicationID",applicationID);
                try
                {
                    clsSettings.connection.Open();
                    int count = (int)command2.ExecuteScalar();
                    if (count > 0) { 
                        return false;
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
            }



                string query = @"INSERT INTO TestAppointments (TestTypeID,LocalDrivingLicenseApplicationID,AppointmentDate,PaidFees,CreatedByUserID,IsLocked)
                            Values
                             (@TestTypeID , @localLicenseApplicationID,@AppointmentDate,@PaidFees,@CreatedBy,0)";
            int LDL_ID = clsHelpers.getLocalApplicationIdByApplicationID(applicationID);
            if (LDL_ID > -1)
            {
                SqlCommand command = new SqlCommand(query, clsSettings.connection);
                command.Parameters.AddWithValue("@TestTypeID", testTypeID);
                command.Parameters.AddWithValue("@localLicenseApplicationID", LDL_ID);
                command.Parameters.AddWithValue("@AppointmentDate", date);
                command.Parameters.AddWithValue("@PaidFees", paidFees);
                command.Parameters.AddWithValue("@CreatedBy", clsSettings.currentUser.UserId);
                return clsHelpers.NonQueryCommandExecuter(command);
            }
            return false;
        }

        public static bool TakeTest(int TestAppointmentID,bool TestResult,string Notes)
        {
            string query = @"INSERT INTO Tests
                    (TestAppointmentID , TestResult , Notes , CreatedByUserID)
                    Values (@AppointmentID,@TestResult,@Notes,@UserID)";
            SqlCommand command = new SqlCommand(query, clsSettings.connection);

            command.Parameters.AddWithValue("@AppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@UserID", clsSettings.currentUser.UserId);

            bool result = clsHelpers.NonQueryCommandExecuter(command);
            if (result) {
                string query1 = @"UPDATE TestAppointments  
                    SET IsLocked = @IsLocked  
                    WHERE TestAppointmentID = @AppointmentID;";
                SqlCommand command1 = new SqlCommand( query1, clsSettings.connection);
                command1.Parameters.AddWithValue("@IsLocked",true);
                command1.Parameters.AddWithValue("@AppointmentID", TestAppointmentID);
                clsHelpers.NonQueryCommandExecuter(command1);
            }

            return result;
        }

        public static bool IssueLocalDrivingLicense(int applicationID,string notes)
        {
            bool result = false;    
            //1-prepare the data needed to create the license
            string query = @"SELECT        LicenseClasses.LicenseClassID,LicenseClasses.ClassFees, Applications.ApplicantPersonID, Applications.ApplicationTypeID, LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                        FROM
                        Applications INNER JOIN
                         LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID INNER JOIN
                         LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID Where Applications.ApplicationID = @applicationID";

            SqlCommand command = new SqlCommand(query, clsSettings.connection);
            command.Parameters.AddWithValue("@applicationID", applicationID);
            int LicenseClassID = -1;
            int PersonID = -1;
            int driverID = -1;
            float paidFees = 0;
            try
            {
                clsSettings.connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    LicenseClassID =  (int)reader["LicenseClassID"];
                    paidFees = Convert.ToSingle(reader["ClassFees"]);
                    PersonID = (int)reader["ApplicantPersonID"];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                clsSettings.connection.Close();
            }
            //2-create the driver entry
            if(PersonID > 0 && LicenseClassID > 0)
            {
                string query1 = @"INSERT INTO Drivers (PersonID,CreatedByUserID,CreatedDate)
                              Values (@personID,@CreatedByUser,@CreatedDate)
                            SELECT CAST(SCOPE_IDENTITY() AS int)";
                SqlCommand command1 = new SqlCommand(query1, clsSettings.connection);
                command1.Parameters.AddWithValue("@personID", PersonID);
                command1.Parameters.AddWithValue("@CreatedByUser", clsSettings.currentUser.UserId);
                command1.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                try
                {
                    clsSettings.connection.Open();
                    driverID = (int)command1.ExecuteScalar();
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                finally
                {
                    clsSettings.connection.Close();
                }
            

            }
            //3-create the license entry
            if (driverID > 0)
            {
                string query2 = @"INSERT INTO Licenses
                        (ApplicationID,DriverID,LicenseCLass,IssueDate,ExpirationDate,Notes,PaidFees,IsActive,IssueReason,CreatedByUserID)
                        Values
                        (@ApplicationID,@DriverID,@LicenseCLass,@IssueDate,@ExpirationDate,@Notes,@PaidFees,@IsActive,@IssueReason,@CreatedByUserID)";
                SqlCommand command2 = new SqlCommand(query2, clsSettings.connection);
                command2.Parameters.AddWithValue("@ApplicationID", applicationID);
                command2.Parameters.AddWithValue("@DriverID", driverID);
                command2.Parameters.AddWithValue("@LicenseCLass", LicenseClassID);
                command2.Parameters.AddWithValue("@IssueDate", DateTime.Now);
                command2.Parameters.AddWithValue("@ExpirationDate", DateTime.Now.AddYears(10));
                command2.Parameters.AddWithValue("@Notes", notes);
                command2.Parameters.AddWithValue("@PaidFees", paidFees);
                command2.Parameters.AddWithValue("@IsActive", true);
                command2.Parameters.AddWithValue("@IssueReason", 0);
                command2.Parameters.AddWithValue("@CreatedByUserID", clsSettings.currentUser.UserId);
                result = clsHelpers.NonQueryCommandExecuter(command2);
            }
            //4-change the application status to completed
            if (result)
            {
                string query3 = @"UPDATE Applications  
                        SET ApplicationStatus = 3  
                        WHERE ApplicationID = @ApplicationID;";
                SqlCommand command3 = new SqlCommand(query3, clsSettings.connection);
                command3.Parameters.AddWithValue("@ApplicationID",applicationID);
                result = clsHelpers.NonQueryCommandExecuter(command3);
            }

            return result;
        }
        public static bool deleteApplication(int applicationID)
        {
            string query = @"
        DELETE FROM Tests 
        WHERE TestAppointmentId IN 
            (SELECT TestAppointmentId FROM TestAppointments 
             WHERE LocalDrivingLicenseApplicationId IN 
                (SELECT LocalDrivingLicenseApplicationId FROM LocalDrivingLicenseApplications 
                 WHERE ApplicationId = @appId));

        DELETE FROM TestAppointments 
        WHERE LocalDrivingLicenseApplicationId IN 
            (SELECT LocalDrivingLicenseApplicationId FROM LocalDrivingLicenseApplications 
             WHERE ApplicationId = @appId);

        DELETE FROM LocalDrivingLicenseApplications 
        WHERE ApplicationId = @appId;

        DELETE FROM Applications WHERE ApplicationId = @appId;
        ";
        SqlCommand command = new SqlCommand(query, clsSettings.connection);
        command.Parameters.AddWithValue("@appID",applicationID);
        return clsHelpers.NonQueryCommandExecuter(command);

        }
        public static bool cancelApplication(int applicationID)
        {
            string query = @"UPDATE Applications  
                        SET ApplicationStatus = 2  
                        WHERE ApplicationID = @ApplicationID;";
            using (SqlCommand command = new SqlCommand(query, clsSettings.connection)) {
                command.Parameters.AddWithValue("@ApplicationID",applicationID);
                return clsHelpers.NonQueryCommandExecuter(command);
            }
        }
    }
}
