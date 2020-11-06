using DL.DataObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DL
{
    // Some kind of database in program's memory
    //public class LibraryContext
    //{
    //    private ICrudOperations<Employee> _staff;
    //    private ICrudOperations<Reader> _readers;
    //    private ICrudOperations<Author> _authors;
    //    private ICrudOperations<Book> _booksCatalog;
    //    private ICrudOperations<Rent> _rentsList;
    //    private ICrudOperations<CopyOfBook> _copiesOfBooks;

    //    public LibraryContext()
    //    {
    //        _staff = new EmployeesCollection();
    //        _readers = new ReadersCollection();
    //        _authors = new AuthorsCollection();
    //        _booksCatalog = new BooksCollection();
    //        _rentsList = new RentsCollection();
    //        _copiesOfBooks = new CopyOfBooksCollection();
    //    }

    //    internal ICrudOperations<Employee> Staff { get => _staff; private set => _staff = value; }
    //    internal ICrudOperations<Reader> Readers { get => _readers; private set => _readers = value; }
    //    internal ICrudOperations<Book> BooksCatalog { get => _booksCatalog; private set => _booksCatalog = value; }
    //    internal ICrudOperations<Rent> RentsList { get => _rentsList; private set => _rentsList = value; }
    //    internal ICrudOperations<CopyOfBook> CopiesOfBooks { get => _copiesOfBooks; private set => _copiesOfBooks = value; }
    //    internal ICrudOperations<Author> Authors { get => _authors; private set => _authors = value; }
    //}

    public class LibraryContext
    {
        private List<Author> _authors;
        private Dictionary<Guid, Book> _books;
        private List<CopyOfBook> _copiesOfBooks;
        private List<Employee> _employees;
        private List<Reader> _readers;
        private ObservableCollection<Rent> _rents;

        public LibraryContext()
        {
            _authors = new List<Author>();
            _books = new Dictionary<Guid, Book>();
            _copiesOfBooks = new List<CopyOfBook>();
            _employees = new List<Employee>();
            _readers = new List<Reader>();
            _rents = new ObservableCollection<Rent>();
        }

        public List<Author> Authors { get => _authors;}
        public Dictionary<Guid, Book> Books { get => _books;}
        public List<CopyOfBook> CopiesOfBooks { get => _copiesOfBooks;}
        public List<Employee> Employees { get => _employees;}
        public List<Reader> Readers { get => _readers;}
        public ObservableCollection<Rent> Rents { get => _rents;}
    }
}
