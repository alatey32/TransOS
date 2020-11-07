using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.MainDatabase.Entity
{
    public class PagesUrlCash
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int UsesCount { get; set; }
    }
}
