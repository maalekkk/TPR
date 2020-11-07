using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using DL;
using System.Linq;
using System;
using DL.DataObjects;
using System.Collections.Generic;
using DL.DataObjects.EventsObjects;
using System.Threading;

namespace BusinessLogicTest
{
    [TestClass]
    public class BusinessLogicTest
    {
        private DataService _dataService = new DataService(new LibraryRepository());

        [TestMethod]
        public void initTest()
        {
            Assert.AreEqual(0, _dataService.GetAllAuthors().Count());
            Assert.AreEqual(0, _dataService.GetAllBooks().Count());
            Assert.AreEqual(0, _dataService.GetAllCopiesOfBook().Count());
            Assert.AreEqual(0, _dataService.GetAllEmployees().Count());
            Assert.AreEqual(0, _dataService.GetAllReaders().Count());
            Assert.AreEqual(0, _dataService.GetAllRents().Count());
            Assert.AreEqual(0, _dataService.GetAllReturns().Count());
        }

        [TestMethod]
        public void AddAuthorTest()
        {
            Assert.AreEqual(0, _dataService.GetAllAuthors().Count());
            _dataService.AddAuthor("Jan", "Kowalski");
            Assert.AreEqual(1, _dataService.GetAllAuthors().Count());
        }

        [TestMethod]
        public void AddReaderTest()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataService.AddReader("Maciej", "Lubicz", new DateTime(1989, 8, 23), "123456789", "maciejL124", Person.Gender.Male, DateTime.Now));
            Assert.ThrowsException<ArgumentException>(() => _dataService.AddReader("Maciej", "Lubicz", new DateTime(1989, 8, 23), "1234569", "maciej@outlook.com", Person.Gender.Male, DateTime.Now));
            _dataService.AddReader("Maciej", "Lubicz", new DateTime(1989, 8, 23), "123456789", "maciej@outlook.com", Person.Gender.Male, DateTime.Now);
            Assert.AreEqual(1, _dataService.GetAllReaders().Count());
        }


        [TestMethod]
        public void AddEmployeeTest()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataService.AddEmployee("Maciej", "Lubicz", new DateTime(1989, 8, 23), "123456789", "maciejL124", Person.Gender.Male, DateTime.Now));
            Assert.ThrowsException<ArgumentException>(() => _dataService.AddEmployee("Maciej", "Lubicz", new DateTime(1989, 8, 23), "1234569", "maciej@outlook.com", Person.Gender.Male, DateTime.Now));
            _dataService.AddEmployee("Maciej", "Lubicz", new DateTime(1989, 8, 23), "123456789", "maciej@outlook.com", Person.Gender.Male, DateTime.Now);
            Assert.AreEqual(1, _dataService.GetAllEmployees().Count());
        }

        [TestMethod]
        public void AddBookTest()
        {
            AddAuthorTest();
            Assert.ThrowsException<ArgumentException>(() => _dataService.AddBook("Pan Tadeusz", new Author(Guid.NewGuid(), "Jan", "Brzechwa"), "ksiazka", Book.BookType.Classics));
            _dataService.AddBook("Pan Tadeusz", _dataService.GetAllAuthors().ElementAt(0), "ksiazka", Book.BookType.Classics);
            Assert.AreEqual(1, _dataService.GetAllBooks().Count());

        }


        [TestMethod]
        public void AddCopyOfBookTest()
        {
            AddBookTest();
            Assert.AreEqual(0, _dataService.GetAllCopiesOfBook().Count());
            Assert.ThrowsException<ArgumentException>(() => _dataService.AddCopyOfBook(null, new DateTime(2010, 10, 10), 1.2));
            _dataService.AddCopyOfBook(_dataService.FindBook(b => b.Name.Equals("Pan Tadeusz")), new DateTime(2010, 10, 10), 1.2);
            Assert.AreEqual(1, _dataService.GetAllCopiesOfBook().Count());
        }

        [TestMethod]
        public void AddRentTest()
        {
            AddReaderTest();
            AddEmployeeTest();
            AddCopyOfBookTest();
            List<CopyOfBook> books = new List<CopyOfBook>();
            books.Add(_dataService.GetAllCopiesOfBook().ElementAt(0));
            Assert.ThrowsException<ArgumentException>(() => _dataService.AddRent(new Reader(Guid.NewGuid(), "Maciej", "Lubicz", new DateTime(1989, 8, 23), "123456789", "maciejL124", Person.Gender.Male, DateTime.Now), _dataService.GetAllEmployees().ElementAt(0), books));
            Assert.ThrowsException<ArgumentException>(() => _dataService.AddRent(_dataService.GetAllReaders().ElementAt(0), new Employee(Guid.NewGuid(), "Maciej", "Lubicz", new DateTime(1989, 8, 23), "123456789", "maciejL124", Person.Gender.Male, DateTime.Now), books));
            _dataService.AddRent(_dataService.GetAllReaders().ElementAt(0), _dataService.GetAllEmployees().ElementAt(0), books);
            Assert.AreEqual(1, _dataService.GetAllRents().Count());
            Assert.AreEqual(1, _dataService.GetRentedCopiesOfBooks().Count());
            Assert.AreEqual(true, _dataService.IsCopyOfBookRented(_dataService.GetAllCopiesOfBook().ElementAt(0)));
            Assert.AreEqual(1, _dataService.GetAllCurrentRents().Count());
            Assert.ThrowsException<ArgumentException>(() => _dataService.AddRent(_dataService.GetAllReaders().ElementAt(0), _dataService.GetAllEmployees().ElementAt(0), books));
        }

        [TestMethod]
        public void AddReturnTest()
        {
            AddRentTest();
            Assert.AreEqual(0, _dataService.GetAllReturns().Count());
            List<CopyOfBook> booksFromCollection = new List<CopyOfBook>();
            booksFromCollection.Add(_dataService.GetAllCopiesOfBook().ElementAt(0));
            List<CopyOfBook> booksRandom = new List<CopyOfBook>();
            booksRandom.Add(new CopyOfBook(Guid.NewGuid(), _dataService.GetAllBooks().ElementAt(0), new DateTime(2018, 8, 21), 1.3));
            Assert.ThrowsException<ArgumentException>(() => _dataService.AddReturn(null, null));
            Assert.ThrowsException<ArgumentException>(() => _dataService.AddReturn(_dataService.GetAllRents().ElementAt(0), booksRandom));
            _dataService.AddReturn(_dataService.GetAllRents().ElementAt(0), booksFromCollection);
            Assert.AreEqual(0, _dataService.GetAllCurrentRents().Count());
            Assert.ThrowsException<ArgumentException>(() => _dataService.AddReturn(_dataService.GetAllRents().ElementAt(0), booksFromCollection));
            Assert.AreNotEqual(DateTime.MinValue, _dataService.GetAllRents().ElementAt(0).DateOfReturn);
        }


        [TestMethod]
        public void DeleteAuthorTest()
        {
            AddAuthorTest();
            Assert.AreEqual(1, _dataService.GetAllAuthors().Count());
            Author author = _dataService.FindAuthor(p => (p.Name.Equals("Jan") && p.Surname.Equals("Kowalski")));
            _dataService.DeleteAuthor(author);
            Assert.AreEqual(0, _dataService.GetAllAuthors().Count());
        }

        [TestMethod]
        public void DeleteBookTest()
        {
            AddCopyOfBookTest();
            Assert.AreEqual(1, _dataService.GetAllBooks().Count());
            _dataService.AddBook("Test1", _dataService.FindAuthor(p => p.Name.Equals("Jan")), "des", Book.BookType.Classics);
            Assert.AreEqual(2, _dataService.GetAllBooks().Count());
            _dataService.DeleteBook(_dataService.FindBook(b => b.Name.Equals("Test1")));
            Assert.AreEqual(1, _dataService.GetAllBooks().Count());
            Book book = _dataService.FindBook(b => b.Name.Equals("Pan Tadeusz"));
            Assert.ThrowsException<ArgumentException>(() => _dataService.DeleteBook(book));

        }

        [TestMethod]
        public void DeleteReaderTest()
        {
            AddRentTest();
            Assert.ThrowsException<ArgumentException>(() => _dataService.DeleteReader(_dataService.GetAllReaders().ElementAt(0)));
            _dataService = new DataService(new LibraryRepository());
            AddReturnTest();
            Assert.ThrowsException<ArgumentException>(() => _dataService.DeleteReader(_dataService.GetAllReaders().ElementAt(0)));
            Reader reader = _dataService.GetAllReaders().ElementAt(0);
            _dataService.AddPayment(reader, -reader.Balance);
            _dataService.DeleteReader(reader);
            Assert.AreEqual(0, _dataService.GetAllReaders().Count());
        }

        [TestMethod]
        public void DeleteEmployeeTest()
        {
            AddEmployeeTest();
            Assert.AreEqual(1, _dataService.GetAllEmployees().Count());
            _dataService.DeleteEmployee(_dataService.GetAllEmployees().ElementAt(0));
            Assert.AreEqual(0, _dataService.GetAllEmployees().Count());

        }

        [TestMethod]
        public void DeleteCopyOfBook()
        {
            AddRentTest();
            Assert.AreEqual(1, _dataService.GetAllCopiesOfBook().Count());
            Assert.ThrowsException<ArgumentException>(() => _dataService.DeleteCopyOfBook(_dataService.GetAllCopiesOfBook().ElementAt(0)));
            List<CopyOfBook> books = new List<CopyOfBook>();
            books.Add(_dataService.GetAllCopiesOfBook().ElementAt(0));
            _dataService.AddReturn(_dataService.GetAllRents().ElementAt(0), books);
            _dataService.DeleteCopyOfBook(_dataService.GetAllCopiesOfBook().ElementAt(0));
            Assert.AreEqual(0, _dataService.GetAllCopiesOfBook().Count());
        }
    }
}
