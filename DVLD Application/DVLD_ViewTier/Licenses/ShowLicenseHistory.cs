using DVLD_BusinessTier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_ViewTier.Licenses
{
    public partial class ShowLicenseHistory : Form
    {
        PersonInfoViewer personInfoViewer;
        string NationalNumber;
        public ShowLicenseHistory(string NationalNumber)
        {
            InitializeComponent();
            this.NationalNumber = NationalNumber;
            Dictionary<string, string> personData = PersonController.FindPersonByNationalNumber(NationalNumber);
            personInfoViewer = new PersonInfoViewer(int.Parse(personData["Id"]),
                personData["NationalNumber"],
                personData["FirstName"],
                personData["SecondName"],
                personData["ThirdName"],
                personData["LastName"],
                DateTime.Parse(personData["DateOfBirth"]),
                Helpers.ConvertToGenderName(int.Parse((personData["Gender"]))),
                personData["Address"],
                personData["Phone"],
                personData["Email"],
                Helpers.GetCountryNameByID(personData["NationalityCountryID"]),
                personData["ImagePath"],
                Helpers.GetUserNameByID(int.Parse(personData["Created_by"])));
            personInfoViewer.Location = new Point(120, 60); 
            personInfoViewer.BackColor = Color.White;

        }

        private void ShowLicenseHistory_Load(object sender, EventArgs e)
        {
            this.Controls.Add(personInfoViewer);
            tabPage1.Text = "Local";
            tabPage2.Text = "International";
            dgvLocalLicense.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLocalLicense.DataSource = LicenseController.getLocalLicenseHistory(NationalNumber);
        }

        private void btnCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
