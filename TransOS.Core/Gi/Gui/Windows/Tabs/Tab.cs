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
    /// Tab in the GUI window
    /// </summary>
    public class Tab : ITab
    {
        public Guid guid { get; private set; }

        internal readonly TabHeader Header_;

        /// <summary>
        /// Tab header
        /// </summary>
        public ITabHeader Header { get => this.Header_; }

        internal readonly TabBody Body_;

        /// <summary>
        /// Tab body
        /// </summary>
        public ITabBody Body { get => this.Body_; }
        
        /// <summary>
        /// Is this tab selected?
        /// </summary>
        public bool IsSelected
        {
            get => this.Os.Gi.Gui.Windows.Tabs.Current == this;
        }

        /// <summary>
        /// Command to save and restore tab with equals content
        /// </summary>
        private string RestoreCommand_ { get; set; }

        /// <summary>
        /// Command to save and restore tab with equals content
        /// </summary>
        public string RestoreCommand
        {
            get => this.RestoreCommand_;
            set
            {
                this.RestoreCommand_ = value;
                int TabPageIndex = this.Os.Gi.Gui.Windows.Tabs.MainTabControl.TabPages.IndexOf(this.tabPage);
                if (TabPageIndex >= 0)
                    Os.Network.Web.Client.Cash.SaveTab(this, TabPageIndex);
            }
        }

        /// <summary>
        /// Select this tab
        /// </summary>
        public void Select()
        {
            this.Os.Gi.Gui.Windows.Tabs.Current = this;
        }

        /// <summary>
        /// Event before closing a tab
        /// </summary>
        public event Action<ITab> Closing;

        private readonly Context Os;

        internal readonly TabPage tabPage;

        internal Tab(Context Os, string Text)
        {
            this.Os = Os;
            this.guid = Guid.NewGuid();

            this.tabPage = new TabPage(Text)
            {
                Tag = this
            };
            this.Os.Gi.Gui.Windows.Tabs.MainTabControl.TabPages.Add(this.tabPage);

            this.Header_ = new TabHeader(this.Os, this, Text);
            this.Body_ = new TabBody(this.Os, this);
        }

        /// <summary>
        /// Close this tab
        /// </summary>
        public void Close()
        {            
            int TabPageIndex = this.Os.Gi.Gui.Windows.Tabs.MainTabControl.TabPages.IndexOf(this.tabPage);
            if (TabPageIndex >= 0)
                Os.Network.Web.Client.Cash.RemoveTab(TabPageIndex);

            this.Closing?.Invoke(this);
            this.Os.Gi.Gui.Windows.Tabs.MainTabControl.TabPages.Remove(this.tabPage);
        }
    }
}
