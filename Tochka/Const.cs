using System;
using System.Collections.Generic;
using System.Text;

namespace Tochka
{
    class Const
    {
        public static string Link(string clientId) =>
            String.Format("https://oauth.vk.com/authorize?client_id={0}&display=page&scope=73730&response_type=token&v=5.120", clientId);
        public const string ArgumentError = "please, use command */help* to get info about commands";
        public static string GetNotIntError(string value) => String.Format("{0} is not valid value, must be positive integer", value);
        public const string TokenSuccChanged = "Token changed successfully";
        public const string TokenExpired = "This token is expired, please, get new token by using command /token";
        public const string OwnerIdChangedSucc = "Owner id changed successfully";
        public const int OwnerId = 51228968;
        public const int AppId = 7540616;
        public const string Token = "1905f9a1483abb3615ec47dd7bd8de9fc18ba4ad7ce0adaf68a1a0096d645fbd8324b55b9a5a8c8418d92";
        public const string SecretKey = "d2efef72d2efef72d2efef7226d29ce0fadd2efd2efef728dffdfe45d0fed095cbef1f4";
        public const string PrintError = "please, enter user or public id in such formats:\r\n◦ id1\r\n◦ durov\r\n◦ public147415323\r\n◦ tech";
        public const string AppSecretChangedSucc = "application secret code changed successfully";
        public const string InvalidSecretKey = "Invalid app's secret key, please, enter valid by using command /secret";
        public const string UnknownError = "Unknown error, please, contact the developer";
        public const string AppIdChangedSucc = "application Id changed successfully";
    }
}
