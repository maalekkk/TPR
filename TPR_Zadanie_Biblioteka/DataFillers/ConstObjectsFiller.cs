using System;
using System.Collections.Generic;
using DL.DataObjects;
using DL.DataObjects.EventsObjects;
using DL.Interfaces;
using System.ComponentModel;
using System.Text;

namespace DL.DataFillers
{
    class ConstObjectsFiller : IDataFiller
    {
        public void Fill(LibraryContext context, string path)
        {
            Reader person1 = new Reader(Guid.NewGuid(), "Adam", "Nowak", new DateTime(1998, 05, 23), 
                "111222333", "adam.nowak@gmail.com",Person.Gender.Male, new DateTime(2019, 9, 11));
            Employee person2 = new Employee(Guid.NewGuid(), "Katarzyna", "Kowalska", new DateTime(1967, 03, 13),
                "123456789", "kaska123@outlook.com", Person.Gender.Female, new DateTime(2019, 9, 11));

            Author tolkien = new Author(Guid.NewGuid(), "John Ronald Reuel", "Tolkien");
            Author fDostojewski = new Author(Guid.NewGuid(), "Fiodor", "Dostojewski");

            Book hobbit = new Book(Guid.NewGuid(), "Hobbit, czyli tam i z powrotem", tolkien,
                "Powieść fantasy dla dzieci autorstwa J.R.R. Tolkiena.", Book.BookType.Fantasy);
            Book zik = new Book(Guid.NewGuid(), "Zbrodnia i Kara", fDostojewski,
                "Tematem powieści są losy byłego studenta, Rodiona Raskolnikowa, który postanawia zamordować i obrabować starą lichwiarkę."
                , Book.BookType.Classics);
            Book wp = new Book(Guid.NewGuid(), "Wladca Pierscieni", tolkien,
                "Powieść high fantasy J.R.R. Tolkiena, której akcja rozgrywa się w mitologicznym świecie Śródziemia.Jest ona kontynuacją innej powieści tego autora zatytułowanej Hobbit, czyli tam i z powrotem.", 
                Book.BookType.Fantasy);

            CopyOfBook hobbit1 = new CopyOfBook(Guid.NewGuid(), hobbit, new DateTime(2004, 11, 21, 0, 0, 0), 0.4);
            CopyOfBook hobbit2 = new CopyOfBook(Guid.NewGuid(), hobbit, new DateTime(2004, 12, 3, 0, 0, 0), 0.4);
            CopyOfBook zik1 = new CopyOfBook(Guid.NewGuid(), zik, new DateTime(2001, 10, 11, 0, 0, 0), 0.5);
            CopyOfBook zik2 = new CopyOfBook(Guid.NewGuid(), zik, new DateTime(2001, 10, 11, 0, 0, 0), 0.5);
            CopyOfBook wp1 = new CopyOfBook(Guid.NewGuid(), wp, new DateTime(2005, 12, 23, 0, 0, 0), 0.7);

            List<CopyOfBook> booksForRent1 = new List<CopyOfBook>();
            booksForRent1.Add(hobbit1);
            Rent rent1 = new Rent(person1, person2, booksForRent1, new DateTime(2010, 1, 6, 0, 0, 0));

            context.Authors.Add(tolkien);
            context.Authors.Add(fDostojewski);
            context.Employees.Add(person2);
            context.Readers.Add(person1);
            context.Books.Add(hobbit.Id, hobbit);
            context.Books.Add(wp.Id, wp);
            context.Books.Add(zik.Id, zik);
            context.CopiesOfBooks.Add(hobbit1);
            context.CopiesOfBooks.Add(hobbit2);
            context.CopiesOfBooks.Add(zik1);
            context.CopiesOfBooks.Add(zik2);
            context.CopiesOfBooks.Add(wp1);
            context.Rents.Add(rent1);
        }
    }
}
