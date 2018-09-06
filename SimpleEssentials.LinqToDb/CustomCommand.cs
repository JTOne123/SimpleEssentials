using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleEssentials.LinqToDb
{
    public class CustomCommand : ICustomCommand
    {
        private string Command { get; set; }

        public void Concat(string command)
        {
            Command += " " + command;
        }

        public string GetCommand()
        {
            return Command;
        }
    }
}
