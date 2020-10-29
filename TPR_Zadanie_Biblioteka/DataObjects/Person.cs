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

        private string _id;
        private string _name;
        private string _surname;
        private DateTime _birthDate;
        private string _phoneNumber;
        private string _email;
        private Gender _gender;

        public Person(int id, string name, string surname, DateTime birthDate, string phoneNumber, string email, Gender gender)
        {
            this._id = id;
            this._name = name;
            this._surname = surname;
            this._phoneNumber = phoneNumber;
            this._birthDate = birthDate;
            this._email = email;
            this._gender = gender;
        }
        public int Id { get => _id; private set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Surname { get => _surname; set => _surname = value; }
        
        public DateTime BirthDate { get => _birthDate; private set => _birthDate = value }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string Email { get => _email; set => _email = value; }
        public Gender Gender1 { get => _gender; set => _gender = value; }
    }
}
