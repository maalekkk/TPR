using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Collections
{
    class EmployeesCollection : ICrudOperations<Employee>
    {
        private List<Employee> _employees;

        public void Add(Employee obj)
        {
            if (_employees.Find(reader => reader.Id.Equals(obj.Id)) != null)
            {
                throw new Exception("Employee with this ID already exists!");
            }
            if (obj.BirthDate.CompareTo(DateTime.Now) >= 0)
            {
                throw new Exception("Unborn employee! (birth day is future date)");
            }
            _employees.Add(obj);
        }

        public void Delete(Employee obj)
        {
            _employees.Remove(obj);
        }

        public Employee Get(string id)
        {
            return _employees.Find(reader => reader.Id.Equals(id));
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employees;
        }

        public void Update(string id, Employee obj)
        {
            if (!id.Equals(obj.Id))
            {
                throw new Exception("ID is permament, it cant be different from old object");
            }
            for (int i = 0; i < _employees.Count; i++)
            {
                if (_employees[i].Id.Equals(id))
                {
                    _employees[i] = obj;
                }
            }
        }
    }
}