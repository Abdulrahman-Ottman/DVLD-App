using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_ViewTier
{
    public partial class PersonInfoViewer : UserControl
    {
        public int Id
        {
            get => int.Parse(lbId.Text);
            set => lbId.Text = value.ToString();
        }

        public string NationalNumber
        {
            get => lbNationalNumber.Text;
            set => lbNationalNumber.Text = value;
        }

        public string FirstName
        {
            //get => lbFirstName.Text;
            //set => lbFirstName.Text = value;
            get; set;
        }

        public string SecondName
        {
            //get => lbSecondName.Text;
            //set => lbSecondName.Text = value;
            get; set;

        }

        public string ThirdName
        {
            //get => lbThirdName.Text;
            //set => lbThirdName.Text = value;
            get; set;

        }

        public string LastName
        {
            //get => lbLastName.Text;
            //set => lbLastName.Text = value;
            get; set;

        }

        public DateTime DateOfBirth
        {
            get => DateTime.Parse(lbDateOfBirth.Text);
            set => lbDateOfBirth.Text = value.ToShortDateString();
        }

        public string Gender
        {
            get => lbGender.Text;
            set => lbGender.Text = value.ToString();
        }

        public string Address
        {
            get => lbAddress.Text;
            set => lbAddress.Text = value;
        }

        public string Phone
        {
            get => lbPhone.Text;
            set => lbPhone.Text = value;
        }

        public string Email
        {
            get => lbEmail.Text;
            set => lbEmail.Text = value;
        }

        public string NationalityCountry
        {
            get => lbCountry.Text;
            set => lbCountry.Text = value;
        }

        public string ImagePath
        {
            get;set;
        }

        public string Created_by
        {
            get;
            set;
        }
        public PersonInfoViewer(
         int id,
         string nationalNumber,
         string firstName,
         string secondName,
         string thirdName,
         string lastName,
         DateTime dateOfBirth,
         string gender,
         string address,
         string phone,
         string email,
         string nationalityCountry,
         string imagePath,
         string createdBy)
        {
            InitializeComponent();

            // Assign data to properties
            Id = id;
            NationalNumber = nationalNumber;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            Phone = phone;
            Email = email;
            NationalityCountry = nationalityCountry;
            ImagePath = imagePath;
            Created_by = createdBy;
        }

        private void PersonInfoViewer_Load(object sender, EventArgs e)
        {
            lbName.Text = $"{FirstName} {SecondName} {ThirdName} {LastName}";
            pictureBox1.Image = Image.FromFile(ImagePath);
        }
    }
}
