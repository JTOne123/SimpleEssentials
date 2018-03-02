using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEssentials.IO.Writers
{
    public class CsvWriter : IFileWriter
    {
        
        public void Write<T>(string filePath, T obj, bool append)
        {
            //using (var streamWriter = new StreamWriter(filePath, append))
            //{
            //    var csvWriter = new CsvHelper.CsvWriter(streamWriter);
            //    csvWriter.WriteRecord(obj);
            //}
        }

        public void Write<T>(string filePath, IEnumerable<T> obj, bool append)
        {
            //using (var streamWriter = new StreamWriter(filePath, append))
            //{
            //    var csvWriter = new CsvHelper.CsvWriter(streamWriter);
            //    csvWriter.WriteRecords(obj);
            //}
        }
    }
}
