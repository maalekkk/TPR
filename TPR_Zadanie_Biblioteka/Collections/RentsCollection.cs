using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DL.Collections
{
    class RentsCollection : ICrudOperations<Rent>
    {
        private ObservableCollection<Rent> _rents;

        public RentsCollection()
        {
            _rents = new ObservableCollection<Rent>();
        }

        public void Add(Rent obj)
        {
            if (_rents.Where(rent => rent.Id.Equals(obj.Id)) != null)
            {
                throw new Exception("Rent with this ID already exists!");
            }
            if(obj.DateOfRental.CompareTo(DateTime.Now) > 0)
            {
                throw new Exception("Invalid date of rental! (future date)");
            }
            if(obj.TotalPricePerDay < 0)
            {
                throw new Exception("Total price per day cannot be negative!");
            }
            _rents.Add(obj);
        }

        public void Delete(Rent obj)
        {
            _rents.Remove(obj);
        }

        public Rent Get(Guid id)
        {
            return (Rent)_rents.Where(rent => rent.Id.Equals(id));
        }

        public IEnumerable<Rent> GetAll()
        {
            return _rents;
        }

        public void Update(Guid id, int option, Object newValue)
        {
            //if (!id.Equals(obj.Id))
            //{
            //    throw new Exception("ID is permament, it cant be different from old object");
            //}
            //for (int i = 0; i < _rents.Count; i++)
            //{
            //    if (_rents[i].Id.Equals(id))
            //    {
            //        _rents[i] = obj;
            //    }
            //}
            Rent updatingRent = _rents.Single(rent => rent.Id.Equals(id));
            if(updatingRent == null)
            {
                throw new Exception("Employee with this ID doesn't exist");
            }
            switch (option)
            {
                case Consts.RentDateOfReturn:
                    updatingRent.DateOfReturn = (DateTime)newValue;
                    break;
                case Consts.RentTotalPricePerDay:
                    updatingRent.TotalPricePerDay = (double)newValue;
                    break;
                default:
                    throw new Exception("Invalid option!");
            }
        }
    }
}