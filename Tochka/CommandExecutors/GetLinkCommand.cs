using System;
using System.Collections.Generic;
using System.Text;

namespace Tochka.CommandExecutors
{
    [Command("/link", "prints link that gets access token", 
        "to get access token link, enter command /link\r\ntoken, that you've got by using /link will be in address line after access_token=\r\n" +
        "please, please make sure you entered your user Id before doing this by command /ownerId")]
    class GetLinkCommand : CommandExecutorBase<object>
    {
        protected override string ExecuteInternal(string[] param)
        {
            var settings = Settings.GetInstance();
            return Const.Link(settings.AppId.ToString());
        }

        protected override void Validate(string[] param)
        {
            if (param.Length > 1)
                throw new ArgumentException(Const.ArgumentError);
        }
    }
}
