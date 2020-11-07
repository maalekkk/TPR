using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using DL.DataObjects;
using DL.DataObjects.EventsObjects;
using DL.Interfaces;

namespace DL.DataFillers
{
    public class XmlFileFiller : IDataFiller
    {
        public Dictionary<string, string> Parse(XmlNode child)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (XmlNode element in child.ChildNodes)
            {
                dictionary.Add(element.Name, element.InnerText);
            }
            return dictionary;
        }
        public void Fill(LibraryContext libraryContext)
        {
            XmlDocument xmlDocument = new XmlDocument();
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            string solutionDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            string path = solutionDir + "\\TPR_Zadanie_Biblioteka\\DataFiles\\data.xml";
            xmlDocument.Load(path);
            foreach (XmlNode node in xmlDocument.DocumentElement)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    Dictionary<string, string> elements = Parse(child);
                    switch (node.Name)
                    {
                        case "authors":
                            libraryContext.Authors.Add(new Author(Guid.Parse(elements["id"]), elements["name"], elements["surname"]));
                            break;
                        case "books":
                            libraryContext.Books.Add(int.Parse(elements["id"]), new Book(elements["name"], libraryContext.Authors.Find(a => a.Id.Equals(Guid.Parse(elements["author"]))), elements["description"], (Book.BookType)Enum.Parse(typeof(Book.BookType), elements["bookType"])));
                            break;
                        case "copiesOfBooks":
                            libraryContext.CopiesOfBooks.Add(new CopyOfBook(Guid.Parse(elements["id"]), libraryContext.Books[int.Parse(elements["book"])], Convert.ToDateTime(elements["purchaseDate"]), Double.Parse(elements["pricePerDay"], nfi)));
                            break;
                        case "employees":
                            libraryContext.Employees.Add(new Employee(Guid.Parse(elements["id"]), elements["name"], elements["surname"], Convert.ToDateTime(elements["birthDate"]), elements["phoneNumber"], elements["email"], (Person.Gender)Enum.Parse(typeof(Person.Gender), elements["gender"]), Convert.ToDateTime(elements["dateOfEmployment"])));
                            break;
                        case "readers":
                            libraryContext.Readers.Add(new Reader(Guid.Parse(elements["id"]), elements["name"], elements["surname"], Convert.ToDateTime(elements["birthDate"]), elements["phoneNumber"], elements["email"], (Person.Gender)Enum.Parse(typeof(Person.Gender), elements["gender"]), Convert.ToDateTime(elements["dateOfRegistration"])));
                            break;
                        case "rents":
                            string[] rentBooksId = elements["rentBooks"].Split(',');
                            List<CopyOfBook> rentBooks = new List<CopyOfBook>();
                            foreach (string rentBookId in rentBooksId)
                            {
                                rentBooks.Add(libraryContext.CopiesOfBooks.Find(c => c.Id.Equals(Guid.Parse(rentBookId))));
                            }
                            libraryContext.Events.Add(new Rent(Guid.Parse(elements["id"]), libraryContext.Readers.Find(r => r.Id.Equals(Guid.Parse(elements["reader"]))), libraryContext.Employees.Find(e => e.Id.Equals(Guid.Parse(elements["employee"]))), rentBooks, Convert.ToDateTime(elements["dateOfRental"]), Convert.ToDateTime(elements["dateOfReturn"])));
                            break;
                    }
                }
            }
        }
    }
}