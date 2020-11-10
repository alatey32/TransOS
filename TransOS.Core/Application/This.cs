using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Application;

namespace TransOS.Core.Application
{
    public class This
    {
        readonly List<AppRegistration> Registrations = new List<AppRegistration>();

        internal This()
        {

        }

        public void Register(AppRegistration appRegistration)
        {
            this.Registrations.Add(appRegistration);
        }

        public void OpenObject(object ObjectToOpen)
        {
            // detecting app on Main or Base types
            var ObjectType = ObjectToOpen?.GetType();
            AppRegistration app = null;
            do
            {
                app = this.Registrations.FirstOrDefault(x => x.ObjectType == ObjectType);

                ObjectType = ObjectType.BaseType;
            }
            while (app == null && ObjectType != null);

            // Open
            app?.OpenObject(ObjectToOpen);
        }
    }
}
