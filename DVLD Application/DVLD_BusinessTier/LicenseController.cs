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
        public static Dictionary<string, string> getLicenseInfoByLicenseID(int LicenseID)
        {
            return clsLicense.getLicenseInfoByLicenseID(LicenseID);
        }

        public static DataTable getLocalLicenseHistory(string NationalNumber)
        {
            return clsLicense.getLocalLicenseHistory(NationalNumber);
        }
        public static DataTable getInternationalLicenseHistory(string NationalNo)
        {
            return clsLicense.getInternationalLicenseHistory(NationalNo);
        }
        public static bool issueInternationalLicense(int LocalLicenseID, int DriverID)
        {
            return clsLicense.issueInternationalLicense(LocalLicenseID, DriverID);
        }

    }
}
