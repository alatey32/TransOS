using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Settings;

namespace TransOS.Core.Implement.Settings
{
    /// <summary>
    /// Strings values param controller (implemented)
    /// </summary>
    public class StringsParam : BaseParam, IStringsParam
    {
        /// <summary>
        /// Get string list by Name
        /// </summary>
        /// <param name="Name">List name</param>
        /// <returns>String items list</returns>
        public IStringItems this[string Name]
        {
            get => new StringItems(this.stringsParam[Name]);
        }

        /// <summary>
        /// Original stritg params values param controller
        /// </summary>
        readonly Core.Settings.StringsParam stringsParam;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringsParam">Original stritg params values param controller</param>
        internal StringsParam(Core.Settings.StringsParam stringsParam) : base(stringsParam)
        {
            this.stringsParam = stringsParam;
        }
    }
}
