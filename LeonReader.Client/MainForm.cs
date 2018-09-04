using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

using LeonReader.Common;
using LeonReader.AbstractSADE;

namespace LeonReader.Client
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //输出程序集内定义的类型全名称
            Console.WriteLine($"程序集内定义的类型：\n\t{string.Join("\t\n", Assembly.LoadFrom("GamerSkySADE.dll").DefinedTypes.Select(type=>type.FullName))}");
            Console.WriteLine($"全局配置-下载目录：{ConfigHelper.GetConfigHelper.DownloadDirectory}");
            
            //TODO: 反射创建 SADE 时对所有的 Type 使用 Lambda 筛选继承名称和基类：基类筛选ASDE类型、名称筛选一个dll内的多组ASDE；
            Scanner scanner = Activator.CreateInstanceFrom("GamerSkySADE.dll", "GamerSkySADE.GamerSkyScanner").Unwrap() as Scanner;
            scanner.Process();

            Analyzer analyzer = Activator.CreateInstanceFrom("GamerSkySADE.dll", "GamerSkySADE.GamerSkyAnalyzer").Unwrap() as Analyzer;
            analyzer.SetTargetURI(@"https://www.gamersky.com/ent/201808/1094495.shtml");
            analyzer.Process();
        }

    }
}
