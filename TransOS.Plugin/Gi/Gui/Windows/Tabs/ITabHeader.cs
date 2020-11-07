using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Gi.Gui.Windows.Tabs
{
    /// <summary>
    /// Tab header
    /// </summary>
    public interface ITabHeader
    {
        /// <summary>
        /// Header icon
        /// </summary>
        Image Icon { get; set; }

        /// <summary>
        /// Header text
        /// </summary>
        string Text { get; set; }

        //ToolStripItem MenuItem { get; }
    }
}
