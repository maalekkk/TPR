using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DL
{
    // Readers List with implemented CRUD operations 
    class ReadersCollection : ICrudOperations<Reader>
    {
        private List<Reader> _readers;

        public void Add(Reader obj)
        {
            if (_readers.Find(reader => reader.Id.Equals(obj.Id)) != null)
            {
                throw new Exception("Reader with this ID already exists!");
            }
            if (obj.BirthDate.CompareTo(DateTime.Now) >= 0)
            {
                throw new Exception("Unborn Reader! (birth day is future date)");
            }

            _readers.Add(obj);
        }

        public void Delete(Reader obj)
        {
            _readers.Remove(obj);
        }

        public Reader Get(string id)
        {
            return _readers.Find(reader => reader.Id.Equals(id));
        }

        public IEnumerable<Reader> GetAll()
        {
            return _readers;
        }

        public void Update(string id, Reader obj)
        {
            for(int i = 0; i < _readers.Count; i++)
            {
                if (_readers[i].Id.Equals(id))
                {
                    _readers[i] = obj;
                }
            }
        }
    }
}
