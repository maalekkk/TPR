using System;
using System.Collections.Generic;
using System.IO;
using DL.DataObjects;
using DL.DataObjects.EventsObjects;
using DL.Interfaces;
using System.Globalization;

namespace DL.DataFillers
{
    public class TxtFileFiller : IDataFiller
    {
        public void Fill(LibraryContext libraryContext)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            string solutionDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            string path = solutionDir + "\\TPR_Zadanie_Biblioteka\\DataFiles\\data.txt";
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
                    case "author":
                        libraryContext.Authors.Add(new Author(Guid.Parse(elements["id"]), elements["name"], elements["surname"]));
                        break;
                    case "book":
                        libraryContext.Books.Add(Convert.ToInt32(elements["id"]), new Book(elements["name"], libraryContext.Authors.Find(a => a.Id.Equals(Guid.Parse(elements["author"]))), elements["description"], (Book.BookType)Enum.Parse(typeof(Book.BookType), elements["bookType"])));
                        break;
                    case "copyOfBook":
                        libraryContext.CopiesOfBooks.Add(new CopyOfBook(Guid.Parse(elements["id"]), libraryContext.Books[Convert.ToInt32(elements["book"])], Convert.ToDateTime(elements["purchaseDate"]), Double.Parse(elements["pricePerDay"], nfi)));
                        break;
                    case "employee":
                        libraryContext.Employees.Add(new Employee(Guid.Parse(elements["id"]), elements["name"], elements["surname"], Convert.ToDateTime(elements["birthDate"]), elements["phoneNumber"], elements["email"], (Person.Gender)Enum.Parse(typeof(Person.Gender), elements["gender"]), Convert.ToDateTime(elements["dateOfEmployment"])));
                        break;
                    case "reader":
                        libraryContext.Readers.Add(new Reader(Guid.Parse(elements["id"]), elements["name"], elements["surname"], Convert.ToDateTime(elements["birthDate"]), elements["phoneNumber"], elements["email"], (Person.Gender)Enum.Parse(typeof(Person.Gender), elements["gender"]), Convert.ToDateTime(elements["dateOfRegistration"])));
                        break;
                    case "rent":
                        string[] rentBooksId = elements["rentBooks"].Split(',');
                        List<CopyOfBook> rentBooks = new List<CopyOfBook>();
                        foreach (string rentBookId in rentBooksId)
                        {
                            rentBooks.Add(libraryContext.CopiesOfBooks.Find(c => c.Id.Equals(Guid.Parse(rentBookId))));
                        }
                        libraryContext.Rents.Add(new Rent(Guid.Parse(elements["id"]), libraryContext.Readers.Find(r => r.Id.Equals(Guid.Parse(elements["reader"]))), libraryContext.Employees.Find(e => e.Id.Equals(Guid.Parse(elements["employee"]))), rentBooks, Convert.ToDateTime(elements["dateOfRental"]), Convert.ToDateTime(elements["dateOfReturn"])));
                        break;
                }
            }
        }
    }
}