using DVLD_BusinessTier;
using DVLD_ViewTier.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_ViewTier
{
    public partial class PersonInfoViewerWithFilters : UserControl
    {
        public PersonInfoViewerWithFilters()
        {
            InitializeComponent();
        }
        public string getSelectedPersonID()
        {
            return personInfoViewer1.Id;
        }
        private void PersonInfoViewerWithFilters_Load(object sender, EventArgs e)
        {
            cmbFilters.SelectedIndex = 0;

            Image AddPersonImage = Properties.Resources.person_man;
            Image SearchImage = Properties.Resources.SearchPerson;

            int reducedWidth = btnSearch.Width - 10;  
            int reducedHeight = btnSearch.Height - 10; 

            // Ensure the dimensions stay positive
            reducedWidth = Math.Max(1, reducedWidth);
            reducedHeight = Math.Max(1, reducedHeight);

            // Create the resized image
            Image resizedAddPersonImage = new Bitmap(AddPersonImage, new Size(reducedWidth, reducedHeight));
            Image resizedSearchImage = new Bitmap(SearchImage, new Size(reducedWidth, reducedHeight));

            btnAddPerson.Image = resizedAddPersonImage;

            btnAddPerson.ImageAlign = ContentAlignment.MiddleCenter;

            btnAddPerson.Text = "";

            btnSearch.Image = resizedSearchImage;

            btnSearch.ImageAlign = ContentAlignment.MiddleCenter;

            btnSearch.Text = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Dictionary<string,string> data = new Dictionary<string,string>();
           
            if(cmbFilters.SelectedIndex == 0)
            {
                data = Helpers.GetPersonDataByNationalNumber(tbFilterValue.Text);
            }
            else
            {
                data = Helpers.GetPersonDataByID(tbFilterValue.Text);
            }

            if (data != null)
            {
                personInfoViewer1.Id = data["Id"];
                personInfoViewer1.FirstName = data["FirstName"];
                personInfoViewer1.SecondName = data["SecondName"];
                personInfoViewer1.ThirdName = data["ThirdName"];
                personInfoViewer1.LastName = data["LastName"];
                personInfoViewer1.NationalNumber = data["NationalNumber"];
                personInfoViewer1.Gender = Helpers.ConvertToGenderName(int.Parse(data["Gender"]));
                personInfoViewer1.Email = data["Email"];
                personInfoViewer1.Address = data["Address"];
                personInfoViewer1.DateOfBirth = data["DateOfBirth"];
                personInfoViewer1.Phone = data["Phone"];
                personInfoViewer1.ImagePath = data["ImagePath"];
                personInfoViewer1.Created_by = data["Created_by"];

                personInfoViewer1.NationalityCountry = Helpers.GetCountryNameByID(data["NationalityCountryID"]);
                personInfoViewer1.RefreshName();
            }
            else
            {
                MessageBox.Show("Person not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            AddEditPerson addEditPerson = new AddEditPerson();
            addEditPerson.ShowDialog();
        }
    }
}
