using System;

namespace DL.DataObjects.EventsObjects
{
    public abstract class Event
    {
        private Guid _id;
        private DateTime _date;

        public Event(Guid id, DateTime date)
        {
            _id = id;
            _date = date;
        }

        public DateTime Date { get => _date; private set => _date = value; }
        public Guid Id { get => _id; set => _id = value; }
    }
}
