using System;
using System.Collections.Generic;
using System.Text;

namespace DL
{
    class Book
    {
        public enum BookType
        {
            Action,
            Classics,
            Detective,
            Fantasy,
            Horror,
            Romance,
            Biographie
        };

        private string _id;
        private string _name;
        private Author _author;
        private string _description;
        private BookType _bookType;

        public Book(string id, string name, Author author, string description, BookType bookType)
        {
            this._id = id;
            this._name = name;
            this._author = author;
            this._description = description;
            this._bookType = bookType;
        }

        public string Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public Author Author { get => _author; set => _author = value; }
        public string Description { get => _description; set => _description = value; }
        internal BookType BookType1 { get => _bookType; set => _bookType = value; }
    }
}
