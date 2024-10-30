using DVLD_DataAccessTier;
using System;
using System.Collections.Generic;
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
    }
}
