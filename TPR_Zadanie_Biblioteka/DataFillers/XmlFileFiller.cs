﻿using System;
using System.Collections.Generic;
using System.Xml;

namespace DL.DataFillers
{
    class XmlFileFiller : IDataFiller
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
        public void Fill(LibraryContext libraryContext, string path)
        {
            libraryContext = new LibraryContext();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);

            foreach (XmlNode node in xmlDocument.DocumentElement)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    Dictionary<string, string> elements = Parse(child);
                    switch (node.Name)
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
}
