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
            var settingsService = this.GetService("TransOS.MainDatabase");

            if (!settingsService.Int.Exists("Version"))
            {
                int CurrentBaseVersion = 1;
                if (settingsService.Int["Version"] < CurrentBaseVersion)
                    settingsService.Int["Version"] = CurrentBaseVersion;
            }
        }

        public SettingsService GetService(string PluginIdName)
        {
            var NameParts = PluginIdName.Split('.');

            SettingsDirectory CurrentDirectory = null;
            int? ParentId = null;
            foreach (var namePart in NameParts)
            {
                ParentId = CurrentDirectory?.Id;
                CurrentDirectory = this.Os.MainDatabase.EntityContext.SettingsDirectory.FirstOrDefault(x => x.Name == namePart && x.ParentId == ParentId);

                if (CurrentDirectory == null)
                {
                    CurrentDirectory = new SettingsDirectory
                    {
                        Name = namePart,
                        ParentId = ParentId
                    };

                    this.Os.MainDatabase.EntityContext.SettingsDirectory.Add(CurrentDirectory);
                    this.Os.MainDatabase.EntityContext.SaveChanges();
                }
            }

            return new SettingsService(this.Os, CurrentDirectory);
        }

        public SettingsService GetService(SettDirectory settDirectory)
        {
            return new SettingsService(this.Os, settDirectory.settingsDirectory);
        }

    }
}
