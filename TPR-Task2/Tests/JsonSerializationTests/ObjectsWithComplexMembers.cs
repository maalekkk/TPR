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
        
        [TestMethod]
        public void LibraryContextSerialization()
        {
            Author author1 = new Author(Guid.NewGuid(), "Adam", "Malysz");
            Author author2 = new Author(Guid.NewGuid(), "Robert", "Maklowicz");
            Author author3 = new Author(Guid.NewGuid(), "Edyta", "Gorniak");
            Book book1 = new Book("DSJ 2.1 Jurii Kosela Tutorial", author1, "How to beat super bots", 
                Book.BookType.Detective);
            Book book2 = new Book("Goplana the best chocolate", author1, "Explanation why i love Goplana",
                Book.BookType.Biographie);
            Book book3 = new Book("How to cook water", author2,
                "Best chef in the word shows how to properly cook water", Book.BookType.Romance);
            Book book4 = new Book("EEEEEEEEEEEE", author2, "EEEEEEE PAPRYKA EEEEEEEE", Book.BookType.Novel);
            Book book5 = new Book("KOKOKOKO EUROSPOKO, next time I'll beat you", author3, "leelelelel",
                Book.BookType.Romance);
            CopyOfBook book1copy1 = new CopyOfBook(Guid.NewGuid(), book1, DateTime.Now, 100);
            CopyOfBook book1copy2 = new CopyOfBook(Guid.NewGuid(), book1, DateTime.Now, 100);
            CopyOfBook book2copy1 = new CopyOfBook(Guid.NewGuid(), book2, DateTime.Now, 50);
            CopyOfBook book2copy2 = new CopyOfBook(Guid.NewGuid(), book2, DateTime.Now, 50);
            CopyOfBook book3copy1 = new CopyOfBook(Guid.NewGuid(), book3, DateTime.Now, 200);
            CopyOfBook book3copy2 = new CopyOfBook(Guid.NewGuid(), book3, DateTime.Now, 200);
            CopyOfBook book4copy1 = new CopyOfBook(Guid.NewGuid(), book4, DateTime.Now, 30);
            CopyOfBook book4copy2 = new CopyOfBook(Guid.NewGuid(), book4, DateTime.Now, 30);
            CopyOfBook book5copy1 = new CopyOfBook(Guid.NewGuid(), book5, DateTime.Now, 10);
            CopyOfBook book5copy2 = new CopyOfBook(Guid.NewGuid(), book5, DateTime.Now, 10);
            Reader reader1 = new Reader(Guid.NewGuid(), "Agnieszka", "Kleczkowska", 
                new DateTime(1970, 3, 1), "111111111", 
                "agnieszka.kleczkowska@p.lodz.pl", Person.Gender.Female, DateTime.Now);
            Reader reader2 = new Reader(Guid.NewGuid(), "Adam", "Nowak", new DateTime(1998, 05, 23),
                "111222333", "adam.nowak@gmail.com", Person.Gender.Male, new DateTime(2019, 9, 11));
            Employee employee1 = new Employee(Guid.NewGuid(), "Katarzyna", "Kowalska", new DateTime(1967, 03, 13),
                "123456789", "kaska123@outlook.com", Person.Gender.Female, new DateTime(2019, 9, 11));
            List<CopyOfBook> booksForRent1 = new List<CopyOfBook>() { book1copy1, book2copy1, book3copy1 };
            List<CopyOfBook> booksForRent2 = new List<CopyOfBook>() { book4copy1, book5copy1, book1copy2, book2copy2};
            List<CopyOfBook> booksForRent3 = new List<CopyOfBook>() { book3copy2, book4copy2, book5copy2 };
            Rent rent1 = new Rent(Guid.NewGuid(), reader1, employee1, booksForRent1, DateTime.Now);
            Rent rent2 = new Rent(Guid.NewGuid(), reader2, employee1, booksForRent2, DateTime.Now);
            Rent rent3 = new Rent(Guid.NewGuid(), reader1, employee1, booksForRent3, DateTime.Now);
            Return return1 = new Return(Guid.NewGuid(), DateTime.Now, booksForRent1, rent1);
            Return return2 = new Return(Guid.NewGuid(), DateTime.Now, booksForRent2, rent2);
            Return return3 = new Return(Guid.NewGuid(), DateTime.Now, booksForRent3, rent1);
            Payment payment1 = new Payment(Guid.NewGuid(), DateTime.Now, reader1, 100);
            Payment payment2 = new Payment(Guid.NewGuid(), DateTime.Now, reader2, 200);
            Payment payment3 = new Payment(Guid.NewGuid(), DateTime.Now, reader1, 300);
            List<Author> authors = new List<Author>() { author1, author2, author3 };
            Dictionary<int, Book> books = new Dictionary<int, Book>() { {1, book1}, {2, book2}, {3, book3}, {4, book4},
                {5, book5} };
            List<CopyOfBook> copiesOfBooks = new List<CopyOfBook>()  { book1copy1, book1copy2, book2copy1, book2copy2,
                book3copy1, book3copy2, book4copy1, book4copy2, book5copy1, book5copy2 };
            List<Employee> employees = new List<Employee>() { employee1 };
            List<Reader> readers = new List<Reader>() { reader1, reader2 };
            ObservableCollection<Event> events = new ObservableCollection<Event>() { rent1, rent2, rent3, return1,
                return2, return3, payment1, payment2, payment3 };
            
            LibraryContext context = new LibraryContext();
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