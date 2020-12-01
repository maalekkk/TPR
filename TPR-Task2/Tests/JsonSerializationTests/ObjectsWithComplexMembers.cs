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
        private Author author1, author2, author3;
        private Book book1, book2, book3, book4, book5;
        private CopyOfBook book1copy1, book1copy2, book2copy1, book2copy2, book3copy1, book3copy2, book4copy1, book4copy2, book5copy1, book5copy2;
        private Reader reader1, reader2;
        private Employee employee1;
        private List<CopyOfBook> booksForRent1, booksForRent2, booksForRent3;
        private Rent rent1, rent2, rent3;
        private Return return1, return2, return3;
        private Payment payment1, payment2, payment3;
        private List<Author> authors;
        private Dictionary<int, Book> books;
        private List<CopyOfBook> copiesOfBooks;
        private List<Employee> employees;
        private List<Reader> readers;
        private ObservableCollection<Event> events;
        private LibraryContext context;
        

        public void Init()
        {
            author1 = new Author(Guid.NewGuid(), "Adam", "Malysz");
            author2 = new Author(Guid.NewGuid(), "Robert", "Maklowicz");
            author3 = new Author(Guid.NewGuid(), "Edyta", "Gorniak");
            book1 = new Book("DSJ 2.1 Jurii Kosela Tutorial", author1, "How to beat super bots", 
                Book.BookType.Detective);
            book2 = new Book("Goplana the best chocolate", author1, "Explanation why i love Goplana",
                Book.BookType.Biographie);
            book3 = new Book("How to cook water", author2,
                "Best chef in the word shows how to properly cook water", Book.BookType.Romance);
            book4 = new Book("abc", author2, "abc", Book.BookType.Novel);
            book5 = new Book("next time I'll beat you", author3, "le",
                Book.BookType.Romance);
            book1copy1 = new CopyOfBook(Guid.NewGuid(), book1, DateTime.Now, 100);
            book1copy2 = new CopyOfBook(Guid.NewGuid(), book1, DateTime.Now, 100);
            book2copy1 = new CopyOfBook(Guid.NewGuid(), book2, DateTime.Now, 50);
            book2copy2 = new CopyOfBook(Guid.NewGuid(), book2, DateTime.Now, 50);
            book3copy1 = new CopyOfBook(Guid.NewGuid(), book3, DateTime.Now, 200);
            book3copy2 = new CopyOfBook(Guid.NewGuid(), book3, DateTime.Now, 200);
            book4copy1 = new CopyOfBook(Guid.NewGuid(), book4, DateTime.Now, 30);
            book4copy2 = new CopyOfBook(Guid.NewGuid(), book4, DateTime.Now, 30);
            book5copy1 = new CopyOfBook(Guid.NewGuid(), book5, DateTime.Now, 10);
            book5copy2 = new CopyOfBook(Guid.NewGuid(), book5, DateTime.Now, 10);
            reader1 = new Reader(Guid.NewGuid(), "Agnieszka", "Kolano", 
                new DateTime(1970, 3, 1), "111111111", 
                "agnieszka@p.lodz.pl", Person.Gender.Female, DateTime.Now);
            reader2 = new Reader(Guid.NewGuid(), "Adam", "Nowak", new DateTime(1998, 05, 23),
                "111222333", "adam.nowak@gmail.com", Person.Gender.Female, new DateTime(2019, 9, 11));
            employee1 = new Employee(Guid.NewGuid(), "Katarzyna", "Kowalska", new DateTime(1967, 03, 13),
                "123456789", "kaska123@outlook.com", Person.Gender.Female, new DateTime(2019, 9, 11));
            booksForRent1 = new List<CopyOfBook>() { book1copy1, book2copy1, book3copy1 };
            booksForRent2 = new List<CopyOfBook>() { book4copy1, book5copy1, book1copy2, book2copy2};
            booksForRent3 = new List<CopyOfBook>() { book3copy2, book4copy2, book5copy2 };
            rent1 = new Rent(Guid.NewGuid(), reader1, employee1, booksForRent1, DateTime.Now);
            rent2 = new Rent(Guid.NewGuid(), reader2, employee1, booksForRent2, DateTime.Now);
            rent3 = new Rent(Guid.NewGuid(), reader1, employee1, booksForRent3, DateTime.Now);
            return1 = new Return(Guid.NewGuid(), DateTime.Now, booksForRent1, rent1);
            return2 = new Return(Guid.NewGuid(), DateTime.Now, booksForRent2, rent2);
            return3 = new Return(Guid.NewGuid(), DateTime.Now, booksForRent3, rent1);
            payment1 = new Payment(Guid.NewGuid(), DateTime.Now, reader1, 100);
            payment2 = new Payment(Guid.NewGuid(), DateTime.Now, reader2, 200);
            payment3 = new Payment(Guid.NewGuid(), DateTime.Now, reader1, 300);
            authors = new List<Author>() { author1, author2, author3 };
            books = new Dictionary<int, Book>() { {1, book1}, {2, book2}, {3, book3}, {4, book4},
                {5, book5} };
            copiesOfBooks = new List<CopyOfBook>()  { book1copy1, book1copy2, book2copy1, book2copy2,
                book3copy1, book3copy2, book4copy1, book4copy2, book5copy1, book5copy2 };
            employees = new List<Employee>() { employee1 };
            readers = new List<Reader>() { reader1, reader2 };
            events = new ObservableCollection<Event>() { rent1, rent2, rent3, return1,
                return2, return3, payment1, payment2, payment3 };
            context = new LibraryContext();
        }

        [TestMethod]
        public void BookSerialization()
        {
            Init();
            
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
            Init();
            
            // Serialize CopyOfBook
            JsonFormatter<CopyOfBook> jsonFormatter = new JsonFormatter<CopyOfBook>();
            using (Stream stream = File.Open("serializedCopyOfBook.json", FileMode.Create, FileAccess.ReadWrite))
            {
                jsonFormatter.Serialize(stream, book1copy1);
            }

            // Deserialize CopyOfBook
            CopyOfBook copyOfBookCopy;
            using (Stream stream = File.Open("serializedCopyOfBook.json", FileMode.Open, FileAccess.Read))
            {
                copyOfBookCopy = jsonFormatter.Deserialize(stream);
            }
            
            // Check
            Assert.AreEqual(book1copy1.Id, copyOfBookCopy.Id);
            Assert.AreEqual(book1copy1.PurchaseDate, copyOfBookCopy.PurchaseDate);
            Assert.AreEqual(book1copy1.PricePerDay, copyOfBookCopy.PricePerDay);
            Assert.AreEqual(book1copy1.Book.Name, copyOfBookCopy.Book.Name);
            Assert.AreEqual(book1copy1.Book.Description, copyOfBookCopy.Book.Description);
        }

        [TestMethod]
        public void RentSerialization()
        {
            Init();
           
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
            Init();
            
            // Serialize Return
            JsonFormatter<Return> jsonFormatter = new JsonFormatter<Return>();
            using (Stream stream = File.Open("serializedReturn.json", FileMode.Create, FileAccess.ReadWrite))
            {
                jsonFormatter.Serialize(stream, return1);
            }

            // Deserialize Return
            Return returnCopy;
            using (Stream stream = File.Open("serializedReturn.json", FileMode.Open, FileAccess.Read))
            {
                returnCopy = jsonFormatter.Deserialize(stream);
            }
            
            // Check
            Assert.AreEqual(return1.Id, returnCopy.Id);
            Assert.AreEqual(return1.Date, returnCopy.Date);
            for(int i = 0; i < return1.Books.Count; i++)
            {
                Assert.AreEqual(return1.Books.ElementAt(i), returnCopy.Books.ElementAt(i));
            }
            Assert.AreEqual(return1.Rent.Id, returnCopy.Rent.Id);
            Assert.AreEqual(return1.Rent.Employee, returnCopy.Rent.Employee);
        }

        [TestMethod]
        public void PaymentSerialization()
        {
            Init();
            
            // Serialize Payment
            JsonFormatter<Payment> jsonFormatter = new JsonFormatter<Payment>();
            using (Stream stream = File.Open("serializedPayment.json", FileMode.Create, FileAccess.ReadWrite))
            {
                jsonFormatter.Serialize(stream, payment1);
            }

            // Deserialize Payment
            Payment paymentCopy;
            using (Stream stream = File.Open("serializedPayment.json", FileMode.Open, FileAccess.Read))
            {
                paymentCopy = jsonFormatter.Deserialize(stream);
            }
            
            // Check
            Assert.AreEqual(payment1.Id, paymentCopy.Id);
            Assert.AreEqual(payment1.Date, paymentCopy.Date);
            Assert.AreEqual(payment1.Cash, paymentCopy.Cash);
            Assert.AreEqual(payment1.Reader.Id, paymentCopy.Reader.Id);
            Assert.AreEqual(payment1.Reader.DateOfRegistration, paymentCopy.Reader.DateOfRegistration);
        }
        
        [TestMethod]
        public void LibraryContextSerialization()
        {
            Init();
            
            context.Authors.AddRange(authors);
            foreach (KeyValuePair<int, Book> book in books)
            {
                context.Books.Add(book.Key, book.Value);
            }
            context.CopiesOfBooks.AddRange(copiesOfBooks);
            context.Employees.AddRange(employees);
            context.Readers.AddRange(readers);
            foreach (Event _event in events)
            {
                context.Events.Add(_event);
            }
            
            JsonFormatter<LibraryContext> jsonFormatter = new JsonFormatter<LibraryContext>();
            using (Stream stream = File.Open("serializedContext.json", FileMode.Create, FileAccess.ReadWrite))
            {
                jsonFormatter.Serialize(stream, context);
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
            Assert.AreSame(((Return)contextCopy.Events.ElementAt(3)).Rent, ((Return)contextCopy.Events.ElementAt(5)).Rent);
            Assert.AreSame(((Payment)contextCopy.Events.ElementAt(6)).Reader, ((Payment)contextCopy.Events.ElementAt(8)).Reader);
        }
    }
}