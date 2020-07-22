using System;
using System.Collections.Generic;
using System.Text;

namespace Tochka.CommandExecutors
{
    [Command("/appId", "changes app id for app",
    "to change the app id enter command /appId *your app id*")]
    class ChangeAppCommand : CommandExecutorBase<object>
    {
        protected override string ExecuteInternal(string[] param)
        {
            var settings = Settings.GetInstance();
            settings.AppId = uint.Parse(param[1]);
            return Const.AppIdChangedSucc;
        }

        protected override void Validate(string[] param)
        {
            if (param.Length > 2)
            {
                throw new ArgumentException(Const.ArgumentError);
            }
            if (!uint.TryParse(param[1], out var a))
            {
                throw new ArgumentException(Const.GetNotIntError(param[1]));
            }
        }
    }
}
