using System.IO;
using System.Linq;
using czesc1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace czesc1.Tests
{
    [TestClass()]
    public class OurSerializerTests
    {

        private RandomDataFiller dataFiller;
        private DataContext context;
        private string fileName;
        private OurSerializer ourSerializer;

        [TestInitialize]
        public void TestInitialize()
        {
            dataFiller = new RandomDataFiller()
            {
                NumberOfBooks = 1000,
                NumberOfBookStates = 1000,
                NumberOfBookReaders = 1000,
                NumberOfEvents = 1000
            };
            context = new DataContext();
            dataFiller.Fill(ref context);
            fileName = "ourSerializer_data.txt";
            ourSerializer = new OurSerializer()
            {
                FileName = fileName
            };
        }

        [TestMethod()]
        public void SerializeTest()
        {
            ourSerializer.Serialize(context);
            Assert.IsTrue(File.Exists(fileName));
        }

        [TestMethod()]
        public void DeserializeTest()
        {
            // first delete the file if it already exists
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // serialize
            ourSerializer.Serialize(context);

            var newContext = new DataContext();

            ourSerializer.Deserialize(ref newContext);

            // check collection sizes
            Assert.AreEqual(context.bookReaders.Count, newContext.bookReaders.Count);
            Assert.AreEqual(context.bookStates.Count, newContext.bookStates.Count);
            Assert.AreEqual(context.books.Count, newContext.books.Count);
            Assert.AreEqual(context.events.Count, newContext.events.Count);

            // check if collections contain objects with the same properties
            foreach (var book in context.books.Values)
            {
                Assert.IsTrue(newContext.books.ContainsValue(book));
            }

            foreach (var bookState in context.bookStates)
            {
                Assert.IsTrue(newContext.bookStates.Contains(bookState));
            }

            foreach (var bookReader in context.bookReaders)
            {
                Assert.IsTrue(newContext.bookReaders.Contains(bookReader));
            }

            foreach (var e in context.events)
            {
                Assert.IsTrue(newContext.events.Contains(e));
            }

            // check book references in book states
            foreach (var book in newContext.books.Values)
            {
                var bookStates = newContext.bookStates.FindAll(bs => bs.Book.Equals(book));

                if (bookStates.Count > 0)
                {
                    foreach (var bookState in bookStates)
                    {
                        Assert.IsTrue(object.ReferenceEquals(book, bookState.Book));
                    }
                }
            }

            // check book state references in events
            foreach (var bookState in newContext.bookStates)
            {
                var events = newContext.events.Where(e => e.BookState.Equals(bookState)).ToList();

                if (events.Count > 0)
                {
                    foreach (var ev in events)
                    {
                        Assert.IsTrue(object.ReferenceEquals(bookState, ev.BookState));
                    }
                }
            }

            // check book reader references in events
            foreach (var bookReader in newContext.bookReaders)
            {
                var events = newContext.events.Where(e => e.BookReader.Equals(bookReader)).ToList();

                if (events.Count > 0)
                {
                    foreach (var ev in events)
                    {
                        Assert.IsTrue(object.ReferenceEquals(bookReader, ev.BookReader));
                    }
                }
            }
        }
    }
}