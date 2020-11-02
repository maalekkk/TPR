using System;
using System.Collections.Generic;
using System.Text;

namespace DL
{
    class Employee : Person
    {
        private DateTime _dateOfEmployment;
        public Employee(Guid id, string name, string surname, DateTime birthDate,
            string phoneNumber, string email, Gender gender, DateTime dateOfEmployment)
            : base(id, name, surname, birthDate, phoneNumber, email, gender)
        {
            _dateOfEmployment = dateOfEmployment;
        }

        public DateTime DateOfEmployment { get => _dateOfEmployment; set => _dateOfEmployment = value; }
    }
}
