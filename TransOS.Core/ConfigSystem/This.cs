using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.ConfigSystem
{
    public class This
    {
        public readonly string ConfigFileName = "transos.conf.json";
        public readonly string DefaultDbFileName = "transos.db";

        /// <summary>
        /// Default encoding - UTF8
        /// </summary>
        public readonly Encoding DefaultEncoding = Encoding.UTF8;

        public string AbsoluteConfigFileName
        {
            get => this.Os.GetFileFullPath(this.ConfigFileName);
        }

        public SerConfigFile Data
        {
            get
            {
                string AbsoluteConfigFileName = this.AbsoluteConfigFileName;

                if (!File.Exists(AbsoluteConfigFileName))
                    this.Fix();

                if (File.Exists(AbsoluteConfigFileName))
                    return JsonConvert.DeserializeObject<SerConfigFile>(File.ReadAllText(AbsoluteConfigFileName));

                return null;
            }
        }

        /// <summary>
        /// TransOS context
        /// </summary>
        private readonly Context Os;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Os">TransOS context</param>
        internal This(Context Os)
        {
            this.Os = Os;
        }

        public void Fix()
        {
            string AbsoluteConfigFileName = this.AbsoluteConfigFileName;
            
            // Create cinfig if not exists
            if(!File.Exists(AbsoluteConfigFileName))
            {
                var SerConfig = new SerConfigFile
                {
                     Db = new SerConfigDb
                     {
                          Type = "SQLite",
                          ConnectionString = $"Data Source={this.DefaultDbFileName};"
                     }
                };

                string SerConfigString = JsonConvert.SerializeObject(SerConfig);

                File.WriteAllText(AbsoluteConfigFileName, SerConfigString, this.DefaultEncoding);
            }
        }
    }
}
