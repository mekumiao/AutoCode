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
        internal Setting setting { get; private set; }

        internal SettingManger()
        {
            var basepath = Directory.GetCurrentDirectory();
            var path = Path.Combine(basepath, "./setting.json");

            using (var reader = new StreamReader(path))
            {
                var jsonstr = reader.ReadToEnd();
                setting = JsonConvert.DeserializeObject<Setting>(jsonstr);

                setting.tempdir = PathHander(basepath, setting.tempdir);
                setting.datadir = PathHander(basepath, setting.datadir);
                setting.outdir = PathHander(basepath, setting.outdir);

                setting.data.ForEach(x => x.outdir = PathHander(basepath, x.outdir, setting.outdir));
            }
        }

        /// <summary>
        /// 相对路径处理
        /// </summary>
        /// <param name="basepath"></param>
        /// <param name="path"></param>
        /// <param name="glpath"></param>
        /// <returns></returns>
        protected string PathHander(string basepath, string path, string glpath = "")
        {
            var result = default(string);
            if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
            {
                result = glpath;
            }
            else if (!Path.IsPathRooted(path))
            {
                result = Path.Combine(basepath, path);
            }
            return result;
        }

        public class Setting
        {
            public string tempdir { get; set; }

            public string datadir { get; set; }

            public string outdir { get; set; }

            public List<Data> data { get; set; }
        }

        public class Data
        {
            public string tempname { get; set; }

            public string filename { get; set; }

            public string outdir { get; set; }

            public List<string> dataname { get; set; }
        }

    }
}
