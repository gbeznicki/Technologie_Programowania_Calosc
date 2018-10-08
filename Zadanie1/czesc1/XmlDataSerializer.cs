using Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace czesc1
{
    public class XmlDataSerializer : IDataSerializer
    {
        private string fileName;

        public string FileName { get => fileName; set => fileName = value; }

        public void Deserialize(ref DataContext context)
        {
            // check if file exists
            if (File.Exists(fileName))
            {
                // clean context
                context = null;

                using (var stream = new FileStream(fileName, FileMode.Open))
                using (var xmlDictionary = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas()))
                {
                    var xmlDeserializer = new DataContractSerializer(typeof(DataContext));
                    context = (DataContext)xmlDeserializer.ReadObject(xmlDictionary, true);
                }
            }
            else
            {
                throw new FileNotFoundException("Nie znaleziono pliku o żądanej nazwie");
            }
        }

        public void Serialize(DataContext context)
        {
            List<Type> types = new List<Type>
            {
                typeof(Event),
                typeof(Book),
                typeof(BookReader),
                typeof(BookState)
            };

            var xmlSerializer = new DataContractSerializer(typeof(DataContext), types, Int32.MaxValue, false, true, null);
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                xmlSerializer.WriteObject(stream, context);
            }
        }
    }
}
