using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace czesc1
{
    public class RandomDataFiller : DataFiller
    {
        private int numberOfBooks;
        private int numberOfBookStates;
        private int numberOfBookReaders;
        private int numberOfEvents;

        public int NumberOfBooks
        {
            get => numberOfBooks;
            set => numberOfBooks = value;
        }

        public int NumberOfBookStates
        {
            get => numberOfBookStates;
            set => numberOfBookStates = value;
        }

        public int NumberOfBookReaders
        {
            get => numberOfBookReaders;
            set => numberOfBookReaders = value;
        }

        public int NumberOfEvents
        {
            get => numberOfEvents;
            set => numberOfEvents = value;
        }

        public override void Fill(ref DataContext context)
        {
            var bookReaders = context.bookReaders;
            var books = context.books;
            var events = context.events;
            var bookStates = context.bookStates;

            string title = "Title";
            string name = "Name";
            string surname = "Surname";
            string author = "Author";
            string isbn = "";
            DateTime start = new DateTime(year: 1995, month: 1, day: 1);
            DateTime end = new DateTime(year: 2010, month: 1, day: 1);
            int rangeForPurchase = (end - start).Days;
            int rangeForBorrowDate = (DateTime.Today - end.AddDays(1)).Days;
            Random rnd = new Random();


            // fill bookReaders container with random objects
            for (int i = 0; i < numberOfBookReaders; i++)
            {
                bookReaders.Add(new BookReader()
                {
                    Age = i,
                    FirstName = name + i,
                    LastName = surname + i,
                    //creates 9 digit random number
                    Telephone = rnd.Next(100000000, 999999999).ToString()
                });
            }

            // fill books container with random objects
            for (int i = 0; i < numberOfBooks; i++)
            {
                isbn = i.ToString();
                books.Add(isbn, new Book()
                {
                    Isbn = isbn,
                    Title = title + i,
                    Author = author + i,
                    ReleaseYear = rnd.Next(1900, 2018)
                });
            }

            // fill bookStates container with random objects
            for (int i = 0; i < numberOfBookStates; i++)
            {
                bookStates.Add(new BookState
                {
                    //generates random date between 01.01.1995 and today
                    DateOfPurchase = start.AddDays(rnd.Next(rangeForPurchase)),
                    //gets book from dicttionary by random isbn number
                    Book = context.books[rnd.Next(0, numberOfBooks).ToString()],
                    Available = true
                });
            }

            // fill events container with random objects
            for (int i = 0; i < numberOfEvents; i++)
            {
                events.Add(new Event
                {
                    BookState = context.bookStates[rnd.Next(0, numberOfBookStates)],
                    BookReader = context.bookReaders[rnd.Next(0, numberOfBookReaders)],
                    BorrowDate = end.AddDays(rnd.Next(rangeForBorrowDate))
                });
            }
        }
    }
}
