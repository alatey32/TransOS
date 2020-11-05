using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Settings
{
    /// <summary>
    /// Strings values param controller (implemented) interface
    /// </summary>
    public interface IStringsParam : IBaseParam
    {
        /// <summary>
        /// Get string list by Name
        /// </summary>
        /// <param name="Name">List name</param>
        /// <returns>String items list</returns>
        IStringItems this[string Name] { get; }
    }
}
