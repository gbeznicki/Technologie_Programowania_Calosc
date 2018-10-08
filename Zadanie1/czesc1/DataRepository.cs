using Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace czesc1
{
    public class DataRepository
    {
        public DataRepository(DataFiller dataFiller)
        {
            this.filler = dataFiller;
        }

        private DataFiller filler;
        private DataContext data;

        public DataContext Data
        {
            set => data = value;
        }

        public void Fill()
        {
            filler.Fill(ref data);
        }

        // CRUD methods for book ****************************************
        public void AddBook(Book book)
        {
            data.books.Add(book.Isbn, book);
        }

        public Book GetBook(string isbn)
        {
            return data.books[isbn];
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return data.books.Values;
        }

        public void UpdateBook(Book oldBook, Book newBook)
        {
            var isbn = oldBook.Isbn;

            oldBook.Author = newBook.Author;
            oldBook.Isbn = newBook.Isbn;
            oldBook.ReleaseYear = newBook.ReleaseYear;
            oldBook.Title = newBook.Title;

            data.books.Remove(isbn);
            data.books.Add(oldBook.Isbn, oldBook);
        }

        public void DeleteBook(Book book)
        {
            data.books.Remove(book.Isbn);
        }

        // CRUD methods for book reader **********************************************
        public void AddBookReader(BookReader bookReader)
        {
            data.bookReaders.Add(bookReader);
        }

        public BookReader GetBookReader(int index)
        {
            return data.bookReaders[index];
        }

        public IEnumerable<BookReader> GetAllBookReaders()
        {
            return data.bookReaders;
        }

        public void UpdateBookReader(BookReader oldBookReader, BookReader newBookReader)
        {
            oldBookReader.Age = newBookReader.Age;
            oldBookReader.FirstName = newBookReader.FirstName;
            oldBookReader.LastName = newBookReader.LastName;
            oldBookReader.Telephone = newBookReader.Telephone;
        }

        public void DeleteBookReader(BookReader bookReader)
        {
            data.bookReaders.Remove(bookReader);
        }

        //CRUD methods for Book State *******************************************************
        public void AddBookState(BookState bookState)
        {
            data.bookStates.Add(bookState);
        }

        public BookState GetBookState(int index)
        {
            return data.bookStates[index];
        }

        public IEnumerable<BookState> GetAllBookStates()
        {
            return data.bookStates;
        }

        public void UpdateBookState(BookState oldBookState, BookState newBookState)
        {
            oldBookState.Available = newBookState.Available;
            oldBookState.Book = newBookState.Book;
            oldBookState.DateOfPurchase = newBookState.DateOfPurchase;
        }

        public void DeleteBookState(BookState bookState)
        {
            data.bookStates.Remove(bookState);
        }

        //CRUD methods for Events *******************************************************
        public void AddEvent(Event event1)
        {
            data.events.Add(event1);
        }

        public Event GetEvent(int index)
        {
            return data.events[index];
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return data.events;
        }

        public void UpdateEvents(Event oldEvent, Event newEvent)
        {
            oldEvent.BookReader = newEvent.BookReader;
            oldEvent.BookState = newEvent.BookState;
            oldEvent.BorrowDate = newEvent.BorrowDate;
            oldEvent.ReturnDate = newEvent.ReturnDate;
        }

        public void DeleteEvent(Event e)
        {
            data.events.Remove(e);
        }
    }
}
