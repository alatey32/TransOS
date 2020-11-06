using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Ridge
{
    /// <summary>
    /// Ridge controller (implemented) interface
    /// </summary>
    public interface IThis
    {
        /// <summary>
        /// Root ridge object
        /// </summary>
        IRidgeObject Root { get; }
    }
}
