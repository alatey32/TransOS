using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.Gi
{
    /// <summary>
    /// Graphic controller
    /// </summary>
    public class This
    {
        /// <summary>
        /// GUI controller
        /// </summary>
        public readonly Gui.This Gui;

        internal This(TransOS.Core.Context Os)
        {
            this.Gui = new Gui.This(Os);
        }
    }
}
