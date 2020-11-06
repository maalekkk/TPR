using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using DL;
using DL.DataFillers;
using DL.Interfaces;
using DL.DataObjects.EventsObjects;
using DL.DataObjects;
using System.Linq;
using System;
using System.Collections.Generic;

namespace DataLayerTest
{
    [TestClass]
    public class DataLayerTest
    {
        private LibraryRepository _dataLayer;

        [TestMethod]
        public void InitTest()
        {
            _dataLayer = new LibraryRepository();
            Assert.IsNull(_dataLayer.DataFiller);
            Assert.AreEqual(0, _dataLayer.GetAllAuthors().Count());
            Assert.AreEqual(0, _dataLayer.GetAllBooks().Count());
            Assert.AreEqual(0, _dataLayer.GetAllCopiesOfBook().Count());
            Assert.AreEqual(0, _dataLayer.GetAllReaders().Count());
            Assert.AreEqual(0, _dataLayer.GetAllEmployees().Count());
            Assert.AreEqual(0, _dataLayer.GetAllRents().Count());
        }

        // Add Tests

        [TestMethod]
        public void AddAuthorTest()
        {
            _dataLayer = new LibraryRepository();
            Assert.AreEqual(0, _dataLayer.GetAllAuthors().Count());
            Author tolkien = new Author(Guid.NewGuid(), "John Ronald Reuel", "Tolkien");
            _dataLayer.AddAuthor(tolkien);
            Assert.AreEqual(1, _dataLayer.GetAllAuthors().Count());
            Author fDostojewski = new Author(Guid.NewGuid(), "Fiodor", "Dostojewski");
            _dataLayer.AddAuthor(fDostojewski);
            Assert.AreEqual(2, _dataLayer.GetAllAuthors().Count());
        }

        [TestMethod]
        public void AddBookTest()
        {
            _dataLayer = new LibraryRepository();
            Assert.AreEqual(0, _dataLayer.GetAllBooks().Count());
            Author tolkien = new Author(Guid.NewGuid(), "John Ronald Reuel", "Tolkien");
            Author fDostojewski = new Author(Guid.NewGuid(), "Fiodor", "Dostojewski");
            Book hobbit = new Book("Hobbit, czyli tam i z powrotem", tolkien,
                "Powieœæ fantasy dla dzieci autorstwa J.R.R. Tolkiena.", Book.BookType.Fantasy);
            _dataLayer.AddBook(hobbit, 1);
            Assert.AreEqual(1, _dataLayer.GetAllBooks().Count());
            Book zik = new Book("Zbrodnia i Kara", fDostojewski,
                "Tematem powieœci s¹ losy by³ego studenta, Rodiona Raskolnikowa, który postanawia zamordowaæ i obrabowaæ star¹ lichwiarkê."
                , Book.BookType.Classics);
            _dataLayer.AddBook(zik, 2);
            Assert.AreEqual(2, _dataLayer.GetAllBooks().Count());
            Book wp = new Book("Wladca Pierscieni", tolkien,
                "Powieœæ high fantasy J.R.R. Tolkiena, której akcja rozgrywa siê w mitologicznym œwiecie Œródziemia.Jest ona kontynuacj¹ innej powieœci tego autora zatytu³owanej Hobbit, czyli tam i z powrotem.",
                Book.BookType.Fantasy);
            _dataLayer.AddBook(wp, 3);
            Assert.AreEqual(3, _dataLayer.GetAllBooks().Count());
        }

        [TestMethod]
        public void AddCopyOfBookTest()
        {
            _dataLayer = new LibraryRepository();
            Author tolkien = new Author(Guid.NewGuid(), "John Ronald Reuel", "Tolkien");
            Book hobbit = new Book("Hobbit, czyli tam i z powrotem", tolkien,
                "Powieœæ fantasy dla dzieci autorstwa J.R.R. Tolkiena.", Book.BookType.Fantasy);
            CopyOfBook hobbit1 = new CopyOfBook(Guid.NewGuid(), hobbit, new DateTime(2004, 12, 3, 0, 0, 0), 0.6);
            CopyOfBook hobbit2 = new CopyOfBook(Guid.NewGuid(), hobbit, new DateTime(2004, 12, 3, 0, 0, 0), 0.6);
            Assert.AreNotEqual(hobbit1, hobbit2);
            Assert.AreEqual(0, _dataLayer.GetAllCopiesOfBook().Count());
            _dataLayer.AddCopyOfBook(hobbit1);
            Assert.AreEqual(1, _dataLayer.GetAllCopiesOfBook().Count());
            _dataLayer.AddCopyOfBook(hobbit2);
            Assert.AreEqual(2, _dataLayer.GetAllCopiesOfBook().Count());
        }

