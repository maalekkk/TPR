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

        public void Update(string id, int option, Object newValue)
        {
            //if (!id.Equals(obj.Id))
            //{
            //    throw new Exception("ID is permament, it cant be different from old object");
            //}
            //for(int i = 0; i < _readers.Count; i++)
            //{
            //    if (_readers[i].Id.Equals(id))
            //    {
            //        _readers[i] = obj;
            //    }
            //}
            Reader updatingReader = _readers.Find(reader => reader.Equals(id));
            if(updatingReader == null)
            {
                throw new Exception("Employee with this ID doesn't exist");
            }
            switch (option)
            {
                case Consts.PersonName:
                    updatingReader.Name = (string)newValue;
                    break;
                case Consts.PersonSurname:
                    updatingReader.Surname = (string)newValue;
                    break;
                case Consts.PersonPhoneNumber:
                    updatingReader.PhoneNumber = (string)newValue;
                    break;
                case Consts.PersonEmail:
                    updatingReader.Email = (string)newValue;
                    break;
                case Consts.ReaderDateOfRegistration:
                    updatingReader.DateOfRegistration = (DateTime)newValue;
                    break;
                default:
                    throw new Exception("Invalid option!");
            }
        }
    }
}
