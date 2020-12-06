using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using DL;
using DL.DataObjects;
using DL.DataObjects.EventsObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPR_Task2.Serialization;

namespace Tests.JsonSerializationTests
{
    [TestClass]
    public class ObjectsWithComplexMembers
    {
        private Author _author1, _author2, _author3;
        private Book _book1, _book2, _book3, _book4, _book5;
        private CopyOfBook _book1copy1, _book1copy2, _book2copy1, _book2copy2, _book3copy1, _book3copy2,
            _book4copy1, _book4copy2, _book5copy1, _book5copy2;
        private Reader _reader1, _reader2;
        private Employee _employee1;
        private List<CopyOfBook> _booksForRent1, _booksForRent2, _booksForRent3;
        private Rent _rent1, _rent2, _rent3;
        private Return _return1, _return2, _return3;
        private Payment _payment1, _payment2, _payment3;
        private List<Author> _authors;
        private Dictionary<int, Book> _books;
        private List<CopyOfBook> _copiesOfBooks;
        private List<Employee> _employees;
        private List<Reader> _readers;
        private ObservableCollection<Event> _events;
        private LibraryContext _context;
        

        public void Init()
        {
            _author1 = new Author(Guid.NewGuid(), "Adam", "Malysz");
            _author2 = new Author(Guid.NewGuid(), "Robert", "Maklowicz");
            _author3 = new Author(Guid.NewGuid(), "Edyta", "Gorniak");
            _book1 = new Book("DSJ 2.1 Jurii Kosela Tutorial", _author1, "How to beat super bots", 
                Book.BookType.Detective);
            _book2 = new Book("Goplana the best chocolate", _author1, "Explanation why i love Goplana",
                Book.BookType.Biographie);
            _book3 = new Book("How to cook water", _author2,
                "Best chef in the word shows how to properly cook water", Book.BookType.Romance);
            _book4 = new Book("abc", _author2, "abc", Book.BookType.Novel);
            _book5 = new Book("next time I'll beat you", _author3, "le",
                Book.BookType.Romance);
            _book1copy1 = new CopyOfBook(Guid.NewGuid(), _book1, DateTime.Now, 100);
            _book1copy2 = new CopyOfBook(Guid.NewGuid(), _book1, DateTime.Now, 100);
            _book2copy1 = new CopyOfBook(Guid.NewGuid(), _book2, DateTime.Now, 50);
            _book2copy2 = new CopyOfBook(Guid.NewGuid(), _book2, DateTime.Now, 50);
            _book3copy1 = new CopyOfBook(Guid.NewGuid(), _book3, DateTime.Now, 200);
            _book3copy2 = new CopyOfBook(Guid.NewGuid(), _book3, DateTime.Now, 200);
            _book4copy1 = new CopyOfBook(Guid.NewGuid(), _book4, DateTime.Now, 30);
            _book4copy2 = new CopyOfBook(Guid.NewGuid(), _book4, DateTime.Now, 30);
            _book5copy1 = new CopyOfBook(Guid.NewGuid(), _book5, DateTime.Now, 10);
            _book5copy2 = new CopyOfBook(Guid.NewGuid(), _book5, DateTime.Now, 10);
            _reader1 = new Reader(Guid.NewGuid(), "Agnieszka", "Kolano", 
                new DateTime(1970, 3, 1), "111111111", 
                "agnieszka@p.lodz.pl", Person.Gender.Female, DateTime.Now);
            _reader2 = new Reader(Guid.NewGuid(), "Adam", "Nowak",
                new DateTime(1998, 05, 23),
                "111222333", "adam.nowak@gmail.com", Person.Gender.Female,
                new DateTime(2019, 9, 11));
            _employee1 = new Employee(Guid.NewGuid(), "Katarzyna", "Kowalska",
                new DateTime(1967, 03, 13),
                "123456789", "kaska123@outlook.com", Person.Gender.Female,
                new DateTime(2019, 9, 11));
            _booksForRent1 = new List<CopyOfBook>() { _book1copy1, _book2copy1, _book3copy1 };
            _booksForRent2 = new List<CopyOfBook>() { _book4copy1, _book5copy1, _book1copy2, _book2copy2};
            _booksForRent3 = new List<CopyOfBook>() { _book3copy2, _book4copy2, _book5copy2 };
            _rent1 = new Rent(Guid.NewGuid(), _reader1, _employee1, _booksForRent1, DateTime.Now);
            _rent2 = new Rent(Guid.NewGuid(), _reader2, _employee1, _booksForRent2, DateTime.Now);
            _rent3 = new Rent(Guid.NewGuid(), _reader1, _employee1, _booksForRent3, DateTime.Now);
            _return1 = new Return(Guid.NewGuid(), DateTime.Now, _booksForRent1, _rent1);
            _return2 = new Return(Guid.NewGuid(), DateTime.Now, _booksForRent2, _rent2);
            _return3 = new Return(Guid.NewGuid(), DateTime.Now, _booksForRent3, _rent1);
            _payment1 = new Payment(Guid.NewGuid(), DateTime.Now, _reader1, 100);
            _payment2 = new Payment(Guid.NewGuid(), DateTime.Now, _reader2, 200);
            _payment3 = new Payment(Guid.NewGuid(), DateTime.Now, _reader1, 300);
            _authors = new List<Author>() { _author1, _author2, _author3 };
            _books = new Dictionary<int, Book>() { {1, _book1}, {2, _book2}, {3, _book3}, {4, _book4},
                {5, _book5} };
            _copiesOfBooks = new List<CopyOfBook>()  { _book1copy1, _book1copy2, _book2copy1, _book2copy2,
                _book3copy1, _book3copy2, _book4copy1, _book4copy2, _book5copy1, _book5copy2 };
            _employees = new List<Employee>() { _employee1 };
            _readers = new List<Reader>() { _reader1, _reader2 };
            _events = new ObservableCollection<Event>() { _rent1, _rent2, _rent3, _return1,
                _return2, _return3, _payment1, _payment2, _payment3 };
            _context = new LibraryContext();
        }

