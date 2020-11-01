using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;

namespace DL
{
    // LibraryRepository contains all collections of data objects. 
    class LibraryRepository
    {
        private LibraryContext _libraryContext;
        private IDataFiller _fillWithConsts = null;

        public IDataFiller FillWithConsts { get => _fillWithConsts; set => _fillWithConsts = value; }

        public LibraryRepository()
        {
            _libraryContext = new LibraryContext();
        }

        public void FillData(String path)
        {
            if (_fillWithConsts != null)
            {
                _fillWithConsts.Fill(_libraryContext, path);
            }
        }

        //Author CRUD
        public void AddAuthor(params Object[] props)
        {
            if(props.Length != 3)
            {
                throw new Exception("Invalid number of properties!");
            }
            Author newAuthor = new Author((string)props[0], (string)props[1], (string)props[2]);
            _libraryContext.Authors.Add(newAuthor);
        }

        public Author GetAuthor(string id)
        {
            return _libraryContext.Authors.Get(id);
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _libraryContext.Authors.GetAll();
        }

        public void UpdateAuthor(string id, int option,params Object[] newValue)
        {
            _libraryContext.Authors.Update(id, option, newValue);
        }

        public void DeleteAuthor(Author author)
        {
            _libraryContext.Authors.Delete(author);
        }

        //Book CRUD
        public void AddBook(params Object[] props)
        {
            if(props.Length != 5)
            {
                throw new Exception("Invalid number of properties!");
            }
            Book newBook = new Book((string)props[0], (string)props[1], (Author)props[2],
                (string)props[3], (Book.BookType)props[4]);
            _libraryContext.BooksCatalog.Add(newBook);
        }

        public Book GetBook(string id)
        {
           return _libraryContext.BooksCatalog.Get(id);
        }

        public IEnumerable<Book> GetAllBooks()
        {
           return _libraryContext.BooksCatalog.GetAll();
        }

        public void UpdateBook(string id, int option,params Object[] newValue)
        {
            _libraryContext.BooksCatalog.Update(id, option, newValue);
        }

        public void DeleteBook(Book book)
        {
            _libraryContext.BooksCatalog.Delete(book);
        }

        //Reader CRUD
        public void AddReader(params Object[] props)
        {
            if(props.Length != 8)
            {
                throw new Exception("Invalid number of properties!");
            }
            Reader newReader = new Reader((string)props[0], (string)props[1], (string)props[2],
                (DateTime)props[3], (string)props[4], (string)props[5], (Person.Gender)props[6],
                (DateTime)props[7]);
            _libraryContext.Readers.Add(newReader);
        }

        public Reader GetReader(string id)
        {
            return _libraryContext.Readers.Get(id);
        }

        public IEnumerable<Reader> GetAllReaders()
        {
            return _libraryContext.Readers.GetAll();
        }

        public void UpdateReader(string id, int option, Object newValue)
        {
            _libraryContext.Readers.Update(id, option, newValue);
        }

        public void DeleteReader(Reader reader)
        {
            _libraryContext.Readers.Delete(reader);
        }

        //CopyOfBook CRUD
        public void AddCopyOfBook(params Object[] props)
        {
            if(props.Length != 4)
            {
                throw new Exception("Invalid number of properties!");
            }
            CopyOfBook newCopyOfBook = new CopyOfBook((string)props[0], (Book)props[1], (DateTime)props[2],
                (double)props[3]);
            _libraryContext.CopiesOfBooks.Add(newCopyOfBook);
        }

        public CopyOfBook GetCopyOfBook(string id)
        {
            return _libraryContext.CopiesOfBooks.Get(id);
        }

        public IEnumerable<CopyOfBook> GetAllCopiesOfBook()
        {
            return _libraryContext.CopiesOfBooks.GetAll();
        }
        
        public void UpdateCopyOfBook(string id, int option, Object newValue)
        {
            _libraryContext.CopiesOfBooks.Update(id, option, newValue);
        }

        public void DeleteCopyOfBook(CopyOfBook copyOfBook)
        {
            _libraryContext.CopiesOfBooks.Delete(copyOfBook);
        }

        //Employee CRUD
        public void AddEmployee(params Object[] props)
        {
            if(props.Length != 8)
            {
                throw new Exception("Invalid number of parameters!");
            }
            Employee newEmployee = new Employee((string)props[0], (string)props[1], (string)props[2],
                (DateTime)props[3], (string)props[4], (string)props[5], (Person.Gender)props[6],
                (DateTime)props[7]);
            _libraryContext.Staff.Add(newEmployee);

        }

        public Employee GetEmployee(string id)
        {
            return _libraryContext.Staff.Get(id);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _libraryContext.Staff.GetAll();
        }

        public void UpdateEmployee(string id, int option, Object newValue)
        {
            _libraryContext.Staff.Update(id, option, newValue);
        }

        public void DeleteEmployee(Employee employee)
        {
            _libraryContext.Staff.Delete(employee);
        }

        //Rent CRUD
        public void AddRent(params Object[] props)
        {
            if(props.Length != 7 && props.Length != 6)
            {
                throw new Exception("Invalid number of parameters!");
            }
            Rent newRent = new Rent((string)props[0], (Reader)props[1], (Employee)props[2], 
                (List<CopyOfBook>)props[3], (DateTime)props[4], (double)props[5]);
            if(props.Length == 7)
            {
                newRent.DateOfReturn = (DateTime)props[6];
            }
            _libraryContext.RentsList.Add(newRent);
        }

        public Rent GetRent(string id)
        {
            return _libraryContext.RentsList.Get(id);
        }

        public IEnumerable<Rent> GetAllRents()
        {
            return _libraryContext.RentsList.GetAll();
        }

        public void UpdateRents(string id, int option, Object newValue)
        {
            _libraryContext.RentsList.Update(id, option, newValue);
        }

        public void DeleteRent(Rent rent)
        {
            _libraryContext.RentsList.Delete(rent);
        }
    }
}
