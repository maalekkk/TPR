using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DL.Interfaces
{
    public interface IDataLayerAPI
    {
        //Library methods

        void SetDataFiller(IDataFiller filler);

        // Author methods

        void AddAuthor(params Object[] props);
        void DeleteAuthor(Guid id);

        // Book methods

        void AddBook(params Object[] props);
        void DeleteBook(Guid id);

        // CopyOfBook methods

        void AddCopyOfBook(params Object[] props);
        void DeleteCopyOfBook(Guid id);

        // Employee methods

        void AddEmployee(params Object[] props);
        void DeleteEmployee(Guid id);

        //Readers methods

        void AddReader(params Object[] props);
        void DeleteReader(Guid id);

        //Rent methods

        void AddRent(params Object[] props);
        void DeleteRent(Guid id);
    }
}
