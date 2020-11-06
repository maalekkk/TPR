using System;

namespace DL.DataObjects
{
    public class Author
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



        public Guid Id { get => _id; private set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Surname { get => _surname; set => _surname = value; }

        public override bool Equals(object obj)
        {
            return Id.Equals(((Author)obj).Id);
        }

        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }
}
