using System;

namespace DL
{
    class Author
    {
        private string _id;
        private string _name;
        private string _surname;

        public Author(string id, string name, string surname)
        {
            _id = id;
            _name = name;
            _surname = surname;
        }

        public string Id { get => _id; private set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Surname { get => _surname; set => _surname = value; }
    }
}
