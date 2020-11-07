using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Network.Web.Client;

namespace TransOS.Core.Implement.Network.Web.Client
{
    public class This : IThis
    {
        readonly TransOS.Core.Network.Web.Client.This Client;
        public TransOS.Plugin.Network.Web.Client.Cash.IThis Cash { get; }

        internal This(TransOS.Core.Network.Web.Client.This Client)
        {
            this.Client = Client;
            this.Cash = new Cash.This(this.Client.Cash);
        }
    }
}
