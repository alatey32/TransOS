using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Gi.Gui
{
    /// <summary>
    /// GUI controller (implemented) interface
    /// </summary>
    public interface IThis
    {
        bool IsSupported { get; }

        /// <summary>
        /// GUI windows controller (implemented)
        /// </summary>
        Windows.IThis Windows { get; }
    }
}
