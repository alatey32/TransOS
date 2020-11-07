using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.Gi.Gui
{
    /// <summary>
    /// GUI controller
    /// </summary>
    public class This
    {
        /// <summary>
        /// Windows system
        /// </summary>
        public readonly Windows.This Windows;

        public bool IsSupported
        {
            get => true;
        }

        internal This(TransOS.Core.Context Os)
        {
            this.Windows = new Windows.This(Os);
        }
    }
}
