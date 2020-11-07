using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.Network
{
    /// <summary>
    /// Network controller
    /// </summary>
    public class This
    {
        public readonly Web.This Web;

        internal This(TransOS.Core.Context Os)
        {
            this.Web = new Web.This(Os);
        }
    }
}
