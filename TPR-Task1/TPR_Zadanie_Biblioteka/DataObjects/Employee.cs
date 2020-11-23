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
            info.AddValue("dateOfEmployment", _dateOfEmployment);
        }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(Name)}={Name}, {nameof(Surname)}={Surname}, {nameof(BirthDate)}={BirthDate.ToString()}, {nameof(PhoneNumber)}={PhoneNumber}, {nameof(Email)}={Email}, {nameof(Gender1)}={Gender1.ToString()}, {nameof(DateOfEmployment)}={DateOfEmployment.ToString()}}}";
        }

    }
}