        [TestMethod]
        public void AddEmployeeTest()
        {
            _dataLayer = new LibraryRepository();
            Assert.AreEqual(0, _dataLayer.GetAllEmployees().Count());
            Employee person2 = new Employee(Guid.NewGuid(), "Katarzyna", "Kowalska", new DateTime(1967, 03, 13),
             "123456789", "kaska123@outlook.com", Person.Gender.Female, new DateTime(2019, 9, 11));
            _dataLayer.AddEmployee(person2);
            Assert.AreEqual(1, _dataLayer.GetAllEmployees().Count());
        }

        [TestMethod]
        public void AddReaderTest()
        {
            _dataLayer = new LibraryRepository();
            Assert.AreEqual(0, _dataLayer.GetAllReaders().Count());
            Reader person1 = new Reader(Guid.NewGuid(), "Adam", "Nowak", new DateTime(1998, 05, 23),
            "111222333", "adam.nowak@gmail.com", Person.Gender.Male, new DateTime(2019, 9, 11));
            _dataLayer.AddReader(person1);
            Assert.AreEqual(1, _dataLayer.GetAllReaders().Count());
        }

        [TestMethod]
        public void AddRentTest()
        {
            _dataLayer = new LibraryRepository();
            Assert.AreEqual(0, _dataLayer.GetAllRents().Count());
            Reader person1 = new Reader(Guid.NewGuid(), "Adam", "Nowak", new DateTime(1998, 05, 23),
                "111222333", "adam.nowak@gmail.com", Person.Gender.Male, new DateTime(2019, 9, 11));
            Employee person2 = new Employee(Guid.NewGuid(), "Katarzyna", "Kowalska", new DateTime(1967, 03, 13),
                "123456789", "kaska123@outlook.com", Person.Gender.Female, new DateTime(2019, 9, 11));
            Author tolkien = new Author(Guid.NewGuid(), "John Ronald Reuel", "Tolkien");
            Author fDostojewski = new Author(Guid.NewGuid(), "Fiodor", "Dostojewski");
            Book hobbit = new Book("Hobbit, czyli tam i z powrotem", tolkien,
                "Powieœæ fantasy dla dzieci autorstwa J.R.R. Tolkiena.", Book.BookType.Fantasy);
            Book zik = new Book("Zbrodnia i Kara", fDostojewski,
                "Tematem powieœci s¹ losy by³ego studenta, Rodiona Raskolnikowa, który postanawia zamordowaæ i obrabowaæ star¹ lichwiarkê."
                , Book.BookType.Classics);
            Book wp = new Book("Wladca Pierscieni", tolkien,
                "Powieœæ high fantasy J.R.R. Tolkiena, której akcja rozgrywa siê w mitologicznym œwiecie Œródziemia.Jest ona kontynuacj¹ innej powieœci tego autora zatytu³owanej Hobbit, czyli tam i z powrotem.",
                Book.BookType.Fantasy);
            CopyOfBook hobbit1 = new CopyOfBook(Guid.NewGuid(), hobbit, new DateTime(2004, 11, 21, 0, 0, 0), 0.4);
            CopyOfBook hobbit2 = new CopyOfBook(Guid.NewGuid(), hobbit, new DateTime(2004, 12, 3, 0, 0, 0), 0.4);
            CopyOfBook zik1 = new CopyOfBook(Guid.NewGuid(), zik, new DateTime(2001, 10, 11, 0, 0, 0), 0.5);
            CopyOfBook zik2 = new CopyOfBook(Guid.NewGuid(), zik, new DateTime(2001, 10, 11, 0, 0, 0), 0.5);
            CopyOfBook wp1 = new CopyOfBook(Guid.NewGuid(), wp, new DateTime(2005, 12, 23, 0, 0, 0), 0.7);
            List<CopyOfBook> booksForRent = new List<CopyOfBook>();
            booksForRent.Add(hobbit1);
            booksForRent.Add(hobbit2);
            double totalPrice = 0;
            foreach (CopyOfBook book in booksForRent)
            {
                totalPrice += book.PricePerDay;
            }
            Rent rent1 = new Rent(Guid.NewGuid(), person1, person2, booksForRent, new DateTime(2010, 1, 6, 0, 0, 0));
            _dataLayer.AddRent(rent1);
            Assert.AreEqual(1, _dataLayer.GetAllRents().Count());
            List<CopyOfBook> booksForRent2 = new List<CopyOfBook>();
            booksForRent2.Add(zik1);
            booksForRent2.Add(zik2);
            booksForRent2.Add(wp1);
            totalPrice = 0;
            foreach (CopyOfBook book in booksForRent2)
            {
                totalPrice += book.PricePerDay;
            }
            Rent rent2 = new Rent(Guid.NewGuid(), person1, person2, booksForRent2, new DateTime(2019, 11, 6, 0, 0, 0));
            _dataLayer.AddRent(rent2);
            Assert.AreEqual(2, _dataLayer.GetAllRents().Count());
        }

