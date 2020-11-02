using DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL
{
    public class DataLayerAPI : IDataLayerAPI
    {
        private LibraryRepository _libraryRepository;

        public DataLayerAPI()
        {
            _libraryRepository = new LibraryRepository();
        }

        public void SetDataFiller(IDataFiller filler)
        {
            _libraryRepository.FillWithConsts = filler;
            _libraryRepository.FillData(null);
        }

        public void AddAuthor(params Object[] props)
        {
            _libraryRepository.AddAuthor(props);
        }

        public void AddBook(params Object[] props)
        {
            if (!props[2].GetType().Equals(typeof(Guid)))
            {
                throw new Exception("Invalid parameters order/values!");
            }
            Author bookAuthor = _libraryRepository.GetAllAuthors().First(author => author.Id.Equals((Guid)props[2]));
            _libraryRepository.AddBook(props[0], props[1], bookAuthor, props[3], props[4]);
        }

        public void AddCopyOfBook(params Object[] props)
        {
            if (!props[1].GetType().Equals(typeof(Guid)))
            {
                throw new Exception("Invalid parameters order/values!");
            }
            Author copiedBook = _libraryRepository.GetAllAuthors().First(book => book.Id.Equals((Guid)props[1]));
            _libraryRepository.AddCopyOfBook(props[0], copiedBook, props[2], props[3]);
        }

        public void AddEmployee(params object[] props)
        {
            throw new NotImplementedException();
        }

        public void AddReader(params object[] props)
        {
            throw new NotImplementedException();
        }

        public void AddRent(params object[] props)
        {
            throw new NotImplementedException();
        }

        public void DeleteAuthor(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteCopyOfBook(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteReader(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteRent(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
