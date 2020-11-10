using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Application
{
    public interface IThis
    {
        void Register(AppRegistration appRegistration);
        void OpenObject(object ObjectToOpen);
    }
}
