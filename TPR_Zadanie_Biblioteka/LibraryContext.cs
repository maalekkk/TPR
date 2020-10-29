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
        private ReadersCollection _readers;
        private BooksCollection _booksCatalog;
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
    }
}
