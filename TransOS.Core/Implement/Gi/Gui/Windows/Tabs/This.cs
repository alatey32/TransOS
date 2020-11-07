using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Gi.Gui.Windows.Tabs;

namespace TransOS.Core.Implement.Gi.Gui.Windows.Tabs
{
    /// <summary>
    /// Tabs controller (implemented)
    /// </summary>
    public class This : IThis
    {
        public bool IsSupported
        {
            get => this.tabs.IsSupported;
        }

        private readonly Core.Gi.Gui.Windows.Tabs.This tabs;

        internal This(Core.Gi.Gui.Windows.Tabs.This tabs)
        {
            this.tabs = tabs;
        }

        /// <summary>
        /// Add tab
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public ITab Add(string Text) => this.tabs.Add(Text);

        /// <summary>
        /// Close tab
        /// </summary>
        /// <param name="tab"></param>
        public void Close(ITab tab) => this.tabs.Close(tab);
    }
}
