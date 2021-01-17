using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Model;

namespace SL
{
    public class DataRepositoryInMemory : IDataRepository
    {
        private List<LocationTest> _locations = new List<LocationTest>();

        public DataRepositoryInMemory() { }

        public void AddLocation(short id, string name, decimal costRate, decimal avaibility, DateTime modifiedDate)
        {
            if (!_locations.Exists(i => i.Id.Equals(id)))
            {
                _locations.Add(new LocationTest(id, name, costRate, avaibility, modifiedDate));
            }
            else
            {
                throw new ArgumentException();
            }

        }

        public void DeleteLocation(short id)
        {
            try
            {
                LocationTest location = _locations.First(i => i.Id.Equals(id));
                _locations.Remove(location);
            }
            catch
            {
                throw new ArgumentException();
            }
            
        }

        public decimal GetLocationAvaibility(short id)
        {
            return _locations.First(i => i.Id.Equals(id)).Availability;
        }

        public decimal GetLocationCostRate(short id)
        {
            return _locations.First(i => i.Id.Equals(id)).CostRate;
        }

        public DateTime GetLocationModifiedDate(short id)
        {
            return _locations.First(i => i.Id.Equals(id)).ModifiedDate;
        }

        public string GetLocationName(short id)
        {
            return _locations.First(i => i.Id.Equals(id)).Name;
        }

        public List<short> GetLocationsIds()
        {
            List<short> ids = new List<short>();
            foreach(LocationTest location in _locations)
            {
                ids.Add(location.Id);
            }
            return ids;
        }

        public void UpdateLocation(short id, string name, decimal costRate, decimal avaibility, DateTime modifiedDate)
        {
            LocationTest location;
            try
            {
                location = _locations.First(i => i.Id.Equals(id));
            }
            catch
            {
                throw new ArgumentException();
            }
            location.Name = name;
            location.CostRate = costRate;
            location.Availability = avaibility;
            location.ModifiedDate = modifiedDate;
        }
    }
}
