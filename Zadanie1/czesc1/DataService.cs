using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace czesc1
{
    public class DataService
    {
        private DataRepository repository;

        public DataService(DataRepository repository)
        {
            this.repository = repository;
        }

        // print methods
        public string PrintBooks(IEnumerable<Book> books)
        {
            string s = "";
            foreach (Book book in books)
            {
                s += book + "\n";
            }
            return s;
        }

        public string PrintBookReaders(IEnumerable<BookReader> bookReaders)
        {
            string s = "";
            foreach (BookReader bookReader in bookReaders)
            {
                s += bookReader + "\n";
            }
            return s;
        }

        public string PrintBookStates(IEnumerable<BookState> bookStates)
        {
            string s = "";
            foreach (BookState bookState in bookStates)
            {
                s += bookState + "\n";
            }
            return s;
        }

        public string PrintEvents(IEnumerable<Event> events)
        {
            string s = "";
            foreach (Event e in events)
            {
                s += e + "\n";
            }
            return s;
        }

        public string PrintAllBinded()
        {
            string s = "";

            IEnumerable<Event> events = this.repository.GetAllEvents();
            IEnumerable<Book> books = this.repository.GetAllBooks();
            IEnumerable<BookState> bookStates = this.repository.GetAllBookStates();
            IEnumerable<BookReader> bookReaders = this.repository.GetAllBookReaders();

            foreach (BookReader bookReader in bookReaders)
            {
                s += bookReader;
                s += "\nWypożyczenia: \n\n";
                foreach(Event e in events)
                {
                    if(e.BookReader == bookReader)
                    {
                        s += e + "\n";
                    }
                }

                s += "\n************************\n";
            }

            return s;
        }

        // add methods
        public void AddBook(Book book)
        {
            // check if given book exists in repository
            if (!repository.GetAllBooks().Contains(book)) repository.AddBook(book);
            else throw new ArgumentException("Nie można dodać książki do repozytorium, książka już istnieje");
        }

        public void AddBookReader(BookReader bookReader)
        {
            // check if given book reader exists in repository
            if (!repository.GetAllBookReaders().Contains(bookReader)) repository.AddBookReader(bookReader);
            else throw new ArgumentException("Nie można dodać czytelnika do repozytirum, czytelnik już istnieje");
        }

        public void AddBookState(BookState bookState)
        {
            // check if given book state exists in repository
            if (!repository.GetAllBookStates().Contains(bookState)) repository.AddBookState(bookState);
            else throw new ArgumentException("Nie można dodać opisu książki do repozytirum, opis już istnieje");
        }

        public void AddEvent(BookReader bookReader, BookState bookState)
        {
            // check if the book is available && if bookReader exists
            if (bookState.Available && repository.GetAllBookReaders().Contains(bookReader))
            {
                Event e = new Event()
                {
                    BookReader = bookReader,
                    BookState = bookState,
                    BorrowDate = DateTimeOffset.Now
                };
                
                // set book state's avaiable value to false
                bookState.Available = false;
                repository.AddEvent(e);
            }
            else throw new InvalidOperationException("Nie można wypożyczyć podanej książki lub podany czytelnik nie istnieje");
        }

        // method that ends given event
        public void EndEvent(Event e)
        {
            // set return date to now
            e.ReturnDate = DateTimeOffset.Now;
            
            // set book state to true
            e.BookState.Available = true;
        }

        // delete methods
        public void DeleteBook(Book book)
        {
            // check if given book exists in repository
            if (repository.GetAllBooks().Contains(book)) repository.DeleteBook(book);
            else throw new ArgumentException("Nie można usunąć książki z repozytorium, książka nie istnieje");
        }

        public void DeleteBookReader(BookReader bookReader)
        {
            // check if given book reader exists in repository
            if (repository.GetAllBookReaders().Contains(bookReader)) repository.DeleteBookReader(bookReader);
            else throw new ArgumentException("Nie można usunąć czytelnika z repozytorium, czytelnik nie istnieje");
        }

        public void DeleteBookState(BookState bookState)
        {
            // check if given book state exists in repository
            if (repository.GetAllBookStates().Contains(bookState)) repository.DeleteBookState(bookState);
            else throw new ArgumentException("Nie można usunąć stanu książki z repozytorium, stan książki nie istnieje");
        }

        public void DeleteEvent(Event e)
        {
            // check if given event exists in the repository
            if (repository.GetAllEvents().Contains(e)) repository.DeleteEvent(e);
            else throw new ArgumentException("Nie można usunąć wypożyczenia z repozytorium, wypożyczenie nie istnieje");
        }

        // update methods
        public void UpdateBook(Book oldBook, Book newBook)
        {
            // check if old book exists in the repository
            if (repository.GetAllBooks().Contains(oldBook))
            {
                repository.UpdateBook(oldBook, newBook);
            }
            else throw new ArgumentException("Nie można zaktualizować książki, książka nie istnieje");
        }

        public void UpdateBookReader(BookReader oldBookReader, BookReader newBookReader)
        {
            // check if old book reader exists in the repository
            if (repository.GetAllBookReaders().Contains(oldBookReader))
            {
                repository.UpdateBookReader(oldBookReader, newBookReader);
            }
            else throw new ArgumentException("Nie można zaktualizować czytelnika, czytelnik nie istnieje");
        }

        public void UpdateBookState(BookState oldBookState, BookState newBookState)
        {
            // check if old book exists in the repository
            if (repository.GetAllBookStates().Contains(oldBookState))
            {
                repository.UpdateBookState(oldBookState, newBookState);
            }
            else throw new ArgumentException("Nie można zaktualizować stanu książki, stan książki nie istnieje");
        }

        public void UpdateEvent(Event oldEvent, Event newEvent)
        {
            // check if old book exists in the repository
            if (repository.GetAllEvents().Contains(oldEvent))
            {
                repository.UpdateEvents(oldEvent, newEvent);
            }
            else throw new ArgumentException("Nie można zaktualizować wypożyczenia, wypożyczenie nie istnieje");
        }

        // filter and search methods

        // get all events for specified book reader
        public IEnumerable<Event> EventsForBookReader(BookReader bookReader)
        {
            List<Event> events = new List<Event>();
            foreach (Event e in repository.GetAllEvents())
            {
                if (e.BookReader == bookReader) events.Add(e);
            }
            return events;
        }

        // get all events that started after beginDate and ended before endDate
        public IEnumerable<Event> EventsBetweenDates(DateTimeOffset beginDate, DateTimeOffset endDate)
        {
            List<Event> events = new List<Event>();
            foreach (Event e in repository.GetAllEvents())
            {
                if (e.BorrowDate > beginDate && e.ReturnDate < endDate) events.Add(e);
            }
            return events;
        }


    }
}
