using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Gi.Gui.Windows.Tabs;

namespace TransOS.Core.Gi.Gui.Windows.Tabs
{
    /// <summary>
    /// Tab header
    /// </summary>
    public class TabHeader : ITabHeader
    {
        /// <summary>
        /// Header icon
        /// </summary>
        public Image Icon
        {
            get; set;
        }

        /// <summary>
        /// Header text
        /// </summary>
        public string Text
        {
            get => this.tab.tabPage.Text;
            set
            {
                this.tab.tabPage.Text = value;

                int TabPageIndex = this.Os.Gi.Gui.Windows.Tabs.MainTabControl.TabPages.IndexOf(this.tab.tabPage);
                if (TabPageIndex >= 0)
                    Os.Network.Web.Client.Cash.SaveTab(this.tab, TabPageIndex);
            }
        }

        private readonly Context Os;
        private readonly Tab tab;

        internal TabHeader(Context Os, Tab tab, string Text)
        {
            this.Os = Os;
            this.tab = tab;
        }
    }
}
