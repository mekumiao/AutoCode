using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCode
{
    internal class SettingManger
    {
        internal setting Setting { get; private set; }

        internal SettingManger()
        {
            var path = "./setting.json";
            using (var reader = new StreamReader(path))
            {
                var jsonstr = reader.ReadToEnd();
                Setting = JsonConvert.DeserializeObject<setting>(jsonstr);
                var list = Setting.data.Where(x => string.IsNullOrWhiteSpace(x.outdir) || !Directory.Exists(x.outdir)).ToList();
                if (list != null || list.Any())
                {
                    list?.ForEach(x => x.outdir = Setting.outdir);
                }
            }
        }


        public class setting
        {
            public string tempdir { get; set; }

            public string datadir { get; set; }

            public string outdir { get; set; }

            public List<data> data { get; set; }
        }

        public class data
        {
            public string tempname { get; set; }

            public string filename { get; set; }

            public string outdir { get; set; }

            public List<string> dataname { get; set; }
        }

    }
}
