using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace czesc1
{
    public class JSONSerializer : IDataSerializer
    {
        private string fileName;

        public string FileName
        {
            get => fileName;
            set => fileName = value;
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

            using (StreamWriter writer = new StreamWriter(fileName))
            using (JsonTextWriter jsonTextWriter = new JsonTextWriter(writer))
            {
                JsonSerializer serializer = new JsonSerializer()
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    Formatting = Formatting.Indented
                };
                serializer.Serialize(jsonTextWriter, context);
            }
        }

        public void Deserialize(ref DataContext context)
        {
            using (StreamReader reader = new StreamReader(fileName))
            using (JsonTextReader jsonReader = new JsonTextReader(reader))
            {
                JsonSerializer serializer = new JsonSerializer();
                context = (DataContext)serializer.Deserialize(jsonReader, typeof(DataContext));
            }
        }
    }
}