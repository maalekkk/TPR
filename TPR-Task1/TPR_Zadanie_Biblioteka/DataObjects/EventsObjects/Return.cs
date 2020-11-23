using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DL.DataObjects.EventsObjects
{
    [Serializable]
    public class Return : Event
    {
        private List<CopyOfBook> _books;
        private Rent _rent;

        public Return(Guid id, DateTime returnDate, List<CopyOfBook> books, Rent rent) : base(id, returnDate)
        {
            _books = books;
            _rent = rent;
        }

        public Return(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            _books = (List<CopyOfBook>)info.GetValue("books", typeof(List<CopyOfBook>));
            _rent = (Rent)info.GetValue("rent", typeof(Rent));
        }

        public List<CopyOfBook> Books { get => _books; private set => _books = value; }
        public Rent Rent { get => _rent; private set => _rent = value; }

        public override string ToString()
        {
            return $"{{{nameof(Books)}={Books}, {nameof(Rent)}={Rent}, {nameof(Date)}={Date.ToString()}, {nameof(Id)}={Id.ToString()}}}";
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("books", _books, typeof(List<CopyOfBook>));
            info.AddValue("rent", _rent, typeof(Rent));
        }
    }
}
