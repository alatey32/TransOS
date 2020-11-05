using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Settings
{
    /// <summary>
    /// Integer values param controller interface
    /// </summary>
    public interface IIntParam : IBaseParam
    {
        /// <summary>
        /// Set or get value by Name
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns></returns>
        int this[string Name] { get; set; }
    }
}
