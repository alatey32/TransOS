using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.Implement
{
    /// <summary>
    /// TransOS context implemented for plugin
    /// </summary>
    public class Context : Plugin.IContext
    {
        /*private IPlugin ThisPlugin_ = null;

        /// <summary>
        /// Плагин, которому передан контекст. Может быть задан только один раз.
        /// </summary>
        public IPlugin ThisPlugin
        {
            get => this.ThisPlugin_;
            set
            {
                if (this.ThisPlugin_ == null)
                    this.ThisPlugin_ = value;
                else
                    throw new Exception("Плагин, которому передан контекст, может быть задан только один раз.");
            }
        }


        /// <summary>
        /// Основное состояние операционной системы
        /// </summary>
        public OsState OsState { get => this.Os.OsState; }

        /// <summary>
        /// Изменено основное состояние операционной системы
        /// </summary>
        public event Action<OsState> OsStateChanged
        {
            add => this.Os.OsStateChanged += value;
            remove => this.Os.OsStateChanged -= value;
        }*/
                
        public Plugin.Network.IThis Network { get; }
        //public Plugin.ObjectFactory.IThis ObjectFactory { get; }
        public Plugin.Settings.IThis Settings { get; }
        public Plugin.Ridge.IThis Ridge { get; }
        /*public Plugin.Log.IThis Log { get; }
        public Plugin.MainMenu.IThis MainMenu { get; }
        public Plugin.IThis Plugins { get; private set; }
        public Plugin.Autorun.IThis Autorun { get; }
        public Plugin.HotKeys.IThis HotKeys { get; }*/
        public Plugin.Gi.IThis Gi { get; }
        public Plugin.Helper.IThis Helper { get; }
        public Plugin.Application.IThis Application { get; }
        /*public Plugin.ParentOs.IThis ParentOs { get; }
        public Plugin.FileSystem.IThis FileSystem { get; }
        public Plugin.Converter.IThis Converter { get; }*/

        /// <summary>
        /// Original TransOS context
        /// </summary>
        internal readonly Core.Context Os;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Os">Original TransOS context</param>
        public Context(Core.Context Os)
        {
            this.Os = Os;

            this.Network = new Network.This(Os.Network);
            /*this.ObjectFactory = new ObjectFactory.This(Os.ObjectFactory);*/
            this.Settings = new Settings.This(this);
            this.Ridge = new Ridge.This(Os.Ridge);
            /*this.Log = new Log.This(Os.Log);
            this.MainMenu = new MainMenu.This(Os.MainMenu);
            this.Plugins = new Plugins.This(Os.Plugins);
            this.Autorun = new Autorun.This(Os.Autorun);
            this.HotKeys = new HotKeys.This(this);*/
            this.Gi = new Gi.This(Os.Gi);
            this.Helper = new Helper.This(Os.Helper);
            this.Application = new Application.This(Os.Application);
            /*this.ParentOs = new ParentOs.This(Os.ParentOs);
            this.FileSystem = new FileSystem.This(Os.FileSystem);
            this.Converter = new Converter.This(Os.Converter);*/
        }

        /*public void AddObject(object SharedObject)
        {
            this.Os.AddObject(SharedObject);
        }

        public bool RemoveObject(object SharedObject)
        {
            return this.Os.RemoveObject(SharedObject);
        }

        public T GetObject<T>()
            where T : class
        {
            return this.Os.GetObject<T>();
        }*/
    }
}
