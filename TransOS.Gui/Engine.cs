using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransOS.Gui
{
    /// <summary>
    /// Application engine
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// TransOS context
        /// </summary>
        public readonly Core.Context Os;

        public readonly WebTab.This WebTab;

        public readonly TransOS.Plugin.WebBrowser.Context WebBrowser;

        public readonly Form1 Mainform;

        internal Engine(Form1 Mainform)
        {
            this.Mainform = Mainform;
            this.Os = new Core.Context();

            this.WebTab = new WebTab.This(this);
            this.WebBrowser = new Plugin.WebBrowser.Context();
        }
    }
}
