using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Library
{
    [DataContract()]
    [Serializable]
    public class Event
    {
        private BookState bookState;
        private BookReader bookReader;
        private DateTimeOffset borrowDate;
        private DateTimeOffset? returnDate;

        [DataMember()]
        public BookState BookState
        {
            get => bookState;
            set => bookState = value;
        }

        [DataMember()]
        public BookReader BookReader
        {
            get => bookReader;
            set => bookReader = value;
        }

        [DataMember()]
        public DateTimeOffset BorrowDate {
            get => borrowDate;
            set => borrowDate = value;
        }

        [DataMember()]
        public DateTimeOffset? ReturnDate {
            get => returnDate;
            set => returnDate = value;
        }

        public override string ToString()
        {
            string s = "Wypożyczone przez:\n" + bookReader + "\n" + bookState + "\nData wypożyczenia " + borrowDate + "\nData zwrotu " + returnDate;
            return s;
        }

        public override bool Equals(object obj)
        {
            if (obj is Event)
            {
                var otherEvent = (Event)obj;
                return bookState.Equals(otherEvent.bookState) && bookReader.Equals(otherEvent.bookReader) && borrowDate.Equals(otherEvent.borrowDate) && returnDate.Equals(otherEvent.returnDate);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            var hashCode = 1523615183;
            hashCode = hashCode * -1521134295 + EqualityComparer<BookState>.Default.GetHashCode(bookState);
            hashCode = hashCode * -1521134295 + EqualityComparer<BookReader>.Default.GetHashCode(bookReader);
            hashCode = hashCode * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(borrowDate);
            hashCode = hashCode * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(returnDate);
            return hashCode;
        }
    }
}
