using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Settings;

namespace TransOS.Core.Implement.Settings
{
    /// <summary>
    /// Settings controller implemented
    /// </summary>
    public class This : IThis
    {
        readonly Context Os;

        /// <summary>
        /// Settings subdirectories
        /// </summary>
        public IEnumerable<ISettDirectory> Directories
        {
            get => this.Os.Os.Settings.Directories;
        }

        /*
        /// <summary>
        /// Default settings service for this plugin
        /// </summary>
        private ISettingsService Default_ = null;

        /// <summary>
        /// Default settings service for this plugin
        /// </summary>
        public ISettingsService Default
        {
            get
            {
                if (this.Default_ == null)
                    this.Default_ = this.GetService();
                return this.Default_;
            }
        }*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Os">TransOS original context</param>
        internal This(Context Os)
        {
            this.Os = Os;
        }

        // <para>Со временем убрать</para>

        /// <summary>
        /// Get settings service
        /// </summary>
        /// <param name="PluginIdName">Plugin id or entry path name</param>
        /// <returns></returns>
        public ISettingsService GetService(string PluginIdName)
        {
            return new SettingsService(this.Os.Os.Settings.GetService(PluginIdName));
        }

        /*
        /// <summary>
        /// получить сервис настроек
        /// </summary>
        /// <returns></returns>
        public ISettingsService GetService()
        {
            return this.GetService(this.Os.ThisPlugin.Id);
        }*/

        /// <summary>
        /// Get settings service from setings directory
        /// </summary>
        /// <param name="settDirectory"></param>
        /// <returns></returns>
        public ISettingsService GetService(ISettDirectory settDirectory)
        {
            return new SettingsService(this.Os.Os.Settings.GetService((Core.Settings.SettDirectory)settDirectory));
        }
    }
}
