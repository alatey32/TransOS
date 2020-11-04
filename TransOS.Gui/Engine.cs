using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransOS.Gui
{
    /// <summary>
    /// Application engine
    /// </summary>
    public class Engine
    {
        public readonly WebTab.This WebTab;

        public readonly Form1 Mainform;

        internal Engine(Form1 Mainform)
        {
            this.Mainform = Mainform;

            this.WebTab = new WebTab.This(this);
        }

        /// <summary>
        /// Application current version
        /// </summary>
        public Version AppVersion
        {
            get => AppAsssembly.GetName().Version;
        }

        private Assembly AppAsssembly
        {
            get => Assembly.GetEntryAssembly();
        }
    }
}
