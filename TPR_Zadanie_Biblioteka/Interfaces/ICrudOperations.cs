using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DL
{
    // Implementor from Bridge design pattern
    public interface ICrudOperations <T>
    {
        void Add(T obj);
        T Get(string id);
        IEnumerable<T> GetAll();
        void Update(string id, T obj);
        void Delete(T obj);
    }
}
