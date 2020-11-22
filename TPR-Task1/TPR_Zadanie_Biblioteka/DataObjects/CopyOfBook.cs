using System;
using System.Runtime.Serialization;

namespace DL.DataObjects
{
    [Serializable]
    public class CopyOfBook : ISerializable
    {
        private Guid _id;
        private Book _book;
        private DateTime _purchaseDate;
        private double _pricePerDay;

        public CopyOfBook(Guid id, Book book, DateTime purchaseDate, double pricePerDay)
        {
            _id = id;
            _book = book;
            _purchaseDate = purchaseDate;
            _pricePerDay = pricePerDay;
        }

        public CopyOfBook(SerializationInfo info, StreamingContext context)
        {
            _id = (Guid)info.GetValue("id", typeof(Guid));
            _book = (Book)info.GetValue("book", typeof(Book));
            _purchaseDate = info.GetDateTime("pruchaseDate");
            _pricePerDay = info.GetDouble("pricePerDay");
        }

        public Guid Id { get => _id; set => _id = value; }
        public Book Book { get => _book; set => _book = value; }
        public DateTime PurchaseDate { get => _purchaseDate; set => _purchaseDate = value; }
        public double PricePerDay { get => _pricePerDay; set => _pricePerDay = value; }

        public override bool Equals(object obj)
        {
            return obj is CopyOfBook book &&
                   _id.Equals(book._id);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("id", _id, typeof(Guid));
            info.AddValue("book", _book, typeof(Book));
            info.AddValue("purchaseDate", _purchaseDate);
            info.AddValue("pricePerDay", _pricePerDay);
        }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(Book)}={Book}, {nameof(PurchaseDate)}={PurchaseDate.ToString()}, {nameof(PricePerDay)}={PricePerDay.ToString()}}}";
        }

    }
}
