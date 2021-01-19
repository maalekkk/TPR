using System.Collections.Generic;

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