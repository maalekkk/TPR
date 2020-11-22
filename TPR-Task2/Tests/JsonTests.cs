using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DL;
using DL.DataObjects;
using DL.DataObjects.EventsObjects;
using TPR_Task2.Serialization;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void JsonSserializeObjectWithoutStructMemebers()
        {
            Author author1 = new Author(Guid.NewGuid(), "Adam", "Malysz");
            JsonFormatter<Author> jsonFormatter = new JsonFormatter<Author>();
            using(Stream stream = File.Open("serialize.json", FileMode.Create, FileAccess.ReadWrite))
                jsonFormatter.Serialize(stream, author1);
            Author author1_copy;

            using(Stream stream = File.Open("serialize.json", FileMode.Open, FileAccess.Read))
                author1_copy = jsonFormatter.Deserialize(stream);

            Assert.AreEqual(author1.Name, author1_copy.Name);
            Assert.AreEqual(author1.Surname, author1_copy.Surname);
            Assert.AreEqual(author1.Id, author1_copy.Id);
        }

        [TestMethod]
        public void JsonSerializeObjectWithStructMemebers()
        {
            Author author1 = new Author(Guid.NewGuid(), "Adam", "Malysz");
            Book book1 = new Book("DSJ Tutorial", author1,"Książka o skokach narciarskich.", Book.BookType.Biographie);
            JsonFormatter<Book> jsonFormatter = new JsonFormatter<Book>();
            using(Stream stream = File.Open("serializeBook.json", FileMode.Create, FileAccess.ReadWrite))
                jsonFormatter.Serialize(stream, book1);
            Book book1Copy;

            using(Stream stream = File.Open("serializeBook.json", FileMode.Open, FileAccess.Read))
                book1Copy = jsonFormatter.Deserialize(stream);
            
            Assert.AreEqual(book1.Name, book1Copy.Name);
            Assert.AreEqual(book1.Description, book1Copy.Description);
            Assert.AreEqual(book1.Author.Name, book1Copy.Author.Name);
            Assert.AreEqual(book1.Author.Surname, book1Copy.Author.Surname);
        }

        [TestMethod]
        public void JsonSerializeObjectWithList()
        {
            Author tolkien = new Author(Guid.NewGuid(), "John Ronald Reuel", "Tolkien");
            Book hobbit = new Book("Hobbit, czyli tam i z powrotem", tolkien, "Powieœæ fantasy dla dzieci autorstwa J.R.R. Tolkiena.", Book.BookType.Fantasy);
            CopyOfBook cob1 = new CopyOfBook(Guid.NewGuid(), hobbit, new DateTime(2004, 12, 3, 0, 0, 0), 0.6);
            CopyOfBook cob2 = new CopyOfBook(Guid.NewGuid(), hobbit, new DateTime(2014, 12, 3, 0, 0, 0), 0.6);
            Employee person2 = new Employee(Guid.NewGuid(), "Katarzyna", "Kowalska", new DateTime(1967, 03, 13), "123456789", "kaska123@outlook.com", Person.Gender.Female, new DateTime(2019, 9, 11));
            Reader person1 = new Reader(Guid.NewGuid(), "Adam", "Nowak", new DateTime(1998, 05, 23), "111222333", "adam.nowak@gmail.com", Person.Gender.Male, new DateTime(2019, 9, 11));
            List<CopyOfBook> booksForRent = new List<CopyOfBook> { cob1, cob2 };
            Rent rent1 = new Rent(Guid.NewGuid(), person1, person2, booksForRent, new DateTime(2010, 1, 6, 0, 0, 0));
            
            JsonFormatter<Rent> jsonFormatter = new JsonFormatter<Rent>();
            
            using(Stream stream = File.Open("serializeRent.json", FileMode.Create, FileAccess.ReadWrite))
                jsonFormatter.Serialize(stream, rent1);
            Rent rent1Copy;

            using(Stream stream = File.Open("serializeRent.json", FileMode.Open, FileAccess.Read))
                rent1Copy = jsonFormatter.Deserialize(stream);
            
            Assert.AreEqual(rent1.Book, rent1Copy.Book);
            Assert.AreEqual(rent1.Employee, rent1Copy.Employee);
            Assert.AreEqual(rent1.Reader, rent1Copy.Reader);
            Assert.AreEqual(rent1.DateOfReturn, rent1Copy.DateOfReturn);
            Assert.AreEqual(rent1.Date, rent1Copy.Date);
            // List<CopyOfBook> booksForReturn = cob1.Book.Keys.ToList();
            // Return returnBooks = new Return(Guid.NewGuid(), new DateTime(2019, 1, 6), booksForReturn, rent1);
        }
    }
}