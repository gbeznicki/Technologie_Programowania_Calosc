using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace czesc1
{
    public class ConstDataFiller : DataFiller
    {
        public override void Fill(ref DataContext context)
        {
            var bookReaders = context.bookReaders;
            var books = context.books;
            var events = context.events;
            var bookStates = context.bookStates;

            // create book reader objects
            BookReader reader1 = new BookReader()
            {
                Age = 30,
                FirstName = "Sławomir",
                LastName = "Desperski",
                Telephone = "343245634"
            };
            BookReader reader2 = new BookReader()
            {
                Age = 50,
                FirstName = "Jacek",
                LastName = "Goc",
                Telephone = "5346345"
            };
            BookReader reader3 = new BookReader()
            {
                Age = 45,
                FirstName = "Mirosław",
                LastName = "Saniewski",
                Telephone = "465432654"
            };

            // initialize book reader list
            bookReaders.Add(reader1);
            bookReaders.Add(reader2);
            bookReaders.Add(reader3);

            // create book objects
            Book book1 = new Book()
            {
                Isbn = "9788380751606",
                Title = "Behawiorysta",
                Author = "Remigiusz Mróz",
                ReleaseYear = 2016
            };
            Book book2 = new Book()
            {
                Isbn = "9788380750210",
                Title = "Ekspozycja",
                Author = "Remigiusz Mróz",
                ReleaseYear = 2015
            };
            Book book3 = new Book()
            {
                Isbn = "9788380750722",
                Title = "Przewieszenie",
                Author = "Remigiusz Mróz",
                ReleaseYear = 2016
            };
            Book book4 = new Book()
            {
                Isbn = "9788380751026",
                Title = "Trawers",
                Author = "Remigiusz Mróz",
                ReleaseYear = 2016
            };
            Book book5 = new Book()
            {
                Isbn = "9788327154590",
                Title = "Enklawa",
                Author = "Ove Logmansbo",
                ReleaseYear = 2016
            };
            Book book6 = new Book()
            {
                Isbn = "9788327155825",
                Title = "Połów",
                Author = "Ove Logmansbo",
                ReleaseYear = 2016
            };
            Book book7 = new Book()
            {
                Isbn = "9788327155917",
                Title = "Prom",
                Author = "Ove Logmansbo",
                ReleaseYear = 2017
            };

            // initialize book dictionary
            context.books.Add(book1.Isbn, book1);
            context.books.Add(book2.Isbn, book2);
            context.books.Add(book3.Isbn, book3);
            context.books.Add(book4.Isbn, book4);
            context.books.Add(book5.Isbn, book5);
            context.books.Add(book6.Isbn, book6);
            context.books.Add(book7.Isbn, book7);

            // create bookStates

            BookState bookState1 = new BookState
            {
                DateOfPurchase = new DateTimeOffset(2017, 5, 21, 00, 00, 00, new TimeSpan(1, 0, 0)),
                Book = book1,
                Available = true
            };

            BookState bookState2 = new BookState
            {
                DateOfPurchase = new DateTimeOffset(2017, 5, 21, 00, 00, 00, new TimeSpan(1, 0, 0)),
                Book = book2,
                Available = true
            };

            BookState bookState3 = new BookState
            {
                DateOfPurchase = new DateTimeOffset(2017, 5, 21, 00, 00, 00, new TimeSpan(1, 0, 0)),
                Book = book3,
                Available = true
            };

            BookState bookState4 = new BookState
            {
                DateOfPurchase = new DateTimeOffset(2017, 5, 21, 00, 00, 00, new TimeSpan(1, 0, 0)),
                Book = book4,
                Available = true
            };

            BookState bookState5 = new BookState
            {
                DateOfPurchase = new DateTimeOffset(2017, 5, 21, 00, 00, 00, new TimeSpan(1, 0, 0)),
                Book = book5,
                Available = true
            };

            BookState bookState6 = new BookState
            {
                DateOfPurchase = new DateTimeOffset(2017, 5, 21, 00, 00, 00, new TimeSpan(1, 0, 0)),
                Book = book6,
                Available = true
            };

            BookState bookState7 = new BookState
            {
                DateOfPurchase = new DateTimeOffset(2017, 5, 21, 00, 00, 00, new TimeSpan(1, 0, 0)),
                Book = book7,
                Available = true
            };

            // initialize bookStates List
            bookStates.Add(bookState1);
            bookStates.Add(bookState2);
            bookStates.Add(bookState3);
            bookStates.Add(bookState4);
            bookStates.Add(bookState5);
            bookStates.Add(bookState6);
            bookStates.Add(bookState7);

            //create events
            Event event1 = new Event()
            {
                BookState = bookState1,
                BookReader = reader1,
                BorrowDate = new DateTimeOffset(2017, 05, 30, 12, 00, 00, new TimeSpan(1, 0, 0)),
                ReturnDate = new DateTimeOffset(2017, 06, 05, 12, 00, 00, new TimeSpan(1, 0, 0)),
            };

            Event event2 = new Event()
            {
                BookState = bookState2,
                BookReader = reader2,
                BorrowDate = new DateTimeOffset(2017, 07, 10, 12, 00, 00, new TimeSpan(1, 0, 0)),
                ReturnDate = new DateTimeOffset(2017, 8, 05, 12, 00, 00, new TimeSpan(1, 0, 0)),
            };

            Event event3 = new Event()
            {
                BookState = bookState1,
                BookReader = reader1,
                BorrowDate = new DateTimeOffset(2017, 9, 11, 12, 00, 00, new TimeSpan(1, 0, 0)),
                ReturnDate = new DateTimeOffset(2017, 9, 12, 12, 00, 00, new TimeSpan(1, 0, 0)),
            };

            Event event4 = new Event()
            {
                BookState = bookState1,
                BookReader = reader1,
                BorrowDate = DateTimeOffset.Now
            };

            // initialize events collection
            events.Add(event1);
            events.Add(event2);
            events.Add(event3);
            events.Add(event4);
        }
    }
}
