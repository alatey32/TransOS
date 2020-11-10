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
    /// Settings service
    /// </summary>
    public class SettingsService : SettDirectory
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Os">TransOS context</param>
        internal SettingsService(Context Os, SettingsServiceType type, string Path) : base(Os)
        {
            switch(type)
            {
                case SettingsServiceType.Database:
                    var NameParts = Path.Split('.');

                    int? ParentId = null;
                    SettingsDirectory CurrentDirectory = null;
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

                    this.Init(CurrentDirectory);
                    break;
            }
        }
    }
}
