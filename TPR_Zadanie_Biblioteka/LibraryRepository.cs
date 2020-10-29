using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DL
{
    // LibraryRepository contains all collections of data objects. 
    class LibraryRepository
    {
        private LibraryContext _libraryContext;
        private ICrudOperations<Reader> _readers;
        private List<Employee> _employees;
        private ICrudOperations<Book> _books;
        private ObservableCollection<CopyOfBook> _copiesOfBook;
        private IDataFiller _fillWithConsts;

        public ICrudOperations<Reader> Readers { get => _readers; set => _readers = value; }
        public List<Employee> Employees { get => _employees; set => _employees = value; }
        public ICrudOperations<Book> Books { get => _books; set => _books = value; }
        public ObservableCollection<CopyOfBook> CopiesOfBook { get => _copiesOfBook; set => _copiesOfBook = value; }
        public IDataFiller FillWithConsts { get => _fillWithConsts; set => _fillWithConsts = value; }

        public LibraryRepository()
        {
            _libraryContext = new LibraryContext();
            _readers = new ReadersCollection();
            _employees = new List<Employee>();
            _books = new BooksCollection();
            _copiesOfBook = new ObservableCollection<CopyOfBook>();
        }
    }
}
