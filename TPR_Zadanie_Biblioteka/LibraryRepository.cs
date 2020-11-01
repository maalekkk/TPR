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

        public void AddBook(Book book)
        {
            _libraryContext.BooksCatalog.Add(book);
        }

        public void GetBook(string id)
        {
            _libraryContext.BooksCatalog.Get(id);
        }

        public void GetAllBooks()
        {
            _libraryContext.BooksCatalog.GetAll();
        }

        public void UpdateBook(string id, Book book)
        {
            _libraryContext.BooksCatalog.Update(id, book);
        }

        public void DeleteBook(Book book)
        {
            _libraryContext.BooksCatalog.Delete(book);
        }

        public void AddReader(Reader reader)
        {
            _libraryContext.Readers.Add(reader);
        }

        public void GetReader(string id)
        {
            _libraryContext.Readers.Get(id);
        }

        public void GetAllReaders()
        {
            _libraryContext.Readers.GetAll();
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
