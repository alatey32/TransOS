using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Settings
{
    public interface IObjectParam : IBaseParam
    {
        T Get<T>(string Name)
            where T : class;

        void Set<T>(string Name, T Value)
           where T : class;

        void Set<T>(T Value)
            where T : class;
    }
}
