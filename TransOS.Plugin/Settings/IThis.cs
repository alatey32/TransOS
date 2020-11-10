using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Settings
{
    /// <summary>
    /// Settings interface for plugin
    /// </summary>
    public interface IThis
    {
        /// <summary>
        /// Settings directories
        /// </summary>
        IEnumerable<ISettDirectory> Directories { get; }

        /*
        /// <summary>
        /// Default settings service for this plugin
        /// </summary>
        ISettingsService Default { get; }*/

        /// <summary>
        /// Get settings service
        /// </summary>
        /// <param name="PluginIdName">Settings section name</param>
        /// <returns>Settings service</returns>
        ISettingsService GetService(string PluginIdName);
    }
}
