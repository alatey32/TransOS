using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.ConfigSystem
{
    /// <summary>
    /// Database config data
    /// </summary>
    [Serializable]
    public class SerConfigDb
    {
        /// <summary>
        /// Database type
        /// </summary>        
        public string Type;

        /// <summary>
        /// Database connection string
        /// </summary>
        public string ConnectionString;

        public SerConfigDb()
        {

        }
    }
}
