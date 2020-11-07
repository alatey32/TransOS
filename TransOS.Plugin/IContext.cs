using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin
{
    /// <summary>
    /// OS context interface for plugin
    /// </summary>
    public interface IContext
    {
        /*/// <summary>
        /// Плагин, которому передан контекст. Может быть задан только один раз.
        /// </summary>
        IPlugin ThisPlugin { get; set; }

        /// <summary>
        /// Основное состояние операционной системы
        /// </summary>
        OsState OsState { get; }

        /// <summary>
        /// Изменено основное состояние операционной системы
        /// </summary>
        event Action<OsState> OsStateChanged;

        /// <summary>
        /// Сеть
        /// </summary>
        Network.IThis Network { get; }
        ObjectFactory.IThis ObjectFactory { get; }*/
        Settings.IThis Settings { get; }
        Ridge.IThis Ridge { get; }
        /*Log.IThis Log { get; }
        MainMenu.IThis MainMenu { get; }
        Plugin.IThis Plugins { get; }
        Autorun.IThis Autorun { get; }
        HotKeys.IThis HotKeys { get; }*/
        Gi.IThis Gi { get; }
        /*Helper.IThis Helper { get; }
        Application.IThis Application { get; }
        ParentOs.IThis ParentOs { get; }
        FileSystem.IThis FileSystem { get; }
        Converter.IThis Converter { get; }

        void AddObject(object SharedObject);

        bool RemoveObject(object SharedObject);

        T GetObject<T>()
            where T : class;*/
    }
}
