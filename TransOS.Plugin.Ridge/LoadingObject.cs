using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Ridge
{
    internal class LoadingObject : ARidgeObject
    {
        public readonly string LoadingText = "Loading...";
        public readonly string NoItemsText = "<none>";

        public bool Loading
        {
            get => this.Text == this.LoadingText;
            set => this.Text = value ? this.LoadingText : this.NoItemsText;
        }

        internal LoadingObject()
        {
            this.Loading = true;
        }
    }
}
