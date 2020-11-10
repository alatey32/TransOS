using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.Helper
{
    /// <summary>
    /// Helper controller
    /// </summary>
    public class This
    {
        internal This()
        {

        }

        public string GetRandomGuidString()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
