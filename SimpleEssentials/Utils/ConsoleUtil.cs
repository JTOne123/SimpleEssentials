using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleEssentials.Utils
{
    public static class ConsoleUtil
    {
        public static string PadElementsInLines(List<string[]> lines, int padding = 1)
        {
            var numElements = lines[0].Length;
            var maxValues = new int[numElements];
            for (var i = 0; i < numElements; i++)
            {
                maxValues[i] = lines.Max(x => x[i].Length) + padding;
            }
            var sb = new StringBuilder();
            var isFirst = true;
            foreach (var line in lines)
            {
                if (!isFirst)
                {
                    sb.AppendLine();
                }
                isFirst = false;
                for (var i = 0; i < line.Length; i++)
                {
                    var value = line[i];
                    sb.Append(value.PadRight(maxValues[i]));
                }
            }
            return sb.ToString();
        }
    }
}
