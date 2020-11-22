using System;
using System.IO;
using System.Runtime.Serialization;

namespace DL.DataObjects
{
    [Serializable]
    public class Author : ISerializable
    {
        private Guid _id;
        private string _name; 
        private string _surname;

        public Author(Guid id, string name, string surname)
        {
            _id = id;
            _name = name;
            _surname = surname;
        }

        public Author(SerializationInfo info, StreamingContext context)
        {
            _id = (Guid)info.GetValue("id", typeof(Guid));
            _name = info.GetString("name");
            _surname = info.GetString("surname");
        }

        public Guid Id { get => _id; private set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Surname { get => _surname; set => _surname = value; }

        public override bool Equals(object obj)
        {
            return obj is Author author &&
                   _id.Equals(author._id);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("id", _id, typeof(Guid));
            info.AddValue("name", _name);
            info.AddValue("surname", _surname);
        }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(Name)}={Name}, {nameof(Surname)}={Surname}}}";
        }

    }
}
