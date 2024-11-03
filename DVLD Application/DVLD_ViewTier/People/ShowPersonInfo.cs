using DVLD_BusinessTier;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DVLD_ViewTier.People
{
    public partial class ShowPersonInfo : Form
    {
        PersonInfoViewer personInfoViewer;
        public ShowPersonInfo(Dictionary<string,string> personData)
        {
            InitializeComponent();
            personInfoViewer = new PersonInfoViewer(
                int.Parse(personData["Id"]),
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
                Helpers.GetUserNameByID(int.Parse(personData["Created_by"]))
                );
        }

        private void ShowPersonInfo_Load(object sender, EventArgs e)
        {
            this.Controls.Add(personInfoViewer);
        }
    }
}
