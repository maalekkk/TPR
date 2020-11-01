using System;
using System.Collections.Generic;
using System.Text;

namespace DL
{
    class Rent
    {
        private string _id;
        private Reader _reader;
        private Employee _employee;
        private List<CopyOfBook> _books;
        private DateTime _dateOfRental;
        private DateTime _dateOfReturn;
        private double _totalPricePerDay;

        public Rent(string id, Reader reader, Employee employee, List<CopyOfBook> books, DateTime dateOfRental, DateTime dateOfReturn, double totalPricePerDay)
        {
            _id = id;
            _reader = reader;
            _employee = employee;
            _books = books;
            _dateOfRental = dateOfRental;
            _dateOfReturn = dateOfReturn;
            _totalPricePerDay = totalPricePerDay;
        }

        public Rent(string id, Reader reader, Employee employee, List<CopyOfBook> books, DateTime dateOfRental)
        {
            _id = id;
            _reader = reader;
            _employee = employee;
            _books = books;
            _dateOfRental = dateOfRental;
        }

        public string Id { get => _id; private set => _id = value; }
        public Reader Reader { get => _reader; private set => _reader = value; }
        public Employee Employee { get => _employee; private set => _employee = value; }
        public DateTime DateOfRental { get => _dateOfRental; private set => _dateOfRental = value; }
        public DateTime DateOfReturn { get => _dateOfReturn; set => _dateOfReturn = value; }
        public List<CopyOfBook> Book { get => _books; private set => _books = value; }
        public double TotalPricePerDay { get => _totalPricePerDay; set => _totalPricePerDay = value; }
    }
}
