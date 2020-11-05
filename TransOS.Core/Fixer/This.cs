using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.Fixer
{
    /// <summary>
    /// Fix all errors system
    /// </summary>
    public class This
    {
        /// <summary>
        /// TransOS context
        /// </summary>
        private readonly Context Os;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Os">TransOS context</param>
        internal This(Context Os)
        {
            this.Os = Os;
        }

        /// <summary>
        /// Fix all errors
        /// </summary>
        public void Fix()
        {
            this.Os.ConfigSystem?.Fix();
            this.Os.MainDatabase?.Fix();
            this.Os.Settings?.Fix();
        }
    }
}
