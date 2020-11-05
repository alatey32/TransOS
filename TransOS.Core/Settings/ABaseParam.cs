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
        /// <summary>
        /// TransOS context
        /// </summary>
        protected readonly Context Os;
        protected readonly SettingsService Service;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Os">TransOS context</param>
        /// <param name="Service"></param>
        internal ABaseParam(Context Os, SettingsService Service)
        {
            this.Os = Os;
            this.Service = Service;
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
        /// <returns></returns>
        public virtual bool Exists(string Name)
        {
            return this.GetNames().IndexOf(Name) >= 0;
        }
    }
}
