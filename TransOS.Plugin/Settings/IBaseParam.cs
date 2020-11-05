using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Settings
{
    public interface IBaseParam
    {
        /// <summary>
        /// Get parameter names
        /// </summary>
        /// <returns>Parameter names</returns>
        IEnumerable<string> GetNames();

        /// <summary>
        /// Delete parameter by name
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns>Found and deleted</returns>
        bool Remove(string Name);

        /// <summary>
        /// Is parameter exists by name
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns>Existed</returns>
        bool Exists(string Name);
    }
}
