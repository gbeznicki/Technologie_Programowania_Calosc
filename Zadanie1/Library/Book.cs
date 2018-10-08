using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Library
{
    [DataContract()]
    [Serializable]
    public class Book
    {
        private string isbn;
        private string title;
        private string author;
        private int releaseYear;

        [DataMember()]
        public string Isbn
        {
            get => isbn;
            set => isbn = value;
        }

        [DataMember()]
        public string Title
        {
            get => title;
            set => title = value;
        }

        [DataMember()]
        public string Author
        {
            get => author;
            set => author = value;
        }

        [DataMember()]
        public int ReleaseYear
        {
            get => releaseYear;
            set => releaseYear = value;
        }

        public override string ToString()
        {
            string s = "Książka: " + "ISBN " + isbn + "; tytuł " + title + "; autor " + author + "; rok wydania " + releaseYear+ "\n";
            return s;
        }

        public override bool Equals(object obj)
        {
            if (obj is Book)
            {
                var otherBook = (Book)obj;
                return isbn.Equals(otherBook.isbn) && title.Equals(otherBook.title) && author.Equals(otherBook.author) && releaseYear.Equals(otherBook.releaseYear);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            var hashCode = 939820747;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(isbn);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(author);
            hashCode = hashCode * -1521134295 + releaseYear.GetHashCode();
            return hashCode;
        }
    }
}
