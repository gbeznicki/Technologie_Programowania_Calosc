using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Library
{
    [DataContract()]
    [Serializable]
    public class BookState
    {
        private DateTimeOffset dateOfPurchase;
        private Book book;
        private bool available;

        [DataMember()]
        public DateTimeOffset DateOfPurchase
        {
            get => dateOfPurchase;
            set => dateOfPurchase = value;
        }

        [DataMember()]
        public Book Book
        {
            get => book;
            set => book = value;
        }

        [DataMember()]
        public bool Available
        {
            get => available;
            set => available = value;
        }

        public override string ToString()
        {
            string s = book + "\nData zakupu " + dateOfPurchase + "\n";
            return s;
        }

        public override bool Equals(object obj)
        {
            if (obj is BookState)
            {
                var otherBookState = (BookState)obj;
                return dateOfPurchase.Equals(otherBookState.dateOfPurchase) && book.Equals(otherBookState.book) && available.Equals(otherBookState.available);
            }
            else
            {
                return true;
            }
        }

        public override int GetHashCode()
        {
            var hashCode = 1998070871;
            hashCode = hashCode * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(dateOfPurchase);
            hashCode = hashCode * -1521134295 + EqualityComparer<Book>.Default.GetHashCode(book);
            hashCode = hashCode * -1521134295 + available.GetHashCode();
            return hashCode;
        }
    }
}
