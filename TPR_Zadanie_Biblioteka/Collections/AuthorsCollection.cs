using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Collections
{
    class AuthorsCollection : ICrudOperations<Author>
    {
        private List<Author> _authors;

        public AuthorsCollection()
        {
            Authors = new List<Author>();
        }

        internal List<Author> Authors { get => _authors; private set => _authors = value; }

        public void Add(Author obj)
        {
            if (Authors.Find(author => author.Id.Equals(obj.Id)) != null)
            {
                throw new Exception("Author with this ID already exists");
            }
            Authors.Add(obj);
        }

        public void Delete(Author obj)
        {
            Authors.Remove(obj);
        }

        public Author Get(string id)
        {
            return Authors.Find(reader => reader.Id.Equals(id));
        }

        public IEnumerable<Author> GetAll()
        {
            return Authors;
        }

        public void Update(string id, int option, Object newValue)
        {
            //if (!id.Equals(obj.Id))
            //{
            //    throw new Exception("ID is permament, it cant be different from old object");
            //}
            //for(int i = 0; i < Authors.Count; i++)
            //{
            //    if (Authors[i].Id.Equals(id))
            //    {
            //        Authors[i] = obj;
            //    }
            //}
            Author updatingAuthor = _authors.Find(author => author.Id.Equals(id));
            if(updatingAuthor == null)
            {
                throw new Exception("Author with this ID doesn't exist");
            }
            switch (option)
            {
                case Consts.AuthorName:
                    updatingAuthor.Name = (string)newValue;
                    break;
                case Consts.AuthorSurname:
                    updatingAuthor.Surname = (string)newValue;
                    break;
                default:
                    throw new Exception("Invalid option!");
            }
        }
    }
}
