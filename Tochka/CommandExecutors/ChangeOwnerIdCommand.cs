using System;
using System.Collections.Generic;
using System.Text;

namespace Tochka.CommandExecutors
{
    [Command("/ownerId", "changes owner Id",
        "to change owner id, enter the command in the format /ownerId *owner id*")]
    class ChangeOwnerIdCommand : CommandExecutorBase<object>
    {
        protected override string ExecuteInternal(string[] param)
        {
            var settings = Settings.GetInstance();
            settings.OwnerId = int.Parse(param[1]);
            return Const.OwnerIdChangedSucc;
        }

        protected override void Validate(string[] param)
        {
            if (param.Length > 2)
                throw new ArgumentException(Const.ArgumentError);
            if (!int.TryParse(param[1], out var a))
                throw new ArgumentException(Const.GetNotIntError(param[1]));
        }
    }
}
