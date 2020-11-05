using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.Implement.Settings
{
    /// <summary>
    /// String values param controller (implemented)
    /// </summary>
    public class StringParam : BaseParam, Plugin.Settings.IStringParam
    {
        /// <summary>
        /// Set or get value by Name
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns>String value</returns>
        public string this[string Name]
        {
            get => this.stringParam[Name];
            set => this.stringParam[Name] = value;
        }

        /// <summary>
        /// Original string values param controller
        /// </summary>
        readonly Core.Settings.StringParam stringParam;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringParam">Original string values param controller</param>
        internal StringParam(Core.Settings.StringParam stringParam) : base(stringParam)
        {
            this.stringParam = stringParam;
        }
    }
}
