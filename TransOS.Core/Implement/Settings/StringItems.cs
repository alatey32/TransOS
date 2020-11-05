using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.Implement.Settings
{
    /// <summary>
    /// String items values param controller (implemented)
    /// </summary>
    public class StringItems : Plugin.Settings.IStringItems
    {
        /// <summary>
        /// Get or set value by Index
        /// </summary>
        /// <param name="Index">Zero based index of item</param>
        /// <returns></returns>
        public string this[int Index]
        {
            get => this.stringItems[Index];
            set => this.stringItems[Index] = value;
        }

        /// <summary>
        /// Items count
        /// </summary>
        public int Count { get => this.stringItems.Count; }

        /// <summary>
        /// Original string items values param controller
        /// </summary>
        readonly Core.Settings.StringItems stringItems;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StringItems">Original string items values param controller</param>
        internal StringItems(Core.Settings.StringItems StringItems)
        {
            this.stringItems = StringItems;
        }

        /// <summary>
        /// Add value to items list
        /// </summary>
        /// <param name="StringValue"></param>
        public void Add(string StringValue) => this.stringItems.Add(StringValue);

        /// <summary>
        /// Remove item at Index
        /// </summary>
        /// <param name="Index">Index of item</param>
        /// <returns></returns>
        public bool RemoveAt(int Index) => this.stringItems.RemoveAt(Index);

        /// <summary>
        /// Clear items (remove all)
        /// </summary>
        public void Clear() => this.stringItems.Clear();

    }
}
