using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace czesc1
{
    public class OurSerializer : IDataSerializer
    {
        private string fileName;

        public string FileName
        {
            get => fileName;
            set => fileName = value;
        }

        public void Serialize(DataContext context)
        {
            var idGenerator = new ObjectIDGenerator();

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                //serializacja książek
                /*
                Author
                ISBN
                ReleaseYear
                Title
                 */
                foreach (var iteratorBook in context.books)
                {
                    var firstTime = false;
                    var bookId = idGenerator.GetId(iteratorBook.Value, out firstTime);
                    string toSerialize = bookId + ";" + iteratorBook.Value.Author + ";" + iteratorBook.Value.Isbn +
                                         ";" +
                                         iteratorBook.Value.ReleaseYear + ";" + iteratorBook.Value.Title;
                    writer.WriteLine(toSerialize);
                }
                writer.WriteLine("@");
                //serializacja czytelników
                /*
                Age
                FirstName
                LastName
                Telephone
                 */
                foreach (var iteratorBookReader in context.bookReaders)
                {
                    var firstTime = false;
                    var bookReaderId = idGenerator.GetId(iteratorBookReader, out firstTime);
                    string toSerialize = bookReaderId + ";" + iteratorBookReader.Age + ";" + iteratorBookReader.FirstName +
                                         ";" +
                                        iteratorBookReader.LastName + ";" + iteratorBookReader.Telephone;

                    writer.WriteLine(toSerialize);
                }
                writer.WriteLine("@");
                //serializacja stanów
                /* Available
                BookId
                DateOfPurchase */
                foreach (var iteratorBookState in context.bookStates)
                {
                    var firstTime = false;
                    var bookStateId = idGenerator.GetId(iteratorBookState, out firstTime);
                    var bookFromBookStateId = idGenerator.GetId(iteratorBookState.Book, out firstTime);
                    string toSerialize = bookStateId + ";" + iteratorBookState.Available + ";" + bookFromBookStateId +
                                         ";" + iteratorBookState.DateOfPurchase;
                    writer.WriteLine(toSerialize);
                }
                writer.WriteLine("@");
                //serializacja zdarzeń
                /* BookReaderId
                 BookStateId
                 BorrowDate
                 ReturnDate*/
                foreach (var iteratorEvent in context.events)
                {
                    var firstTime = false;
                    var bookEventId = idGenerator.GetId(iteratorEvent, out firstTime);
                    var bookStateFromEventId = idGenerator.GetId(iteratorEvent.BookState, out firstTime);
                    var bookReaderFromEventId = idGenerator.GetId(iteratorEvent.BookReader, out firstTime);
                    string toSerialize = bookEventId + ";" + bookReaderFromEventId + ";" + bookStateFromEventId + ";" +
                                         iteratorEvent.BorrowDate + ";" + iteratorEvent.ReturnDate;
                    writer.WriteLine(toSerialize);
                }
                writer.WriteLine("@");
            }
        }

        public void Deserialize(ref DataContext context)
        {
            Dictionary<string, Book> tempBookDictionary = new Dictionary<string, Book>();
            Dictionary<string, Book> tempBookDictionaryToContext = new Dictionary<string, Book>();
            Dictionary<string, BookReader> tempBookReadersDictionary = new Dictionary<string, BookReader>();
            List<BookReader> tempBookReadersListToContext = new List<BookReader>();
            Dictionary<string, BookState> tempBookStatesDictionary = new Dictionary<string, BookState>();
            List<BookState> tempBookStatesListToContext = new List<BookState>();
            Dictionary<string, Event> tempEventDictionary = new Dictionary<string, Event>();
            ObservableCollection<Event> tempEventObservableCollectionToContext = new ObservableCollection<Event>();

            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while (!(line = reader.ReadLine()).StartsWith("@"))
                {
                    var bookProperties = line.Split(';');
                    var bookId = bookProperties[0];
                    var author = bookProperties[1];
                    var isbn = bookProperties[2];
                    var releaseYear = bookProperties[3];
                    var title = bookProperties[4];

                    if (tempBookDictionary.ContainsKey(bookId))
                    {
                        var deserializedBook = tempBookDictionary[bookId];
                        tempBookDictionaryToContext.Add(deserializedBook.Isbn, deserializedBook);
                    }
                    else
                    {
                        var deserializedBook = new Book()
                        {
                            Author = author,
                            Isbn = isbn,
                            ReleaseYear = Convert.ToInt32(releaseYear),
                            Title = title
                        };
                        tempBookDictionary.Add(bookId, deserializedBook);
                        tempBookDictionaryToContext.Add(deserializedBook.Isbn, deserializedBook);
                    }
                }

                while (!(line = reader.ReadLine()).StartsWith("@"))
                {
                    var bookReaderProperties = line.Split(';');
                    var bookReaderId = bookReaderProperties[0];
                    var age = bookReaderProperties[1];
                    var firstName = bookReaderProperties[2];
                    var lastName = bookReaderProperties[3];
                    var telephone = bookReaderProperties[4];

                    if (tempBookReadersDictionary.ContainsKey(bookReaderId))
                    {
                        var deserializedBookReader = tempBookReadersDictionary[bookReaderId];
                        tempBookReadersListToContext.Add(deserializedBookReader);
                    }
                    else
                    {
                        var deserializedBookReader = new BookReader()
                        {
                            Age = Convert.ToInt32(age),
                            FirstName = firstName,
                            LastName = lastName,
                            Telephone = telephone
                        };
                        tempBookReadersDictionary.Add(bookReaderId, deserializedBookReader);
                        tempBookReadersListToContext.Add(deserializedBookReader);
                    }
                }

                while (!(line = reader.ReadLine()).StartsWith("@"))
                {
                    var bookStateProperties = line.Split(';');
                    var bookStateId = bookStateProperties[0];
                    var available = bookStateProperties[1];
                    var bookId = bookStateProperties[2];
                    var dateOfPurchase = bookStateProperties[3];

                    if (tempBookStatesDictionary.ContainsKey(bookStateId))
                    {
                        var deserializedBookState = tempBookStatesDictionary[bookStateId];
                        tempBookStatesListToContext.Add(deserializedBookState);
                    }
                    else
                    {
                        var deserializedBookState = new BookState()
                        {
                            Available = Convert.ToBoolean(available),
                            Book = tempBookDictionary[bookId],
                            DateOfPurchase = Convert.ToDateTime(dateOfPurchase)
                        };
                        tempBookStatesDictionary.Add(bookStateId, deserializedBookState);
                        tempBookStatesListToContext.Add(deserializedBookState);
                    }
                }

                while (!(line = reader.ReadLine()).StartsWith("@"))
                {
                    var eventProperties = line.Split(';');
                    var eventId = eventProperties[0];
                    var bookReaderId = eventProperties[1];
                    var bookStateId = eventProperties[2];
                    var borrowDate = eventProperties[3];
                    var returnDate = eventProperties[4];

                    if (tempEventDictionary.ContainsKey(eventId))
                    {
                        var deserializedEvent = tempEventDictionary[eventId];
                        tempEventObservableCollectionToContext.Add(deserializedEvent);
                    }
                    else
                    {
                        if (returnDate.Length <= 0)
                        {
                            var deserializedEvent = new Event()
                            {
                                BookReader = tempBookReadersDictionary[bookReaderId],
                                BookState = tempBookStatesDictionary[bookStateId],
                                BorrowDate = Convert.ToDateTime(borrowDate),
                                ReturnDate = null
                            };
                            tempEventDictionary.Add(eventId, deserializedEvent);
                            tempEventObservableCollectionToContext.Add(deserializedEvent);
                        }
                        else
                        {
                            var deserializedEvent = new Event()
                            {
                                BookReader = tempBookReadersDictionary[bookReaderId],
                                BookState = tempBookStatesDictionary[bookStateId],
                                BorrowDate = Convert.ToDateTime(borrowDate),
                                ReturnDate = Convert.ToDateTime(returnDate)
                            };
                            tempEventDictionary.Add(eventId, deserializedEvent);
                            tempEventObservableCollectionToContext.Add(deserializedEvent);
                        }


                    }
                }

            }

            context.books = tempBookDictionaryToContext;
            context.bookReaders = tempBookReadersListToContext;
            context.bookStates = tempBookStatesListToContext;
            context.events = tempEventObservableCollectionToContext;

        }
    }
}
