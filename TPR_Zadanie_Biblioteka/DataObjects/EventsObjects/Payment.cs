using System;

namespace DL.DataObjects.EventsObjects
{
    public class Payment : Event
    {
        private double _cash;
        private Reader _reader;

        public Payment(Guid id, DateTime paymentDate, Reader reader, double cash) : base(id, paymentDate)
        {
            Cash = cash;
            Reader = reader;
        }

        public double Cash { get => _cash; private set => _cash = value; }
        public Reader Reader { get => _reader; private set => _reader = value; }
    }
}
