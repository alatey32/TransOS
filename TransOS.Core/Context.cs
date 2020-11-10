using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Ridge;

namespace TransOS.Core
{
    /// <summary>
    /// TransOS core context
    /// </summary>
    public class Context : ARidgeObject
    {
        public readonly ConfigSystem.This ConfigSystem;
        public readonly MainDatabase.This MainDatabase;
        public readonly Fixer.This Fixer;
        public readonly Settings.This Settings;
        public readonly OsInfo.This OsInfo;
        public readonly Ridge.This Ridge;
        public readonly Gi.This Gi;
        public readonly Network.This Network;
        public readonly Bookmarks.This Bookmarks;
        public readonly Helper.This Helper;
        public readonly Application.This Application;

        /// <summary>
        /// TransOS core context constructor
        /// </summary>
        public Context()
        {
            this.ConfigSystem = new ConfigSystem.This(this);
            this.MainDatabase = new MainDatabase.This(this);
            this.OsInfo = new OsInfo.This();

            this.Fixer = new Fixer.This(this);
            this.Fixer.Fix();

            this.MainDatabase.Init();

            this.Settings = new Settings.This(this);
            this.Settings.Fix();

            // Ridge
            this.Ridge = new Ridge.This(this);
            this.Id = "fa5ac5a7-f151-419b-ac1e-c878326f54ff";
            this.Text = "TransOS";

            this.Gi = new Gi.This(this);
            this.Network = new Network.This(this);
            this.Bookmarks = new Bookmarks.This(this);
            this.Helper = new Helper.This();
            this.Application = new Application.This();
        }
    }
}
