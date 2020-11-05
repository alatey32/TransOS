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
                var Record = this.Os.MainDatabase.EntityContext.SettingsDirectoryParamString.FirstOrDefault(x =>
                x.DirectoryId == this.Service.CurrentDirectory.Id && x.Name == Name);
                if (Record != null)
                {
                    return Record.Value;
                }
                return null;
            }
            set
            {
                var Record = this.Os.MainDatabase.EntityContext.SettingsDirectoryParamString.FirstOrDefault(x =>
                x.DirectoryId == this.Service.CurrentDirectory.Id && x.Name == Name);
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
                        DirectoryId = this.Service.CurrentDirectory.Id,
                        Name = Name,
                        Value = value
                    };
                    this.Os.MainDatabase.EntityContext.SettingsDirectoryParamString.Add(Record);
                }
                this.Os.MainDatabase.EntityContext.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Os">TransOS context</param>
        /// <param name="Service"></param>
        internal StringParam(Context Os, SettingsService Service) : base(Os, Service)
        {
        }

        public override IEnumerable<string> GetNames()
        {
            return this.Os.MainDatabase.EntityContext.SettingsDirectoryParamString
                .Where(x => x.DirectoryId == this.Service.CurrentDirectory.Id)
                .Select(x => x.Name).ToArray();
        }

        private SettingsDirectoryParamString FindRecord(string Name)
        {
            return this.Os.MainDatabase.EntityContext.SettingsDirectoryParamString.FirstOrDefault(x =>
                    x.DirectoryId == this.Service.CurrentDirectory.Id && x.Name == Name);
        }

        public override bool Remove(string Name)
        {
            var Record = this.FindRecord(Name);
            if (Record != null)
            {
                this.Os.MainDatabase.EntityContext.SettingsDirectoryParamString.Remove(Record);
                this.Os.MainDatabase.EntityContext.SaveChanges();
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
