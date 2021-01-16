using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL
{
    class DataRepository : IDataRepository
    {
        private IDataContext _dataContext;

        public event IDataRepository.OwnHandler OnRepositoryChange;

        public DataRepository(IDataContext data)
        {
            _dataContext = data;
        }

        public DataRepository()
        {
            _dataContext = new DataContext();
        }

        public void AddLocation(short id,string name, decimal costRate, decimal avaibility, DateTime modifiedDate)
        {
/*            Task.Run(() =>
            {*/
                Location newLocation = new Location();
                newLocation.LocationID = id;
                newLocation.Name = name;
                newLocation.CostRate = costRate;
                newLocation.Availability = avaibility;
                newLocation.ModifiedDate = modifiedDate;
                _dataContext.Add(newLocation);
            OnRepositoryChange?.Invoke();
            /*            });*/
        }

        public void DeleteLocation(short id)
        {
/*            Task.Run(() =>
            {*/
                Location deletingLocation = _dataContext.Get(id);
                _dataContext.Delete(deletingLocation);
            OnRepositoryChange?.Invoke();
            /*            });*/
        }

        public decimal GetLocationAvaibility(short id)
        {
             return _dataContext.Get(id).Availability;
        }

        public decimal GetLocationCostRate(short id)
        {
            return _dataContext.Get(id).CostRate;
        }

        public DateTime GetLocationModifiedDate(short id)
        {
            return _dataContext.Get(id).ModifiedDate;
        }

        public string GetLocationName(short id)
        {
            return _dataContext.Get(id).Name;
        }

        public List<short> GetLocationsIds()
        {
            List<short> result = new List<short>();
            foreach(Location location in _dataContext.GetAll())
            {
                result.Add(location.LocationID);
            }
            return result;
        }

        public void UpdateLocation(short id, string name, decimal costRate, decimal avaibility, DateTime modifiedDate)
        {
/*            Task.Run(() =>
            {*/
                Location location = new Location();
                location.LocationID = id;
                location.Name = name;
                location.CostRate = costRate;
                location.Availability = avaibility;
                location.ModifiedDate = modifiedDate;
                _dataContext.Update(id, location);
            OnRepositoryChange?.Invoke();
            /*            });*/
        }
    }
}
