using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Settings
{
    public interface ISettDirectory
    {
        string Name { get; }
        IEnumerable<ISettDirectory> Directories { get; }
    }
}
