using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Settings
{
    public interface IIntParam : IBaseParam
    {
        int this[string Name] { get; set; }
    }
}
