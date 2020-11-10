using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.MainDatabase.Entity
{
    public class SavedTabs
    {
        public int Id { get; set; }
        public string TabId { get; set; }
        public string RestoreCommand { get; set; }
        public string Text { get; set; }
    }
}
