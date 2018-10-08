using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace czesc1
{
    public class BinaryDataSerializer : IDataSerializer
    {
        private string fileName;

        public string FileName { get => fileName; set => fileName = value; }

        public void Serialize(DataContext context)
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, context);
                stream.Flush();
            }
        }

        public void Deserialize(ref DataContext context)
        {
            IFormatter formatter = new BinaryFormatter();

            // check if file exists
            if (File.Exists(fileName))
            {
                // clean context
                context = null;

                using (Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    context = (DataContext)formatter.Deserialize(stream);
                }
            }
            else
            {
                throw new FileNotFoundException("Nie znaleziono pliku o żądanej nazwie");
            }
        }
    }
}
