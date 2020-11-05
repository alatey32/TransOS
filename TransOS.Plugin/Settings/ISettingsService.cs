using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Settings
{
    public interface ISettingsService
    {
        IStringParam String { get; }
        IIntParam Int { get; }
        IStringsParam Strings { get; }
        IObjectParam Object { get; }
    }
}
