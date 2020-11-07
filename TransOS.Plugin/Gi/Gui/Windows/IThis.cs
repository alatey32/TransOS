using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Gi.Gui.Windows
{
    /// <summary>
    /// GUI windows controller (implemented) interface
    /// </summary>
    public interface IThis
    {
        bool IsSupported { get; }

        Tabs.IThis Tabs { get; }

        // Dialog.IThis Dialog { get; }
    }
}
