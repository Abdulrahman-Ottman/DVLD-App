using DVLD_DataAccessTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessTier
{
    public class ApplicationController
    {
        public static int addLocalDrivingLicenseApplication(Dictionary<string,string> data)
        {
            return clsApplication.addLocalDrivingLicenseApplication(data);
        }

        public static DataTable GetAllLocalApplications()
        {
            return clsApplication.GetAllLocalApplications();
        }
    }
}
