using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Settings
{
    public interface IStringsParam : IBaseParam
    {
        IStringItems this[string Name] { get; }
    }
}
