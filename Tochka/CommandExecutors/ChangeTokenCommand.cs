using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using VkNet;
using VkNet.Exception;
using VkNet.Model;

namespace Tochka.CommandExecutors
{
    [Command("/token", "changes access token and other parameters",
        "to change the access token, enter the command in the format /token *application token*")]
    class ChangeTokenCommand : CommandExecutorBase<object>
    {
        protected override string ExecuteInternal(string[] param)
        {
            var parameters = Settings.GetInstance();
            parameters.AppToken = param[1];
            return Const.TokenSuccChanged;
        }

        protected override void Validate(string[] param)
        {
            if (param.Length != 2)
            {
                throw new ArgumentException(Const.ArgumentError);
            }
            var parameters = Settings.GetInstance();
            using (var api = new VkApi())
            {
                try
                {
                    api.Authorize(new ApiAuthParams
                    {
                        AccessToken = parameters.AppSecret,
                        ApplicationId = parameters.AppId
                    }) ;
                    var ip = Tools.GetIPAddress();
                    var result = api.Secure.CheckToken(param[1], ip);
                    if (result.Success)
                    {
                        return;
                    }
                }
                catch (VkApiException ex)
                {
                    if (ex.Message == "Access denied: Invalid ip")
                        throw new VkApiException(Const.TokenExpired);
                    else if(ex.Message == "User authorization failed: invalid access_token (4).")
                        throw new VkApiException(Const.InvalidSecretKey);
                    else
                        throw(ex);
                }
            }
        }
    }
}
