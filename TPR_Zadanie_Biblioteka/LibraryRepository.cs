using System;
using System.Collections.Generic;
using DL.Interfaces;
using DL.DataObjects;
using DL.DataObjects.EventsObjects;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;
using System.Linq;
using System.Net.Sockets;
using DL.Collections;

namespace DL
{
    // LibraryRepository contains all collections of data objects. 
    public class LibraryRepository : IDataLayerAPI
    {
        private LibraryContext _libraryContext;
        private IDataFiller _dataFiller = null;

        public IDataFiller DataFiller { get => _dataFiller; set => _dataFiller = value; }

        public LibraryRepository()
        {
            _libraryContext = new LibraryContext();
        }

        public void FillData(String path)
        {
            if (_dataFiller != null)
            {
                _dataFiller.Fill(_libraryContext, path);
            }
        }

        //Author CRUD
        public void AddAuthor(Author author)
        {
            if (_libraryContext.Authors.Find(a => a.Id.Equals(author.Id)) != null)
            {
                throw new Exception("Author with this ID already exists");
            }
            _libraryContext.Authors.Add(author);
        }

        public Author GetAuthor(Guid id)
        {
            return _libraryContext.Authors.Find(a => a.Id.Equals(id));
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _libraryContext.Authors;
        }

        public void UpdateAuthor(Guid id, Author author)
        {
            Author updatingAuthor = _libraryContext.Authors.Find(a => a.Id.Equals(id));
            if (updatingAuthor == null)
            {
                throw new Exception("Author with this ID doesn't exist");
            }
            _libraryContext.Authors[_libraryContext.Authors.IndexOf(updatingAuthor)] = author;
        }

        public void DeleteAuthor(Author author)
        {
            _libraryContext.Authors.Remove(author);
        }

        //Book CRUD
        public void AddBook(Book book, int position)
        {
            if (_libraryContext.Books.ContainsKey(position))
            {
                throw new Exception("Book on this position already exists!");
            }
            _libraryContext.Books.Add(position, book);
        }

        public Book GetBook(int position)
        {
            return _libraryContext.Books[position];
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _libraryContext.Books.Values;
        }

        public void UpdateBook(int position, Book book)
        {
            if (!_libraryContext.Books.ContainsKey(position))
            {
                throw new Exception("Book with this ID doesn't exist");
            }
            _libraryContext.Books[position] = book;
        }

        public void DeleteBook(Book book)
        {
            KeyValuePair<int, Book> deletingBook = _libraryContext.Books.First(kvp => kvp.Value.Equals(book));
            _libraryContext.Books.Remove(deletingBook.Key);
        }

        //Reader CRUD
        public void AddReader(Reader reader)
        {
            if (_libraryContext.Readers.Find(r => r.Id.Equals(reader.Id)) != null)
            {
                throw new Exception("Reader with this ID already exists!");
            }
            if (reader.BirthDate.CompareTo(DateTime.Now) >= 0)
            {
                throw new Exception("Unborn Reader! (birth day is future date)");
            }
            if (reader.DateOfRegistration.CompareTo(DateTime.Now) > 0)
            {
                throw new Exception("Date of registration is future date!");
            }

            _libraryContext.Readers.Add(reader);
        }

        public Reader GetReader(Guid id)
        {
            return _libraryContext.Readers.Find(r => r.Id.Equals(id));
        }

        public IEnumerable<Reader> GetAllReaders()
        {
            return _libraryContext.Readers;
        }

        public void UpdateReader(Guid id, Reader reader)
        {
            Reader updatingReader = _libraryContext.Readers.Find(r => r.Equals(id));
            if (updatingReader == null)
            {
                throw new Exception("Employee with this ID doesn't exist");
            }
            _libraryContext.Readers[_libraryContext.Readers.IndexOf(updatingReader)] = reader;
        }

        public void DeleteReader(Reader reader)
        {
            _libraryContext.Readers.Remove(reader);
        }

