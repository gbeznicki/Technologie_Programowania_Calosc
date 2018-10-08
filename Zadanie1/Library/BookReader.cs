using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Library
{
    [DataContract()]
    [Serializable]
    public class BookReader
    {
        private int age;
        private string firstName;
        private string lastName;
        private string telephone;

        [DataMember()]
        public int Age
        {
            get => age;
            set => age = value;
        }

        [DataMember()]
        public string FirstName
        {
            get => firstName;
            set => firstName = value;
        }

        [DataMember()]
        public string LastName
        {
            get => lastName;
            set => lastName = value;
        }

        [DataMember()]
        public string Telephone
        {
            get => telephone;
            set => telephone = value;
        }

        public override string ToString()
        {
            string s = "Czytelnik " + firstName + " " + lastName + ", wiek " + age + ", numer telefonu " + telephone + "\n";
            return s;
        }

        public override bool Equals(object obj)
        {
            if (obj is BookReader)
            {
                var otherBookReader = (BookReader)obj;
                return age.Equals(otherBookReader.age) && firstName.Equals(otherBookReader.firstName) && lastName.Equals(otherBookReader.lastName) && telephone.Equals(otherBookReader.telephone);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            var hashCode = -1877943345;
            hashCode = hashCode * -1521134295 + age.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(firstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(lastName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(telephone);
            return hashCode;
        }
    }
}
