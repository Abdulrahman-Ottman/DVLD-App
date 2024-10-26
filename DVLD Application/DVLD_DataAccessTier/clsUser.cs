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
        public clsUser(string userName, string password, bool isActive = true)
        {
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
    }
}
