using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DL.DataObjects;
using DL.DataObjects.EventsObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPR_Task2.Serialization;

namespace Tests.JsonSerializationTests
{
    [TestClass]
    public class ObjectsWithComplexMembers
    {
        [TestMethod]
        public void BookSerialization()
        {
            // Creating Books members
            Author author1 = new Author(Guid.NewGuid(), "Adam", "Malysz");
            
            // Creating Book
            Book book1 = new Book("DSJ Tutorial", author1,"Książka o skokach narciarskich.", Book.BookType.Biographie);
            
            // Serialize Book
            JsonFormatter<Book> jsonFormatter = new JsonFormatter<Book>();
            using(Stream stream = File.Open("serializeBook.json", FileMode.Create, FileAccess.ReadWrite))
                jsonFormatter.Serialize(stream, book1);
            
            //Deserialize Book
            Book book1Copy;
            using(Stream stream = File.Open("serializeBook.json", FileMode.Open, FileAccess.Read))
                book1Copy = jsonFormatter.Deserialize(stream);
            
            // Check
            Assert.AreEqual(book1.Name, book1Copy.Name);
            Assert.AreEqual(book1.Description, book1Copy.Description);
            Assert.AreEqual(book1.Author.Name, book1Copy.Author.Name);
            Assert.AreEqual(book1.Author.Surname, book1Copy.Author.Surname);
        }

        [TestMethod]
        public void CopyOfBookSerialization()
        {
            // Creating CopyOfBooks members
            Author orgAuthor = new Author(Guid.NewGuid(), "Adam", "Malysz");
            Book orgBook = new Book("DSJ Tutorial", orgAuthor,"Książka o skokach narciarskich.", Book.BookType.Biographie);
            
            // Creating CopyOfBook
            CopyOfBook orgCopyOfBook = new CopyOfBook(Guid.NewGuid(), orgBook, DateTime.Now, 100);
            
            // Serialize CopyOfBook
            JsonFormatter<CopyOfBook> jsonFormatter = new JsonFormatter<CopyOfBook>();
            using (Stream stream = File.Open("serializedCopyOfBook.json", FileMode.Create, FileAccess.ReadWrite))
            {
                jsonFormatter.Serialize(stream, orgCopyOfBook);
            }

            // Deserialize CopyOfBook
            CopyOfBook copyOfBookCopy;
            using (Stream stream = File.Open("serializedCopyOfBook.json", FileMode.Open, FileAccess.Read))
            {
                copyOfBookCopy = jsonFormatter.Deserialize(stream);
            }
            
            // Check
            Assert.AreEqual(orgCopyOfBook.Id, copyOfBookCopy.Id);
            Assert.AreEqual(orgCopyOfBook.PurchaseDate, copyOfBookCopy.PurchaseDate);
            Assert.AreEqual(orgCopyOfBook.PricePerDay, copyOfBookCopy.PricePerDay);
            Assert.AreEqual(orgCopyOfBook.Book.Name, copyOfBookCopy.Book.Name);
            Assert.AreEqual(orgCopyOfBook.Book.Description, copyOfBookCopy.Book.Description);
        }

        [TestMethod]
        public void RentSerialization()
        {
            // Creating Rents members
            Author tolkien = new Author(Guid.NewGuid(), "John Ronald Reuel", "Tolkien");
            Book hobbit = new Book("Hobbit, czyli tam i z powrotem", tolkien, "Powieœæ fantasy dla dzieci autorstwa J.R.R. Tolkiena.", Book.BookType.Fantasy);
            CopyOfBook cob1 = new CopyOfBook(Guid.NewGuid(), hobbit, new DateTime(2004, 12, 3, 0, 0, 0), 0.6);
            CopyOfBook cob2 = new CopyOfBook(Guid.NewGuid(), hobbit, new DateTime(2014, 12, 3, 0, 0, 0), 0.6);
            Employee person2 = new Employee(Guid.NewGuid(), "Katarzyna", "Kowalska", new DateTime(1967, 03, 13), "123456789", "kaska123@outlook.com", Person.Gender.Female, new DateTime(2019, 9, 11));
            Reader person1 = new Reader(Guid.NewGuid(), "Adam", "Nowak", new DateTime(1998, 05, 23), "111222333", "adam.nowak@gmail.com", Person.Gender.Male, new DateTime(2019, 9, 11));
            List<CopyOfBook> booksForRent = new List<CopyOfBook> { cob1, cob2 };
            
            // Creating Rent
            Rent rent1 = new Rent(Guid.NewGuid(), person1, person2, booksForRent, new DateTime(2010, 1, 6, 0, 0, 0));
           
            // Serialize Rent
            JsonFormatter<Rent> jsonFormatter = new JsonFormatter<Rent>();
            using(Stream stream = File.Open("serializeRent.json", FileMode.Create, FileAccess.ReadWrite))
                jsonFormatter.Serialize(stream, rent1);
           
            // Deserialize Rent
            Rent rent1Copy;
            using(Stream stream = File.Open("serializeRent.json", FileMode.Open, FileAccess.Read))
                rent1Copy = jsonFormatter.Deserialize(stream);
            
            // Check
            Assert.AreEqual(rent1Copy.Book.ElementAt(0), rent1.Book.ElementAt(0));
            Assert.AreEqual(rent1Copy.Book.ElementAt(1), rent1.Book.ElementAt(1));
            Assert.AreEqual(rent1.Employee, rent1Copy.Employee);
            Assert.AreEqual(rent1.Reader, rent1Copy.Reader);
            Assert.AreEqual(rent1.DateOfReturn, rent1Copy.DateOfReturn);
            Assert.AreEqual(rent1.Date, rent1Copy.Date);
            Assert.AreEqual(rent1.Id, rent1Copy.Id);
        }

