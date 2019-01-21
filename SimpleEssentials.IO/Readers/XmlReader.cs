using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleEssentials.IO.Readers
{
    public class XmlReader : IFileReader
    {
        public T Read<T>(string filePath, Dictionary<string, string> metaData = null) where T : class, new()
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (System.IO.TextReader fileReader = System.IO.File.OpenText(filePath))
            {
                return (T)ser.Deserialize(fileReader);
            }
        }

        public IEnumerable<T> ReadToList<T>(string filePath, Dictionary<string, string> metaData = null) where T : class, new()
        {
            throw new NotImplementedException();
        }
    }
}
