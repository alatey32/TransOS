using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.Implement.Network
{
    public class This : Plugin.Network.IThis
    {
        public Plugin.Network.Web.IThis Web { get; }

        internal This(Core.Network.This Network)
        {
            this.Web = new Web.This(Network.Web);
        }
    }
}
