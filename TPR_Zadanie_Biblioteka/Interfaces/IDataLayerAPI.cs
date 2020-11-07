using System;
using System.Collections.Generic;
using DL.DataObjects;
using DL.DataObjects.EventsObjects;

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
        Author FindAuthor(Predicate<Author> parameter);

        // Book methods

        void AddBook(Book book);
        void DeleteBook(Book book);
        Book GetBook(int position);
        int GetBookPosition(Book book);
        IEnumerable<Book> GetAllBooks();
        void UpdateBook(int position, Book book);
        Book FindBook(Predicate<Book> parameter);

        // CopyOfBook methods

        void AddCopyOfBook(CopyOfBook copyOfBook);
        void DeleteCopyOfBook(CopyOfBook copyOfBook);
        CopyOfBook GetCopyOfBook(Guid id);
        IEnumerable<CopyOfBook> GetAllCopiesOfBook();
        void UpdateCopyOfBook(Guid id, CopyOfBook copyOfBook);
        CopyOfBook FindCopyOfBook(Predicate<CopyOfBook> parameter);

        // Employee methods

        void AddEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        Employee GetEmployee(Guid id);
        IEnumerable<Employee> GetAllEmployees();
        void UpdateEmployee(Guid id, Employee employee);
        Employee FindEmployee(Predicate<Employee> parameter);

        //Readers methods

        void AddReader(Reader reader);
        void DeleteReader(Reader reader);
        Reader GetReader(Guid id);
        IEnumerable<Reader> GetAllReaders();
        void UpdateReader(Guid id, Reader reader);
        Reader FindReader(Predicate<Reader> parameter);

        //Event methods

        void AddEvent(Event eventObject);
        Event GetEvent(Guid id);
        IEnumerable<Event> GetAllEvents();
        Event FindEvent(Predicate<Event> parameter);
    }
}