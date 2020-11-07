using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.Gi.Gui.Windows
{
    /// <summary>
    /// Windows system controller
    /// </summary>
    public class This
    {
        /// <summary>
        /// Tabs system controller
        /// </summary>
        public readonly Tabs.This Tabs;

        public bool IsSupported
        {
            get => false;
        }

        internal This(TransOS.Core.Context Os)
        {
            this.Tabs = new Tabs.This(Os);
        }
    }
}
