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
    /// Tabs controller
    /// </summary>
    public class This
    {
        public bool IsSupported
        {
            get => this.Os.Gi.Gui.IsSupported;
        }

        /// <summary>
        /// All tabs
        /// </summary>
        public IEnumerable<ITab> All
        {
            get => this.MainTabControl.TabPages.Cast<TabPage>().Select(x => x.Tag as ITab);
        }

        /// <summary>
        /// Current tab
        /// </summary>
        public ITab Current
        {
            get => this.MainTabControl.SelectedTab?.Tag as ITab;
            set
            {
                var Founded = this.MainTabControl.TabPages.Cast<TabPage>().FirstOrDefault(x => x.Tag == value);
                if (Founded != null)
                    this.MainTabControl.SelectedTab = Founded;
            }
        }

        private readonly TransOS.Core.Context Os;

        public TabControl MainTabControl;

        internal This(TransOS.Core.Context Os)
        {
            this.Os = Os;
        }

        /// <summary>
        /// Add tab
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public ITab Add(string Text)
        {
            var NewTab = new Tab(this.Os, Text);            
            return NewTab;
        }

        /// <summary>
        /// Close tab
        /// </summary>
        /// <param name="tab"></param>
        public void Close(ITab tab)
        {
            tab?.Close();
        }
    }
}
