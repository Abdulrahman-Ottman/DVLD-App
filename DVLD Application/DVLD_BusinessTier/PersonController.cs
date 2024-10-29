﻿
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

        public static bool AddNewPerson(string nationalNumber, string firstName, string secondName,
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
                    NationalityCountryID = nationalityCountryID,
                    ImagePath = imagePath,
                    Created_by = createdBy
                };

                return clsPerson.AddNewPerson(person);
            }
    }
}
