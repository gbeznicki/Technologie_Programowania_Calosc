using Microsoft.VisualStudio.TestTools.UnitTesting;
using czesc1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using Library;
using System.Runtime.Serialization;

namespace czesc1.Tests
{
    [TestClass()]
    public class XmlDataFillerTests
    {
        [TestMethod()]
        public void FillTest()
        {
            // maximum time difference in milliseconds
            int maxTimeDifference = 1000;

            // name of the xml file
            string fileName = "testContext.xml";

            // creating contexts and fillers
            DataContext initialContext = new DataContext();
            DataContext deserialisedContext = new DataContext();
            RandomDataFiller randomFiller = new RandomDataFiller()
            {
                NumberOfBooks = 100,
                NumberOfBookStates = 100,
                NumberOfBookReaders = 100,
                NumberOfEvents = 100
            };
            XmlDataFiller xmlFiller = new XmlDataFiller()
            {
                FileName = fileName
            };

            // fill context using RandomDataFiller
            randomFiller.Fill(ref initialContext);

            // save the context to xml file
            FileStream writer = new FileStream(fileName, FileMode.Create);
            List<Type> types = new List<Type>
            {
                typeof(Event),
                typeof(Book),
                typeof(BookReader),
                typeof(BookState)
            };
            var dataContractSerializer = new DataContractSerializer(typeof(DataContext), types, Int32.MaxValue, false, true, null);
            dataContractSerializer.WriteObject(writer, initialContext);
            writer.Flush();
            writer.Close();

            // get time value just before beginning of filling context
            DateTimeOffset begin = DateTimeOffset.Now;

            // fill another context using XmlDataFiller
            xmlFiller.Fill(ref deserialisedContext);

            // get time value right after finishing filling
            DateTimeOffset end = DateTimeOffset.Now;

            // calc time difference in milliseconds
            int difference = end.Subtract(begin).Milliseconds;

            // check if context has been filled quickly enough
            Assert.IsTrue(difference < maxTimeDifference);

            // check if all data has been read from xml file properly
            var actualBooks = deserialisedContext.books;
            var actualBookReaders = deserialisedContext.bookReaders;
            var actualBookStates = deserialisedContext.bookStates;
            var actualEvents = deserialisedContext.events;

            var expectedBooks = initialContext.books;
            var expectedBookReaders = initialContext.bookReaders;
            var expectedBookStates = initialContext.bookStates;
            var expectedEvents = initialContext.events;

            // check books
            foreach (var book in actualBooks)
            {
                Assert.IsTrue(expectedBooks.Contains(book));
            }

            // check book states
            foreach (var bookState in actualBookStates)
            {
                Assert.IsTrue(expectedBookStates.Contains(bookState));
            }

            // check book readers
            foreach (var bookReaders in actualBookReaders)
            {
                Assert.IsTrue(expectedBookReaders.Contains(bookReaders));
            }

            // check events
            foreach (var e in actualEvents)
            {
                Assert.IsTrue(expectedEvents.Contains(e));
            }

        }
    }
}