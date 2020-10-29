using System;
using System.Collections.Generic;
using System.Text;

namespace DL
{
    class Reader : Person
    {
        public Reader(int id, string name, string surname, DateTime birthDate, 
            string phoneNumber, string email, Gender gender) 
            : base(id, name, surname, birthDate, phoneNumber, email, gender)
        {
        }


    }
}
