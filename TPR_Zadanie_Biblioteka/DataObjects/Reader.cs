using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DL
{
    class Reader : Person
    {
        private DateTime _dateOfRegistration;
        public Reader(Guid id, string name, string surname, DateTime birthDate,
            string phoneNumber, string email, Gender gender, DateTime dateOfRegistration)
            : base(id, name, surname, birthDate, phoneNumber, email, gender)
        {
            _dateOfRegistration = dateOfRegistration;
        }

        public DateTime DateOfRegistration { get => _dateOfRegistration; set => _dateOfRegistration = value; }

        public override string ToString()
        {
            return "Reader: " + base.ToString();
        }
    }
}
