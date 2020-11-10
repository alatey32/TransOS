using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Helper;

namespace TransOS.Core.Implement.Helper
{
    public class This : IThis
    {
        readonly Core.Helper.This helper;

        internal This(Core.Helper.This helper)
        {
            this.helper = helper;
        }

        public string GetRandomGuidString() => this.helper.GetRandomGuidString();
    }
}
