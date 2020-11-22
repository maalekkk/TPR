using Microsoft.VisualStudio.TestTools.UnitTesting;
using DL;
using DL.DataFillers;
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
        private LibraryRepository _dataLayer = new LibraryRepository();

        [TestMethod]
        public void InitTest()
        {
            Assert.IsNull(_dataLayer.DataFiller);
            Assert.AreEqual(0, _dataLayer.GetAllAuthors().Count());
            Assert.AreEqual(0, _dataLayer.GetAllBooks().Count());
            Assert.AreEqual(0, _dataLayer.GetAllCopiesOfBook().Count());
            Assert.AreEqual(0, _dataLayer.GetAllReaders().Count());
            Assert.AreEqual(0, _dataLayer.GetAllEmployees().Count());
            Assert.AreEqual(0, _dataLayer.GetAllEvents().Count());
        }

        // Add Tests

        [TestMethod]
        public void AddAuthorTest()
        {
            Assert.AreEqual(0, _dataLayer.GetAllAuthors().Count());
            Author tolkien = new Author(Guid.NewGuid(), "John Ronald Reuel", "Tolkien");
            _dataLayer.AddAuthor(tolkien);
            Assert.AreEqual(1, _dataLayer.GetAllAuthors().Count());
            Author fDostojewski = new Author(Guid.NewGuid(), "Fiodor", "Dostojewski");
            _dataLayer.AddAuthor(fDostojewski);
            Assert.AreEqual(2, _dataLayer.GetAllAuthors().Count());
            Assert.ThrowsException<ArgumentException>(() => _dataLayer.AddAuthor(fDostojewski));
        }

        [TestMethod]
        public void AddBookTest()
        {
            AddAuthorTest();
            Assert.AreEqual(0, _dataLayer.GetAllBooks().Count());
            Book hobbit = new Book("Hobbit, czyli tam i z powrotem", _dataLayer.FindAuthor(a => a.Name.Equals("Fiodor")), "Powie�� fantasy dla dzieci autorstwa J.R.R. Tolkiena.", Book.BookType.Fantasy);
            _dataLayer.AddBook(hobbit);
            Assert.AreEqual(1, _dataLayer.GetAllBooks().Count());
            Book zik = new Book("Zbrodnia i Kara", _dataLayer.FindAuthor(a => a.Name.Equals("Fiodor")), "Tematem powie�ci s� losy by�ego studenta, Rodiona Raskolnikowa, kt�ry postanawia zamordowa� i obrabowa� star� lichwiark�.", Book.BookType.Classics);
            _dataLayer.AddBook(zik);
            Assert.AreEqual(2, _dataLayer.GetAllBooks().Count());
            Book wp = new Book("Wladca Pierscieni", _dataLayer.FindAuthor(a => a.Surname.Equals("Tolkien")), "Powie�� high fantasy J.R.R. Tolkiena, kt�rej akcja rozgrywa si� w mitologicznym �wiecie �r�dziemia.Jest ona kontynuacj� innej powie�ci tego autora zatytu�owanej Hobbit, czyli tam i z powrotem.", Book.BookType.Fantasy);
            _dataLayer.AddBook(wp);
            Assert.AreEqual(3, _dataLayer.GetAllBooks().Count());
            Assert.ThrowsException<ArgumentException>(() => _dataLayer.AddBook(wp));
        }

        [TestMethod]
        public void AddCopyOfBookTest()
        {
            AddBookTest();
            CopyOfBook cob1 = new CopyOfBook(Guid.NewGuid(), _dataLayer.FindBook(b => b.Name.Equals("Zbrodnia i Kara")), new DateTime(2004, 12, 3, 0, 0, 0), 0.6);
            CopyOfBook cob2 = new CopyOfBook(Guid.NewGuid(), _dataLayer.FindBook(b => b.Name.Equals("Zbrodnia i Kara")), new DateTime(2014, 12, 3, 0, 0, 0), 0.6);
            Assert.AreNotEqual(cob1, cob2);
            Assert.AreEqual(0, _dataLayer.GetAllCopiesOfBook().Count());
            _dataLayer.AddCopyOfBook(cob1);
            Assert.AreEqual(1, _dataLayer.GetAllCopiesOfBook().Count());
            _dataLayer.AddCopyOfBook(cob2);
            Assert.AreEqual(2, _dataLayer.GetAllCopiesOfBook().Count());
            Assert.ThrowsException<ArgumentException>(() => _dataLayer.AddCopyOfBook(cob1));
            CopyOfBook zik1 = new CopyOfBook(Guid.NewGuid(), _dataLayer.GetAllBooks().ElementAt(0), new DateTime(2001, 10, 11, 0, 0, 0), 0.5);
            CopyOfBook zik2 = new CopyOfBook(Guid.NewGuid(), _dataLayer.GetAllBooks().ElementAt(0), new DateTime(2002, 10, 11, 0, 0, 0), 0.5);
            CopyOfBook wp1 = new CopyOfBook(Guid.NewGuid(), _dataLayer.GetAllBooks().ElementAt(0), new DateTime(2005, 12, 23, 0, 0, 0), 0.7);
            _dataLayer.AddCopyOfBook(zik1);
            _dataLayer.AddCopyOfBook(zik2);
            _dataLayer.AddCopyOfBook(wp1);
            Assert.AreEqual(5, _dataLayer.GetAllCopiesOfBook().Count());
        }

        [TestMethod]
        public void AddEmployeeTest()
        {
            Assert.AreEqual(0, _dataLayer.GetAllEmployees().Count());
            Employee person2 = new Employee(Guid.NewGuid(), "Katarzyna", "Kowalska", new DateTime(1967, 03, 13), "123456789", "kaska123@outlook.com", Person.Gender.Female, new DateTime(2019, 9, 11));
            _dataLayer.AddEmployee(person2);
            Assert.AreEqual(1, _dataLayer.GetAllEmployees().Count());
            Assert.ThrowsException<ArgumentException>(() => _dataLayer.AddEmployee(person2));
        }

        [TestMethod]
        public void AddReaderTest()
        {
            Assert.AreEqual(0, _dataLayer.GetAllReaders().Count());
            Reader person1 = new Reader(Guid.NewGuid(), "Adam", "Nowak", new DateTime(1998, 05, 23), "111222333", "adam.nowak@gmail.com", Person.Gender.Male, new DateTime(2019, 9, 11));
            _dataLayer.AddReader(person1);
            Assert.AreEqual(1, _dataLayer.GetAllReaders().Count());
            Assert.ThrowsException<ArgumentException>(() => _dataLayer.AddReader(person1));
        }

        [TestMethod]
        public void AddRentTest()
        {
            AddCopyOfBookTest();
            AddEmployeeTest();
            AddReaderTest();
            Assert.AreEqual(0, _dataLayer.GetAllEvents().Count());
            List<CopyOfBook> booksForRent = new List<CopyOfBook> { _dataLayer.FindCopyOfBook(c => (c.Book.Name.Equals("Zbrodnia i Kara") && c.PurchaseDate.Year.Equals(2014))), _dataLayer.FindCopyOfBook(c => (c.Book.Name.Equals("Zbrodnia i Kara") && c.PurchaseDate.Year.Equals(2004))) };
            Rent rent1 = new Rent(Guid.NewGuid(), _dataLayer.FindReader(r => r.Name.Equals("Adam")), _dataLayer.FindEmployee(e => e.Name.Equals("Katarzyna")), booksForRent, new DateTime(2010, 1, 6, 0, 0, 0));
            _dataLayer.AddEvent(rent1);
            Assert.AreEqual(1, _dataLayer.GetAllEvents().Count());
            List<CopyOfBook> booksForRent2 = new List<CopyOfBook> { _dataLayer.GetAllCopiesOfBook().ElementAt(2), _dataLayer.GetAllCopiesOfBook().ElementAt(3), _dataLayer.GetAllCopiesOfBook().ElementAt(4) };
            Rent rent2 = new Rent(Guid.NewGuid(), _dataLayer.FindReader(r => r.Name.Equals("Adam")), _dataLayer.FindEmployee(e => e.Name.Equals("Katarzyna")), booksForRent2, new DateTime(2019, 11, 6, 0, 0, 0));
            _dataLayer.AddEvent(rent2);
            Assert.AreEqual(2, _dataLayer.GetAllEvents().Count());
            Assert.ThrowsException<ArgumentException>(() => _dataLayer.AddEvent(rent2));
        }

        [TestMethod]
        public void AddReturnTest()
        {
            AddRentTest();
            Assert.AreEqual(2, _dataLayer.GetAllEvents().Count());
            List<CopyOfBook> booksForReturn = ((Rent)_dataLayer.GetAllEvents().ElementAt(0)).Book;
            Return returnBooks = new Return(Guid.NewGuid(), new DateTime(2019, 1, 6), booksForReturn, (Rent)_dataLayer.GetAllEvents().ElementAt(0));
            _dataLayer.AddEvent(returnBooks);
            Assert.AreEqual(3, _dataLayer.GetAllEvents().Count());
            Assert.ThrowsException<ArgumentException>(() => _dataLayer.AddEvent(returnBooks));
        }

        // Delete Tests

        [TestMethod]
        public void DeleteReaderTest()
        {
            AddReaderTest();
            Assert.AreEqual(1, _dataLayer.GetAllReaders().Count());
            Reader reader = _dataLayer.GetAllReaders().First();
            _dataLayer.DeleteReader(reader);
            Assert.AreEqual(0, _dataLayer.GetAllReaders().Count());
            Assert.IsNull(_dataLayer.GetReader(reader.Id));
        }

        [TestMethod]
        public void DeleteEmployeeTest()
        {
            AddEmployeeTest();
            Assert.AreEqual(1, _dataLayer.GetAllEmployees().Count());
            Employee employee = _dataLayer.GetAllEmployees().First();
            _dataLayer.DeleteEmployee(employee);
            Assert.AreEqual(0, _dataLayer.GetAllEmployees().Count());
            Assert.IsNull(_dataLayer.GetEmployee(employee.Id));
        }

        [TestMethod]
        public void DeleteCopyOfBookTest()
        {
            AddCopyOfBookTest();
            Assert.AreEqual(5, _dataLayer.GetAllCopiesOfBook().Count());
            CopyOfBook copy = _dataLayer.GetAllCopiesOfBook().First();
            _dataLayer.DeleteCopyOfBook(copy);
            Assert.AreEqual(4, _dataLayer.GetAllCopiesOfBook().Count());
            Assert.IsNull(_dataLayer.GetCopyOfBook(copy.Id));
        }

        [TestMethod]
        public void DeleteBookTest()
        {
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
            Author authorFromCollection = _dataLayer.GetAllAuthors().First();
            Assert.AreEqual(_dataLayer.GetAuthor(authorFromCollection.Id), authorFromCollection);
            Assert.AreEqual(null, _dataLayer.GetAuthor(Guid.NewGuid()));
        }

        [TestMethod]
        public void GetBookTest()
        {
            AddBookTest();
            Book bookFromColletion = _dataLayer.GetAllBooks().First();
            Assert.AreEqual(_dataLayer.GetBook(0), bookFromColletion);
            Assert.ThrowsException<KeyNotFoundException>(() => _dataLayer.GetBook(8));
        }

        [TestMethod]
        public void GetCopyOfBookTest()
        {
            AddCopyOfBookTest();
            CopyOfBook copyOfBookFromCollection = _dataLayer.GetAllCopiesOfBook().First();
            Assert.AreEqual(copyOfBookFromCollection, _dataLayer.GetCopyOfBook(copyOfBookFromCollection.Id));
            Assert.AreEqual(null, _dataLayer.GetCopyOfBook(Guid.NewGuid()));
        }

        [TestMethod]
        public void GetEmployeeTest()
        {
            AddEmployeeTest();
            Employee employee = new Employee(Guid.NewGuid(), "Alan", "Kot", new DateTime(1990, 10, 8), "123456789", "ak@gmail.com", Person.Gender.Male, new DateTime(2002,7,2));
            Employee employeeFromCollection = _dataLayer.GetAllEmployees().First();
            Assert.AreEqual(employeeFromCollection, _dataLayer.GetEmployee(employeeFromCollection.Id));
            Assert.AreEqual(null, _dataLayer.GetEmployee(Guid.NewGuid()));
        }

        [TestMethod]
        public void GetReaderTest()
        {
            AddReaderTest();
            Reader readerFromCollection = _dataLayer.GetAllReaders().First();
            Assert.AreEqual(readerFromCollection, _dataLayer.GetReader(readerFromCollection.Id));
            Assert.AreEqual(null, _dataLayer.GetReader(Guid.NewGuid()));
        }

        [TestMethod]
        public void GetRentTest()
        {
            AddRentTest();
            Rent rentFromCollection = (Rent)_dataLayer.GetAllEvents().First(e => e.GetType().Equals(typeof(Rent)));
            Assert.AreEqual(rentFromCollection, _dataLayer.GetEvent(rentFromCollection.Id));
            Assert.ThrowsException<InvalidOperationException>(() => _dataLayer.GetEvent(Guid.NewGuid()));
        }

        [TestMethod]
        public void GetReturnTest()
        {
            AddReturnTest();
            Return returnFromCollection = (Return)_dataLayer.GetAllEvents().First(e => e.GetType().Equals(typeof(Return)));
            Assert.AreEqual(returnFromCollection, _dataLayer.GetEvent(returnFromCollection.Id));
            Assert.ThrowsException<InvalidOperationException>(() => _dataLayer.GetEvent(Guid.NewGuid()));
        }

        // Update Tests

        [TestMethod]
        public void UpdateAuthorTest()
        {
            AddAuthorTest();
            IEnumerable<Author> authors = _dataLayer.GetAllAuthors();
            Guid id = authors.ElementAt(0).Id;
            Author newAuthor = new Author(id, "Matt", "Stone");
            _dataLayer.UpdateAuthor(id, newAuthor);
            Assert.AreEqual(newAuthor.Name, authors.ElementAt(0).Name);
            Assert.AreEqual(newAuthor.Surname, authors.ElementAt(0).Surname);
            Author newAuthor2 = new Author(Guid.NewGuid(), "Trey", "Parker");
            Assert.ThrowsException<ArgumentException>(() => _dataLayer.UpdateAuthor(id, newAuthor2));

        }

        [TestMethod]
        public void UpdateBookTest()
        {
            AddBookTest();
            Author tolkien = new Author(Guid.NewGuid(), "John Ronald Reuel", "Tolkien");
            Book newBook = new Book("Silmarillion", tolkien, "123", Book.BookType.Fantasy);
            Assert.ThrowsException<ArgumentException>(() => _dataLayer.UpdateBook(5, newBook));
            _dataLayer.UpdateBook(1, newBook);
            Assert.AreEqual(newBook, _dataLayer.GetBook(1));

        }

        [TestMethod]
        public void UpdateCopyOfBookTest()
        {
            ConstObjectsFiller cof = new ConstObjectsFiller();
            _dataLayer = new LibraryRepository();
            _dataLayer.DataFiller = cof;
            _dataLayer.FillData();
            CopyOfBook newBook = new CopyOfBook(Guid.NewGuid(), _dataLayer.GetAllBooks().ElementAt(0), DateTime.Now, 21);
            Guid id = _dataLayer.GetAllCopiesOfBook().ElementAt(0).Id;
            Assert.ThrowsException<ArgumentException>(() => _dataLayer.UpdateCopyOfBook(id, newBook));
            Assert.ThrowsException<ArgumentException>(() => _dataLayer.UpdateCopyOfBook(Guid.NewGuid(), newBook));
            newBook = new CopyOfBook(id, _dataLayer.GetAllBooks().ElementAt(0), DateTime.Now, 21);
            _dataLayer.UpdateCopyOfBook(id, newBook);
            Assert.AreEqual(_dataLayer.GetCopyOfBook(id).PricePerDay, newBook.PricePerDay);
        }

        [TestMethod]
        public void UpdateEmployeeTest()
        {
            ConstObjectsFiller cof = new ConstObjectsFiller();
            _dataLayer = new LibraryRepository();
            _dataLayer.DataFiller = cof;
            _dataLayer.FillData();
            Employee newEmployee = new Employee(Guid.NewGuid(), "Robert", "Mak�owicz", new DateTime(1973, 3, 12), "123456789", "rm@123.com", Person.Gender.Male, DateTime.Now);
            Guid id = _dataLayer.GetAllEmployees().ElementAt(0).Id;
            Assert.ThrowsException<ArgumentException>(() => _dataLayer.UpdateEmployee(id, newEmployee));
            Assert.ThrowsException<ArgumentException>(() => _dataLayer.UpdateEmployee(Guid.NewGuid(), newEmployee));
            newEmployee = new Employee(id, "Robert", "Mak�owicz", new DateTime(1973, 3, 12), "123456789", "rm@123.com", Person.Gender.Male, DateTime.Now);
            _dataLayer.UpdateEmployee(id, newEmployee);
            Assert.AreEqual(newEmployee.Email, _dataLayer.GetEmployee(id).Email);
            Assert.AreEqual(newEmployee.Name, _dataLayer.GetEmployee(id).Name);
        }

        [TestMethod]
        public void UpdateReaderTest()
        {
            ConstObjectsFiller cof = new ConstObjectsFiller();
            _dataLayer = new LibraryRepository();
            _dataLayer.DataFiller = cof;
            _dataLayer.FillData();
            Reader newReader = new Reader(Guid.NewGuid(), "Andrzej", "Go�ota", new DateTime(1973, 10, 10), "987654321", "goandrew@hotmail.com", Person.Gender.Male, DateTime.Now);
            Guid id = _dataLayer.GetAllReaders().ElementAt(0).Id;
            Assert.ThrowsException<ArgumentException>(() => _dataLayer.UpdateReader(id, newReader));
            Assert.ThrowsException<ArgumentException>(() => _dataLayer.UpdateReader(Guid.NewGuid(), newReader));
            newReader = new Reader(id, "Andrzej", "Go�ota", new DateTime(1973, 10, 10), "987654321", "goandrew@hotmail.com", Person.Gender.Male, DateTime.Now);
            _dataLayer.UpdateReader(id, newReader);
            Assert.AreEqual(newReader.Email, _dataLayer.GetReader(id).Email);
            Assert.AreEqual(newReader.Name, _dataLayer.GetReader(id).Name);
        }

        // Dependency Injection Test

        [TestMethod]
        public void DependencyInjectionTest()
        {
            //Checking DataFiller property is null
            _dataLayer = new LibraryRepository();
            Assert.AreEqual(_dataLayer.DataFiller, null);
            _dataLayer.FillData();
            Assert.AreEqual(0, _dataLayer.GetAllAuthors().Count());
            Assert.AreEqual(0, _dataLayer.GetAllBooks().Count());
            Assert.AreEqual(0, _dataLayer.GetAllReaders().Count());
            Assert.AreEqual(0, _dataLayer.GetAllEmployees().Count());
            Assert.AreEqual(0, _dataLayer.GetAllCopiesOfBook().Count());
            Assert.AreEqual(0, _dataLayer.GetAllEvents().Count());
            //Inject IDataFiller impelentation (Fill with Consts)
            ConstObjectsFiller cof = new ConstObjectsFiller();
            _dataLayer.DataFiller = cof;
            _dataLayer.FillData();
            Assert.AreEqual(2, _dataLayer.GetAllAuthors().Count());
            Assert.AreEqual(3, _dataLayer.GetAllBooks().Count());
            Assert.AreEqual(1, _dataLayer.GetAllReaders().Count());
            Assert.AreEqual(1, _dataLayer.GetAllEmployees().Count());
            Assert.AreEqual(5, _dataLayer.GetAllCopiesOfBook().Count());
            Assert.AreEqual(1, _dataLayer.GetAllEvents().Count());
            //Inject IDatafiller implementation (Fill from Xml)
            XmlFileFiller xml = new XmlFileFiller();
            _dataLayer = new LibraryRepository();
            _dataLayer.DataFiller = xml;
            _dataLayer.FillData();
            Assert.AreEqual(2, _dataLayer.GetAllAuthors().Count());
            Assert.AreEqual(1, _dataLayer.GetAllBooks().Count());
            Assert.AreEqual(1, _dataLayer.GetAllReaders().Count());
            Assert.AreEqual(1, _dataLayer.GetAllEmployees().Count());
            Assert.AreEqual(1, _dataLayer.GetAllCopiesOfBook().Count());
            Assert.AreEqual(1, _dataLayer.GetAllEvents().Count());
            TxtFileFiller txt = new TxtFileFiller();
            _dataLayer = new LibraryRepository();
            _dataLayer.DataFiller = txt;
            _dataLayer.FillData();
            Assert.AreEqual(2, _dataLayer.GetAllAuthors().Count());
            Assert.AreEqual(1, _dataLayer.GetAllBooks().Count());
            Assert.AreEqual(1, _dataLayer.GetAllReaders().Count());
            Assert.AreEqual(1, _dataLayer.GetAllEmployees().Count());
            Assert.AreEqual(1, _dataLayer.GetAllCopiesOfBook().Count());
            Assert.AreEqual(1, _dataLayer.GetAllEvents().Count());
        }
    }
}
