using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DL
{
    // Some kind of database in program's memory
    class LibraryContext
    {
        private List<Employee> _staff;
        private ICrudOperations<Reader> _readers;
        private ICrudOperations<Book> _booksCatalog;
        private ObservableCollection<Rent> _rentsList;
        private ObservableCollection<CopyOfBook> _copiesOfBooks;

        public LibraryContext()
        {
            _staff = new List<Employee>();
            _readers = new ReadersCollection();
            _booksCatalog = new BooksCollection();
            _rentsList = new ObservableCollection<Rent>();
            _copiesOfBooks = new ObservableCollection<CopyOfBook>();
        }

        internal List<Employee> Staff { get => _staff; set => _staff = value; }
        internal ICrudOperations<Reader> Readers { get => _readers; set => _readers = value; }
        internal ICrudOperations<Book> BooksCatalog { get => _booksCatalog; set => _booksCatalog = value; }
        internal ObservableCollection<Rent> RentsList { get => _rentsList; set => _rentsList = value; }
        internal ObservableCollection<CopyOfBook> CopiesOfBooks { get => _copiesOfBooks; set => _copiesOfBooks = value; }
    }
}
