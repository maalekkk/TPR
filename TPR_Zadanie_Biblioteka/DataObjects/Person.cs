using System;
using System.Collections.Generic;
using System.Text;

namespace DL
{
    class Person
    {
        public enum Gender
        {
            Male, Female
        };

        private Guid _id;
        private string _name;
        private string _surname;
        private DateTime _birthDate;
        private string _phoneNumber;
        private string _email;
        private Gender _gender;

        public Person(Guid id, string name, string surname, DateTime birthDate, string phoneNumber, string email, Gender gender)
        {
            _id = id;
            _name = name;
            _surname = surname;
            _phoneNumber = phoneNumber;
            _birthDate = birthDate;
            _email = email;
            _gender = gender;
        }
        public Guid Id { get => _id; private set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Surname { get => _surname; set => _surname = value; }
        
        public DateTime BirthDate { get => _birthDate; private set => _birthDate = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string Email { get => _email; set => _email = value; }
        public Gender Gender1 { get => _gender; set => _gender = value; }

        public override bool Equals(object obj)
        {
            return Id.Equals(((Person)obj).Id);
        }

        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }
}
