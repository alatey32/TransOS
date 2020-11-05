using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Settings
{
    /// <summary>
    /// Settings service (implemented) interface
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Access to string values
        /// </summary>
        IStringParam String { get; }

        /// <summary>
        /// Access to string list values
        /// </summary>
        IIntParam Int { get; }

        /// <summary>
        /// Access to string list values
        /// </summary>
        IStringsParam Strings { get; }

        /// <summary>
        /// Access to serialyzed object values
        /// </summary>
        IObjectParam Object { get; }
    }
}
