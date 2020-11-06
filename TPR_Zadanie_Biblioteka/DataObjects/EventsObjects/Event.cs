using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DataObjects.EventsObjects
{
    public abstract class Event
    {
        private DateTime _date;

        public Event(DateTime date)
        {
            Date = date;
        }

        public DateTime Date { get => _date; private set => _date = value; }
    }
}