        [TestMethod]
        public void BookSerialization()
        {
            Init();
            
            // Serialize Book
            JsonFormatter<Book> jsonFormatter = new JsonFormatter<Book>();
            using(Stream stream = File.Open("serializeBook.json", FileMode.Create, FileAccess.ReadWrite))
                jsonFormatter.Serialize(stream, _book1);
            
            //Deserialize Book
            Book book1Copy;
            using(Stream stream = File.Open("serializeBook.json", FileMode.Open, FileAccess.Read))
                book1Copy = jsonFormatter.Deserialize(stream);
            
            // Check
            Assert.AreEqual(_book1.Name, book1Copy.Name);
            Assert.AreEqual(_book1.Description, book1Copy.Description);
            Assert.AreEqual(_book1.Author.Name, book1Copy.Author.Name);
            Assert.AreEqual(_book1.Author.Surname, book1Copy.Author.Surname);
        }

        [TestMethod]
        public void CopyOfBookSerialization()
        {
            Init();
            
            // Serialize CopyOfBook
            JsonFormatter<CopyOfBook> jsonFormatter = new JsonFormatter<CopyOfBook>();
            using (Stream stream = File.Open("serializedCopyOfBook.json", FileMode.Create, FileAccess.ReadWrite))
            {
                jsonFormatter.Serialize(stream, _book1copy1);
            }

            // Deserialize CopyOfBook
            CopyOfBook copyOfBookCopy;
            using (Stream stream = File.Open("serializedCopyOfBook.json", FileMode.Open, FileAccess.Read))
            {
                copyOfBookCopy = jsonFormatter.Deserialize(stream);
            }
            
            // Check
            Assert.AreEqual(_book1copy1.Id, copyOfBookCopy.Id);
            Assert.AreEqual(_book1copy1.PurchaseDate, copyOfBookCopy.PurchaseDate);
            Assert.AreEqual(_book1copy1.PricePerDay, copyOfBookCopy.PricePerDay);
            Assert.AreEqual(_book1copy1.Book.Name, copyOfBookCopy.Book.Name);
            Assert.AreEqual(_book1copy1.Book.Description, copyOfBookCopy.Book.Description);
        }

        [TestMethod]
        public void RentSerialization()
        {
            Init();
           
            // Serialize Rent
            JsonFormatter<Rent> jsonFormatter = new JsonFormatter<Rent>();
            using(Stream stream = File.Open("serializeRent.json", FileMode.Create, FileAccess.ReadWrite))
                jsonFormatter.Serialize(stream, _rent1);
           
            // Deserialize Rent
            Rent rent1Copy;
            using(Stream stream = File.Open("serializeRent.json", FileMode.Open, FileAccess.Read))
                rent1Copy = jsonFormatter.Deserialize(stream);
            
            // Check
            Assert.AreEqual(rent1Copy.Book.ElementAt(0), _rent1.Book.ElementAt(0));
            Assert.AreEqual(rent1Copy.Book.ElementAt(1), _rent1.Book.ElementAt(1));
            Assert.AreEqual(_rent1.Employee, rent1Copy.Employee);
            Assert.AreEqual(_rent1.Reader, rent1Copy.Reader);
            Assert.AreEqual(_rent1.DateOfReturn, rent1Copy.DateOfReturn);
            Assert.AreEqual(_rent1.Date, rent1Copy.Date);
            Assert.AreEqual(_rent1.Id, rent1Copy.Id);
        }

