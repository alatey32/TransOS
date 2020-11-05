using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Core.Helper.Attributes;
using TransOS.Plugin.Settings;

namespace TransOS.Core.Settings
{
    /// <summary>
    /// Serialized objects param controller (implemented)
    /// </summary>
    public class ObjectParam : ABaseParam
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Os">TransOS context</param>
        /// <param name="Service"></param>
        internal ObjectParam(Context Os, SettingsService Service) : base(Os, Service)
        {
        }

        /// <summary>
        /// Get object by Name
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="Name">Object setting name</param>
        /// <returns></returns>
        public T Get<T>(string Name)
            where T : class
        {
            var Record = this.Os.MainDatabase.EntityContext.SettingsDirectoryParamObject.FirstOrDefault(x =>
            x.DirectoryId == this.Service.CurrentDirectory.Id && x.Name == Name);
            if (Record != null)
            {
                if (!string.IsNullOrWhiteSpace(Record.Value))
                {
                    var newObject = JsonConvert.DeserializeObject<T>(Record.Value);
                    return newObject;
                }
            }
            return null;
        }

        /// <summary>
        /// Set object by Name
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="Name">Object setting name</param>
        /// <param name="Value">Object for set</param>
        public void Set<T>(string Name, T Value)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("The argument cannot be empty", "Name");

            var Record = this.FindRecord(Name);
            if (Record != null)
            {
                // get existed
                Record.Value = JsonConvert.SerializeObject(Value);
            }
            else
            {
                // creating new
                Record = new MainDatabase.Entity.SettingsDirectoryParamObject
                {
                    DirectoryId = this.Service.CurrentDirectory.Id,
                    Name = Name,
                    Value = JsonConvert.SerializeObject(Value)
                };
                this.Os.MainDatabase.EntityContext.SettingsDirectoryParamObject.Add(Record);
            }
            this.Os.MainDatabase.EntityContext.SaveChanges();
        }

        /// <summary>
        /// Set object by Name (Тame is specified by attribute SettingIdAttribute for property or field)
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="Value">Object for set</param>
        public void Set<T>(T Value)
            where T : class
        {
            string Name = null;
            var type = typeof(T);

            // находим поля
            // finding fields
            foreach (var field in type.GetFields())
            {
                if (field.FieldType == typeof(string))
                {
                    if (BaseMethods.Existed<SettingIdAttribute>(field))
                    {
                        Name = (string)field.GetValue(Value);
                        break;
                    }
                }
            }

            // находим свойства
            // finding properties
            if (Name == null)
            {
                foreach (var property in type.GetProperties())
                {
                    if (property.PropertyType == typeof(string))
                    {
                        if (BaseMethods.Existed<SettingIdAttribute>(property))
                        {
                            Name = (string)property.GetValue(Value);
                            break;
                        }
                    }
                }
            }

            // set a named value
            // задаём именованое значение
            this.Set<T>(Name, Value);
        }

        /// <summary>
        /// Get parameter names
        /// </summary>
        /// <returns>Parameter names</returns>
        public override IEnumerable<string> GetNames()
        {
            return this.Os.MainDatabase.EntityContext.SettingsDirectoryParamObject
                .Where(x => x.DirectoryId == this.Service.CurrentDirectory.Id)
                .Select(x => x.Name).ToArray();
        }

        private MainDatabase.Entity.SettingsDirectoryParamObject FindRecord(string Name)
        {
            return this.Os.MainDatabase.EntityContext.SettingsDirectoryParamObject.FirstOrDefault(x =>
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
                this.Os.MainDatabase.EntityContext.SettingsDirectoryParamObject.Remove(Record);
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
