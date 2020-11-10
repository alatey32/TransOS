using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Settings;

namespace TransOS.Core.Settings
{
    /// <summary>
    /// Base param controller
    /// </summary>
    public abstract class ABaseParam : IBaseParam
    {
        protected readonly SettDirectory Directory;
                
        internal ABaseParam(SettDirectory Directory)
        {
            this.Directory = Directory;
        }

        /// <summary>
        /// Get parameter names
        /// </summary>
        /// <returns>Parameter names</returns>
        public abstract IEnumerable<string> GetNames();

        /// <summary>
        /// Delete parameter by name
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns>Found and deleted</returns>
        public abstract bool Remove(string Name);

        /// <summary>
        /// Is parameter exists by name
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns>Existed</returns>
        public virtual bool Exists(string Name)
        {
            return this.GetNames().IndexOf(Name) >= 0;
        }
    }
}
