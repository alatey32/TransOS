using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.Implement.Gi.Gui.Windows
{
    /// <summary>
    /// GUI windows controller (implemented)
    /// </summary>
    public class This : Plugin.Gi.Gui.Windows.IThis
    {
        public bool IsSupported
        {
            get => this.windows.IsSupported;
        }

        /// <summary>
        /// GUI tabs controller (implemented)
        /// </summary>
        public Plugin.Gi.Gui.Windows.Tabs.IThis Tabs { get; private set; }

        /// <summary>
        /// GUI windows controller
        /// </summary>
        private readonly Core.Gi.Gui.Windows.This windows;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="windows">GUI windows controller</param>
        internal This(Core.Gi.Gui.Windows.This windows)
        {
            this.windows = windows;

            this.Tabs = new Tabs.This(this.windows.Tabs);
        }
    }
}
