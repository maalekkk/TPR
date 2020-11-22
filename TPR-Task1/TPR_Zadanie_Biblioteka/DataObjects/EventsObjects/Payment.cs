using System;
using System.Runtime.Serialization;

namespace DL.DataObjects.EventsObjects
{
    [Serializable]
    public class Payment : Event
    {
        private double _cash;
        private Reader _reader;

        public Payment(Guid id, DateTime paymentDate, Reader reader, double cash) : base(id, paymentDate)
        {
            _cash = cash;
            _reader = reader;
        }

        public Payment(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            _cash = info.GetDouble("cash");
            _reader = (Reader)info.GetValue("reader", typeof(Reader));
        }

        public double Cash { get => _cash; private set => _cash = value; }
        public Reader Reader { get => _reader; private set => _reader = value; }

        public override string ToString()
        {
            return $"{{{nameof(Cash)}={Cash.ToString()}, {nameof(Reader)}={Reader}, {nameof(Date)}={Date.ToString()}, {nameof(Id)}={Id.ToString()}}}";
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("cash", _cash);
            info.AddValue("reader", _reader, typeof(Reader));
        }
    }
}
