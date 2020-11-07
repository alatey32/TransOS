using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Gi.Gui.Windows.Tabs
{
    /// <summary>
    /// GUI tabs controller (implemented) interface
    /// </summary>
    public interface IThis
    {
        bool IsSupported { get; }

        /// <summary>
        /// Add tab
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        ITab Add(string Text);

        /// <summary>
        /// Close tab
        /// </summary>
        /// <param name="tab"></param>
        void Close(ITab tab);
    }
}
