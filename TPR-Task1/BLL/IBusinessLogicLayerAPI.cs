using DL.DataObjects;
using DL.DataObjects.EventsObjects;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface IBusinessLogicLayerAPI
    {
        //AUTHORS 

        void AddAuthor(string name, string surname);
        void DeleteAuthor(Author author);
        IEnumerable<Author> GetAllAuthors();
        Author FindAuthor(Predicate<Author> paramenter);

        //BOOKS

        void AddBook(string name, Author author, string description, Book.BookType bookType);
        void DeleteBook(Book book);
        IEnumerable<Book> GetAllBooks();
        Book FindBook(Predicate<Book> parameter);

        //COPIES OF BOOKS

        void AddCopyOfBook(Book book, DateTime purchase, double pricePerDay);
        void DeleteCopyOfBook(CopyOfBook copyOfBook);
        bool IsCopyOfBookRented(CopyOfBook copyOfBook);
        IEnumerable<CopyOfBook> GetAllCopiesOfBook();
        CopyOfBook FindCopyOfBook(Predicate<CopyOfBook> parameter);
        IEnumerable<CopyOfBook> GetRentedCopiesOfBooks();
        IEnumerable<CopyOfBook> GetRentedCopiesOfBooks(Rent rent);

        //EMPLOYEES

        void AddEmployee(string name, string surname, DateTime birthDate,
            string phoneNumber, string email, Employee.Gender gender, DateTime dateOfEmployment);
        void DeleteEmployee(Employee employee);
        IEnumerable<Employee> GetAllEmployees();
        void UpdateEmployee(Guid id, Employee employee);
        Employee FindEmployee(Predicate<Employee> parameter);

        //READERS

        void AddReader(string name, string surname, DateTime birthDate,
            string phoneNumber, string email, Employee.Gender gender, DateTime dateOfRegistration);
        void DeleteReader(Reader reader);
        IEnumerable<Reader> GetAllReaders();
        void UpdateReader(Guid id, Reader reader);
        Reader FindReader(Predicate<Reader> parameter);

        //EVENTS

        IEnumerable<Event> GetAllEvents();
        Event FindEvent(Predicate<Event> parameter);

        //RENTS

        void AddRent(Reader reader, Employee employee, List<CopyOfBook> books);
        IEnumerable<Rent> GetAllRents();
        IEnumerable<Rent> GetAllCurrentRents();

        //RETURNS

        void AddReturn(Rent rent, List<CopyOfBook> books);
        IEnumerable<Return> GetAllReturns();
        IEnumerable<Return> GetAllReturnsByRent(Rent rent);

        //PAYMENTS

        void AddPayment(Reader reader, double cash);
        List<Payment> GetAllPayments();

    }
}
