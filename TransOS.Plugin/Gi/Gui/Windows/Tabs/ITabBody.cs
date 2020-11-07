using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransOS.Plugin.Gi.Gui.Windows.Tabs
{
    /// <summary>
    /// Tab body
    /// </summary>
    public interface ITabBody
    {
        /// <summary>
        /// Tab body panel
        /// </summary>
        //Panel MainPanel { get; }       

        /// <summary>
        /// Add control to the body panel of the tab
        /// </summary>
        /// <param name="control"></param>
        void Add(Control control);
    }
}
