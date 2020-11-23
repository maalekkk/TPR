using System;
using DL.DataObjects;
using DL.DataObjects.EventsObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace DL
{
    [Serializable]
    public class LibraryContext : ISerializable
    {
        private List<Author> _authors;
        private Dictionary<int, Book> _books;
        private List<CopyOfBook> _copiesOfBooks;
        private List<Employee> _employees;
        private List<Reader> _readers;
        private ObservableCollection<Event> _events;

        public LibraryContext()
        {
            _authors = new List<Author>();
            _books = new Dictionary<int, Book>();
            _copiesOfBooks = new List<CopyOfBook>();
            _employees = new List<Employee>();
            _readers = new List<Reader>();
            _events = new ObservableCollection<Event>();
        }

        public LibraryContext(SerializationInfo info, StreamingContext context)
        {
            _authors = (List<Author>) info.GetValue("authors", typeof(List<Author>));
            _books = (Dictionary<int, Book>) info.GetValue("books", typeof(Dictionary<int, Book>));
            _copiesOfBooks = (List<CopyOfBook>) info.GetValue("copiesOfBooks", typeof(List<CopyOfBook>));
            _employees = (List<Employee>) info.GetValue("employees", typeof(List<Employee>));
            _readers = (List<Reader>) info.GetValue("readers", typeof(List<Reader>));
            _events = (ObservableCollection<Event>) info.GetValue("events", typeof(ObservableCollection<Event>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("authors", _authors);
            info.AddValue("books", _books);
            info.AddValue("copiesOfBooks", _copiesOfBooks);
            info.AddValue("employees", _employees);
            info.AddValue("readers", _readers);
            info.AddValue("events", _events);
        }

        public List<Author> Authors { get => _authors; }
        public Dictionary<int, Book> Books { get => _books; }
        public List<CopyOfBook> CopiesOfBooks { get => _copiesOfBooks; }
        public List<Employee> Employees { get => _employees; }
        public List<Reader> Readers { get => _readers; }
        public ObservableCollection<Event> Events { get => _events; }
    }
}
