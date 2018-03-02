using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEssentials.IO.Readers
{
    public class CsvReader : IFileReader
    {
        public T Read<T>(string filePath)
        {
            
            throw new NotImplementedException();
        }

        public IEnumerable<T> ReadAll<T>(string filePath)
        {
            IEnumerable<T> records = null;
            //using (System.IO.TextReader fileReader = System.IO.File.OpenText(filePath))
            //{
            //    var csv = new CsvHelper.CsvReader(fileReader);
            //    records = csv.GetRecords<T>().ToList();
            //}
        
            return records;
        }
    }
}
