using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Ridge;

namespace TransOS.Core.Ridge
{
    public class This
    {
        public readonly IRidgeObject Root;

        internal This(IRidgeObject Root)
        {
            this.Root = Root;
        }
    }
}
