using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.MainDatabase.Entity
{
    /// <summary>
    /// Entity DB context
    /// </summary>
    public class Context : DbContext
    {
        /*public DbSet<NetworkIp> NetworkIp { get; set; }
        public DbSet<NetworkDnsLayerList> NetworkDnsLayerList { get; set; }
        public DbSet<NetworkDnsLayer> NetworkDnsLayer { get; set; }
        public DbSet<NetworkDnsLayerZone> NetworkDnsLayerZone { get; set; }
        public DbSet<NetworkHost> NetworkHost { get; set; }
        public DbSet<NetworkMainhost> NetworkMainhost { get; set; }
        public DbSet<NetworkServer> NetworkServer { get; set; }
        public DbSet<NetworkServerService> NetworkServerService { get; set; }
        public DbSet<TransOsUserAccount> TransOsUserAccount { get; set; }*/

        #region SettingsDirectory

        public DbSet<SettingsDirectory> SettingsDirectory { get; set; }
        public DbSet<SettingsDirectoryParamString> SettingsDirectoryParamString { get; set; }
        public DbSet<SettingsDirectoryParamInt> SettingsDirectoryParamInt { get; set; }
        public DbSet<SettingsDirectoryParamObject> SettingsDirectoryParamObject { get; set; }
        public DbSet<SettingsDirectoryParamStrings> SettingsDirectoryParamStrings { get; set; }
        public DbSet<SettingsDirectoryParamStringsItem> SettingsDirectoryParamStringsItem { get; set; }

        #endregion SettingsDirectory

        /*public DbSet<Plugin> Plugin { get; set; }
        public DbSet<Autorun> Autorun { get; set; }
        public DbSet<PagesUrlCash> PagesUrlCash { get; set; }*/
        internal Context(DbContextOptions options): base(options)
        {

        }
    }
}
