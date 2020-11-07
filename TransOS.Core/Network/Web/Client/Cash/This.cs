using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