        [TestMethod]
        public void ReturnSerialization()
        {
            Init();
            
            // Serialize Return
            JsonFormatter<Return> jsonFormatter = new JsonFormatter<Return>();
            using (Stream stream = File.Open("serializedReturn.json", FileMode.Create, FileAccess.ReadWrite))
            {
                jsonFormatter.Serialize(stream, _return1);
            }

            // Deserialize Return
            Return returnCopy;
            using (Stream stream = File.Open("serializedReturn.json", FileMode.Open, FileAccess.Read))
            {
                returnCopy = jsonFormatter.Deserialize(stream);
            }
            
            // Check
            Assert.AreEqual(_return1.Id, returnCopy.Id);
            Assert.AreEqual(_return1.Date, returnCopy.Date);
            for(int i = 0; i < _return1.Books.Count; i++)
            {
                Assert.AreEqual(_return1.Books.ElementAt(i), returnCopy.Books.ElementAt(i));
            }
            Assert.AreEqual(_return1.Rent.Id, returnCopy.Rent.Id);
            Assert.AreEqual(_return1.Rent.Employee, returnCopy.Rent.Employee);
        }

        [TestMethod]
        public void PaymentSerialization()
        {
            Init();
            
            // Serialize Payment
            JsonFormatter<Payment> jsonFormatter = new JsonFormatter<Payment>();
            using (Stream stream = File.Open("serializedPayment.json", FileMode.Create, FileAccess.ReadWrite))
            {
                jsonFormatter.Serialize(stream, _payment1);
            }

            // Deserialize Payment
            Payment paymentCopy;
            using (Stream stream = File.Open("serializedPayment.json", FileMode.Open, FileAccess.Read))
            {
                paymentCopy = jsonFormatter.Deserialize(stream);
            }
            
            // Check
            Assert.AreEqual(_payment1.Id, paymentCopy.Id);
            Assert.AreEqual(_payment1.Date, paymentCopy.Date);
            Assert.AreEqual(_payment1.Cash, paymentCopy.Cash);
            Assert.AreEqual(_payment1.Reader.Id, paymentCopy.Reader.Id);
            Assert.AreEqual(_payment1.Reader.DateOfRegistration, paymentCopy.Reader.DateOfRegistration);
        }
        
        [TestMethod]
        public void LibraryContextSerialization()
        {
            Init();
            
            _context.Authors.AddRange(_authors);
            foreach (KeyValuePair<int, Book> book in _books)
            {
                _context.Books.Add(book.Key, book.Value);
            }
            _context.CopiesOfBooks.AddRange(_copiesOfBooks);
            _context.Employees.AddRange(_employees);
            _context.Readers.AddRange(_readers);
            foreach (Event _event in _events)
            {
                _context.Events.Add(_event);
            }
            
            JsonFormatter<LibraryContext> jsonFormatter = new JsonFormatter<LibraryContext>();
            using (Stream stream = File.Open("serializedContext.json", FileMode.Create, FileAccess.ReadWrite))
            {
                jsonFormatter.Serialize(stream, _context);
            }

            LibraryContext contextCopy;
            using (Stream stream = File.Open("serializedContext.json", FileMode.Open, FileAccess.ReadWrite))
            {
                contextCopy = jsonFormatter.Deserialize(stream);
            }
            
            Assert.AreSame(contextCopy.Books.ElementAt(0).Value.Author, 
                contextCopy.Books.ElementAt(1).Value.Author);
            Assert.AreSame(contextCopy.Books.ElementAt(2).Value.Author, 
                contextCopy.Books.ElementAt(3).Value.Author);
            Assert.AreSame(contextCopy.CopiesOfBooks.ElementAt(0).Book, 
                contextCopy.CopiesOfBooks.ElementAt(1).Book);
            Assert.AreSame(contextCopy.CopiesOfBooks.ElementAt(2).Book, 
                contextCopy.CopiesOfBooks.ElementAt(3).Book);
            Assert.AreSame(contextCopy.CopiesOfBooks.ElementAt(4).Book, 
                contextCopy.CopiesOfBooks.ElementAt(5).Book);
            Assert.AreSame(contextCopy.CopiesOfBooks.ElementAt(6).Book, 
                contextCopy.CopiesOfBooks.ElementAt(7).Book);
            Assert.AreSame(contextCopy.CopiesOfBooks.ElementAt(8).Book, 
                contextCopy.CopiesOfBooks.ElementAt(9).Book);
            Assert.AreSame(((Rent)contextCopy.Events.ElementAt(0)).Reader, 
                ((Rent)contextCopy.Events.ElementAt(2)).Reader);
            Assert.AreSame(((Rent)contextCopy.Events.ElementAt(0)).Employee, 
                ((Rent)contextCopy.Events.ElementAt(2)).Employee);
            Assert.AreSame(((Return)contextCopy.Events.ElementAt(3)).Rent,
                ((Return)contextCopy.Events.ElementAt(5)).Rent);
            Assert.AreSame(((Payment)contextCopy.Events.ElementAt(6)).Reader,
                ((Payment)contextCopy.Events.ElementAt(8)).Reader);
        }
    }
}