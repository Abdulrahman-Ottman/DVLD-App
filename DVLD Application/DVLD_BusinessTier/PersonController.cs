
using System;
using System.Data;
using DVLD_DataAccessTier;


namespace DVLD_BusinessTier
{
    public class PersonController
    {
        public static DataTable GetAllPeople()
        {
            return clsPerson.GetAllPeople();
        }

        public static int AddNewPerson(string nationalNumber, string firstName, string secondName,
                              string thirdName, string lastName, DateTime dateOfBirth, int gender,
                              string address, string phone, string email, string nationalityCountryID,
                              string imagePath, int createdBy)
            {

                clsPerson person = new clsPerson
                {
                    NationalNumber = nationalNumber,
                    FirstName = firstName,
                    SecondName = secondName,
                    ThirdName = thirdName,
                    LastName = lastName,
                    DateOfBirth = dateOfBirth,
                    Gender = gender,
                    Address = address,
                    Phone = phone,
                    Email = email,
                    NationalityCountryID = nationalityCountryID.ToString(),
                    ImagePath = imagePath,
                    Created_by = createdBy
                };

                return clsPerson.AddNewPerson(person);
            }

        public static int UpdatePerson(int id ,string nationalNumber, string firstName, string secondName,
                              string thirdName, string lastName, DateTime dateOfBirth, int gender,
                              string address, string phone, string email, string nationalityCountryID,
                              string imagePath, int createdBy)
        {
            clsPerson person = new clsPerson
            {
                Id = id,
                NationalNumber = nationalNumber,
                FirstName = firstName,
                SecondName = secondName,
                ThirdName = thirdName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Gender = gender,
                Address = address,
                Phone = phone,
                Email = email,
                NationalityCountryID = nationalityCountryID,
                ImagePath = imagePath,
                Created_by = createdBy
            };

            return clsPerson.UpdatePerson(person);
        }
        public static DataTable GetPeopleBasedOnFilter(string filter, string value)
        {
            return clsPerson.GetPeopleBasedOnFilter(filter,value);
        }

        public static bool DeletePerson(int personID)
        {
            return clsPerson.DeletePerson(personID);
        }
    }
}
