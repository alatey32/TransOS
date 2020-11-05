using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.MainDatabase.Entity
{
    public class SettingsDirectoryParamInt
    {
        public int Id { get; set; }
        public int DirectoryId { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
