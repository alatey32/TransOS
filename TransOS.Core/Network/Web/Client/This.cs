using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.Network.Web.Client
{
    /// <summary>
    /// Web client controller
    /// </summary>
    public class This
    {
        readonly Context Os;
        public readonly Cash.This Cash;

        internal This(Context Os)
        {
            this.Os = Os;
            this.Cash = new Cash.This(this.Os);
        }
    }
}
