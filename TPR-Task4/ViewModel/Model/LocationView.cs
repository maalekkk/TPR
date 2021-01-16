using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Model
{
    public class LocationView
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
    }
}
