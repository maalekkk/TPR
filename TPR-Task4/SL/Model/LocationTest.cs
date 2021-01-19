using System;

namespace SL.Model
{
    public class LocationTest
    {
        private short _id;
        private String _name;
        private decimal _costRate;
        private decimal _availability;
        private DateTime _modifiedDate;

        public short Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public decimal CostRate { get => _costRate; set => _costRate = value; }
        public decimal Availability { get => _availability; set => _availability = value; }
        public DateTime ModifiedDate { get => _modifiedDate; set => _modifiedDate = value; }

        public LocationTest() {}

        public LocationTest(short id, string name, decimal costRate, decimal availability, DateTime modifiedDate)
        {
            _id = id;
            _name = name;
            _costRate = costRate;
            _availability = availability;
            _modifiedDate = modifiedDate;
        }
    }
}