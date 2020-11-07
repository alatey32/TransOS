using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Network
{
    public interface IThis
    {
        Web.IThis Web { get; }
    }
}
