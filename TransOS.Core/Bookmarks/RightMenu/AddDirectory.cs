using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Ridge;

namespace TransOS.Core.Bookmarks.RightMenu
{
    internal class AddDirectory : ARidgeObject
    {
        readonly Context Os;

        internal AddDirectory(Context Os)
        {
            this.Os = Os;
            this.Text = "Add directory";
        }

        public override void Run(params object[] args)
        {
            var bookmarkDirectoryParent = this.Parent as IBookmarkDirectory;

            if(bookmarkDirectoryParent != null)
            {
                string NewName = "NewDirectory";
                var SdName = bookmarkDirectoryParent.settingsService.FullName;
                SdName += "." + NewName;

                var NewDirectory = new BookmarkDirectory(this.Os, SdName);

                ((ARidgeObject)bookmarkDirectoryParent).Child.Add(NewDirectory);
            }
        }
    }
}
