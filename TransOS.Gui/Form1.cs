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

            this.Text = $"TransOS v{this.Engine.AppVersion.Major}.{this.Engine.AppVersion.Minor}";

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
    }
}
