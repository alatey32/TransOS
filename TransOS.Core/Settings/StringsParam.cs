using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Core.MainDatabase.Entity;

namespace TransOS.Core.Settings
{
    public class StringsParam : ABaseParam
    {
        public StringItems this[string Name]
        {
            get
            {
                var Record = this.Directory.Os.MainDatabase.EntityContext.SettingsDirectoryParamStrings.FirstOrDefault(x =>
                    x.DirectoryId == this.Directory.DirectoryRecord.Id && x.Name == Name);
                if (Record == null)
                {
                    // создаём новый
                    Record = new SettingsDirectoryParamStrings
                    {
                        DirectoryId = this.Directory.DirectoryRecord.Id,
                        Name = Name
                    };
                    this.Directory.Os.MainDatabase.EntityContext.SettingsDirectoryParamStrings.Add(Record);
                    this.Directory.Os.MainDatabase.EntityContext.SaveChanges();
                }
                return new StringItems(this.Directory.Os, Record.Id);
            }
        }

        
        internal StringsParam(SettDirectory Directory) : base(Directory)
        {
        }

        public override IEnumerable<string> GetNames()
        {
            return this.Directory.Os.MainDatabase.EntityContext.SettingsDirectoryParamStrings
                .Where(x => x.DirectoryId == this.Directory.DirectoryRecord.Id)
                .Select(x => x.Name).ToArray();
        }

        private SettingsDirectoryParamStrings FindRecord(string Name)
        {
            return this.Directory.Os.MainDatabase.EntityContext.SettingsDirectoryParamStrings.FirstOrDefault(x =>
                    x.DirectoryId == this.Directory.DirectoryRecord.Id && x.Name == Name);
        }

        public override bool Remove(string Name)
        {
            var Record = this.FindRecord(Name);
            if (Record != null)
            {
                this.Directory.Os.MainDatabase.EntityContext.SettingsDirectoryParamStrings.Remove(Record);
                this.Directory.Os.MainDatabase.EntityContext.SaveChanges();
                return true;
            }
            return false;
        }

        public override bool Exists(string Name)
        {
            return this.FindRecord(Name) != null;
        }
    }
}
