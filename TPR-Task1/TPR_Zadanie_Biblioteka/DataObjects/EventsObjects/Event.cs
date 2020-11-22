using System;
using System.Runtime.Serialization;

namespace DL.DataObjects.EventsObjects
{
    public abstract class Event : ISerializable
    {
        private Guid _id;
        private DateTime _date;

        public Event(Guid id, DateTime date)
        {
            _id = id;
            _date = date;
        }

        public Event(SerializationInfo info, StreamingContext context)
        {
            _id = (Guid)info.GetValue("id", typeof(Guid));
            _date = info.GetDateTime("date");
        }

        public DateTime Date { get => _date; set => _date = value; }
        public Guid Id { get => _id; set => _id = value; }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("id", _id, typeof(Guid));
            info.AddValue("date", _date);
        }
    }
}
