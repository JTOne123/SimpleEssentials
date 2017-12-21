using System;
using System.Text;

namespace SimpleEssentials.Extensions
{
    public static class ConsoleEx
    {
        public static void WriteLine(string text, ConsoleColor color = ConsoleColor.Yellow)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteTitle(string text, int tileCount = 40, ConsoleColor color = ConsoleColor.Yellow)
        {
            var strLength = text.Length;
            var newCount = tileCount - strLength;
            var sb = new StringBuilder();
            sb.Append('-', (newCount / 2));
            var finalLength = (sb.Length * 2 + strLength);
            var endString = sb + ((tileCount - finalLength) == 0 ? string.Empty : "-");
            var finalString = $"{sb}{text}{endString}";

            WriteLine(finalString, color);
        }
    }
}
