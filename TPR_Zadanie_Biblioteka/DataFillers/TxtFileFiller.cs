﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DL.DataFillers
{
    class TxtFileFiller : IDataFiller
    {
        public void Fill(LibraryContext libraryContext, string path)
        {
            libraryContext = new LibraryContext();
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                int index = line.IndexOf('[');
                string elementName = line.Substring(0, index);
                string[] keysAndValues = line.Substring(index + 1, line.Length - index - 2).Split(';');
                Dictionary<string, string> elements = new Dictionary<string, string>();
                foreach (string keyAndValue in keysAndValues)
                {
                    string[] temp = keyAndValue.Split('=');
                    if (temp.Length == 2)
                        elements.Add(temp[0], temp[1]);
                    else
                        elements.Add(temp[0], null);
                }
                switch (elementName)
                {
                    case "authors":
                        libraryContext.Authors.Add(new Author(elements["id"], elements["name"], elements["surname"]));
                        break;
                    case "books":
                        libraryContext.BooksCatalog.Add(new Book(elements["id"], elements["name"], libraryContext.Authors.Get(elements["author"]), elements["decription"], (Book.BookType)Enum.Parse(typeof(Book.BookType), elements["bBookType"])));
                        break;
                    case "copiesOfBooks":
                        libraryContext.CopiesOfBooks.Add(new CopyOfBook(elements["id"], libraryContext.BooksCatalog.Get(elements["book"]), Convert.ToDateTime(elements["purchaseDate"]), Double.Parse(elements["pricePerDay"])));
                        break;
                    case "employees":
                        libraryContext.Staff.Add(new Employee(elements["id"], elements["name"], elements["surname"], Convert.ToDateTime(elements["birthDate"]), elements["phoneNumber"], elements["email"], (Person.Gender)Enum.Parse(typeof(Person.Gender), elements["gender"]), Convert.ToDateTime(elements["dateOfEmployment"])));
                        break;
                    case "readers":
                        libraryContext.Readers.Add(new Reader(elements["id"], elements["name"], elements["surname"], Convert.ToDateTime(elements["birthDate"]), elements["phoneNumber"], elements["email"], (Person.Gender)Enum.Parse(typeof(Person.Gender), elements["gender"]), Convert.ToDateTime(elements["dateOfRegistration"])));
                        break;
                    case "rents":
                        string[] rentBooksId = elements["rentBooks"].Split(',');
                        List<CopyOfBook> rentBooks = new List<CopyOfBook>();
                        foreach (string rentBookId in rentBooksId)
                        {
                            rentBooks.Add(libraryContext.CopiesOfBooks.Get(rentBookId));
                        }
                        libraryContext.RentsList.Add(new Rent(elements["id"], libraryContext.Readers.Get(elements["reader"]), libraryContext.Staff.Get(elements["employee"]), rentBooks, Convert.ToDateTime(elements["dateOfRental"]), Double.Parse(elements["totalPricePerDay"]), Convert.ToDateTime(elements["dateOfReturn"])));
                        break;
                }
            }
        }
    }
}