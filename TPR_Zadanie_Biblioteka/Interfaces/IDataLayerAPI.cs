using System;
using System.Collections.Generic;
using System.Text;
using DL.DataObjects;
using DL.DataObjects.EventsObjects;
using System.Xml.Serialization;

namespace DL.Interfaces
{
    public interface IDataLayerAPI
    {
        // Author methods

        void AddAuthor(Author author);
        void DeleteAuthor(Author author);
        Author GetAuthor(Guid id);
        IEnumerable<Author> GetAllAuthors();
        void UpdateAuthor(Guid id, Author author);

        // Book methods

        void AddBook(Book book);
        void DeleteBook(Book book);
        Book GetBook(int position);
        int GetBookPosition(Book book);
        IEnumerable<Book> GetAllBooks();
        void UpdateBook(int position, Book book);

        // CopyOfBook methods

        void AddCopyOfBook(CopyOfBook copyOfBook);
        void DeleteCopyOfBook(CopyOfBook copyOfBook);
        CopyOfBook GetCopyOfBook(Guid id);
        IEnumerable<CopyOfBook> GetAllCopiesOfBook();
        void UpdateCopyOfBook(Guid id, CopyOfBook copyOfBook);

        // Employee methods

        void AddEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        Employee GetEmployee(Guid id);
        IEnumerable<Employee> GetAllEmployees();
        void UpdateEmployee(Guid id, Employee employee);

        //Readers methods

        void AddReader(Reader reader);
        void DeleteReader(Reader reader);
        Reader GetReader(Guid id);
        IEnumerable<Reader> GetAllReaders();
        void UpdateReader(Guid id, Reader reader);

        //Event methods

        void AddEvent(Event eventObject);
        Event GetEvent(Guid id);
        IEnumerable<Event> GetAllEvents();
    }
}