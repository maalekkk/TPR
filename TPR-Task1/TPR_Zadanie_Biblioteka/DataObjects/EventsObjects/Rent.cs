using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DL.DataObjects.EventsObjects
{
    [Serializable]
    public class Rent : Event
    {
        private Reader _reader;
        private Employee _employee;
        private List<CopyOfBook> _books;
        private DateTime _dateOfReturn;

        public Rent(Guid id, Reader reader, Employee employee, List<CopyOfBook> books, DateTime dateOfRental,
            DateTime dateOfReturn) : base(id, dateOfRental)
        {
            _reader = reader;
            _employee = employee;
            _dateOfReturn = dateOfReturn;
            _books = books;
        }

        public Rent(Guid id, Reader reader, Employee employee, List<CopyOfBook> books, DateTime dateOfRental) :
            base(id, dateOfRental)
        {
            _reader = reader;
            _employee = employee;
            _books = books;
        }


        public Rent(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            _reader = (Reader) info.GetValue("reader", typeof(Reader));
            _employee = (Employee) info.GetValue("employee", typeof(Employee));
            _dateOfReturn = info.GetDateTime("dateOfReturn");
            _books = (List<CopyOfBook>) info.GetValue("books", typeof(List<CopyOfBook>));
        }


        public Reader Reader
        {
            get => _reader;
            private set => _reader = value;
        }

        public Employee Employee
        {
            get => _employee;
            private set => _employee = value;
        }

        public DateTime DateOfReturn
        {
            get => _dateOfReturn;
            set => _dateOfReturn = value;
        }

        public List<CopyOfBook> Book
        {
            get => _books;
            private set => _books = value;
        }

        public override string ToString()
        {
            string returnString = "Rent[GUID{" + Id.ToString() + "}, Reader{" + Reader.ToString() + "}, Employee{" +
                                  Employee.ToString()
                                  + "}, Rent_Date{" + Date.ToString() + "}, Return_Date{";
            if (_dateOfReturn != null)
            {
                returnString += DateOfReturn.ToString() + "}";
            }
            else
            {
                returnString += "NOT_RETURNED}";
            }

            return returnString;
        }


        public override bool Equals(object obj)
        {
            return obj is Rent rent &&
                   Id.Equals(rent.Id);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("reader", _reader, typeof(Reader));
            info.AddValue("employee", _employee, typeof(Employee));
            info.AddValue("books", _books, typeof(List<CopyOfBook>));
            info.AddValue("dateOfReturn", _dateOfReturn);
        }
    }
}
