using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace czesc1
{
    [DataContract()]
    [Serializable]
    public class DataContext
    {
        [DataMember()]
        public List<BookReader> bookReaders;

        [DataMember()]
        public Dictionary<string, Book> books;

        [DataMember()]
        public ObservableCollection<Event> events;

        [DataMember()]
        public List<BookState> bookStates;

        public DataContext()
        {
            bookReaders = new List<BookReader>();
            books = new Dictionary<string, Book>();
            events = new ObservableCollection<Event>();
            bookStates = new List<BookState>();

            // initialize event handlers for ObservableCollection
            events.CollectionChanged += (sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    Console.WriteLine("Dodano nowe wypożyczenie");
                    foreach (Event ev in e.NewItems)
                    {
                        Console.WriteLine(ev);
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    Console.WriteLine("Usunięto wypożyczenie: ");
                    foreach (Event ev in e.OldItems)
                    {
                        Console.WriteLine(ev);
                    }
                }
            };
        }
    }
}
