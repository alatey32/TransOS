using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.OsInfo
{
    /// <summary>
    /// Base info about TransOS core
    /// </summary>
    public class This
    {
        internal This()
        {

        }

        /// <summary>
        /// Entry assembly
        /// </summary>
        public Assembly CoreAssembly
        {
            get => Assembly.GetExecutingAssembly();
        }

        /// <summary>
        /// Application current version
        /// </summary>
        public Version Version
        {
            get => this.CoreAssembly.GetName().Version;
        }

        public string GetFileFullPath(string FilePath)
            => Path.Combine(this.CurrentDirectory, FilePath);

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
