using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessTier;

namespace DVLD_BusinessTier
{
    public class LicenseController
    {

        public static Dictionary<string, string> getLicenseInfo(int applicationID)
        {
            return clsLicense.getLicenseInfo(applicationID);
        }

        public static DataTable getLocalLicenseHistory(string NationalNumber)
        {
            return clsLicense.getLocalLicenseHistory(NationalNumber);
        }
    }
}
