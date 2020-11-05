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
                var Record = this.Os.MainDatabase.EntityContext.SettingsDirectoryParamStrings.FirstOrDefault(x =>
                    x.DirectoryId == this.Service.CurrentDirectory.Id && x.Name == Name);
                if (Record == null)
                {
                    // создаём новый
                    Record = new SettingsDirectoryParamStrings
                    {
                        DirectoryId = this.Service.CurrentDirectory.Id,
                        Name = Name
                    };
                    this.Os.MainDatabase.EntityContext.SettingsDirectoryParamStrings.Add(Record);
                    this.Os.MainDatabase.EntityContext.SaveChanges();
                }
                return new StringItems(this.Os, Record.Id);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Os">TransOS context</param>
        /// <param name="Service"></param>
        internal StringsParam(Context Os, SettingsService Service) : base(Os, Service)
        {
        }

        public override IEnumerable<string> GetNames()
        {
            return this.Os.MainDatabase.EntityContext.SettingsDirectoryParamStrings
                .Where(x => x.DirectoryId == this.Service.CurrentDirectory.Id)
                .Select(x => x.Name).ToArray();
        }

        private SettingsDirectoryParamStrings FindRecord(string Name)
        {
            return this.Os.MainDatabase.EntityContext.SettingsDirectoryParamStrings.FirstOrDefault(x =>
                    x.DirectoryId == this.Service.CurrentDirectory.Id && x.Name == Name);
        }

        public override bool Remove(string Name)
        {
            var Record = this.FindRecord(Name);
            if (Record != null)
            {
                this.Os.MainDatabase.EntityContext.SettingsDirectoryParamStrings.Remove(Record);
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
