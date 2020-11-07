using System;
using System.Collections.Generic;
using System.Linq;
using DL;
using DL.DataObjects;
using DL.Interfaces;
using DL.DataObjects.EventsObjects;
using System.ComponentModel;

namespace BLL
{
    public class DataService
    {
        private IDataLayerAPI _dataLayer;

        public DataService(IDataLayerAPI dataLayer)
        {
            this._dataLayer = dataLayer;
        }

        // Authors methods

        public void AddAuthor(string name, string surname)
        {
            _dataLayer.AddAuthor(new Author(Guid.NewGuid(), name, surname));
        }

        public void DeleteAuthor(Author author)
        {
            if (_dataLayer.GetAllBooks().First(b => b.Author.Equals(author)) != null)
                throw new ArgumentException("At least one book has reference to author!");
            _dataLayer.DeleteAuthor(author);
        }

        public Author GetAuthor(Author author)
        {
            return _dataLayer.GetAuthor(author.Id);
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _dataLayer.GetAllAuthors();
        }

        // Books methods

        public void AddBook(string name, Author author, string description, Book.BookType bookType)
        {
            if (_dataLayer.GetAuthor(author.Id) == null)
                throw new ArgumentException("Author doesn't exists in repository!");
            _dataLayer.AddBook(new Book(name, author, description, bookType));
        }

        public void DeleteBook(Book book)
        {
            if (_dataLayer.GetAllCopiesOfBook().First(b => b.Book.Equals(book)) != null)
                throw new ArgumentException("At least one copy of book has reference to book!");
            _dataLayer.DeleteBook(book);
        }

        public Book GetBook(Book book)
        {
            return _dataLayer.GetBook(_dataLayer.GetBookPosition(book));
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _dataLayer.GetAllBooks();
        }

        // Copies of Books methods

        public void AddCopyOfBook(Book book, DateTime purchaseDate, double pricePerDay)
        {
            if (GetBook(book) == null)
                throw new ArgumentException("This book doesn't exists in repository!");
            _dataLayer.AddCopyOfBook(new CopyOfBook(Guid.NewGuid(), book, purchaseDate, pricePerDay));
        } 

        public void DeleteCopyOfBook(CopyOfBook copyOfBook)
        {
            if (IsCopyOfBookRented(copyOfBook))
            {
                throw new ArgumentException("This copy of book is already rented!");
            }
            _dataLayer.DeleteCopyOfBook(copyOfBook);
        }

        public bool IsCopyOfBookRented(CopyOfBook copyOfBook)
        {
            IEnumerable<CopyOfBook> rentedBooks = GetRentedCopiesOfBooks();
            return rentedBooks.Contains(copyOfBook);
        } 
    
        public CopyOfBook GetCopyOfBook(CopyOfBook copyOfBook)
        {
            return _dataLayer.GetCopyOfBook(copyOfBook.Id);
        }

        public IEnumerable<CopyOfBook> GetAllCopiesOfBook()
        {
            return _dataLayer.GetAllCopiesOfBook();
        }

        // Employees methods

        public void AddEmployee(string name, string surname, DateTime birthDate,
            string phoneNumber, string email, Employee.Gender gender, DateTime dateOfEmployment)
        {
            if ((_dataLayer.GetAllEmployees().First(e => e.PhoneNumber.Equals(phoneNumber)) == null) && (_dataLayer.GetAllEmployees().First(e => e.Email.Equals(email)) == null))
                _dataLayer.AddEmployee(new Employee(Guid.NewGuid(), name, surname, birthDate, phoneNumber, email, gender, dateOfEmployment));
            else
                throw new ArgumentException("Person with the same email or phone number exists!");
        }

        public void DeleteEmployee(Employee employee)
        {
            _dataLayer.DeleteEmployee(employee);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _dataLayer.GetAllEmployees();
        }

        public void UpdateEmployee(Guid id, Employee employee)
        {
            _dataLayer.UpdateEmployee(id, employee);
        }

        // Rents methods

