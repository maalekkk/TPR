using System;
using System.Collections.Generic;

namespace DL.DataObjects.EventsObjects
{
    public class Rent : Event
    {
        private Reader _reader;
        private Employee _employee;
        private Dictionary<CopyOfBook, double> _books;
        private DateTime _dateOfReturn;

        public Rent(Guid id, Reader reader, Employee employee, List<CopyOfBook> books, DateTime dateOfRental, 
            DateTime dateOfReturn) : base(id, dateOfRental)
        {
            _reader = reader;
            _employee = employee;
            _dateOfReturn = dateOfReturn;
            _books = new Dictionary<CopyOfBook, double>();
            addBooksToDictionary(books);
        }

        public Rent(Guid id, Reader reader, Employee employee, List<CopyOfBook> books, DateTime dateOfRental) : 
            base(id, dateOfRental)
        {
            _reader = reader;
            _employee = employee;
            _books = new Dictionary<CopyOfBook, double>();
            addBooksToDictionary(books);
        }

        public Reader Reader { get => _reader; private set => _reader = value; }
        public Employee Employee { get => _employee; private set => _employee = value; }
        public DateTime DateOfReturn { get => _dateOfReturn; set => _dateOfReturn = value; }
        public Dictionary<CopyOfBook, double> Book { get => _books; private set => _books = value; }

        public override string ToString()
        {
            string returnString = "Rent[GUID{" + Id.ToString() + "}, Reader{" + Reader.ToString() + "}, Employee{" + Employee.ToString()
                + "}, Rent_Date{" + Date.ToString() + "}, Return_Date{";
            if(_dateOfReturn != null)
            {
                returnString += DateOfReturn.ToString() + "}";
            }
            else
            {
                returnString += "NOT_RETURNED}";
            }

            return returnString;
        }


        private void addBooksToDictionary(List<CopyOfBook> books)
        {
            foreach (CopyOfBook book in books)
            {
                _books.Add(book, book.PricePerDay);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Rent rent &&
                   Id.Equals(rent.Id);
        }
    }
}
