using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Network.Web.Client.Cash
{
    public interface IThis
    {
        IEnumerable<string> GetLastAdrersses();

        void AppendLastAddress(Uri Url);
    }
}
