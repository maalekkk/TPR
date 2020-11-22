using System;
using System.Runtime.Serialization;

namespace DL.DataObjects
{
    [Serializable]
    public class Book : ISerializable
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

        private string _name;
        private Author _author;
        private string _description;
        private BookType _bookType;

        public Book(string name, Author author, string description, BookType bookType)
        {
            _name = name;
            _author = author;
            _description = description;
            _bookType = bookType;
        }

        public Book(SerializationInfo info, StreamingContext context)
        {
            _name = info.GetString("name");
            _author = (Author)info.GetValue("author", typeof(Author));
            _description = info.GetString("description");
            _bookType = (BookType)info.GetValue("bookType", typeof(BookType));
        }

        public string Name { get => _name; set => _name = value; }
        public Author Author { get => _author; set => _author = value; }
        public string Description { get => _description; set => _description = value; }
        internal BookType BookType1 { get => _bookType; set => _bookType = value; }

        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, {nameof(Author)}={Author}, {nameof(Description)}={Description}}}";
        }

        public override bool Equals(object obj)
        {
            return obj is Book book &&
                   _name == book._name &&
                   Author.Equals(_author, book._author);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("name", _name);
            info.AddValue("author", _author, typeof(Author));
            info.AddValue("description", _description);
            info.AddValue("bookType", _bookType, typeof(BookType));
        }
    }
}
