using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransOS.Core.Settings;
using TransOS.Plugin.Helper.InformationTable;
using TransOS.Plugin.Helper.InformationTable.Attr;

namespace TransOS.Core.Bookmarks.View
{
    public class InfoTable : Таблица<BookmarkItem>
    {
        readonly Context Os;
        readonly SettingsService settingsService;

        public InfoTable(Context Os, SettingsService settingsService)
        {
            this.Os = Os;
            this.settingsService = settingsService;
        }

        [Функция(ПоУмолчанию: true)]
        public void Open(BookmarkItem bookmarkItem)
        {
            var Url = new Uri(bookmarkItem.Command);
            this.Os.Application.OpenObject(Url);
        }


        [Функция(Подпись: "Add")]
        public void AddBookmark()
        {
            var forma = new Form_AddBookmark(this.Os);
            if(forma.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var NewItem = forma.bookmarkItem;
                this.settingsService.Object.Set(NewItem.Serialized()); // TODO: Add enshore
                this.ДобавитьЗапись(NewItem);
            }
        }

        [Функция(Подпись: "Remove")]
        public void RemoveBookmark(BookmarkItem bookmarkItem)
        {
            if(MessageBox.Show($"Remove bookmark?\r\n{bookmarkItem}", "Removing bookmark", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.settingsService.Object.Remove(bookmarkItem.Id);
                this.УдалитьЗапись(bookmarkItem);
            }
        }

        [Функция(Подпись: "Edit")]
        public void EditBookmark(BookmarkItem bookmarkItem)
        {
            var forma = new Form_AddBookmark(this.Os)
            {
                Text = "Edit bookmark",
                bookmarkItem = bookmarkItem
            };
            if (forma.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var Id = bookmarkItem.Id;
                bookmarkItem.Set(forma.bookmarkItem);
                bookmarkItem.Id = Id;
                this.settingsService.Object.Set(bookmarkItem.Serialized());
                this.ОбновитьЗапись(bookmarkItem);
            }
        }

        public override void ОбновитьИзБазы()
        {
            this.Записи.Clear();

            foreach(var BookmarkName in this.settingsService.Object.GetNames())
            {
                var BookmarkDeserialized = this.settingsService.Object.Get<BookmarkItemSer>(BookmarkName);
                this.Записи.Add(new BookmarkItem(BookmarkDeserialized));
            }

            base.ОбновитьИзБазы();
        }
    }
}
