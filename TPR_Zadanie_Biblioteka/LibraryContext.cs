using DL.Collections;
using DL.DataObjects;
using DL.Interfaces;

namespace DL
{
    // Some kind of database in program's memory
    public class LibraryContext
    {
        private ICrudOperations<Employee> _staff;
        private ICrudOperations<Reader> _readers;
        private ICrudOperations<Author> _authors;
        private ICrudOperations<Book> _booksCatalog;
        private ICrudOperations<Rent> _rentsList;
        private ICrudOperations<CopyOfBook> _copiesOfBooks;

        public LibraryContext()
        {
            _staff = new EmployeesCollection();
            _readers = new ReadersCollection();
            _authors = new AuthorsCollection();
            _booksCatalog = new BooksCollection();
            _rentsList = new RentsCollection();
            _copiesOfBooks = new CopyOfBooksCollection();
        }

        internal ICrudOperations<Employee> Staff { get => _staff; private set => _staff = value; }
        internal ICrudOperations<Reader> Readers { get => _readers; private set => _readers = value; }
        internal ICrudOperations<Book> BooksCatalog { get => _booksCatalog; private set => _booksCatalog = value; }
        internal ICrudOperations<Rent> RentsList { get => _rentsList; private set => _rentsList = value; }
        internal ICrudOperations<CopyOfBook> CopiesOfBooks { get => _copiesOfBooks; private set => _copiesOfBooks = value; }
        internal ICrudOperations<Author> Authors { get => _authors; private set => _authors = value; }
    }
}
