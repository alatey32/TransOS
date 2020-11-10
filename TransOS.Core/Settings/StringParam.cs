using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Core.MainDatabase.Entity;

namespace TransOS.Core.Settings
{
    public class StringParam : ABaseParam
    {
        public string this[string Name]
        {
            get
            {
                var Record = this.Directory.Os.MainDatabase.EntityContext.SettingsDirectoryParamString.FirstOrDefault(x =>
                x.DirectoryId == this.Directory.DirectoryRecord.Id && x.Name == Name);
                if (Record != null)
                {
                    return Record.Value;
                }
                return null;
            }
            set
            {
                var Record = this.Directory.Os.MainDatabase.EntityContext.SettingsDirectoryParamString.FirstOrDefault(x =>
                x.DirectoryId == this.Directory.DirectoryRecord.Id && x.Name == Name);
                if (Record != null)
                {
                    // get existed
                    Record.Value = value;
                }
                else
                {
                    // creating new
                    Record = new SettingsDirectoryParamString
                    {
                        DirectoryId = this.Directory.DirectoryRecord.Id,
                        Name = Name,
                        Value = value
                    };
                    this.Directory.Os.MainDatabase.EntityContext.SettingsDirectoryParamString.Add(Record);
                }
                this.Directory.Os.MainDatabase.EntityContext.SaveChanges();
            }
        }
        
        internal StringParam(SettDirectory Directory) : base(Directory)
        {
        }

        public override IEnumerable<string> GetNames()
        {
            return this.Directory.Os.MainDatabase.EntityContext.SettingsDirectoryParamString
                .Where(x => x.DirectoryId == this.Directory.DirectoryRecord.Id)
                .Select(x => x.Name).ToArray();
        }

        private SettingsDirectoryParamString FindRecord(string Name)
        {
            return this.Directory.Os.MainDatabase.EntityContext.SettingsDirectoryParamString.FirstOrDefault(x =>
                    x.DirectoryId == this.Directory.DirectoryRecord.Id && x.Name == Name);
        }

        public override bool Remove(string Name)
        {
            var Record = this.FindRecord(Name);
            if (Record != null)
            {
                this.Directory.Os.MainDatabase.EntityContext.SettingsDirectoryParamString.Remove(Record);
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