        // Delete Tests

        [TestMethod]
        public void DeleteRentTest()
        {
            _dataLayer = new LibraryRepository();
            AddRentTest();
            Assert.AreEqual(2, _dataLayer.GetAllRents().Count());
            Rent rent = _dataLayer.GetAllRents().First();
            _dataLayer.DeleteRent(_dataLayer.GetAllRents().First());
            Assert.AreEqual(1, _dataLayer.GetAllRents().Count());
        }

        [TestMethod]
        public void DeleteReaderTest()
        {
            _dataLayer = new LibraryRepository();
            AddReaderTest();
            Assert.AreEqual(1, _dataLayer.GetAllReaders().Count());
            _dataLayer.DeleteReader(_dataLayer.GetAllReaders().First());
            Assert.AreEqual(0, _dataLayer.GetAllReaders().Count());
        }

        [TestMethod]
        public void DeleteEmployeeTest()
        {
            _dataLayer = new LibraryRepository();
            AddEmployeeTest();
            Assert.AreEqual(1, _dataLayer.GetAllEmployees().Count());
            _dataLayer.DeleteEmployee(_dataLayer.GetAllEmployees().First());
            Assert.AreEqual(0, _dataLayer.GetAllEmployees().Count());
        }

        [TestMethod]
        public void DeleteCopyOfBookTest()
        {
            _dataLayer = new LibraryRepository();
            AddCopyOfBookTest();
            Assert.AreEqual(2, _dataLayer.GetAllCopiesOfBook().Count());
            _dataLayer.DeleteCopyOfBook(_dataLayer.GetAllCopiesOfBook().First());
            Assert.AreEqual(1, _dataLayer.GetAllCopiesOfBook().Count());

        }

        [TestMethod]
        public void DeleteBookTest()
        {
            _dataLayer = new LibraryRepository();
            Assert.Inconclusive();
        }

        [TestMethod]
        public void DeleteAuthorTest()
        {
            _dataLayer = new LibraryRepository();
            Assert.Inconclusive();
        }

        // Get Object Tests

        [TestMethod]
        public void GetAuthorTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetBookTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetCopyOfBookTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetEmployeeTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetReaderTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetRentTest()
        {
            Assert.Inconclusive();
        }

        //Get All Objects Tests

        [TestMethod]
        public void GetAllAuthorsTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetAllBooksTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetAllCopiesOfBooksTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetAllEmployeesTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetAllReadersTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetAllRentsTest()
        {
            Assert.Inconclusive();
        }

        // Update Tests

        [TestMethod]
        public void UpdateAuthorTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void UpdateBookTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void UpdateCopyOfBookTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void UpdateEmployeeTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void UpdateReaderTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void UpdateRentTest()
        {
            Assert.Inconclusive();
        }

        // Dependency Injection Test

        [TestMethod]
        public void DependencyInjectionTest()
        {
            Assert.Inconclusive();
        }
    }
}
