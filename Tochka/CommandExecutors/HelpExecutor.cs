using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;

namespace Tochka.CommandExecutors
{
    [Command("/help", "prints all command descriptions", "use */help* command to get info about other commands")]
    public class HelpExecutor : CommandExecutorBase<object>
    {
        protected override string ExecuteInternal(string[] param)
        {
            var allDescriptions = CommandTypes
                .Select(_ => _.CustomAttributes
                .FirstOrDefault().ConstructorArguments.ElementAt(2).Value.ToString());
            return allDescriptions.Aggregate((x, y) => x + "\r\n" + y);
        }

        protected override void Validate(string[] param)
        {
            var check = param as string[];
            if(check.Length > 1)
            {
                throw new ArgumentException("Too many parameters for command /help");
            }
        }
        public string GetHelpMessage()
        {
            var type = MethodBase.GetCurrentMethod().DeclaringType;
            return type.CustomAttributes.FirstOrDefault()
                .ConstructorArguments.ElementAt(2).Value.ToString();
        }
    }
}
