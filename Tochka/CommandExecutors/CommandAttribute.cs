using System;
using System.Collections.Generic;
using System.Text;

namespace Tochka.CommandExecutors
{
    public class CommandAttribute : Attribute
    {
        public string Command { get; }
        public string Description { get; }
        public string HelpDescription { get; }

        public CommandAttribute(string command, string description, string helpDescription)
        {
            Command = command;
            Description = description;
            HelpDescription = helpDescription;
        }
    }
}
