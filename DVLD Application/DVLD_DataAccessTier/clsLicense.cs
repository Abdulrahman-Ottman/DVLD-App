using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccessTier
{
    public class clsLicense
    {
        static string getLicenseInfoQuery = @"SELECT      Applications.ApplicationID,  LicenseClasses.ClassName, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.NationalNo, Licenses.LicenseID, Licenses.IssueDate, Licenses.LicenseClass, Licenses.ExpirationDate, 
                         Licenses.Notes, Licenses.IsActive, Licenses.IssueReason, People.DateOfBirth, People.Gender, Drivers.DriverID, People.ImagePath
FROM            Drivers INNER JOIN
                         Licenses ON Drivers.DriverID = Licenses.DriverID
						INNER JOIN Applications on Applications.ApplicationID = Licenses.ApplicationID
						INNER JOIN
                         LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID INNER JOIN
                         People ON Drivers.PersonID = People.PersonID";
        public static Dictionary<string ,string> getLicenseInfo(int applicationID)
        {
            Dictionary<string,string> data = new Dictionary<string,string>();
            string query = $"{getLicenseInfoQuery} where Applications.ApplicationID = @applicationID";
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
        public static DataTable getInternationalLicenseHistory(string NationalNo)
        {
            string query = @"SELECT        InternationalLicenses.InternationalLicenseID,InternationalLicenses.IsActive, InternationalLicenses.ApplicationID, InternationalLicenses.ExpirationDate, InternationalLicenses.IssueDate, People.NationalNo, 
                         InternationalLicenses.IssuedUsingLocalLicenseID as LocalLicenseID
FROM            Drivers INNER JOIN
                         InternationalLicenses ON Drivers.DriverID = InternationalLicenses.DriverID INNER JOIN
                         People ON Drivers.PersonID = People.PersonID
						 Where NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(query, clsSettings.connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            DataTable data = new DataTable();
            data.Columns.Add("InternationalLicenseID", typeof(int));
            data.Columns.Add("ApplicationID", typeof(int));
            data.Columns.Add("LocalLicenseID", typeof(int));
            data.Columns.Add("IssueDate", typeof(DateTime));
            data.Columns.Add("ExpirationDate", typeof(DateTime));
            data.Columns.Add("IsActive", typeof(bool));
            DataColumn[] keyColumns = new DataColumn[1];
            keyColumns[0] = data.Columns["InternationalLicenseID"];
            data.PrimaryKey = keyColumns;

            try
            {
                clsSettings.connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data.Rows.Add(
                        reader["InternationalLicenseID"],
                        reader["ApplicationID"],
                        reader["LocalLicenseID"],
                        reader["IssueDate"],
                        reader["ExpirationDate"],
                        reader["IsActive"]
                        );
                }
            }
            catch (Exception ex) { throw ex; }
            finally { clsSettings.connection.Close(); }


            return data;
        }

        public static Dictionary<string, string> getLicenseInfoByLicenseID(int LicenseID , string LicenseClass = null,bool IsActive=false)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            string query = $"{getLicenseInfoQuery} where Licenses.LicenseID = @LicenseID ";
            if( LicenseClass != null)
            {
                query += " and Licenses.LicenseClass = @LicenseClass ";
            }
            query += " and Licenses.IsActive = @IsActive ";
            SqlCommand command = new SqlCommand(query, clsSettings.connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            if (LicenseClass != null)
            {
                command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            }
            command.Parameters.AddWithValue("@IsActive", IsActive);
            try
            {
                clsSettings.connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    data.Add("Class", reader["ClassName"].ToString());
                    data.Add("Name", reader["FirstName"].ToString() + " " + reader["SecondName"].ToString() + " " + reader["ThirdName"].ToString() + " " + reader["LastName"].ToString());
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
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                clsSettings.connection.Close();
            }
            return data;
        }
        public static bool issueInternationalLicense(int LocalLicenseID , int DriverID)
        {
            //check if there is no International License related to this Local License ID
            string query = @"select count(InternationalLicenseID) from InternationalLicenses Where IssuedUsingLocalLicenseID = @LocalLicenseID";
            SqlCommand cmd = new SqlCommand(query,clsSettings.connection);
            cmd.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);
            clsSettings.connection.Open();
            int count = (int)cmd.ExecuteScalar();
            clsSettings.connection.Close();
            if (count > 0) { 
                return false;
            }
            //create international license application
            bool result = false;
            int applicationID=-1;
            int personID = -1;
            string createApplicationQuery = @"INSERT INTO Applications 
                          (ApplicantPersonID,ApplicationDate,ApplicationTypeID,ApplicationStatus,LastStatusDate,PaidFees,CreatedByUserID)
                            Values
                          (@ApplicantPersonID,@ApplicationDate,@ApplicationTypeID,@ApplicationStatus,@LastStatusDate,@PaidFees,@CreatedByUserID)
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";
            string getPersonIDQuery = @"SELECT PersonID From Drivers Where DriverID = @driverID";
            SqlCommand getPersonIDCommand = new SqlCommand(getPersonIDQuery, clsSettings.connection);
            getPersonIDCommand.Parameters.AddWithValue("@driverID", DriverID);
            clsSettings.connection.Open();
            personID = (int)getPersonIDCommand.ExecuteScalar();
            clsSettings.connection.Close();
            if (personID > -1)
            {
                SqlCommand createApplicationCommand = new SqlCommand(createApplicationQuery, clsSettings.connection);
                createApplicationCommand.Parameters.AddWithValue("@ApplicantPersonID", personID);
                createApplicationCommand.Parameters.AddWithValue("@ApplicationDate", DateTime.Now);
                createApplicationCommand.Parameters.AddWithValue("@ApplicationTypeID", 6);
                createApplicationCommand.Parameters.AddWithValue("@ApplicationStatus", 3);
                createApplicationCommand.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
                createApplicationCommand.Parameters.AddWithValue("@PaidFees", clsHelpers.GetApplicationTypeFeesByID(6));
                createApplicationCommand.Parameters.AddWithValue("@CreatedByUserID", clsSettings.currentUser.UserId);

                clsSettings.connection.Open();

                applicationID = (int)createApplicationCommand.ExecuteScalar();
                 clsSettings.connection.Close();
}
            if (applicationID > -1) {
                //create the international license
                string createInternationalLicenseQuery = @"INSERT INTO InternationalLicenses 
                          (ApplicationID,DriverID,IssuedUsingLocalLicenseID,IssueDate,ExpirationDate,IsActive,CreatedByUserID)
                            Values
                          (@ApplicationID,@DriverID,@IssuedUsingLocalLicenseID,@IssueDate,@ExpirationDate,@IsActive,@CreatedByUserID)
                        ";

                SqlCommand createInternationalLicenseCommand = new SqlCommand(createInternationalLicenseQuery, clsSettings.connection);
                createInternationalLicenseCommand.Parameters.AddWithValue("@ApplicationID",applicationID);
                createInternationalLicenseCommand.Parameters.AddWithValue("@DriverID",DriverID);
                createInternationalLicenseCommand.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", LocalLicenseID);
                createInternationalLicenseCommand.Parameters.AddWithValue("@IssueDate", DateTime.Now);
                createInternationalLicenseCommand.Parameters.AddWithValue("@ExpirationDate", DateTime.Now.AddYears(10));
                createInternationalLicenseCommand.Parameters.AddWithValue("@IsActive", 1);
                createInternationalLicenseCommand.Parameters.AddWithValue("@CreatedByUserID", clsSettings.currentUser.UserId);

                result = clsHelpers.NonQueryCommandExecuter(createInternationalLicenseCommand); 
            }
            return result;
        }
        public static bool renewLicense(int LicenseID , int DriverID)
        {

            bool result = false;
            int applicationID = -1;
            int personID = -1;
            string createRenewApplication = @"INSERT INTO Applications 
                          (ApplicantPersonID,ApplicationDate,ApplicationTypeID,ApplicationStatus,LastStatusDate,PaidFees,CreatedByUserID)
                            Values
                          (@ApplicantPersonID,@ApplicationDate,@ApplicationTypeID,@ApplicationStatus,@LastStatusDate,@PaidFees,@CreatedByUserID)
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";
            string getPersonIDQuery = @"SELECT PersonID From Drivers Where DriverID = @driverID";
            SqlCommand getPersonIDCommand = new SqlCommand(getPersonIDQuery, clsSettings.connection);
            getPersonIDCommand.Parameters.AddWithValue("@driverID", DriverID);
            clsSettings.connection.Open();
            personID = (int)getPersonIDCommand.ExecuteScalar();
            clsSettings.connection.Close();

            if (personID > -1)
            {
                SqlCommand createApplicationCommand = new SqlCommand(createRenewApplication, clsSettings.connection);
                createApplicationCommand.Parameters.AddWithValue("@ApplicantPersonID", personID);
                createApplicationCommand.Parameters.AddWithValue("@ApplicationDate", DateTime.Now);
                createApplicationCommand.Parameters.AddWithValue("@ApplicationTypeID", 2);
                createApplicationCommand.Parameters.AddWithValue("@ApplicationStatus", 3);
                createApplicationCommand.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
                createApplicationCommand.Parameters.AddWithValue("@PaidFees", clsHelpers.GetApplicationTypeFeesByID(2));
                createApplicationCommand.Parameters.AddWithValue("@CreatedByUserID", clsSettings.currentUser.UserId);

                clsSettings.connection.Open();
                applicationID = (int)createApplicationCommand.ExecuteScalar();
                clsSettings.connection.Close();
            }

            if (applicationID > -1)
            {
                string getOldLicenseQuery = @"Select * from Licenses Where LicenseID = @LicenseID";
                string createNewLicenseQuery = @"INSERT INTO Licenses
                        (ApplicationID,DriverID,LicenseClass,IssueDate,ExpirationDate,Notes,PaidFees,IsActive,IssueReason,CreatedByUserID)
                        Values
                        (@ApplicationID,@DriverID,@LicenseClass,@IssueDate,@ExpirationDate,@Notes,@PaidFees,1,@IssueReason,@CreatedByUserID)";
                SqlCommand createNewLicenseCommand = new SqlCommand(createNewLicenseQuery, clsSettings.connection);
                SqlCommand getOldLicenseCommand = new SqlCommand(getOldLicenseQuery, clsSettings.connection);
                getOldLicenseCommand.Parameters.AddWithValue("@LicenseID",LicenseID);
                clsSettings.connection.Open();
                SqlDataReader reader = getOldLicenseCommand.ExecuteReader();
                if(reader.Read()) {
                    createNewLicenseCommand.Parameters.AddWithValue("@ApplicationID",applicationID);
                    createNewLicenseCommand.Parameters.AddWithValue("@DriverID", DriverID);
                    createNewLicenseCommand.Parameters.AddWithValue("@LicenseClass", reader["LicenseClass"]);
                    createNewLicenseCommand.Parameters.AddWithValue("@IssueDate", DateTime.Now);
                    createNewLicenseCommand.Parameters.AddWithValue("@ExpirationDate", DateTime.Now.AddYears(10));
                    createNewLicenseCommand.Parameters.AddWithValue("@Notes", reader["Notes"]);
                    createNewLicenseCommand.Parameters.AddWithValue("@PaidFees", reader["PaidFees"]);
                    createNewLicenseCommand.Parameters.AddWithValue("@IssueReason", "Renew");
                    createNewLicenseCommand.Parameters.AddWithValue("@CreatedByUserID", clsSettings.currentUser.UserId);
                }
                clsSettings.connection.Close();
                result = clsHelpers.NonQueryCommandExecuter(createNewLicenseCommand);
                if (result)
                {
                    string updateOldLicenseActive = @"Update Licenses set IsActive=0 Where LicenseID = @LicenseID";
                    SqlCommand updateOldLicenseCommand = new SqlCommand(updateOldLicenseActive, clsSettings.connection);
                    updateOldLicenseCommand.Parameters.AddWithValue("@LicenseID",LicenseID);
                    result = clsHelpers.NonQueryCommandExecuter(updateOldLicenseCommand);
                }
            }

            return result;
        }

    }
}
