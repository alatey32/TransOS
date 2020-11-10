using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Settings;

namespace TransOS.Core.Bookmarks
{
    [Serializable]
    public class BookmarkItemSer
    {
        [SettingId, JsonIgnore]
        public string Id;

        public string Text;
        public string Command;
        public string Description;
    }
}
