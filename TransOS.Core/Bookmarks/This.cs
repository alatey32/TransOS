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
    /// <summary>
    /// Bookmarks controller
    /// </summary>
    public class This : ARidgeObject, IBookmarkDirectory
    {
        readonly Context Os;
        public SettingsService settingsService { get; }

        internal This(Context Os)
        {
            this.Os = Os;

            // As rodge object
            this.Id = "F5CE8760-C180-4CBC-BD81-66FFA84D2DC4";
            this.Text = "Bookmarks";
            this.Os.Child.Add(this);

            this.RightMenu = new RidgeList(this);
            this.RightMenu.Add(new RightMenu.AddDirectory(this.Os));

            // Setting
            this.settingsService = this.Os.Settings.GetDbService("TransOS.Bookmarks");

            // load subdirs
            foreach(var SubDirFullName in this.settingsService.GetSubDirFullNames())
            {
                this.Child.Add(new BookmarkDirectory(this.Os, SubDirFullName));
            }
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
