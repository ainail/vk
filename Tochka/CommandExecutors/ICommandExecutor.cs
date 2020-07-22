using System;
using System.Collections.Generic;
using System.Text;

namespace Tochka.CommandExecutors
{
    
   public interface ICommandExecutor
    {
        string Execute(string[] command);
    }
}
