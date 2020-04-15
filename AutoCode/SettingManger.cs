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
            var path = "./setting.json";

            using (var reader = new StreamReader(path))
            {
                var jsonstr = reader.ReadToEnd();
                setting = JsonConvert.DeserializeObject<Setting>(jsonstr);

                setting.data.ForEach(x =>
                {
                    if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
                    {
                        x.outdir = setting.outdir;
                    }
                });
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
            /// <summary>
            /// 模板文件夹
            /// </summary>
            public string tempdir { get; set; }

            /// <summary>
            /// 数据文件夹
            /// </summary>
            public string datadir { get; set; }

            /// <summary>
            /// 全局输出目录
            /// </summary>
            public string outdir { get; set; }

            /// <summary>
            /// 数据\模板 对应配置
            /// </summary>
            public List<Data> data { get; set; }
        }

        public class Data
        {
            /// <summary>
            /// 模板名称
            /// </summary>
            public string tempname { get; set; }

            /// <summary>
            /// 输出文件名
            /// {0} 表示 数据文件名称(不包含后缀名)
            /// </summary>
            public string filename { get; set; }

            /// <summary>
            /// 输出目录 (如果目录不存在,则使用全局目录)
            /// </summary>
            public string outdir { get; set; }

            /// <summary>
            /// 数据文件名 (包含后缀名)
            /// </summary>
            public List<string> dataname { get; set; }
        }

    }
}
