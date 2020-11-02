using System;
using System.Collections.Generic;
using System.Text;

namespace DL
{
    class Rent
    {
        private Guid _id;
        private Reader _reader;
        private Employee _employee;
        private List<CopyOfBook> _books;
        private DateTime _dateOfRental;
        private DateTime _dateOfReturn;
        private double _totalPricePerDay;

        public Rent(Guid id, Reader reader, Employee employee, List<CopyOfBook> books, DateTime dateOfRental, double totalPricePerDay, DateTime dateOfReturn)
        {
            _id = id;
            _reader = reader;
            _employee = employee;
            _books = books;
            _dateOfRental = dateOfRental;
            _dateOfReturn = dateOfReturn;
            _totalPricePerDay = totalPricePerDay;
        }

        public Rent(Guid id, Reader reader, Employee employee, List<CopyOfBook> books, DateTime dateOfRental, double totalPricePerDay)
        {
            _id = id;
            _reader = reader;
            _employee = employee;
            _books = books;
            _dateOfRental = dateOfRental;
            _totalPricePerDay = totalPricePerDay;
        }

        public Guid Id { get => _id; private set => _id = value; }
        public Reader Reader { get => _reader; private set => _reader = value; }
        public Employee Employee { get => _employee; private set => _employee = value; }
        public DateTime DateOfRental { get => _dateOfRental; private set => _dateOfRental = value; }
        public DateTime DateOfReturn { get => _dateOfReturn; set => _dateOfReturn = value; }
        public List<CopyOfBook> Book { get => _books; private set => _books = value; }
        public double TotalPricePerDay { get => _totalPricePerDay; set => _totalPricePerDay = value; }

        public override bool Equals(object obj)
        {
            return Id.Equals(((Rent)obj).Id);
        }

        public override string ToString()
        {
            string returnString = "Rent[GUID{" + Id.ToString() + "}, Reader{" + Reader.ToString() + "}, Employee{" + Employee.ToString()
                + "}, Rent_Date{" + DateOfRental.ToString() + "}, Return_Date{";
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
    }
}
