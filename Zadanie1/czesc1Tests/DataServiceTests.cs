using Microsoft.VisualStudio.TestTools.UnitTesting;
using czesc1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace czesc1.Tests
{
    [TestClass()]
    public class DataServiceTests
    {
        private DataFiller dataFiller;
        private DataContext context;
        private DataRepository repository;
        private DataService service;

        [TestInitialize]
        public void TestInitialize()
        {
            dataFiller = new ConstDataFiller();
            context = new DataContext();
            repository = new DataRepository(dataFiller)
            {
                Data = context
            };
            repository.Fill();
            service = new DataService(repository);
        }

        [TestMethod()]
        public void DataServiceTest()
        {
        }
        
        [TestMethod()]
        public void AddBookTest()
        {
            int beforeSize = repository.GetAllBooks().Count();
            var beforeLastBook = repository.GetAllBooks().Last();
            var bookToAdd = new Book()
            {
                Isbn = "9788327152305",
                Title = "Człowiek nietoperz",
                Author = "Jo Nesbo",
                ReleaseYear = 2014
            };
            service.AddBook(bookToAdd);
            int afterSize = repository.GetAllBooks().Count();
            var afterLastBook = repository.GetAllBooks().Last();
            
            // check sizes
            Assert.AreNotEqual(beforeSize, afterSize);
            
            // check if last books aren't equal
            Assert.AreNotEqual(beforeLastBook, afterLastBook);
            
            // check if the book is in the list
            Assert.IsTrue(context.books.ContainsKey(bookToAdd.Isbn));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddBookTestException()
        {
            var bookToAdd = repository.GetAllBooks().ElementAt(0);

            service.AddBook(bookToAdd);
        }

        [TestMethod()]
        public void AddBookReaderTest()
        {
            int beforeSize = context.bookReaders.Count;
            var beforeLastBookReader = context.bookReaders.Last();
            var bookReaderToAdd = new BookReader()
            {
                Age = 35,
                FirstName = "Wiktor",
                LastName = "Forst",
                Telephone = "123456789"
            };
            service.AddBookReader(bookReaderToAdd);
            int afterSize = context.bookReaders.Count;
            var afterLastBookReader = context.bookReaders.Last();
            
            // check sizes
            Assert.AreNotEqual(beforeSize, afterSize);
           
            // check if last books aren't equal
            Assert.AreNotEqual(beforeLastBookReader, afterLastBookReader);
           
            // check if the book is in the list
            Assert.IsTrue(context.bookReaders.Contains(bookReaderToAdd));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddBookReaderTestException()
        {
            var bookReaderToAdd = repository.GetAllBookReaders().ElementAt(0);
            service.AddBookReader(bookReaderToAdd);
        }

        [TestMethod()]
        public void AddBookStateTest()
        {
            int beforeSize = context.bookStates.Count;
            var beforeLastBookState = context.bookStates.Last();
            var bookStateToAdd = new BookState()
            {
                DateOfPurchase = new DateTimeOffset(2017, 5, 21, 00, 00, 00, new TimeSpan(1, 0, 0)),
                Book = new Book()
                {
                    Isbn = "9788327152305",
                    Title = "Człowiek nietoperz",
                    Author = "Jo Nesbo",
                    ReleaseYear = 2014
                }
            };
            service.AddBookState(bookStateToAdd);
            int afterSize = context.bookStates.Count;
            var afterLastBookState = context.bookStates.Last();
            
            // check sizes
            Assert.AreNotEqual(beforeSize, afterSize);
         
            // check if last books aren't equal
            Assert.AreNotEqual(beforeLastBookState, afterLastBookState);
         
            // check if the book is in the list
            Assert.IsTrue(context.bookStates.Contains(bookStateToAdd));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddBookStateTestException()
        {
            var bookStateToAdd = repository.GetAllBookStates().ElementAt(0);
            service.AddBookState(bookStateToAdd);
        }

        [TestMethod()]
        public void AddEventTest()
        {
            int beforeSize = context.events.Count;
            var beforeLastEvent = context.events.Last();
            var bookState = repository.GetAllBookStates().ElementAt(0);
            var bookReader = repository.GetAllBookReaders().ElementAt(0);

            service.AddEvent(bookReader, bookState);
            int afterSize = context.events.Count;
            var afterLastEvent = context.events.Last();
            
            // check sizes
            Assert.AreNotEqual(beforeSize, afterSize);
            
            // check if last Events aren't equal
            Assert.IsFalse(object.ReferenceEquals(beforeLastEvent, afterLastEvent));
        }

        // test for wrong BookState
        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddEventTestException1()
        {
            var bookState = new BookState()
            {
                DateOfPurchase = new DateTimeOffset(year: 2018, month: 1, day: 24, hour: 15, minute: 29, second: 00, offset: new TimeSpan(1, 0, 0)),
                Book = new Book()
                {
                    Isbn = "0000",
                    Title = "test2",
                    Author = "testowy",
                    ReleaseYear = 2018
                }
            };
            var bookReader = context.bookReaders[0];
            service.AddEvent(bookReader, bookState);
        }

        // test for wrong BookReader
        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddEventTestException2()
        {
            var bookState = context.bookStates[0];
            var bookReader = new BookReader()
            {
                Age = 99,
                FirstName = "Tester",
                LastName = "Kodu",
                Telephone = "123456789"
            };
            service.AddEvent(bookReader, bookState);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddEventTestException3()
        {
            var bookStateToTest = repository.GetAllBookStates().ElementAt(0);
            var bookReaderToTest = repository.GetAllBookReaders().ElementAt(0);
            var bookReaderToTest1 = repository.GetAllBookReaders().ElementAt(1);
            service.AddEvent(bookReaderToTest, bookStateToTest);
            
            // because book is already rented new event with this book cannot be added
            service.AddEvent(bookReaderToTest1, bookStateToTest);
        }


        [TestMethod()]
        public void DeleteBookTest()
        {
            var book = repository.GetBook("9788380751026");
            Assert.IsTrue(context.books.ContainsKey("9788380751026"));
            service.DeleteBook(book);
            Assert.IsFalse(context.books.ContainsKey("9788380751026"));
            Assert.IsFalse(context.books.ContainsValue(book));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteBookTestException()
        {
            Book testBook = new Book()
            {
                Isbn = "0000",
                Title = "test2",
                Author = "testowy",
                ReleaseYear = 2018
            };

            service.DeleteBook(testBook);
        }

        [TestMethod()]
        public void DeleteBookReaderTest()
        {
            int bookReaderIndex = new Random().Next(0, context.bookReaders.Count);
            var bookReader = context.bookReaders[bookReaderIndex];
            Assert.IsTrue(context.bookReaders.Contains(bookReader));
            service.DeleteBookReader(bookReader);
            Assert.IsFalse(context.bookReaders.Contains(bookReader));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteBookReaderTestException()
        {
            var bookReaderToDelete = new BookReader()
            {
                Age = 99,
                FirstName = "Testowy",
                LastName = "Testowy",
                Telephone = "123456789"
            };
            service.DeleteBookReader(bookReaderToDelete);
        }

        [TestMethod()]
        public void DeleteBookStateTest()
        {
            int bookStateIndex = new Random().Next(0, context.bookStates.Count);
            var bookState = context.bookStates[bookStateIndex];
            Assert.IsTrue(context.bookStates.Contains(bookState));
            service.DeleteBookState(bookState);
            Assert.IsFalse(context.bookStates.Contains(bookState));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteBookStateTestException()
        {
            var bookStateToDelete = new BookState()
            {
                DateOfPurchase = new DateTimeOffset(year: 2018, month: 1, day: 24, hour: 15, minute: 29, second: 00, offset: new TimeSpan(1, 0, 0)),
                Book = new Book()
                {
                    Isbn = "0000",
                    Title = "test2",
                    Author = "testowy",
                    ReleaseYear = 2018
                }
            };
            service.DeleteBookState(bookStateToDelete);
        }

        [TestMethod()]
        public void DeleteEventTest()
        {
            int eventIndex = new Random().Next(0, context.events.Count);
            var event1 = context.events[eventIndex];
            Assert.IsTrue(context.events.Contains(event1));
            service.DeleteEvent(event1);
            Assert.IsFalse(context.events.Contains(event1));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteEventTestException()
        {
            var eventToDelete = new Event()
            {
                BookState = new BookState()
                {
                    Available = true,
                    Book = new Book()
                    {
                        Isbn = "1",
                        Title = "Tytuł",
                        Author = "Autor",
                        ReleaseYear = 2018
                    },
                    DateOfPurchase = DateTimeOffset.Now
                },
                BookReader = new BookReader()
                {
                    Age = 50,
                    FirstName = "Tester",
                    LastName = "Testowy",
                    Telephone = new Random().Next(1000000, 9999999).ToString()
                },
                BorrowDate = DateTimeOffset.Now
            };
            service.DeleteEvent(eventToDelete);
        }

        [TestMethod()]
        public void EventsForBookReaderTest()
        {
            BookReader bookReader = context.bookReaders[0];
            var eventsExpected = new List<Event>();
            foreach (Event ev in context.events)
            {
                if (ev.BookReader == bookReader) eventsExpected.Add(ev);
            }
            var eventsActual = service.EventsForBookReader(bookReader).ToList();
           
            // check sizes of both lists
            Assert.AreEqual(eventsExpected.Count, eventsActual.Count);
          
            // check if two lists contain the same objects
            foreach (Event ev in eventsExpected)
            {
                Assert.IsTrue(eventsActual.Contains(ev));
            }
        }

        [TestMethod()]
        public void EventsBetweenDatesTest()
        {
            var eventsExpected = new List<Event>();
            DateTimeOffset startDate = new DateTimeOffset(2017, 7, 1, 12, 0, 0, new TimeSpan(1, 0, 0));
            DateTimeOffset endDate = new DateTimeOffset(2017, 9, 1, 12, 0, 0, new TimeSpan(1, 0, 0));
            foreach (Event ev in context.events)
            {
                if (ev.BorrowDate > startDate && ev.ReturnDate < endDate)
                {
                    eventsExpected.Add(ev);
                }
            }
            var eventsActual = service.EventsBetweenDates(startDate, endDate).ToList();
          
            // check sizes of both lists
            Assert.AreEqual(eventsExpected.Count, eventsActual.Count);
          
            // check if two lists contain the same objects
            foreach (Event ev in eventsExpected)
            {
                Assert.IsTrue(eventsActual.Contains(ev));
            }
        }
    }
}