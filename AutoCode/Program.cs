using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TempTop;

namespace AutoCode
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Create();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生异常:{ex.Message}!!!");
            }
            Console.Write("按Enter键退出!!!");
            Console.Read();
        }

        public static void Create()
        {
            var set = new SettingManger().setting;
            var tempdict = new Dictionary<string, ITempBuild>();
            var build = default(ITempBuild);

            //编译模板
            foreach (var item in set.data)
            {
                var temppath = Path.Combine(set.tempdir, item.tempname);

                if (!File.Exists(temppath)) throw new Exception($"模板文件不存在:{Path.GetFileName(temppath)}");

                build = new TempManager(temppath).GetTempBuild();
                Console.WriteLine($"模板编译成功! {Path.GetFileName(temppath)}");
                tempdict.Add(item.tempname, build);
            }

            //全局输出文件夹是否存在
            if (!Directory.Exists(set.outdir))
                Directory.CreateDirectory(set.outdir);

            //输出结果
            foreach (var item in set.data)
            {
                foreach (var dtname in item.dataname)
                {
                    var datapath = Path.Combine(set.datadir, dtname);
                    bool issuccess = tempdict.TryGetValue(item.tempname, out build);
                    if (issuccess)
                    {
                        if (!File.Exists(datapath))
                        {
                            Console.WriteLine($"数据文件不存在:{Path.GetFileName(datapath)}");
                            break;
                        }
                        //加载数据
                        build.LoadFromFile(datapath);
                        Console.WriteLine($"数据加载成功! {Path.GetFileName(datapath)}");
                        //生成代码
                        var code = build.Execute();
                        var outpath = Path.Combine(item.outdir, string.Format(item.filename, Path.GetFileNameWithoutExtension(dtname)));
                        using (var writer = new StreamWriter(outpath))
                        {
                            writer.Write(code);
                            Console.WriteLine($"代码生成成功! {Path.GetFileName(outpath)}");
                        }
                    }
                }
            }

        }

    }
}
