using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Settings
{
    public interface IStringParam : IBaseParam
    {
        string this[string Name] { get; set; }
    }
}
