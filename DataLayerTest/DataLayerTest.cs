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
            Assert.ThrowsException<Exception>(() => _dataLayer.AddAuthor(fDostojewski));
        }

        [TestMethod]
        public void AddBookTest()
        {
            _dataLayer = new LibraryRepository();
            Assert.AreEqual(0, _dataLayer.GetAllBooks().Count());
            Author tolkien = new Author(Guid.NewGuid(), "John Ronald Reuel", "Tolkien");
            Author fDostojewski = new Author(Guid.NewGuid(), "Fiodor", "Dostojewski");
            Book hobbit = new Book("Hobbit, czyli tam i z powrotem", tolkien,
                "Powie�� fantasy dla dzieci autorstwa J.R.R. Tolkiena.", Book.BookType.Fantasy);
            _dataLayer.AddBook(hobbit, 1);
            Assert.AreEqual(1, _dataLayer.GetAllBooks().Count());
            Book zik = new Book("Zbrodnia i Kara", fDostojewski,
                "Tematem powie�ci s� losy by�ego studenta, Rodiona Raskolnikowa, kt�ry postanawia zamordowa� i obrabowa� star� lichwiark�."
                , Book.BookType.Classics);
            _dataLayer.AddBook(zik, 2);
            Assert.AreEqual(2, _dataLayer.GetAllBooks().Count());
            Book wp = new Book("Wladca Pierscieni", tolkien,
                "Powie�� high fantasy J.R.R. Tolkiena, kt�rej akcja rozgrywa si� w mitologicznym �wiecie �r�dziemia.Jest ona kontynuacj� innej powie�ci tego autora zatytu�owanej Hobbit, czyli tam i z powrotem.",
                Book.BookType.Fantasy);
            _dataLayer.AddBook(wp, 3);
            Assert.AreEqual(3, _dataLayer.GetAllBooks().Count());
            Assert.ThrowsException<Exception>(() => _dataLayer.AddBook(wp, 3));
        }

        [TestMethod]
        public void AddCopyOfBookTest()
        {
            _dataLayer = new LibraryRepository();
            Author tolkien = new Author(Guid.NewGuid(), "John Ronald Reuel", "Tolkien");
            Book hobbit = new Book("Hobbit, czyli tam i z powrotem", tolkien,
                "Powie�� fantasy dla dzieci autorstwa J.R.R. Tolkiena.", Book.BookType.Fantasy);
            CopyOfBook hobbit1 = new CopyOfBook(Guid.NewGuid(), hobbit, new DateTime(2004, 12, 3, 0, 0, 0), 0.6);
            CopyOfBook hobbit2 = new CopyOfBook(Guid.NewGuid(), hobbit, new DateTime(2004, 12, 3, 0, 0, 0), 0.6);
            Assert.AreNotEqual(hobbit1, hobbit2);
            Assert.AreEqual(0, _dataLayer.GetAllCopiesOfBook().Count());
            _dataLayer.AddCopyOfBook(hobbit1);
            Assert.AreEqual(1, _dataLayer.GetAllCopiesOfBook().Count());
            _dataLayer.AddCopyOfBook(hobbit2);
            Assert.AreEqual(2, _dataLayer.GetAllCopiesOfBook().Count());
            Assert.ThrowsException<Exception>(() => _dataLayer.AddCopyOfBook(hobbit1));
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
            Assert.ThrowsException<Exception>(() => _dataLayer.AddEmployee(person2));
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
            Assert.ThrowsException<Exception>(() => _dataLayer.AddReader(person1));
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
                "Powie�� fantasy dla dzieci autorstwa J.R.R. Tolkiena.", Book.BookType.Fantasy);
            Book zik = new Book("Zbrodnia i Kara", fDostojewski,
                "Tematem powie�ci s� losy by�ego studenta, Rodiona Raskolnikowa, kt�ry postanawia zamordowa� i obrabowa� star� lichwiark�."
                , Book.BookType.Classics);
            Book wp = new Book("Wladca Pierscieni", tolkien,
                "Powie�� high fantasy J.R.R. Tolkiena, kt�rej akcja rozgrywa si� w mitologicznym �wiecie �r�dziemia.Jest ona kontynuacj� innej powie�ci tego autora zatytu�owanej Hobbit, czyli tam i z powrotem.",
                Book.BookType.Fantasy);
            CopyOfBook hobbit1 = new CopyOfBook(Guid.NewGuid(), hobbit, new DateTime(2004, 11, 21, 0, 0, 0), 0.4);
            CopyOfBook hobbit2 = new CopyOfBook(Guid.NewGuid(), hobbit, new DateTime(2004, 12, 3, 0, 0, 0), 0.4);
            CopyOfBook zik1 = new CopyOfBook(Guid.NewGuid(), zik, new DateTime(2001, 10, 11, 0, 0, 0), 0.5);
            CopyOfBook zik2 = new CopyOfBook(Guid.NewGuid(), zik, new DateTime(2001, 10, 11, 0, 0, 0), 0.5);
            CopyOfBook wp1 = new CopyOfBook(Guid.NewGuid(), wp, new DateTime(2005, 12, 23, 0, 0, 0), 0.7);
            List<CopyOfBook> booksForRent = new List<CopyOfBook>();
            booksForRent.Add(hobbit1);
            booksForRent.Add(hobbit2);
            Rent rent1 = new Rent(Guid.NewGuid(), person1, person2, booksForRent, new DateTime(2010, 1, 6, 0, 0, 0));
            _dataLayer.AddRent(rent1);
            Assert.AreEqual(1, _dataLayer.GetAllRents().Count());
            List<CopyOfBook> booksForRent2 = new List<CopyOfBook>();
            booksForRent2.Add(zik1);
            booksForRent2.Add(zik2);
            booksForRent2.Add(wp1);
            Rent rent2 = new Rent(Guid.NewGuid(), person1, person2, booksForRent2, new DateTime(2019, 11, 6, 0, 0, 0));
            _dataLayer.AddRent(rent2);
            Assert.AreEqual(2, _dataLayer.GetAllRents().Count());
            Assert.ThrowsException<Exception>(() => _dataLayer.AddRent(rent2));
        }

        // Delete Tests

        [TestMethod]
        public void DeleteRentTest()
        {
            _dataLayer = new LibraryRepository();
            AddRentTest();
            Assert.AreEqual(2, _dataLayer.GetAllRents().Count());
            Rent rent = _dataLayer.GetAllRents().First();
            _dataLayer.DeleteRent(rent);
            Assert.AreEqual(1, _dataLayer.GetAllRents().Count());
            Assert.ThrowsException<InvalidOperationException>(() => _dataLayer.GetRent(rent.Id));
        }

        [TestMethod]
        public void DeleteReaderTest()
        {
            _dataLayer = new LibraryRepository();
            AddReaderTest();
            Assert.AreEqual(1, _dataLayer.GetAllReaders().Count());
            Reader reader = _dataLayer.GetAllReaders().First();
            _dataLayer.DeleteReader(reader);
            Assert.AreEqual(0, _dataLayer.GetAllReaders().Count());
            Assert.AreEqual(null, _dataLayer.GetReader(reader.Id));
        }

        [TestMethod]
        public void DeleteEmployeeTest()
        {
            _dataLayer = new LibraryRepository();
            AddEmployeeTest();
            Assert.AreEqual(1, _dataLayer.GetAllEmployees().Count());
            Employee employee = _dataLayer.GetAllEmployees().First();
            _dataLayer.DeleteEmployee(employee);
            Assert.AreEqual(0, _dataLayer.GetAllEmployees().Count());
            Assert.AreEqual(null, _dataLayer.GetEmployee(employee.Id));
        }

        [TestMethod]
        public void DeleteCopyOfBookTest()
        {
            _dataLayer = new LibraryRepository();
            AddCopyOfBookTest();
            Assert.AreEqual(2, _dataLayer.GetAllCopiesOfBook().Count());
            CopyOfBook copy = _dataLayer.GetAllCopiesOfBook().First();
            _dataLayer.DeleteCopyOfBook(copy);
            Assert.AreEqual(1, _dataLayer.GetAllCopiesOfBook().Count());
            Assert.AreEqual(null, _dataLayer.GetCopyOfBook(copy.Id));
        }

        [TestMethod]
        public void DeleteBookTest()
        {
            _dataLayer = new LibraryRepository();
            AddBookTest();
            Assert.AreEqual(3, _dataLayer.GetAllBooks().Count());
            Book book = _dataLayer.GetAllBooks().First();
            _dataLayer.DeleteBook(book);
            Assert.AreEqual(2, _dataLayer.GetAllBooks().Count());
            Assert.ThrowsException<KeyNotFoundException>(() => _dataLayer.GetBook(4));
        }

        [TestMethod]
        public void DeleteAuthorTest()
        {
            _dataLayer = new LibraryRepository();
            AddAuthorTest();
            Assert.AreEqual(2, _dataLayer.GetAllAuthors().Count());
            Author author = _dataLayer.GetAllAuthors().First();
            _dataLayer.DeleteAuthor(author);
            Assert.AreEqual(1, _dataLayer.GetAllAuthors().Count());
            Assert.AreEqual(null, _dataLayer.GetAuthor(author.Id));
        }

        // Get Object Tests

        [TestMethod]
        public void GetAuthorTest()
        {
            AddAuthorTest();
            Author author = new Author(Guid.NewGuid(), "Alan", "Nieznaniec");
            Author authorFromCollection = _dataLayer.GetAllAuthors().First();
            Assert.AreEqual(_dataLayer.GetAuthor(authorFromCollection.Id), authorFromCollection);
            Assert.AreEqual(null, _dataLayer.GetAuthor(author.Id));
        }

        [TestMethod]
        public void GetBookTest()
        {
            AddBookTest();
            Author author = new Author(Guid.NewGuid(), "Alan", "Nieznaniec");
            Book book = new Book("Book name", author, "Description", Book.BookType.Action);
            Book bookFromColletion = _dataLayer.GetAllBooks().First();
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
