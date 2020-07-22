using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using VkNet.Model;

namespace Tochka
{
    class Tools
    {
        public static string GetIPAddress()
        {
            var st = new WebClient().DownloadString("http://icanhazip.com");
            return st.Substring(0, st.Length - 1);
        }
        public static Dictionary<char,double> GetFreqAnalysis(WallGetObject posts)
        {
            var chars = posts.WallPosts.Select(_ => _.Text)
                        .SelectMany(_ => _.Where(c => Char.IsLetter(c))
                        .Select(_ => Char.ToLower(_)));
            var count = chars.Count();
            var freqs = chars.GroupBy(_ => _)
            .OrderBy(_ => _.Key)
            .ToDictionary(g => g.Key, g => Math.Round((double)g.Count() / count, 3));
            return freqs;
        }
    }
}
