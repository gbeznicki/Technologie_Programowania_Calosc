using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Library;


//program obslugujacy biblioteke
namespace czesc1
{
    class Program
    {
        static void Main(string[] args)
        {
            //DataFiller dataFiller = new XmlDataFiller()
            //{
            //    FileName = "../../../context.xml"
            //};
            DataFiller dataFiller = new RandomDataFiller()
            {
                NumberOfBooks = 10,
                NumberOfBookStates = 10,
                NumberOfBookReaders = 10,
                NumberOfEvents = 10
            }
            ;
            DataContext context = new DataContext();
            DataRepository repository = new DataRepository(dataFiller)
            {
                Data = context
            };
            repository.Fill();
            DataService service = new DataService(repository);

            //Console.WriteLine(service.PrintAllBinded());
            //var books = repository.GetAllBooks();
            //Console.WriteLine(service.PrintBooks(books));
            Console.WriteLine(service.PrintAllBinded());

            // test dla ObservableCollection
            //repository.AddEvent(new Event());

            //repository.DeleteEvent(repository.GetAllEvents().Last());


            // zapisz context do pliku xml

            //FileStream writer = new FileStream("context.xml", FileMode.Create);
            //var xmlWriterSettings = new XmlWriterSettings()
            //{
            //    Indent = true,
            //    IndentChars = "\t"
            //};
            //var xmlWriter = XmlWriter.Create(writer, xmlWriterSettings);

            FileStream writer = new FileStream("context.xml", FileMode.Create);

            //dodanie typów, które ma rozpoznać DataContractSerializer
            List<Type> types = new List<Type> {typeof(Event), typeof(Book), typeof(BookReader), typeof(BookState)};
            var dataContractSerializer = new DataContractSerializer(typeof(DataContext), types, 0x7FFF, false, true, null);
            dataContractSerializer.WriteObject(writer, context);

            writer.Flush();
            writer.Close();
        }
    }
}
