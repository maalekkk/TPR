using System;
using System.Collections.Generic;

namespace SL
{
    public interface IDataRepository
    {
        void AddLocation(short id, String name, decimal costRate, decimal avaibility, DateTime modifiedDate);
        void UpdateLocation(short id, string name, decimal costRate, decimal avaibility, DateTime modifiedDate);
        void DeleteLocation(short id);
        List<short> GetLocationsIds();
        String GetLocationName(short id);
        decimal GetLocationCostRate(short id);
        decimal GetLocationAvaibility(short id);
        DateTime GetLocationModifiedDate(short id);
    }
}