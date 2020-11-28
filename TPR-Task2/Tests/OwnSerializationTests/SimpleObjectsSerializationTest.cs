using System;
using System.IO;
using DL.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPR_Task2.Serialization;

namespace Tests.OwnSerializationTests
{
    [TestClass]
    public class SimpleObjectsSerializationTest
    {
        [TestMethod]
        public void BookSerializationTest()
        {
            Employee person2 = new Employee(Guid.NewGuid(), "Katarzyna", "Kowalska", new DateTime(1967, 03, 13), "123456789", "kaska123@outlook.com", Employee.Gender.Male, new DateTime(2019, 9, 11));
            Author author1 = new Author(Guid.NewGuid(), "Adam", "Malysz");
            Book book1 = new Book("DSJ Tutorial", author1,"Książka o skokach narciarskich.", Book.BookType.Biographie);
            Book book2 = new Book("Goplana the best chocolate", author1, "Explanation why i love Goplana",
                Book.BookType.Biographie);
            CopyOfBook book1copy1 = new CopyOfBook(Guid.NewGuid(), book1, DateTime.Now, 100);
            CopyOfBook book1copy2 = new CopyOfBook(Guid.NewGuid(), book1, new DateTime(2020, 10, 10), 150);
            // Reader reader2 = new Reader(Guid.NewGuid(), "Adam", "Nowak", new DateTime(1998, 05, 23),
            //     "111222333", "adam.nowak@gmail.com", Person.Gender.Male, new DateTime(2019, 9, 11));
            // Employee employee1 = new Employee(Guid.NewGuid(), "Katarzyna", "Kowalska", new DateTime(1967, 03, 13),
            //     "123456789", "kaska123@outlook.com", Person.Gender.Female, new DateTime(2019, 9, 11));
            // List<CopyOfBook> booksForRent1 = new List<CopyOfBook>() { book1copy1, book1copy2 };
            // Rent rent1 = new Rent(Guid.NewGuid(), reader2, employee1, booksForRent1, DateTime.Now);
            
            OwnFormatter formatter = new OwnFormatter();
            using (Stream stream = File.Open("ownSerializBooook.txt", FileMode.Create, FileAccess.ReadWrite))
            {
                formatter.Serialize(stream,book1copy1);
            }
            
            CopyOfBook book1copy1copy;
            Book book1Copy;
            using (Stream stream = File.Open("ownSerializBooook.txt", FileMode.Open, FileAccess.Read))
            {
                book1copy1copy = (CopyOfBook)formatter.Deserialize(stream);
            }
        }
    }
}