using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.MainDatabase.Entity
{
    public class SettingsDirectoryParamObject
    {
        public int Id { get; set; }
        public int DirectoryId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
