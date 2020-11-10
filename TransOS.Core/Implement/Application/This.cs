using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Application;

namespace TransOS.Core.Implement.Application
{
    public class This : IThis
    {
        readonly TransOS.Core.Application.This application;

        internal This(TransOS.Core.Application.This application)
        {
            this.application = application;
        }

        public void Register(AppRegistration appRegistration)
            => this.application.Register(appRegistration);

        public void OpenObject(object ObjectToOpen)
            => this.application.OpenObject(ObjectToOpen);
    }
}
