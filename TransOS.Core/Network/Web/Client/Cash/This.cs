using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransOS.Plugin.Gi.Gui.Windows.Tabs;

namespace TransOS.Core.Network.Web.Client.Cash
{
    /// <summary>
    /// Web cash controller
    /// </summary>
    public class This
    {
        readonly Context Os;

        internal This(Context Os)
        {
            this.Os = Os;
        }

        #region SavedTabs

        public bool RemoveTab(int TabIndex)
        {
            var Record = this.Os.MainDatabase.EntityContext.SavedTabs.FirstOrDefault(x => x.TabIndex == TabIndex);
            if (Record == null)
            {
                this.Os.MainDatabase.EntityContext.SavedTabs.Remove(Record);

                this.Os.MainDatabase.EntityContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SaveTab(ITab tab, int TabIndex)
        {
            if (!string.IsNullOrWhiteSpace(tab.RestoreCommand))
            {
                var Record = this.Os.MainDatabase.EntityContext.SavedTabs.FirstOrDefault(x => x.TabIndex == TabIndex);
                if (Record == null)
                {
                    Record = new MainDatabase.Entity.SavedTabs
                    {
                        TabIndex = TabIndex,
                        RestoreCommand = tab.RestoreCommand,
                        Text = tab.Header.Text
                    };
                    this.Os.MainDatabase.EntityContext.SavedTabs.Add(Record);
                }
                else
                {
                    Record.RestoreCommand = tab.RestoreCommand;
                    Record.Text = tab.Header.Text;
                }

                this.Os.MainDatabase.EntityContext.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<MainDatabase.Entity.SavedTabs> GetTabsRestoreContents()
        {
            return this.Os.MainDatabase.EntityContext.SavedTabs.OrderBy(x => x.TabIndex);
        }

        #endregion

        #region LastAdrersses

        public IEnumerable<string> GetLastAdrersses()
        {
            return this.Os.MainDatabase.EntityContext.PagesUrlCash.OrderByDescending(x => x.UsesCount)
                .Select(x => x.Url);
        }

        public void AppendLastAddress(Uri Url)
        {
            var record = this.Os.MainDatabase.EntityContext.PagesUrlCash.FirstOrDefault(x => x.Url == Url.ToString());
            if (record == null)
            {
                record = new MainDatabase.Entity.PagesUrlCash
                {
                    Url = Url.ToString(),
                    UsesCount = 1
                };
                this.Os.MainDatabase.EntityContext.PagesUrlCash.Add(record);
            }
            else
                record.UsesCount++;

            this.Os.MainDatabase.EntityContext.SaveChanges();
        }

        #endregion
    }
}
