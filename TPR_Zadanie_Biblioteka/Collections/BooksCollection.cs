using System;
using System.Collections.Generic;
using System.Text;

namespace DL
{
    // Books Dictionary with implemented CRUD operations 
    class BooksCollection : ICrudOperations<Book>
    {
        private Dictionary<Guid, Book> _books;

        public BooksCollection()
        {
            _books = new Dictionary<Guid, Book>();
        }

        public void Add(Book obj)
        {
            if (_books.ContainsKey(obj.Id))
            {
                throw new Exception("Book with this ID already exists!");
            }
            _books.Add(obj.Id, obj);
        }

        public void Delete(Book obj)
        {
            _books.Remove(obj.Id);
        }

        public Book Get(Guid id)
        {
            return _books[id];
        }
        // I really want to simply return _books Dictionary, but i can't change returned type because it's generic :(  
        public IEnumerable<Book> GetAll()
        {
            List<Book> returnList = new List<Book>();
            foreach(KeyValuePair<Guid, Book> book in _books)
            {
                returnList.Add(book.Value);
            }
            return returnList;
        }

        public void Update(Guid id, int option, Object newValue)
        {
            //if (id.Equals(obj.Id)) 
            //{
            //    _books[id] = obj;
            //}
            if (!_books.ContainsKey(id))
            {
                throw new Exception("Book with this ID doesn't exist");
            }
            switch (option)
            {
                case Consts.BookName:
                    _books[id].Name = (string)newValue;
                    break;
                case Consts.BookAuthor:
                    _books[id].Author = (Author)newValue;
                    break;
                case Consts.BookDescription:
                    _books[id].Description = (string)newValue;
                    break;
                case Consts.BookBookType:
                    _books[id].BookType1 = (Book.BookType)newValue;
                    break;
                default:
                    throw new Exception("Invalid option!");
            }
        }
    }
}
