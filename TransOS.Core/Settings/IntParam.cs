﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.Settings
{
    /// <summary>
    /// Integer values param controller
    /// </summary>
    public class IntParam : ABaseParam
    {
        /// <summary>
        /// Set or get value by Name
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns></returns>
        public int this[string Name]
        {
            get
            {
                var Record = this.Os.MainDatabase.EntityContext.SettingsDirectoryParamInt.FirstOrDefault(x =>
                x.DirectoryId == this.Service.CurrentDirectory.Id && x.Name == Name);
                if (Record != null)
                {
                    return Record.Value;
                }
                return -1;
            }
            set
            {
                var Record = this.Os.MainDatabase.EntityContext.SettingsDirectoryParamInt.FirstOrDefault(x =>
                x.DirectoryId == this.Service.CurrentDirectory.Id && x.Name == Name);
                if (Record != null)
                {
                    // get existed
                    Record.Value = value;
                }
                else
                {
                    // creating new
                    Record = new MainDatabase.Entity.SettingsDirectoryParamInt
                    {
                        DirectoryId = this.Service.CurrentDirectory.Id,
                        Name = Name,
                        Value = value
                    };
                    this.Os.MainDatabase.EntityContext.SettingsDirectoryParamInt.Add(Record);
                }
                this.Os.MainDatabase.EntityContext.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Os">TransOS context</param>
        /// <param name="Service">SettingsService database record</param>
        internal IntParam(Context Os, SettingsService Service) : base(Os, Service)
        {
        }

        /// <summary>
        /// Get parameter names
        /// </summary>
        /// <returns>Parameter names</returns>
        public override IEnumerable<string> GetNames()
        {
            return this.Os.MainDatabase.EntityContext.SettingsDirectoryParamInt
                .Where(x => x.DirectoryId == this.Service.CurrentDirectory.Id)
                .Select(x => x.Name).ToArray();
        }

        /// <summary>
        /// Find setting database record by Name
        /// </summary>
        /// <param name="Name">'Name' field for search</param>
        /// <returns>Setting database record</returns>
        private MainDatabase.Entity.SettingsDirectoryParamInt FindRecord(string Name)
        {
            return this.Os.MainDatabase.EntityContext.SettingsDirectoryParamInt.FirstOrDefault(x =>
                    x.DirectoryId == this.Service.CurrentDirectory.Id && x.Name == Name);
        }

        /// <summary>
        /// Delete parameter by name
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns>Found and deleted</returns>
        public override bool Remove(string Name)
        {
            var Record = this.FindRecord(Name);
            if (Record != null)
            {
                this.Os.MainDatabase.EntityContext.SettingsDirectoryParamInt.Remove(Record);
                this.Os.MainDatabase.EntityContext.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Is parameter exists by name
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns>Existed</returns>
        public override bool Exists(string Name)
        {
            return this.FindRecord(Name) != null;
        }
    }
}
