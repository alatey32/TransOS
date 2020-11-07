using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Gi
{
    /// <summary>
    /// Graphic interface controller (implemented) interface
    /// </summary>
    public interface IThis
    {
        Gui.IThis Gui { get; }
    }
}
