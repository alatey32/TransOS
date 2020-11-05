using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Settings
{
    /// <summary>
    /// String items values param controller (implemented) interface
    /// </summary>
    public interface IStringItems
    {
        /// <summary>
        /// Get or set value by Index
        /// </summary>
        /// <param name="Index">Zero based index of item</param>
        /// <returns></returns>
        string this[int Index] { get; set; }

        /// <summary>
        /// Items count
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Add value to items list
        /// </summary>
        /// <param name="StringValue"></param>
        void Add(string StringValue);

        /// <summary>
        /// Remove item at Index
        /// </summary>
        /// <param name="Index">Index of item</param>
        /// <returns></returns>
        bool RemoveAt(int Index);

        /// <summary>
        /// Clear items (remove all)
        /// </summary>
        void Clear();
    }
}
