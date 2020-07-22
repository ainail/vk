using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tochka.CommandExecutors;

namespace Tochka
{
    class CommandList
    {
        private static CommandList instance;
        private static readonly object syncRoot = new Object();
        public IEnumerable<Type> Commands { get; set; }

        private CommandList()
        {
        }

        public static CommandList GetInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new CommandList();
                        var type = typeof(CommandExecutorBase<object>);
                        instance.Commands = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(s => s.GetTypes())
                            .Where(p => type.IsAssignableFrom(p));
                    }
                }
            }
            return instance;
        }
        public static string GetHelpStrings()
        {
            if(instance != null)
            {
                return instance.Commands.Select(_ => _.CustomAttributes
                .FirstOrDefault().ConstructorArguments.ElementAt(2).Value.ToString())
                    .Aggregate((x, y) => x + "\r\n" + y); 
            }
            return null;
        }
    }
}
