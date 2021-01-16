using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    class DataContext : IDataContext
    {
        private readonly LocationDataContext _locationDataContext;

        public DataContext()
        {
            _locationDataContext = new LocationDataContext();
        }
        public void Add(Location location)
        {
                _locationDataContext.GetTable<Location>().InsertOnSubmit(location);
                _locationDataContext.SubmitChanges();

        }

        public void Delete(Location location)
        {
                _locationDataContext.GetTable<Location>().DeleteOnSubmit(location);
                _locationDataContext.SubmitChanges();
        }

        public Location Get(short id)
        {

                IQueryable<Location> query = from location in _locationDataContext.Locations
                                             where location.LocationID == id
                                             select location;
                return query.First();
        }

        public List<Location> GetAll()
        {
                return _locationDataContext.GetAllLocations();

        }

        public void Update(short id, Location location)
        {
                Location updatingLocation = Get(id);
                updatingLocation.Name = location.Name;
                updatingLocation.CostRate = location.CostRate;
                updatingLocation.ModifiedDate = location.ModifiedDate;
                updatingLocation.Availability = location.Availability;
                _locationDataContext.SubmitChanges();

        }
    }
}
