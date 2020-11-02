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
        T Get(Guid id);
        IEnumerable<T> GetAll();
        void Update(Guid id, int option, Object newValue);
        void Delete(T obj);
    }
}
