using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Settings
{
    /// <summary>
    /// String values param controller (implemented) interface
    /// </summary>
    public interface IStringParam : IBaseParam
    {
        /// <summary>
        /// Set or get value by Name
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns>String value</returns>
        string this[string Name] { get; set; }
    }
}
