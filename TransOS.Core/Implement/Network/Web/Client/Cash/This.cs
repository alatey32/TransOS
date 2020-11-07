using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Network.Web.Client.Cash;

namespace TransOS.Core.Implement.Network.Web.Client.Cash
{
    public class This : IThis
    {
        readonly TransOS.Core.Network.Web.Client.Cash.This Cash;

        internal This(TransOS.Core.Network.Web.Client.Cash.This Cash)
        {
            this.Cash = Cash;
        }

        public IEnumerable<string> GetLastAdrersses() => this.Cash.GetLastAdrersses();
                
        public void AppendLastAddress(Uri Url) => this.Cash.AppendLastAddress(Url);
    }
}
