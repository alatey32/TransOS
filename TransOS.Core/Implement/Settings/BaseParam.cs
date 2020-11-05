using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Settings;

namespace TransOS.Core.Implement.Settings
{
    /// <summary>
    /// Base param controller (implemented)
    /// </summary>
    public abstract class BaseParam : IBaseParam
    {
        /// <summary>
        /// Original base param controller
        /// </summary>
        readonly IBaseParam iBaseParam;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iBaseParam">Original base param controller</param>
        internal BaseParam(IBaseParam iBaseParam)
        {
            this.iBaseParam = iBaseParam;
        }

        /// <summary>
        /// Get parameter names
        /// </summary>
        /// <returns>Parameter names</returns>
        public IEnumerable<string> GetNames() => this.iBaseParam.GetNames();

        /// <summary>
        /// Delete parameter by name
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns>Found and deleted</returns>
        public bool Remove(string Name) => this.iBaseParam.Remove(Name);

        /// <summary>
        /// Is parameter exists by name
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns>Existed</returns>
        public bool Exists(string Name) => this.iBaseParam.Exists(Name);
    }
}
