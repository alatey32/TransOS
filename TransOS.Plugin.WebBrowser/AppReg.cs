using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Application;

namespace TransOS.Plugin.WebBrowser
{
    internal class AppReg : AppRegistration
    {
        readonly Context context;

        internal AppReg(Context context) : base(typeof(Uri), AppActionType.Exec)
        {
            this.context = context;
        }

        public override void OpenObject(object ObjectToOpen)
        {
            this.context.OpenUrl(ObjectToOpen as Uri);
            this.context.LastForm_Browser?.GoToAddress();
        }
    }
}
