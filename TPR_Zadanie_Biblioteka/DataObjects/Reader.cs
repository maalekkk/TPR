using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DL.DataObjects
{
    public class Reader : Person
    {
        private DateTime _dateOfRegistration;
        private double _balance;
        public Reader(Guid id, string name, string surname, DateTime birthDate,
            string phoneNumber, string email, Gender gender, DateTime dateOfRegistration)
            : base(id, name, surname, birthDate, phoneNumber, email, gender)
        {
            _dateOfRegistration = dateOfRegistration;
            Balance = 0.0;
        }

        public DateTime DateOfRegistration { get => _dateOfRegistration; set => _dateOfRegistration = value; }
        public double Balance { get => _balance; set => _balance = value; }

        public override bool Equals(object obj)
        {
            return obj is Reader reader &&
                   base.Equals(obj) &&
                   Id.Equals(reader.Id);
        }

        public override string ToString()
        {
            return $"{{{nameof(DateOfRegistration)}={DateOfRegistration.ToString()}, {nameof(Id)}={Id.ToString()}, {nameof(Name)}={Name}, {nameof(Surname)}={Surname}, {nameof(BirthDate)}={BirthDate.ToString()}, {nameof(PhoneNumber)}={PhoneNumber}, {nameof(Email)}={Email}, {nameof(Gender1)}={Gender1.ToString()}}}";
        }


    }
}
