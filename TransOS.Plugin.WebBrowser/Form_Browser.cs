using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransOS.Plugin.Gi.Gui.Windows.Tabs;
using TransOS.Plugin.WebBrowser.WebPageProperties;
using TransOS.Plugin.WebBrowser.WebPageSystem;

namespace TransOS.Plugin.WebBrowser
{
    public partial class Form_Browser : Form
    {
        public Uri Url
        {
            get
            {
                Uri ParsedUrl;
                try
                {
                    ParsedUrl = new Uri(this.toolStripComboBox_Url.Text);
                }
                catch
                {
                    return null;
                }
                return ParsedUrl;
            }
            set
            {
                if (value != null)
                    this.toolStripComboBox_Url.Text = value.ToString();
            }
        }

        private readonly WebPage webPage;
        private ITab newTab;
        readonly Context PluginContext;

        public Form_Browser(ITab newTab, Context PluginContext)
        {
            InitializeComponent();
            this.newTab = newTab;
            this.PluginContext = PluginContext;
            this.webPage = new WebPage(PluginContext);
        }

        /// <summary>
        /// Update width of the control this.toolStripComboBox_Url
        /// </summary>
        private void UpdateAddressCbWidth()
        {
            int TotalWidth = 0;
            foreach (ToolStripItem menuItem in this.menuStrip1.Items)
            {
                if (menuItem != this.toolStripComboBox_Url)
                {
                    TotalWidth += menuItem.Width;
                }
            }

            this.toolStripComboBox_Url.Width = this.menuStrip1.Size.Width - TotalWidth - 10;
        }

        public async void GoToAddress()
        {
            this.panel_ContentView.Controls.Clear();

            var TrgetUrl = this.Url;
            if(TrgetUrl != null)
            {
                // disable interface
                this.refreshToolStripMenuItem.Enabled = false;
                this.toolStripComboBox_Url.Enabled = false;
                this.MainMenuToolStrip.Enabled = false;

                // await processing
                await webPage.LoadAsync(TrgetUrl);

                if (webPage.CurrentDocument != null)
                {
                    if (!string.IsNullOrWhiteSpace(webPage.CurrentDocument.Title))
                        this.newTab.Header.Text = webPage.CurrentDocument.Title;
                    else
                        this.newTab.Header.Text = "<no title>";

                    var view = await webPage.GetViewAsync(this.panel_ContentView.Width);
                    if (view != null)
                    {
                        this.panel_ContentView.Controls.Add(view);
                    }
                }

                // enable interface
                this.refreshToolStripMenuItem.Enabled = true;
                this.toolStripComboBox_Url.Enabled = true;
                this.MainMenuToolStrip.Enabled = true;
            }
        }

        private void menuStrip1_SizeChanged(object sender, EventArgs e)
        {
            this.UpdateAddressCbWidth();
        }

        private void menuStrip1_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            this.UpdateAddressCbWidth();
        }

        private void menuStrip1_ItemRemoved(object sender, ToolStripItemEventArgs e)
        {
            this.UpdateAddressCbWidth();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.GoToAddress();
        }

        private void toolStripComboBox_Url_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.GoToAddress();
            }
        }

        private void pagePropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var forma = new Form_Properties(this.webPage);
            forma.ShowDialog();
        }
    }
}
