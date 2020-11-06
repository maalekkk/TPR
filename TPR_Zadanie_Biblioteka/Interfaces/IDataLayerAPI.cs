using System;
using System.Collections.Generic;
using System.Text;
using DL.DataObjects;
using System.Xml.Serialization;

namespace DL.Interfaces
{
    public interface IDataLayerAPI
    {
        //Library methods

        void SetDataFiller(IDataFiller filler);

        // Author methods

        void AddAuthor(params Object[] props);
        void DeleteAuthor(Guid id);
        Author GetAuthor(Guid id);
        IEnumerable<Author> GetAllAuthors();
        void UpdateAuthor(Guid id, int option, params Object[] newValue);

        // Book methods

        void AddBook(params Object[] props);
        void DeleteBook(Guid id);
        Book GetBook(Guid id);
        IEnumerable<Book> GetAllBooks();
        void UpdateBook(Guid id, int option, params Object[] newValue);

        // CopyOfBook methods

        void AddCopyOfBook(params Object[] props);
        void DeleteCopyOfBook(Guid id);
        CopyOfBook GetCopyOfBook(Guid id);
        IEnumerable<CopyOfBook> GetAllCopiesOfBook();
        void UpdateCopyOfBook(Guid id, int option, Object newValue);
        void DeleteCopyOfBook(CopyOfBook copyOfBook);

        // Employee methods

        void AddEmployee(params Object[] props);
        void DeleteEmployee(Guid id);
        Employee GetEmployee(Guid id);
        IEnumerable<Employee> GetAllEmployees();
        void UpdateEmployee(Guid id, int option, Object newValue);

        //Readers methods

        void AddReader(params Object[] props);
        void DeleteReader(Guid id);
        Reader GetReader(Guid id);
        IEnumerable<Reader> GetAllReaders();
        void UpdateReader(Guid id, int option, Object newValue);

        //Rent methods

        void AddRent(params Object[] props);
        void DeleteRent(Guid id);
        Rent GetRent(Guid id);
        IEnumerable<Rent> GetAllRents();
        void UpdateRents(Guid id, int option, Object newValue);
        void DeleteRent(Rent rent);
    }
}