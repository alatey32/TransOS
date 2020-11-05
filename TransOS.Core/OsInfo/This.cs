using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.OsInfo
{
    /// <summary>
    /// Base info about TransOS core
    /// </summary>
    public class This
    {
        internal This()
        {

        }

        /// <summary>
        /// Entry assembly
        /// </summary>
        private Assembly CoreAsssembly
        {
            get => Assembly.GetExecutingAssembly();
        }

        /// <summary>
        /// Application current version
        /// </summary>
        public Version Version
        {
            get => this.CoreAsssembly.GetName().Version;
        }
    }
}