        [TestMethod]
        public void ReturnSerialization()
        {
            // Creating Returns members
            Author tolkien = new Author(Guid.NewGuid(), "John Ronald Reuel", "Tolkien");
            Book hobbit = new Book("Hobbit, czyli tam i z powrotem", tolkien, "Powieœæ fantasy dla dzieci autorstwa J.R.R. Tolkiena.", Book.BookType.Fantasy);
            CopyOfBook cob1 = new CopyOfBook(Guid.NewGuid(), hobbit, new DateTime(2004, 12, 3, 0, 0, 0), 0.6);
            CopyOfBook cob2 = new CopyOfBook(Guid.NewGuid(), hobbit, new DateTime(2014, 12, 3, 0, 0, 0), 0.6);
            Employee person2 = new Employee(Guid.NewGuid(), "Katarzyna", "Kowalska", new DateTime(1967, 03, 13), "123456789", "kaska123@outlook.com", Person.Gender.Female, new DateTime(2019, 9, 11));
            Reader person1 = new Reader(Guid.NewGuid(), "Adam", "Nowak", new DateTime(1998, 05, 23), "111222333", "adam.nowak@gmail.com", Person.Gender.Male, new DateTime(2019, 9, 11));
            List<CopyOfBook> booksForRent = new List<CopyOfBook> { cob1, cob2 };
            Rent rent1 = new Rent(Guid.NewGuid(), person1, person2, booksForRent, new DateTime(2010, 1, 6, 0, 0, 0));

            // Creating Return
            Return orgReturn = new Return(Guid.NewGuid(), DateTime.Now, booksForRent, rent1);
            
            // Serialize Return
            JsonFormatter<Return> jsonFormatter = new JsonFormatter<Return>();
            using (Stream stream = File.Open("serializedReturn.json", FileMode.Create, FileAccess.ReadWrite))
            {
                jsonFormatter.Serialize(stream, orgReturn);
            }

            // Deserialize Return
            Return returnCopy;
            using (Stream stream = File.Open("serializedReturn.json", FileMode.Open, FileAccess.Read))
            {
                returnCopy = jsonFormatter.Deserialize(stream);
            }
            
            // Check
            Assert.AreEqual(orgReturn.Id, returnCopy.Id);
            Assert.AreEqual(orgReturn.Date, returnCopy.Date);
            for(int i = 0; i < booksForRent.Count; i++)
            {
                Assert.AreEqual(orgReturn.Books.ElementAt(i), returnCopy.Books.ElementAt(i));
            }
            Assert.AreEqual(orgReturn.Rent.Id, returnCopy.Rent.Id);
            Assert.AreEqual(orgReturn.Rent.Employee, returnCopy.Rent.Employee);
        }

        [TestMethod]
        public void PaymentSerialization()
        {
            // Creating Payments members
            Reader person1 = new Reader(Guid.NewGuid(), "Adam", "Nowak", new DateTime(1998, 05, 23), "111222333", "adam.nowak@gmail.com", Person.Gender.Male, new DateTime(2019, 9, 11));
            
            // Creating Payment
            Payment orgPayment = new Payment(Guid.NewGuid(), DateTime.Now, person1, 100);
            
            // Serialize Payment
            JsonFormatter<Payment> jsonFormatter = new JsonFormatter<Payment>();
            using (Stream stream = File.Open("serializedPayment.json", FileMode.Create, FileAccess.ReadWrite))
            {
                jsonFormatter.Serialize(stream, orgPayment);
            }

            // Deserialize Payment
            Payment paymentCopy;
            using (Stream stream = File.Open("serializedPayment.json", FileMode.Open, FileAccess.Read))
            {
                paymentCopy = jsonFormatter.Deserialize(stream);
            }
            
            // Check
            Assert.AreEqual(orgPayment.Id, paymentCopy.Id);
            Assert.AreEqual(orgPayment.Date, paymentCopy.Date);
            Assert.AreEqual(orgPayment.Cash, paymentCopy.Cash);
            Assert.AreEqual(orgPayment.Reader.Id, paymentCopy.Reader.Id);
            Assert.AreEqual(orgPayment.Reader.DateOfRegistration, paymentCopy.Reader.DateOfRegistration);
        }
    }
}