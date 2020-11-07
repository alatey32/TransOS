using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.Network.Web
{
    /// <summary>
    /// Web controller
    /// </summary>
    public class This
    {
        public readonly Client.This Client;

        internal This(Context Ось)
        {
            this.Client = new Client.This(Ось);
        }
    }
}
