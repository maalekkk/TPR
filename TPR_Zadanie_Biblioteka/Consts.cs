using System;
using System.Collections.Generic;
using System.Text;

namespace DL
{
    public class Consts
    {
        // Magic Consts for option parameter in ICrudOperation.Update() method

        //Author
        public const int AuthorName = 1;
        public const int AuthorSurname = 2;
        //Book
        public const int BookName = 1;
        public const int BookAuthor = 2;
        public const int BookDescription = 3;
        public const int BookBookType = 4;
        //CopyOfBook
        public const int CopyOfBookBook = 1;
        public const int CopyOfBookPurchaseDate = 2;
        public const int CopyOfBookPricePerDay = 3;
        //Person
        public const int PersonName = 1;
        public const int PersonSurname = 2;
        public const int PersonPhoneNumber = 3;
        public const int PersonEmail = 4;
        //Employee
        public const int EmployeeDateOfEmployment = 5;
        //Reader
        public const int ReaderDateOfRegistration = 5;
        //Rent
        public const int RentReader = 1;
        public const int RentEmployee = 2;
        public const int RentBooks = 3;
        public const int RentDateOfRental = 4;
        public const int RentDateOfReturn = 5;
        public const int RentTotalPricePerDay = 6;
    }
}
