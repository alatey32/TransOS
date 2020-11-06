using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Ridge;

namespace TransOS.Core.Implement.Ridge
{
    /// <summary>
    /// Ridge controller (implemented)
    /// </summary>
    public class This : IThis
    {
        /// <summary>
        /// Root ridge object (TransOS.Core.Context)
        /// </summary>
        public IRidgeObject Root { get => this.ridge.Root; }

        /// <summary>
        /// Original Ridge controller
        /// </summary>
        private readonly Core.Ridge.This ridge;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ridge">Original Ridge controller</param>
        internal This(Core.Ridge.This ridge)
        {
            this.ridge = ridge;
        }
    }
}
