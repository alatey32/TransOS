using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.Implement.Settings
{
    /// <summary>
    /// Serialized objects param controller (implemented)
    /// </summary>
    public class ObjectParam : BaseParam, Plugin.Settings.IObjectParam
    {
        /// <summary>
        /// Original serialized objects param controller
        /// </summary>
        readonly Core.Settings.ObjectParam objectParam;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectParam">Original serialized objects param controller</param>
        internal ObjectParam(Core.Settings.ObjectParam objectParam) : base(objectParam)
        {
            this.objectParam = objectParam;
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
            return this.objectParam.Get<T>(Name);
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
            this.objectParam.Set<T>(Name, Value);
        }

        /// <summary>
        /// Set object by Name (Тame is specified by attribute SettingIdAttribute for property or field)
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="Value">Object for set</param>
        public void Set<T>(T Value)
            where T : class
        {
            this.objectParam.Set<T>(Value);
        }
    }
}
