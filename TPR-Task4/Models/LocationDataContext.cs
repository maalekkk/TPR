using System.Collections.Generic;
using System.Linq;

namespace DL
{
    public partial class LocationDataContext
    {
        public List<Location> GetAllLocations()
        {
            IQueryable<Location> query = from location in Locations
                                         select location;
            return query.ToList();
        }
    }
}