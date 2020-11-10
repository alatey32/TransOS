using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Core.Settings;

namespace TransOS.Core.Bookmarks
{
    internal interface IBookmarkDirectory
    {
        SettingsService settingsService { get; }
    }
}
