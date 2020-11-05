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

            // Add first page
            this.Engine.WebTab.Add();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Engine.WebTab.Add();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Engine.WebTab.Remove();
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
