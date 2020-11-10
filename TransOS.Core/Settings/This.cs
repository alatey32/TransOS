using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Core.MainDatabase.Entity;
using TransOS.Plugin.Settings;

namespace TransOS.Core.Settings
{
    public class This
    {
        /// <summary>
        /// TransOS context
        /// </summary>
        readonly Context Os;

        public IEnumerable<ISettDirectory> Directories
        {
            get
            {
                return this.Os.MainDatabase.EntityContext.SettingsDirectory
                    .Where(x => x.ParentId == null)
                    .OrderBy(x => x.Name)
                    .Select(x => new SettDirectory(this.Os, x));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Os">TransOS context</param>
        internal This(Context Os)
        {
            this.Os = Os;
        }

        public void Fix()
        {
            var settingsService = this.GetDbService("TransOS.MainDatabase");

            if (!settingsService.Int.Exists("Version"))
            {
                int CurrentBaseVersion = 1;
                if (settingsService.Int["Version"] < CurrentBaseVersion)
                    settingsService.Int["Version"] = CurrentBaseVersion;
            }
        }

        public SettingsService GetDbService(string Path)
        {
            return new SettingsService(this.Os, SettingsServiceType.Database, Path);
        }
    }
}