        //CopyOfBook CRUD
        public void AddCopyOfBook(CopyOfBook copyOfBook)
        {
            if (_libraryContext.CopiesOfBooks.Find(c => c.Id.Equals(copyOfBook.Id)) != null)
            {
                throw new Exception("Copy of book with this ID already exists!");
            }
            if (copyOfBook.PurchaseDate.CompareTo(DateTime.Now) > 0)
            {
                throw new Exception("Invalid purchase date! (date from future)");
            }
            if (copyOfBook.PricePerDay < 0)
            {
                throw new Exception("Price cannot be negative!");
            }
            _libraryContext.CopiesOfBooks.Add(copyOfBook);
        }

        public CopyOfBook GetCopyOfBook(Guid id)
        {
            return _libraryContext.CopiesOfBooks.Find(c => c.Id.Equals(id));
        }

        public IEnumerable<CopyOfBook> GetAllCopiesOfBook()
        {
            return _libraryContext.CopiesOfBooks;
        }
        
        public void UpdateCopyOfBook(Guid id, CopyOfBook copyOfBook)
        {
            CopyOfBook updatingCopyOfBook = _libraryContext.CopiesOfBooks.Find(c => c.Id.Equals(id));
            if (updatingCopyOfBook == null)
            {
                throw new Exception("Copy of book with this ID doesn't exist");
            }
            _libraryContext.CopiesOfBooks[_libraryContext.CopiesOfBooks.IndexOf(updatingCopyOfBook)] = copyOfBook;
        }

        public void DeleteCopyOfBook(CopyOfBook copyOfBook)
        {
            _libraryContext.CopiesOfBooks.Remove(copyOfBook);
        }

        //Employee CRUD
        public void AddEmployee(Employee employee)
        {
            if (_libraryContext.Employees.Find(e => e.Id.Equals(employee.Id)) != null)
            {
                throw new Exception("Employee with this ID already exists!");
            }
            if (employee.BirthDate.CompareTo(DateTime.Now) >= 0)
            {
                throw new Exception("Unborn employee! (birth day is future date)");
            }
            if (employee.DateOfEmployment.CompareTo(DateTime.Now) > 0)
            {
                throw new Exception("Invalid Date of employment! (future date)");
            }
            _libraryContext.Employees.Add(employee);
        }

        public Employee GetEmployee(Guid id)
        {
            return _libraryContext.Employees.Find(e => e.Id.Equals(id));
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _libraryContext.Employees;
        }

        public void UpdateEmployee(Guid id, Employee employee)
        {
            Employee updatingEmployee = _libraryContext.Employees.Find(e => e.Id.Equals(id));
            if (updatingEmployee == null)
            {
                throw new Exception("Employee with this ID doesn't exist");
            }
            _libraryContext.Employees[_libraryContext.Employees.IndexOf(updatingEmployee)] = employee;
        }

        public void DeleteEmployee(Employee employee)
        {
            _libraryContext.Employees.Remove(employee);
        }

        //Rent CRUD
        public void AddRent(Rent rent)
        {
            if (_libraryContext.Rents.Contains(rent))
            {
                throw new Exception("Rent with this ID already exists!");
            }
            if (rent.Date.CompareTo(DateTime.Now) > 0)
            {
                throw new Exception("Invalid date of rental! (future date)");
            }
            _libraryContext.Rents.Add(rent);
        }

        public Rent GetRent(Guid id)
        {
            return (Rent)_libraryContext.Rents.Where(r => r.Id.Equals(id));
        }

        public IEnumerable<Rent> GetAllRents()
        {
            return _libraryContext.Rents;
        }

        public void UpdateRents(Guid id, Rent rent)
        {
            Rent updatingRent = _libraryContext.Rents.Single(r => r.Id.Equals(id));
            if (updatingRent == null)
            {
                throw new Exception("Employee with this ID doesn't exist");
            }
            _libraryContext.Rents[_libraryContext.Rents.IndexOf(updatingRent)] = rent;
        }

        public void DeleteRent(Rent rent)
        {
            _libraryContext.Rents.Remove(rent);
        }
    }
}
