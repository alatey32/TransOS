using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Application
{
    [Flags]
    public enum AppActionType
    {
        PreView = 1,

        View = 2,

        Exec = 4,

        Properties = 8
    }
}
