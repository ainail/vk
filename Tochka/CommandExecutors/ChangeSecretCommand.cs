using System;
using System.Collections.Generic;
using System.Text;

namespace Tochka.CommandExecutors
{
    [Command("/secret", "changes secret key for app",
    "to change the secret key of app enter command /secret *your secret key*")]
    class ChangeSecretCommand : CommandExecutorBase<object>
    {
        protected override string ExecuteInternal(string[] param)
        {
            var settings = Settings.GetInstance();
            settings.AppSecret = param[1];
            return Const.AppSecretChangedSucc;
        }

        protected override void Validate(string[] param)
        {
            if(param.Length > 2)
            {
                throw new ArgumentException(Const.ArgumentError);
            }
        }
    }
}
