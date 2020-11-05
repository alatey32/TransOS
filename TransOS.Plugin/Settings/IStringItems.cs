using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Settings
{
    public interface IStringItems
    {
        string this[int Index] { get; set; }
        int Count { get; }

        void Add(string StringValue);
        bool RemoveAt(int Index);
        void Clear();
    }
}
