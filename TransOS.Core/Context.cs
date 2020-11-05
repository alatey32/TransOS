using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core
{
    /// <summary>
    /// TransOS core context
    /// </summary>
    public class Context
    {
        public readonly ConfigSystem.This ConfigSystem;
        public readonly MainDatabase.This MainDatabase;
        public readonly Fixer.This Fixer;
        public readonly Settings.This Settings;
        public readonly OsInfo.This OsInfo;

        /// <summary>
        /// TransOS core context constructor
        /// </summary>
        public Context()
        {
            this.ConfigSystem = new ConfigSystem.This(this);
            this.MainDatabase = new MainDatabase.This(this);
            
            this.Fixer = new Fixer.This(this);
            this.Fixer.Fix();

            this.MainDatabase.Init();

            this.Settings = new Settings.This(this);
            this.Settings.Fix();

            this.OsInfo = new OsInfo.This();
        }

        public string GetFileFullPath(string FilePath)
            => Path.Combine(this.CurrentDirectory, FilePath);

        public Assembly CoreAssembly
        {
            get => Assembly.GetEntryAssembly();
        }

        public string CoreFileUri
        {
            get => this.CoreAssembly.CodeBase;
        }

        public string CoreFile
        {
            get => new Uri(this.CoreFileUri).LocalPath;
        }

        /// <summary>
        /// Core current directory
        /// </summary>
        public string CurrentDirectory
        {
            get => Path.GetDirectoryName(this.CoreFile);
        }
    }
}
