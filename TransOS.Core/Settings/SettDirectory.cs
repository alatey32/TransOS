using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Core.MainDatabase.Entity;
using TransOS.Plugin.Settings;

namespace TransOS.Core.Settings
{
    /// <summary>
    /// Abstract settings directory.
    /// </summary>
    public class SettDirectory : ISettDirectory
    {
        /// <summary>
        /// Directory name
        /// </summary>
        public string Name { get => this.settingsDirectory.Name; }

        /// <summary>
        /// Sub directories
        /// </summary>
        public IEnumerable<ISettDirectory> Directories
        {
            get
            {
                return this.Os.MainDatabase.EntityContext.SettingsDirectory
                    .Where(x => x.ParentId == this.settingsDirectory.Id)
                    .OrderBy(x => x.Name)
                    .Select(x => new SettDirectory(this.Os, x));
            }
        }

        /// <summary>
        /// TransOS context
        /// </summary>
        readonly Context Os;

        /// <summary>
        /// Settings directory DB record
        /// </summary>
        internal readonly SettingsDirectory settingsDirectory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Os">TransOS context</param>
        /// <param name="settingsDirectory">Settings directory DB record</param>
        internal SettDirectory(Context Os, SettingsDirectory settingsDirectory)
        {
            this.Os = Os;
            this.settingsDirectory = settingsDirectory;
        }
    }
}
