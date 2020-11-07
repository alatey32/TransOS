using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.Implement.Gi
{
    /// <summary>
    /// Graphic interface controller (implemented)
    /// </summary>
    public class This : Plugin.Gi.IThis
    {
        /// <summary>
        /// GUI controller (implemented)
        /// </summary>
        public Plugin.Gi.Gui.IThis Gui { get; private set; }

        private readonly Core.Gi.This gi;

        internal This(Core.Gi.This gi)
        {
            this.gi = gi;

            this.Gui = new Gui.This(this.gi.Gui);
        }
    }
}
