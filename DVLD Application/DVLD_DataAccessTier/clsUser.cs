using System;
using System.Data;
using System.Data.SqlClient;


namespace DVLD_DataAccessTier
{
    public class clsUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsUser() { }
        public clsUser(int userID , string userName, string password, bool isActive = true)
        {
            UserId = userID;
            UserName = userName;
            Password = password;
            IsActive = isActive;
        }

        public static DataTable GetAllUsers()
        {
            string query = "select UserID,UserName,IsActive from Users";
            SqlCommand command = new SqlCommand(query, clsSettings.connection);
            return clsHelpers.UsersQueryCommandExecuter(command);
        }
        public static DataTable GetUsersBasedOnFilter(string filter , string value) {
            string query = "select * from Users";
            string parameterName = null;
            switch (filter)
            {
                case "None":
                    break;
                case "UserName":
                    query = $"{query} where UserName Like '%' + @UserName + '%'";
                    parameterName = "@UserName";
                    break;

                case "IsActive":
                    query = $"{query} where IsActive = @IsActive ";
                    parameterName = "@IsActive";
                    break;
            }
            SqlCommand command = new SqlCommand(query, clsSettings.connection);
            command.Parameters.AddWithValue(parameterName, value);
            return clsHelpers.UsersQueryCommandExecuter(command);
            }
        public static bool AttemptLogin(clsUser user)
        {
            bool auth = false;  
            if (user != null)
            {
                string query = "Select * from Users where UserName = @name and Password = @password";
                SqlCommand command = new SqlCommand(query, clsSettings.connection);
                command.Parameters.AddWithValue("@name", user.UserName);
                command.Parameters.AddWithValue("@password", user.Password);
                try
                {
                    clsSettings.connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        auth = true;
                        clsSettings.currentUser = new clsUser((int)reader["UserID"], reader["UserName"].ToString() , reader["Password"].ToString() , (bool)reader["IsActive"]);
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    clsSettings.connection.Close();
                }
            }
            return auth;
        }

        public static string GetUserNameByID(int id)
        {
            string result = null;
            string query = "select * from Users where UserID = @id";
            SqlCommand command = new SqlCommand(query, clsSettings.connection);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                clsSettings.connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    result = reader["UserName"].ToString();
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
        public static clsUser GetUserByID(int id) {
            clsUser user = new clsUser();
            string query = "select * from Users where UserID = @id";
            SqlCommand command = new SqlCommand(query, clsSettings.connection);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                clsSettings.connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    user.UserId = int.Parse(reader["UserID"].ToString());
                    user.UserName = reader["UserName"].ToString();
                    user.Password = reader["Password"].ToString();
                    user.IsActive = bool.Parse(reader["IsActive"].ToString());
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


            return user;
        }

        public static int AddUser(clsUser user)
        {
            string query = "INSERT INTO Users (UserName, Password , IsActive) Values (@UserName, @Password , @IsActive)";
            SqlCommand command = new SqlCommand(query , clsSettings.connection);
            command.Parameters.AddWithValue ("@UserName", user.UserName);
            command.Parameters.AddWithValue ("@IsActive", user.IsActive);
            command.Parameters.AddWithValue ("@Password", user.Password);

            if (clsHelpers.NonQueryCommandExecuter(command))
            {
                query = "select * from Users where UserName = @UserName";
                command = new SqlCommand (query , clsSettings.connection);
                command.Parameters.AddWithValue("@UserName" , user.UserName);

                return clsHelpers.FindUserCommandExecuter(command).UserId;
            }
            return -1;
        }
        public static bool UpdateUser(clsUser user)
        {
            bool updated = false;
            if (user != null)
            {
                string query = @"UPDATE Users
                 SET UserName = @UserName,
                     Password = @Password,
                     IsActive = @IsActive
                 WHERE UserID = @Id";

                SqlCommand command = new SqlCommand(query , clsSettings.connection);
                command.Parameters.AddWithValue("@UserName" , user.UserName);
                command.Parameters.AddWithValue("@Password" , user.Password);
                command.Parameters.AddWithValue("@IsActive" , user.IsActive);
                command.Parameters.AddWithValue("@Id" , user.UserId);
                updated = clsHelpers.NonQueryCommandExecuter(command);
            }
            return updated;
        }
    }
}
