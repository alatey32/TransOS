using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TransOS.Plugin.Helper.Attr;
using TransOS.Plugin.Settings;

namespace TransOS.Core.Settings
{
    /// <summary>
    /// Serialized objects param controller (implemented)
    /// </summary>
    public class ObjectParam : ABaseParam
    {
        internal ObjectParam(SettDirectory Directory) : base(Directory)
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
            var Record = this.Directory.Os.MainDatabase.EntityContext.SettingsDirectoryParamObject.FirstOrDefault(x =>
            x.DirectoryId == this.Directory.DirectoryRecord.Id && x.Name == Name);
            if (Record != null)
            {
                if (!string.IsNullOrWhiteSpace(Record.Value))
                {
                    var newObject = JsonConvert.DeserializeObject<T>(Record.Value);
                                        
                    var type = typeof(T);
                    bool IdSetted = false;

                    // находим поля
                    // finding fields
                    foreach (var field in type.GetFields())
                    {
                        if (field.FieldType == typeof(string))
                        {
                            if (BaseMethods.Existed<SettingIdAttribute>(field))
                            {
                                if (field.FieldType == typeof(string))
                                {
                                    field.SetValue(newObject, Name);
                                    IdSetted = true;
                                }
                                break;
                            }
                        }
                    }

                    // находим свойства
                    // finding properties
                    if (!IdSetted)
                    {
                        foreach (var property in type.GetProperties())
                        {
                            if (property.PropertyType == typeof(string))
                            {
                                if (BaseMethods.Existed<SettingIdAttribute>(property))
                                {
                                    if (property.PropertyType == typeof(string))
                                    {
                                        property.SetValue(newObject, Name);
                                        IdSetted = true;
                                    }
                                    break;
                                }
                            }
                        }
                    }

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
                    DirectoryId = this.Directory.DirectoryRecord.Id,
                    Name = Name,
                    Value = JsonConvert.SerializeObject(Value)
                };
                this.Directory.Os.MainDatabase.EntityContext.SettingsDirectoryParamObject.Add(Record);
            }
            this.Directory.Os.MainDatabase.EntityContext.SaveChanges();
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
            return this.Directory.Os.MainDatabase.EntityContext.SettingsDirectoryParamObject
                .Where(x => x.DirectoryId == this.Directory.DirectoryRecord.Id)
                .Select(x => x.Name).ToArray();
        }

        private MainDatabase.Entity.SettingsDirectoryParamObject FindRecord(string Name)
        {
            return this.Directory.Os.MainDatabase.EntityContext.SettingsDirectoryParamObject.FirstOrDefault(x =>
                    x.DirectoryId == this.Directory.DirectoryRecord.Id && x.Name == Name);
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
                this.Directory.Os.MainDatabase.EntityContext.SettingsDirectoryParamObject.Remove(Record);
                this.Directory.Os.MainDatabase.EntityContext.SaveChanges();
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
