using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Core.MainDatabase.Entity;

namespace TransOS.Core.Settings
{
    public class StringItems
    {
        public string this[int Index]
        {
            get
            {
                var Record = this.Os.MainDatabase.EntityContext.SettingsDirectoryParamStringsItem.Where(x => x.ParamId == this.ParamId).Skip(Index).FirstOrDefault();
                return Record?.Value;
            }
            set
            {
                var Record = this.Os.MainDatabase.EntityContext.SettingsDirectoryParamStringsItem.Where(x => x.ParamId == this.ParamId).Skip(Index).FirstOrDefault();
                if (Record != null)
                {
                    // get existed
                    Record.Value = value;
                }
                else
                    throw new ArgumentOutOfRangeException("Index");
                this.Os.MainDatabase.EntityContext.SaveChanges();
            }
        }

        public int Count
        {
            get => this.Os.MainDatabase.EntityContext.SettingsDirectoryParamStringsItem.Where(x => x.ParamId == this.ParamId).Count();
        }

        /// <summary>
        /// TransOS context
        /// </summary>
        readonly Context Os;

        readonly int ParamId;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Os">TransOS context</param>
        /// <param name="ParamId"></param>
        internal StringItems(Context Os, int ParamId)
        {
            this.Os = Os;
            this.ParamId = ParamId;
        }

        public void Add(string StringValue)
        {
            var NewRecord = new SettingsDirectoryParamStringsItem
            {
                ParamId = this.ParamId,
                Value = StringValue
            };
            this.Os.MainDatabase.EntityContext.SettingsDirectoryParamStringsItem.Add(NewRecord);
            this.Os.MainDatabase.EntityContext.SaveChanges();
        }

        public bool RemoveAt(int Index)
        {
            var Record = this.Os.MainDatabase.EntityContext.SettingsDirectoryParamStringsItem.Where(x => x.ParamId == this.ParamId).Skip(Index).FirstOrDefault();
            if (Record != null)
            {
                this.Os.MainDatabase.EntityContext.SettingsDirectoryParamStringsItem.Remove(Record);
                this.Os.MainDatabase.EntityContext.SaveChanges();
                return true;
            }
            return false;
        }

        public void Clear()
        {
            var Records = this.Os.MainDatabase.EntityContext.SettingsDirectoryParamStringsItem.Where(x => x.ParamId == this.ParamId);
            this.Os.MainDatabase.EntityContext.SettingsDirectoryParamStringsItem.RemoveRange(Records);
            this.Os.MainDatabase.EntityContext.SaveChanges();
        }
    }
}
