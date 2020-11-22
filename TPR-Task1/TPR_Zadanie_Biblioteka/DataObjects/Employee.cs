using System;
using System.Runtime.Serialization;

namespace DL.DataObjects
{
    [Serializable]
    public class Employee : Person
    {
        private DateTime _dateOfEmployment;
        public Employee(Guid id, string name, string surname, DateTime birthDate,
            string phoneNumber, string email, Gender gender, DateTime dateOfEmployment)
            : base(id, name, surname, birthDate, phoneNumber, email, gender)
        {
            _dateOfEmployment = dateOfEmployment;
        }

        public Employee(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            //base.Id = (Guid)info.GetValue("id", typeof(Guid));
            //base.Name = info.GetString("name");
            //base.Surname = info.GetString("surname");
            //base.PhoneNumber = info.GetString("phoneNumber");
            //base.BirthDate = info.GetDateTime("birthDate");
            //base.Email = info.GetString("email");
            //base.Gender1 = (Gender)info.GetValue("gender", typeof(Gender));
            _dateOfEmployment = info.GetDateTime("dateOfEmployment");
        }

        public DateTime DateOfEmployment { get => _dateOfEmployment; set => _dateOfEmployment = value; }

        public override bool Equals(object obj)
        {
            return obj is Employee employee &&
                   Id.Equals(employee.Id);
        }

        
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            //info.AddValue("id", base.Id, typeof(Guid));
            //info.AddValue("name", base.Name);
            //info.AddValue("surname", base.Surname);
            //info.AddValue("phoneNumber", base.PhoneNumber);
            //info.AddValue("birthDate", base.BirthDate);
            //info.AddValue("email", base.Email);
            //info.AddValue("gender", base.Gender1, typeof(Gender));
            info.AddValue("dateOfEmployment", _dateOfEmployment);
        }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(Name)}={Name}, {nameof(Surname)}={Surname}, {nameof(BirthDate)}={BirthDate.ToString()}, {nameof(PhoneNumber)}={PhoneNumber}, {nameof(Email)}={Email}, {nameof(Gender1)}={Gender1.ToString()}, {nameof(DateOfEmployment)}={DateOfEmployment.ToString()}}}";
        }

    }
}
