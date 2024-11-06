using DVLD_DataAccessTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
