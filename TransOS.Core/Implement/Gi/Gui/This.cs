using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.Implement.Gi.Gui
{
    /// <summary>
    /// GUI controller (implemented)
    /// </summary>
    public class This : Plugin.Gi.Gui.IThis
    {
        public bool IsSupported
        {
            get => this.gui.IsSupported;
        }

        /// <summary>
        /// GUI windows controller (implemented)
        /// </summary>
        public Plugin.Gi.Gui.Windows.IThis Windows { get; private set; }

        /// <summary>
        /// GUI controller
        /// </summary>
        private readonly Core.Gi.Gui.This gui;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gui">GUI controller</param>
        internal This(Core.Gi.Gui.This gui)
        {
            this.gui = gui;
            this.Windows = new Windows.This(this.gui.Windows);
        }
    }
}
