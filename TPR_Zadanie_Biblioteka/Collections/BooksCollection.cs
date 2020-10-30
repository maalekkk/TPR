using System;
using System.Collections.Generic;
using System.Text;

namespace DL
{
    // Books Dictionary with implemented CRUD operations 
    class BooksCollection : ICrudOperations<Book>
    {
        private Dictionary<string, Book> _books;

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

        public Book Get(string id)
        {
            return _books[id];
        }
        // I really want to simply return _books Dictionary, but i can't change returned type because it's generic :(  
        public IEnumerable<Book> GetAll()
        {
            List<Book> returnList = new List<Book>();
            foreach(KeyValuePair<string, Book> book in _books)
            {
                returnList.Add(book.Value);
            }
            return returnList;
        }

        public void Update(string id, Book obj)
        {
            if (id.Equals(obj.Id)) 
            {
                _books[id] = obj;
            }
        }
    }
}
