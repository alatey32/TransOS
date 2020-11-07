using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransOS.Plugin.Gi.Gui.Windows.Tabs;

namespace TransOS.Core.Gi.Gui.Windows.Tabs
{
    /// <summary>
    /// Tab body
    /// </summary>
    public class TabBody : ITabBody
    {
        private readonly Context Os;
        private readonly Tab tab;

        internal TabBody(Context Os, Tab tab)
        {
            this.Os = Os;
            this.tab = tab;
        }

        /// <summary>
        /// Add control to tab body
        /// </summary>
        /// <param name="control"></param>
        public void Add(Control control)
        {
            this.tab.tabPage.Controls.Add(control);
        }
    }
}
