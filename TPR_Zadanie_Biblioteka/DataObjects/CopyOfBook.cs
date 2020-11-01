using System;
using System.Collections.Generic;
using System.Text;

namespace DL
{
    class CopyOfBook
    {
        private string _id;
        private Book _book;
        private DateTime _purchaseDate;
        private double _pricePerDay;      //double is better than float imo

        public CopyOfBook(string id, Book book, DateTime purchaseDate, double pricePerDay)
        {
            _id = id;
            _book = book;
            _purchaseDate = purchaseDate;
            _pricePerDay = pricePerDay;
        }
        public string Id { get => _id; set => _id = value; }
        public Book Book { get => _book; set => _book = value; }
        public DateTime PurchaseDate { get => _purchaseDate; set => _purchaseDate = value; }
        public double PricePerDay { get => _pricePerDay; set => _pricePerDay = value; }
    }
}
