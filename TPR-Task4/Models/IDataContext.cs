using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public interface IDataContext
    {
        void Add(Location location);
        List<Location> GetAll();
        Location Get(short id);
        void Delete(Location location);
        void Update(short id, Location location);
    }
}
