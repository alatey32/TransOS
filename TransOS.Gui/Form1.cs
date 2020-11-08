using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransOS.Gui
{
    public partial class Form1 : Form
    {
        readonly Engine Engine;

        public Form1()
        {
            InitializeComponent();

            this.Engine = new Engine(this);

            var version = this.Engine.Os.OsInfo.Version;
            this.Text = $"TransOS v{version.Major}.{version.Minor}";


            // Restore saved tabs
            foreach(var Record in this.Engine.Os.Network.Web.Client.Cash.GetTabsRestoreContents())
            {
                this.Engine.WebBrowser.OpenUrl(new Uri(Record.RestoreCommand), Record.Text);
            }

            if (this.Engine.Os.Gi.Gui.Windows.Tabs.All.Count() == 0)
            {
                // Add first page
                this.addToolStripMenuItem_Click(null, null);
            }

            // Load active tab ???
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Engine.WebBrowser.OpenUrl(new Uri("https://github.com/alatey32/TransOS"));
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Engine.Os.Gi.Gui.Windows.Tabs.Current?.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void queryToCodeQueryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var forma = new DevTools.Form_QueryToCodeQuery();
            forma.ShowDialog();
        }
    }
}
