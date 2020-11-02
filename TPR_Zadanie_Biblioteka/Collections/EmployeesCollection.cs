using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Collections
{
    class EmployeesCollection : ICrudOperations<Employee>
    {
        private List<Employee> _employees;

        public EmployeesCollection()
        {
            _employees = new List<Employee>();
        }

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
            if(obj.DateOfEmployment.CompareTo(DateTime.Now) > 0)
            {
                throw new Exception("Invalid Date of employment! (future date)");
            }
            _employees.Add(obj);
        }

        public void Delete(Employee obj)
        {
            _employees.Remove(obj);
        }

        public Employee Get(Guid id)
        {
            return _employees.Find(reader => reader.Id.Equals(id));
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employees;
        }

        public void Update(Guid id, int option, Object newValue)
        {
            //if (!id.Equals(obj.Id))
            //{
            //    throw new Exception("ID is permament, it cant be different from old object");
            //}
            //for (int i = 0; i < _employees.Count; i++)
            //{
            //    if (_employees[i].Id.Equals(id))
            //    {
            //        _employees[i] = obj;
            //    }
            //}
            Employee updatingEmployee = _employees.Find(employee => employee.Id.Equals(id));
            if(updatingEmployee == null)
            {
                throw new Exception("Employee with this ID doesn't exist");
            }
            switch (option)
            {
                case Consts.PersonName:
                    updatingEmployee.Name = (string)newValue;
                    break;
                case Consts.PersonSurname:
                    updatingEmployee.Surname = (string)newValue;
                    break;
                case Consts.PersonPhoneNumber:
                    updatingEmployee.PhoneNumber = (string)newValue;
                    break;
                case Consts.PersonEmail:
                    updatingEmployee.Email = (string)newValue;
                    break;
                case Consts.EmployeeDateOfEmployment:
                    updatingEmployee.DateOfEmployment = (DateTime)newValue;
                    break;
                default:
                    throw new Exception("Invalid option!");
            }
        }
    }
}