using DVLD_DataAccessTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_BusinessTier
{
    public class UserController
    {
        public static int GetCurrentUserID()
        {
            return clsSettings.currentUser.UserId;
        }
        public static string GetCurrentUserName()
        {
            return clsSettings.currentUser.UserName;
        }
        public static void ClearCurrentUserData()
        {
            clsSettings.currentUser = null;
        }
        public static DataTable GetAllUsers() { 
            return clsUser.GetAllUsers();
        }
        public static int AddUser(string UserName , string Password , bool IsActive)
        {
            clsUser user = new clsUser { 
                UserName = UserName ,
                Password = Password ,
                IsActive = IsActive
            };
            return clsUser.AddUser(user);
        }
        public static DataTable GetUsersBasedOnFilter(string filter , string value)
        {
            return clsUser.GetUsersBasedOnFilter (filter, value);
        }
        public static Dictionary<string , string> FindUserByID(int id)
        {
            clsUser user = clsUser.GetUserByID(id);
            Dictionary<string,string> result = new Dictionary<string,string>();

            result.Add("UserID", user.UserId.ToString());
            result.Add("UserName", user.UserName);
            result.Add("Password", user.Password);
            result.Add("IsActive", user.IsActive.ToString());
            return result;
        }
        public static bool UpdateUser(int id , string userName , string Password , bool isActive)
        {
            clsUser user = new clsUser { 
                UserId = id,
                UserName = userName ,
                Password = Password ,
                IsActive = isActive
            };
            return clsUser.UpdateUser(user);
        }
        public static bool DeleteUser(int id) { return clsUser.DeleteUser(id); }

    }
}
