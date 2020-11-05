using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.Implement.Settings
{
    /// <summary>
    /// Integer values param controller (implemented)
    /// </summary>
    public class IntParam : BaseParam, Plugin.Settings.IIntParam
    {
        /// <summary>
        /// Set or get value by Name
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns></returns>
        public int this[string Name]
        {
            get => this.intParam[Name];
            set => this.intParam[Name] = value;
        }

        /// <summary>
        /// Original integer values param controller
        /// </summary>
        readonly Core.Settings.IntParam intParam;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intParam">Original integer values param controller</param>
        internal IntParam(Core.Settings.IntParam intParam) : base(intParam)
        {
            this.intParam = intParam;
        }
    }
}
