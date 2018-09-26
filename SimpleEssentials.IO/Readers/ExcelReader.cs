using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SimpleEssentials.IO.Readers
{
    public class ExcelReader : IFileReader
    {
        public T Read<T>(string filePath) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> ReadAll<T>(string filePath) where T : class, new()
        {
            var rawData = GetDataTableFromExcel(filePath, true);
            return rawData == null ? null : DataTableToList<T>(rawData);
        }

        private static DataTable GetDataTableFromExcel(string path, bool hasHeader)
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = System.IO.File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets.First();
                var tbl = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    tbl.Columns.Add(hasHeader
                        ? firstRowCell.Text
                        : $"Column {firstRowCell.Start.Column}");
                }
                var startRow = hasHeader ? 2 : 1;
                for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    var row = tbl.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
                return tbl;
            }
        }

        public IEnumerable<T> DataTableToList<T>(DataTable table) where T : class, new()
        {
            try
            {
                var list = new List<T>();
                foreach (DataRow row in table.Rows)
                {
                    var obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            var propertyInfo = obj.GetType().GetProperty(prop.Name);
                            if (propertyInfo != null)
                            {
                                var t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                                var safeValue = (row[prop.Name] == null) ? null : Convert.ChangeType(row[prop.Name], t);

                                propertyInfo.SetValue(obj, safeValue, null);
                            }
                        }
                        catch
                        {
                            // ignored
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
    }
}
