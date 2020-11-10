using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Ridge;

namespace TransOS.Core.Bookmarks.RightMenu
{
    internal class RenameDirectory : ARidgeObject
    {
        readonly Context Os;

        internal RenameDirectory(Context Os)
        {
            this.Os = Os;
            this.Text = "Rename";
        }

        public override void Run(params object[] args)
        {
            var ParentAsRidge = this.Parent as IRidgeObject;
            ParentAsRidge?.BeginUserEdit();
        }
    }
}
