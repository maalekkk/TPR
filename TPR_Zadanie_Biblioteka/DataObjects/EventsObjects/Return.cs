using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DataObjects.EventsObjects
{
    public class Return : Event
    {
        private List<CopyOfBook> _books;
        private Rent _rent;

        public Return(Guid id, DateTime returnDate, List<CopyOfBook> books, Rent rent) : base(id, returnDate)
        {
            _books = books;
            _rent = rent;
        }

        public List<CopyOfBook> Books { get => _books; private set => _books = value; }
        public Rent Rent { get => _rent; private set => _rent = value; }
    }
}