        public void AddRent(Reader reader, Employee employee, List<CopyOfBook> books)
        {
            if (!_dataLayer.GetAllReaders().Contains(reader))
            {
                throw new ArgumentException("Reader doesn't exist in repository!");
            }
            if (!_dataLayer.GetAllEmployees().Contains(employee))
            {
                throw new ArgumentException("Employee doesn't exist in repository!");
            }
            foreach(CopyOfBook book in books)
            {
                if (!_dataLayer.GetAllCopiesOfBook().Contains(book))
                {
                    throw new ArgumentException("At least one of book doesn't exists in repository!");
                }
                if (IsCopyOfBookRented(book))
                {
                    throw new ArgumentException("This copy of book is already rented!");
                }
            }
            IEnumerable<Rent> rents = GetAllCurrentRents();
            if(rents.First(rent => rent.Reader.Equals(reader)) != null)
            {
                throw new ArgumentException("This reader already has rent");
            }
            _dataLayer.AddEvent(new Rent(Guid.NewGuid(), reader, employee, books, DateTime.Now));
        }

        public IEnumerable<Rent> GetAllRents() 
        {
            List<Rent> rents = new List<Rent>();
            foreach(Event _event in _dataLayer.GetAllEvents())
            {
                if (typeof(Rent).Equals(_event.GetType()))
                {
                    rents.Add((Rent)_event);
                }
            }
            return rents;
        }

        public IEnumerable<Rent> GetAllCurrentRents()
        {
            List<Rent> rents = GetAllRents().ToList();
            foreach(Rent rent in GetAllRents())
            {
                if (rent.DateOfReturn != null)
                {
                    rents.Remove(rent);
                }
            }
            return rents;
        }

        // Returns methods

        public void AddReturn(Rent rent, List<CopyOfBook> books)
        {
            if (!GetAllRents().Contains(rent))
            {
                throw new ArgumentException("Rent didn't happen!");
            }
            if (!GetAllCurrentRents().Contains(rent))
            {
                throw new ArgumentException("Rent is closed!");
            }
            foreach (CopyOfBook book in books)
            {
                if (!GetRentedCopiesOfBooks(rent).Contains(book))
                {
                    throw new ArgumentException("At least one of returned copies doesn't contain in rent or has been already returned!");
                }
            }
            _dataLayer.AddEvent(new Return(Guid.NewGuid(), DateTime.Now, books, rent));
            if(GetRentedCopiesOfBooks(rent).Count() == 0)
            {
                rent.DateOfReturn = DateTime.Now;
            }
        }

        public IEnumerable<Return> GetAllReturns()
        {
            List<Return> returns = new List<Return>();
            foreach(Event _event in _dataLayer.GetAllEvents())
            {
                if (typeof(Return).Equals(_event.GetType()))
                {
                    returns.Add((Return)_event);
                }
            }
            return returns;
        }

        public IEnumerable<Return> GetAllReturnsByRent(Rent rent)
        {
            List<Return> returns = new List<Return>();
            foreach (Return _return in GetAllReturns())
            {
                if (_return.Rent.Equals(rent))
                {
                    returns.Add(_return);
                }
            }
            return returns;
        }

        public IEnumerable<CopyOfBook> GetRentedCopiesOfBooks()
        {
            List<CopyOfBook> rentedCopiesOfBooks = new List<CopyOfBook>();
            foreach(Rent rent in GetAllRents())
            {
                if(rent.DateOfReturn == null)
                {
                    rentedCopiesOfBooks = MergeCollections<CopyOfBook>(rentedCopiesOfBooks, GetRentedCopiesOfBooks(rent));
                }
                
            }
            return rentedCopiesOfBooks;
        }

        public IEnumerable<CopyOfBook> GetRentedCopiesOfBooks(Rent rent)
        {
            List<CopyOfBook> rentedCopiesOfBooks = new List<CopyOfBook>(rent.Book.Keys);
            IEnumerable<Return> returns = GetAllReturnsByRent(rent);
            foreach(Return _return in returns)
            {
                foreach(CopyOfBook copy in _return.Books)
                {
                    rentedCopiesOfBooks.Remove(copy);
                }
            }
            return rentedCopiesOfBooks;
        }

        private List<T> MergeCollections<T>(List<T> enum1, IEnumerable<T> enum2)
        {
            foreach(T item in enum2)
            {
                enum1.Add(item);
            }
            return enum1;
        }
    }
}
