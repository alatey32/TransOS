using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransOS.Core.Settings;
using TransOS.Plugin.Ridge;

namespace TransOS.Core.Bookmarks
{
    public class BookmarkDirectory : ARidgeObject, IBookmarkDirectory
    {
        public SettingsService settingsService { get; }

        readonly Context Os;

        internal BookmarkDirectory(Context Os, string DirectoryName)
        {
            this.Os = Os;
            this.settingsService = this.Os.Settings.GetDbService(DirectoryName);

            this.CanEditText = true;
            this.Text = this.settingsService.Name;
            this.RightMenu = new RidgeList(this);
            this.RightMenu.Add(new RightMenu.AddDirectory(this.Os));
            this.RightMenu.Add(new RightMenu.RenameDirectory(this.Os));
            this.RightMenu.Add(new RightMenu.RemoveDirectory(this.Os));

            // load subdirs
            foreach (var SubDirFullName in this.settingsService.GetSubDirFullNames())
            {
                this.Child.Add(new BookmarkDirectory(this.Os, SubDirFullName));
            }

            this.TextEdited += BookmarkDirectory_TextEdited;
            this.Removed += BookmarkDirectory_Removed;
        }

        private void BookmarkDirectory_Removed(IRidgeObject obj)
        {
            this.settingsService.Remove();
                
        }

        private void BookmarkDirectory_TextEdited(string NewName)
        {
            this.settingsService.Name = NewName;
        }

        public override Control View
        {
            get
            {
                var infTable = new View.InfoTable(this.Os, this.settingsService);
                var control = infTable.Панель;
                infTable.ОбновитьИзБазы();
                return control;
            }
            protected set => base.View = value;
        }
    }
}
