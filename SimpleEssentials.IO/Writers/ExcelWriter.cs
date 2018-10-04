using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FastMember;
using OfficeOpenXml;

namespace SimpleEssentials.IO.Writers
{
    public class ExcelWriter : IFileWriter
    {
        public void Write<T>(string filePath, T obj, bool append)
        {
            if (typeof(IEnumerable).IsAssignableFrom(typeof(T)))
            {
                throw new Exception("Cast your list into IEnumerable<>");
            }
            byte[] bytes;

            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                var accessor = GetAccessor(obj);
                var properties = GetProperties(accessor).ToList();

                for (var i = 0; i < properties.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = properties[i].Name;
                }

                for (var j = 0; j < properties.Count; j++)
                {
                    worksheet.Cells[2, j + 1].Value = accessor[obj, properties[j].Name];
                }

                bytes = package.GetAsByteArray();
            }

            System.IO.File.WriteAllBytes(filePath, bytes);
        }

        public void Write<T>(string filePath, IEnumerable<T> obj, bool append)
        {
            byte[] bytes;

            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                var objList = obj as IList<T> ?? obj.ToList();
                var totalRows = objList.Count();

                var accessor = GetAccessor(objList.FirstOrDefault());
                var properties = GetProperties(accessor).ToList();

                for (var i = 0; i < properties.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = properties[i].Name;
                }

                var iterator = 0;
                for (var row = 2; row <= totalRows + 1; row++)
                {
                    for (var j = 0; j < properties.Count; j++)
                    {
                        worksheet.Cells[row, j + 1].Value = accessor[objList[iterator], properties[j].Name];
                    }
                    iterator++;
                }

                bytes = package.GetAsByteArray();
            }

            System.IO.File.WriteAllBytes(filePath, bytes);
        }

        private TypeAccessor GetAccessor(object obj)
        {
            if (obj == null)
                return null;
            var type = obj.GetType();
            return TypeAccessor.Create(type, true);
        }

        private IEnumerable<Member> GetProperties(TypeAccessor accessor)
        {
            return accessor?.GetMembers();
        }
    }
}
