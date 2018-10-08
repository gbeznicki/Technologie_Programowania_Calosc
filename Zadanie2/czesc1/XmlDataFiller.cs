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

namespace czesc1
{
    public class XmlDataFiller : DataFiller
    {
        private string fileName;

        public string FileName
        {
            get => fileName;
            set => fileName = value;
        }

        public override void Fill(ref DataContext context)
        {
            var fileReader = new FileStream(fileName, FileMode.Open);
            var xmlReader = XmlDictionaryReader.CreateTextReader(fileReader, new XmlDictionaryReaderQuotas());
            var serializer = new DataContractSerializer(typeof(DataContext));

            // deserialize DataContext from XML file
            context = (DataContext) serializer.ReadObject(xmlReader, true);
           
            // close streams
            xmlReader.Close();
            fileReader.Close();
        }
    }
}
