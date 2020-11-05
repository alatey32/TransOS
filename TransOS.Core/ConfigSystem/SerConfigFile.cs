using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.ConfigSystem
{
    /// <summary>
    /// Config file
    /// </summary>
    [Serializable]
    public class SerConfigFile
    {
        /// <summary>
        /// Database config data
        /// </summary>
        public SerConfigDb Db;

        public SerConfigFile()
        {

        }
    }
}
