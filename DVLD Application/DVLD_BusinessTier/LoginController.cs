using System;
using System.Collections.Generic;
using DVLD_DataAccessTier;
namespace DVLD_BusinessTier
{
    public class LoginController
    {

        public static bool AttemptLogin(string username, string password)
        {
            clsUser user = new clsUser(-1,username, password);
            return clsUser.AttemptLogin(user);
        }
    }
}
