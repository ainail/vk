using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using Tochka.CommandExecutors;
using VkNet;
using VkNet.Exception;
using VkNet.Infrastructure;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;
using static System.Console;

namespace Tochka
{
    class Program
    {
        static CommandExecutorBase<object> BuildCommand(string[] parameters)
        {
            if (parameters.Length > 0)
            {
                var type = CommandExecutorBase<object>.CommandTypes
                .FirstOrDefault(_ => _.CustomAttributes
                .FirstOrDefault().ConstructorArguments[0]
                .Value.ToString().Equals(parameters[0]));
                if (type != null)
                {
                    return (CommandExecutorBase<object>)Activator.CreateInstance(type);
                }
                else
                {
                    return new PrintExecutor();
                }
            }
            return null;
        }
        static void Main(string[] args)
        {
            var settings = Settings.GetInstance();
            settings.AppId = Const.AppId;
            settings.AppToken = Const.Token;
            settings.OwnerId = Const.OwnerId;
            settings.AppSecret = Const.SecretKey;
            settings = null;
            var wannaexit = false;

            var help = new HelpExecutor();
            WriteLine(help.GetHelpMessage());
            while (true)
            {
                var command = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var builtCommand = BuildCommand(command);
                if (builtCommand == null)
                {
                    if (!wannaexit)
                    {
                        WriteLine("Press enter to exit");
                        wannaexit = true;
                    }
                    else
                        break;
                    continue;
                }
                else
                {
                    wannaexit = false;
                }
                Console.WriteLine(builtCommand.Execute(command));
            }
        }
    }
}
