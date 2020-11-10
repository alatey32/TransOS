using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Gi.Gui.Windows.Tabs
{
    /// <summary>
    /// Tab in the GUI window
    /// </summary>
    public interface ITab
    {
        string Id { get; set; }

        /// <summary>
        /// Is the menu item activated
        /// </summary>
        bool IsSelected { get; }

        /// <summary>
        /// Tab title
        /// </summary>
        ITabHeader Header { get; }

        /// <summary>
        /// Tab body
        /// </summary>
        ITabBody Body { get; }

        /// <summary>
        /// Switch to this tab
        /// </summary>
        void Select();

        /// <summary>
        /// Event before closing a tab
        /// </summary>
        event Action<ITab> Closing;

        // int ZIndex { get; }

        /// <summary>
        /// Close tab
        /// </summary>
        void Close();

        /// <summary>
        /// Command to save and restore tab with equals content
        /// </summary>
        string RestoreCommand { get; set; }
    }
}
