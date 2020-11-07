using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Network.Web;

namespace TransOS.Core.Implement.Network.Web
{
    public class This : IThis
    {
        public Plugin.Network.Web.Client.IThis Client { get; }

        internal This(Core.Network.Web.This Веб)
        {
            this.Client = new Client.This(Веб.Client);
        }
    }
}
