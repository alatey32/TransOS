using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Core.MainDatabase.Entity;

namespace TransOS.Core.Settings
{
    public class SettingsService
    {
        /// <summary>
        /// Access to string values
        /// </summary>
        public StringParam String { get; }

        /// <summary>
        /// Access to string list values
        /// </summary>
        public StringsParam Strings { get; }

        /// <summary>
        /// Access to Int values
        /// </summary>
        public IntParam Int { get; }

        /// <summary>
        /// Access to serialyzed object values
        /// </summary>
        public ObjectParam Object { get; }

        /// <summary>
        /// Settings directory DB record
        /// </summary>
        internal readonly SettingsDirectory CurrentDirectory = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Os">TransOS context</param>
        /// <param name="CurrentDirectory">Settings directory DB record</param>
        internal SettingsService(Context Os, SettingsDirectory CurrentDirectory)
        {
            this.CurrentDirectory = CurrentDirectory;

            this.String = new StringParam(Os, this);
            this.Strings = new StringsParam(Os, this);
            this.Int = new IntParam(Os, this);
            this.Object = new ObjectParam(Os, this);
        }
    }
}
