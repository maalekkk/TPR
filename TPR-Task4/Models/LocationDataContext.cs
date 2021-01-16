using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
