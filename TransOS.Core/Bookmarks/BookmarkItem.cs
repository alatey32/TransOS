using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Helper.InformationTable;

namespace TransOS.Core.Bookmarks
{
    public class BookmarkItem : BaseElement<BookmarkItem>
    {
        public string Id;

        public string Text { get; set; }
        public string Command { get; set; }
        public string Description { get; set; }

        internal BookmarkItem()
        {

        }
        internal BookmarkItem(BookmarkItemSer bookmarkItemSer)
        {
            this.Id = bookmarkItemSer.Id;
            this.Text = bookmarkItemSer.Text;
            this.Command = bookmarkItemSer.Command;
            this.Description = bookmarkItemSer.Description;
        }

        public void Set(BookmarkItem item)
        {
            this.Id = item.Id;
            this.Text = item.Text;
            this.Command = item.Command;
            this.Description = item.Description;
        }

        public BookmarkItemSer Serialized()
        {
            return new BookmarkItemSer
            {
                Id = this.Id,
                Text = this.Text,
                Command = this.Command,
                Description = this.Description
            };
        }

        public override string ToString()
        {
            return $"{Text}=>{Command}";
        }
    }
}
