using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.MainDatabase
{
    /// <summary>
    /// Main database
    /// </summary>
    public class This
    {
        /// <summary>
        /// OS context
        /// </summary>
        private readonly Context Os;

        /// <summary>
        /// SQL connection
        /// </summary>
        private SQLiteConnection Connection { get; set; }

        private bool Connected
        {
            get
            {
                if (this.Connection != null)
                    return this.Connection.State == System.Data.ConnectionState.Open;
                return false;
            }
        }

        public Entity.Context EntityContext { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Os">TransOS context</param>
        internal This(Context Os)
        {
            this.Os = Os;
        }

        /// <summary>
        /// Initialize entity DB context
        /// </summary>
        public void Init()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Entity.Context>();
            optionsBuilder.UseSqlite(this.Os.ConfigSystem.Data.Db.ConnectionString);

            this.EntityContext = new Entity.Context(optionsBuilder.Options);
        }

        /// <summary>
        /// Connect to DB as SQL
        /// </summary>
        private void Connect()
        {
            if (!this.Connected)
                this.Disconnect();

            this.Connection = new SQLiteConnection(this.Os.ConfigSystem.Data.Db.ConnectionString);
            this.Connection.Open();
        }

        /// <summary>
        /// Disconnect to DB as SQL
        /// </summary>
        private void Disconnect() => this.Connection?.Close();

        private int ExecuteNonQuery(string sql)
        {
            var command = new SQLiteCommand(sql, this.Connection);
            return command.ExecuteNonQuery();
        }
        private ArrayList GetTables()
        {
            var list = new ArrayList();

            // executes query that select names of all tables in master table of the database
            String query = "SELECT name FROM sqlite_master " +
                    "WHERE type = 'table'" +
                    "ORDER BY 1";
            try
            {

                var table = GetDataTable(query);

                // Return all table names in the ArrayList

                foreach (DataRow row in table.Rows)
                {
                    list.Add(row.ItemArray[0].ToString());
                }
            }
            catch // (Exception e)
            {
            }
            return list;
        }

        private DataTable GetDataTable(string sql)
        {
            try
            {
                var dt = new DataTable();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, this.Connection))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        dt.Load(rdr);
                        return dt;
                    }
                }
            }
            catch// (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Fix database tables
        /// </summary>
        public void Fix()
        {
            // Create DB file if not exists
            var sb = new SqliteConnectionStringBuilder(this.Os.ConfigSystem.Data.Db.ConnectionString);
            if (!File.Exists(sb.DataSource))
                SQLiteConnection.CreateFile(sb.DataSource);

            // Connect to DB (file)
            this.Connect();

            var AllTables = GetTables();

            // fix tables


            if(!AllTables.Contains("SettingsDirectory"))
                this.ExecuteNonQuery("CREATE TABLE 'SettingsDirectory' ('Id' INTEGER NOT NULL UNIQUE, 'ParentId' INTEGER, 'Name'  TEXT NOT NULL, FOREIGN KEY('ParentId') REFERENCES 'SettingsDirectory'('Id') ON DELETE CASCADE, PRIMARY KEY('Id' AUTOINCREMENT));");

            if (!AllTables.Contains("SettingsDirectoryParamInt"))
                this.ExecuteNonQuery("CREATE TABLE 'SettingsDirectoryParamInt' ('Id' INTEGER NOT NULL, 'DirectoryId' INTEGER NOT NULL, 'Name' TEXT NOT NULL, 'Value' INTEGER DEFAULT 0, FOREIGN KEY('DirectoryId') REFERENCES 'SettingsDirectory'('Id') ON DELETE CASCADE, PRIMARY KEY('Id' AUTOINCREMENT));");

            if (!AllTables.Contains("SettingsDirectoryParamObject"))
                this.ExecuteNonQuery("CREATE TABLE 'SettingsDirectoryParamObject' ( 'Id' INTEGER NOT NULL UNIQUE, 'DirectoryId' INTEGER NOT NULL, 'Name' TEXT NOT NULL, 'Value' TEXT, FOREIGN KEY('DirectoryId') REFERENCES 'SettingsDirectory'('Id') ON DELETE CASCADE, PRIMARY KEY('Id' AUTOINCREMENT));");

            if (!AllTables.Contains("SettingsDirectoryParamString"))
                this.ExecuteNonQuery("CREATE TABLE 'SettingsDirectoryParamString' ( 'Id' INTEGER NOT NULL UNIQUE, 'DirectoryId' INTEGER NOT NULL, 'Name' TEXT NOT NULL, 'Value' TEXT, FOREIGN KEY('DirectoryId') REFERENCES 'SettingsDirectory'('Id') ON DELETE CASCADE, PRIMARY KEY('Id' AUTOINCREMENT));");

            if (!AllTables.Contains("SettingsDirectoryParamStrings"))
                this.ExecuteNonQuery("CREATE TABLE 'SettingsDirectoryParamStrings' ( 'Id' INTEGER NOT NULL UNIQUE, 'DirectoryId' INTEGER NOT NULL, 'Name' TEXT NOT NULL, FOREIGN KEY('DirectoryId') REFERENCES 'SettingsDirectory'('Id') ON DELETE CASCADE, PRIMARY KEY('Id' AUTOINCREMENT));");

            if (!AllTables.Contains("SettingsDirectoryParamStringsItem"))
                this.ExecuteNonQuery("CREATE TABLE 'SettingsDirectoryParamStringsItem' ( 'Id' INTEGER NOT NULL UNIQUE, 'ParamId' INTEGER NOT NULL, 'Value' TEXT NOT NULL, FOREIGN KEY('ParamId') REFERENCES 'SettingsDirectoryParamStrings'('Id') ON DELETE CASCADE, PRIMARY KEY('Id' AUTOINCREMENT));");

            if (!AllTables.Contains("PagesUrlCash"))
                this.ExecuteNonQuery("CREATE TABLE 'PagesUrlCash' ( 'Id' INTEGER NOT NULL UNIQUE, 'Url' TEXT NOT NULL UNIQUE, 'UsesCount' INTEGER NOT NULL DEFAULT 0, PRIMARY KEY('Id' AUTOINCREMENT))");

            if (!AllTables.Contains("SavedTabs"))
                this.ExecuteNonQuery("CREATE TABLE 'SavedTabs' ( 'Id' INTEGER NOT NULL UNIQUE, 'TabIndex' INTEGER NOT NULL UNIQUE, 'RestoreCommand' TEXT NOT NULL, 'Text' TEXT, PRIMARY KEY('Id' AUTOINCREMENT))");



            // Disconnect to DB (file)
            this.Disconnect();
        }
    }
}
