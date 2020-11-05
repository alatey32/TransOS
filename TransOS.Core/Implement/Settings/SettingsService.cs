using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.Implement.Settings
{
    /// <summary>
    /// Settings service (implemented)
    /// </summary>
    public class SettingsService : Plugin.Settings.ISettingsService
    {
        /// <summary>
        /// Access to string values
        /// </summary>
        public Plugin.Settings.IStringParam String { get; }

        /// <summary>
        /// Access to string list values
        /// </summary>
        public Plugin.Settings.IIntParam Int { get; }

        /// <summary>
        /// Access to string list values
        /// </summary>
        public Plugin.Settings.IStringsParam Strings { get; }

        /// <summary>
        /// Access to serialyzed object values
        /// </summary>
        public Plugin.Settings.IObjectParam Object { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingsService">Original settings service</param>
        internal SettingsService(Core.Settings.SettingsService settingsService)
        {
            this.String = new StringParam(settingsService.String);
            this.Int = new IntParam(settingsService.Int);
            this.Strings = new StringsParam(settingsService.Strings);
            this.Object = new ObjectParam(settingsService.Object);
        }
    }
}
