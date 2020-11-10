using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Core.MainDatabase.Entity;
using TransOS.Plugin.Settings;

namespace TransOS.Core.Settings
{
    public class SettDirectory : ISettDirectory
    {
        /// <summary>
        /// Directory name
        /// </summary>
        public string Name
        {
            get => this.DirectoryRecord.Name;
            set
            {
                this.DirectoryRecord.Name = value;
                this.Os.MainDatabase.EntityContext.SaveChanges();
            }
        }

        public string FullName
        {
            get
            {
                var result = new List<string>();

                var cd = this.DirectoryRecord;

                do
                {
                    result.Insert(0, cd.Name);
                    cd = this.Os.MainDatabase.EntityContext.SettingsDirectory.FirstOrDefault(x => x.Id == cd.ParentId);
                }
                while (cd != null);

                return string.Join(".", result);
            }
        }

        /// <summary>
        /// Sub directories
        /// </summary>
        public IEnumerable<SettDirectory> Directories
        {
            get => this.GetSubDirNames().Select(x => new SettDirectory(this, x));
        }
        
        /// <summary>
        /// Access to string values
        /// </summary>
        public StringParam String { get; private set; }

        /// <summary>
        /// Access to string list values
        /// </summary>
        public StringsParam Strings { get; private set; }

        /// <summary>
        /// Access to Int values
        /// </summary>
        public IntParam Int { get; private set; }

        /// <summary>
        /// Access to serialyzed object values
        /// </summary>
        public ObjectParam Object { get; private set; }

        /// <summary>
        /// TransOS context
        /// </summary>
        internal Context Os { get; }

        /// <summary>
        /// Settings directory DB record
        /// </summary>
        internal SettingsDirectory DirectoryRecord { get; private set; }

        protected readonly SettDirectory Parent;

        /// <summary>
        /// Constructor for SettingsService
        /// </summary>
        /// <param name="Os"></param>
        protected SettDirectory(Context Os)
        {
            this.Os = Os;
        }

        /// <summary>
        /// Independent constructor, with parent
        /// </summary>
        internal SettDirectory(SettDirectory Parent, string SubDirectory) : this(Parent.Os)
        {            
            this.Parent = Parent;

            var NameParts = SubDirectory.Split('.');
            int? ParentId = null;
            SettingsDirectory CurrentDirectory = this.Parent.DirectoryRecord;
            foreach (var namePart in NameParts)
            {
                ParentId = CurrentDirectory?.Id;
                CurrentDirectory = this.Os.MainDatabase.EntityContext.SettingsDirectory.FirstOrDefault(x => x.Name == namePart && x.ParentId == ParentId);

                if (CurrentDirectory == null)
                {
                    CurrentDirectory = new SettingsDirectory
                    {
                        Name = namePart,
                        ParentId = ParentId
                    };

                    this.Os.MainDatabase.EntityContext.SettingsDirectory.Add(CurrentDirectory);
                    this.Os.MainDatabase.EntityContext.SaveChanges();
                }
            }

            this.Init(CurrentDirectory);
        }

        internal SettDirectory(Context Os, SettingsDirectory DirectoryRecord) : this(Os)
        {
            this.Init(DirectoryRecord);
        }

            protected void Init(SettingsDirectory DirectoryRecord)
        {
            if (this.DirectoryRecord == null)
            {
                this.DirectoryRecord = DirectoryRecord;

                this.String = new StringParam(this);
                this.Strings = new StringsParam(this);
                this.Int = new IntParam(this);
                this.Object = new ObjectParam(this);
            }
        }

        public IEnumerable<string> GetSubDirNames()
        {
            return this.Os.MainDatabase.EntityContext.SettingsDirectory
                .Where(x => x.ParentId == this.DirectoryRecord.Id)
                .Select(x => x.Name);
        }

        public IEnumerable<string> GetSubDirFullNames()
        {
            return this.GetSubDirNames().Select(x => $"{this.FullName}.{x}");
        }

        /// <summary>
        /// Delete settings directory and subdirectories
        /// </summary>
        public void Remove()
        {
            this.Remove(this.DirectoryRecord);            
        }

        private void Remove(SettingsDirectory settingsDirectory)
        {
            // remove childs
            foreach(var child in this.Os.MainDatabase.EntityContext.SettingsDirectory
                .Where(x => x.ParentId == settingsDirectory.Id))
            {
                this.Remove(child);
            }

            // remove parameters
            this.Os.MainDatabase.EntityContext.SettingsDirectoryParamInt.RemoveRange(
                this.Os.MainDatabase.EntityContext.SettingsDirectoryParamInt.Where(x => x.DirectoryId == settingsDirectory.Id)
            );

            // remove directpry
            this.Os.MainDatabase.EntityContext.SettingsDirectory.Remove(settingsDirectory);
            this.Os.MainDatabase.EntityContext.SaveChanges();
        }
    }
}
