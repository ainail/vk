using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using VkNet.Model;

namespace Tochka
{
    class Settings
    {
        private static Settings instance;
        private static readonly object syncRoot = new Object();
        public int OwnerId { get; set; }
        public string AppToken { get; set; }
        public uint AppId { get; set; }
        public string AppSecret { get; set; }

        private Settings()
        {
        }

        public static Settings GetInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new Settings();
                }
            }
            return instance;
        }
    }
}
