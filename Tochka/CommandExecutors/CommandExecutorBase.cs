using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VkNet.Exception;

namespace Tochka.CommandExecutors
{
    public abstract class CommandExecutorBase<T> : ICommandExecutor
    {
        public static IEnumerable<Type> CommandTypes;
        public CommandExecutorBase()
        {
            var type = typeof(CommandExecutorBase<object>);
            CommandTypes = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(s => s.GetTypes())
                            .Where(p => type.IsAssignableFrom(p));
        }

        public string Execute(string[] command)
        {
            try
            {
                Validate(command);
                return ExecuteInternal(command);
            }
            catch (Exception ex)
            {
                return CatchException(ex);
            }
        }
        
        protected abstract void Validate(string[] param);
        protected abstract string ExecuteInternal(string[] param);
        protected virtual string CatchException(Exception ex) 
        {
            if(ex is VkApiException || ex is ArgumentException)
                return ex.Message;
            else
            {
#if DEBUG
                return ex.StackTrace;
#else
                return Const.UnknownError;
#endif
            }
        }
    }
}
