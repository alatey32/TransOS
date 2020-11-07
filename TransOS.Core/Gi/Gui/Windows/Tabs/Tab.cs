using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransOS.Plugin.Gi.Gui.Windows.Tabs;

namespace TransOS.Core.Gi.Gui.Windows.Tabs
{    
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
        /// Select this tab
        /// </summary>
        public void Select()
        {
            this.Os.Gi.Gui.Windows.Tabs.Current = this;
        }

        /// <summary>
        /// Событие перед закрытием вкладки
        /// </summary>
        public event Action<ITab> Closed;

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
            this.Closed?.Invoke(this);
            this.Os.Gi.Gui.Windows.Tabs.MainTabControl.TabPages.Remove(this.tabPage);
        }
    }
}
