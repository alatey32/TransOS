using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Application
{
    public abstract class AppRegistration
    {
        public readonly Type ObjectType;
        public readonly AppActionType ActionType;

        public AppRegistration(Type ObjectType, AppActionType ActionType)
        {
            this.ObjectType = ObjectType;
            this.ActionType = ActionType;
        }

        public virtual void OpenObject(object ObjectToOpen)
        {
            throw new NotImplementedException();
        }
    }
}
