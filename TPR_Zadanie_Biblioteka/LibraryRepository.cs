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

        public void AddBook(Object[] props)
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

        public void UpdateBook(string id, Book book)
        {
            _libraryContext.BooksCatalog.Update(id, book);
        }

        public void DeleteBook(Book book)
        {
            _libraryContext.BooksCatalog.Delete(book);
        }

        public void AddReader(Object[] props)
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

        public void UpdateReader(string id, Reader reader)
        {
            _libraryContext.Readers.Update(id, reader);
        }

        public void DeleteReader(Reader reader)
        {
            _libraryContext.Readers.Delete(reader);
        }
    }
}
