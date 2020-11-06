using System;
using System.Collections.Generic;
using System.Text;
using DL.DataObjects;
using System.Xml.Serialization;

namespace DL.Interfaces
{
    public interface IDataLayerAPI
    {
        // Author methods

        void AddAuthor(params Object[] props);
        void DeleteAuthor(Author author);
        Author GetAuthor(Guid id);
        IEnumerable<Author> GetAllAuthors();
        void UpdateAuthor(Guid id, int option, params Object[] newValue);

        // Book methods

        void AddBook(params Object[] props);
        void DeleteBook(Book book);
        Book GetBook(Guid id);
        IEnumerable<Book> GetAllBooks();
        void UpdateBook(Guid id, int option, params Object[] newValue);

        // CopyOfBook methods

        void AddCopyOfBook(params Object[] props);
        void DeleteCopyOfBook(CopyOfBook copyOfBook);
        CopyOfBook GetCopyOfBook(Guid id);
        IEnumerable<CopyOfBook> GetAllCopiesOfBook();
        void UpdateCopyOfBook(Guid id, int option, Object newValue);

        // Employee methods

        void AddEmployee(params Object[] props);
        void DeleteEmployee(Employee employee);
        Employee GetEmployee(Guid id);
        IEnumerable<Employee> GetAllEmployees();
        void UpdateEmployee(Guid id, int option, Object newValue);

        //Readers methods

        void AddReader(params Object[] props);
        void DeleteReader(Reader reader);
        Reader GetReader(Guid id);
        IEnumerable<Reader> GetAllReaders();
        void UpdateReader(Guid id, int option, Object newValue);

        //Rent methods

        void AddRent(params Object[] props);
        void DeleteRent(Rent rent);
        Rent GetRent(Guid id);
        IEnumerable<Rent> GetAllRents();
        void UpdateRents(Guid id, int option, Object newValue);
    }
}