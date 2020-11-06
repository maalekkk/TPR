using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DataObjects.EventsObjects
{
    class Return : Event
    {
        private List<CopyOfBook> _books;
        private Rent _rent;

        public Return(DateTime returnDate, List<CopyOfBook> books, Rent _rent) : base(returnDate)
        {
            _books = books;
            
        }

        public List<CopyOfBook> Books { get => _books; private set => _books = value; }
        public Rent Rent { get => _rent; private set => _rent = value; }
    }
}
