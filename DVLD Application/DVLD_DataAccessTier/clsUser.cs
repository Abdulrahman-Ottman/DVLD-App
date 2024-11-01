using System;
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
    }
}
