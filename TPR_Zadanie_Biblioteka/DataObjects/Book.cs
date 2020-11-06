using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DataObjects
{
    public class Book
    {
        public enum BookType
        {
            Action,
            Classics,
            Detective,
            Fantasy,
            Horror,
            Romance,
            Biographie,
            Novel
        };

        private Guid _id;
        private string _name;
        private Author _author;
        private string _description;
        private BookType _bookType;

        public Book(Guid id, string name, Author author, string description, BookType bookType)
        {
            _id = id;
            _name = name;
            _author = author;
            _description = description;
            _bookType = bookType;
        }

        public Guid Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public Author Author { get => _author; set => _author = value; }
        public string Description { get => _description; set => _description = value; }
        internal BookType BookType1 { get => _bookType; set => _bookType = value; }

        public override bool Equals(object obj)
        {
            return Id.Equals(((Book)obj).Id);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
