using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VkNet;
using VkNet.Exception;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace Tochka.CommandExecutors
{
    [Command("", "prints posts frequency analysis", "enter user or public id to get it frequency analysis")]
    class PrintExecutor : CommandExecutorBase<object>
    {
        private int? _ownerId;
        private string _domain;
        protected override string ExecuteInternal(string[] param)
        {
            var settings = Settings.GetInstance();
            using (var api = new VkApi() { })
            {
                api.Authorize(new ApiAuthParams { AccessToken = settings.AppToken, ApplicationId = settings.AppId });
                try
                {
                    var posts = api.Wall.Get(new WallGetParams { Count = 5, OwnerId = _ownerId, Domain = _domain });

                    var freqs = Tools.GetFreqAnalysis(posts);
                    api.Wall.Post(new WallPostParams
                    {
                        OwnerId = settings.OwnerId,
                        Message = JsonConvert.SerializeObject(freqs)
                    });
                    return String.Join("\r\n", freqs.Select(kvp => kvp.Key + " : " + kvp.Value));
                }
                catch (VkApiException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw(ex);
                }
            }
        }


        protected override void Validate(string[] param)
        {

            if (param.Length > 1)
                throw new ArgumentException(Const.ArgumentError);
            var command = param[0];
            if (command.StartsWith("id"))
            {
                if (int.TryParse(command.Substring(2), out var userId))
                    _ownerId = userId;
                else
                {
                    throw new ArgumentException(Const.PrintError);
                }

            }

            else if (param[0].StartsWith("public"))
            {
                if (int.TryParse(command.Substring(6), out var publicId))
                {
                    _ownerId = publicId * -1;
                }
                else
                {
                    throw new ArgumentException(Const.PrintError);
                }
            }
            else _domain = command;
        }
    }
}
