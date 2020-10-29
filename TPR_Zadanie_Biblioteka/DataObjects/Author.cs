using System;

namespace DL
{
    class Author
    {
        private string _id;
        private string _name;

        public Author(string id, string name)
        {
            this._id = id;
            this._name = name;
        }

        public string Id { get => _id; private set => _id = value; }
        public string Name { get => _name; set => _name = value; }
    }
}
