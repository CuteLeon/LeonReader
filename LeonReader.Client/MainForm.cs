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
using LeonDirectUI.Container;
using LeonReader.Client.Factory;
using LeonReader.Client.DirectUI.Container;
using LeonReader.ArticleContentManager;

namespace LeonReader.Client
{
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {

        #region 变量

        ArticleManager articleManager = new ArticleManager();

        /// <summary>
        /// 反射工厂
        /// </summary>
        AssemblyFactory assemblyFactory = new AssemblyFactory();

        /// <summary>
        /// SADE 工厂
        /// </summary>
        SADEFactory sadeFactory = new SADEFactory();

        /// <summary>
        /// 卡片工厂
        /// </summary>
        CardContainerFactory cardFactory = new CardContainerFactory();

        #endregion


        Assembly GS_ASDE;
        Type ScannerType;
        Scanner scanner;
        Type AnalyzerType;
        Analyzer analyzer;
        Type DownloaderType;
        Downloader downloader;
        Type ExporterType;
        Exporter exporter;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string DownloadDirectory = ConfigHelper.GetConfigHelper.DownloadDirectory;
            if (!IOHelper.DirectoryExists(DownloadDirectory))
            {
                LogHelper.Info($"正在创建下载目录：{DownloadDirectory}");
                try
                {
                    IOHelper.CreateDirectory(DownloadDirectory);
                }
                catch (Exception ex)
                {
                    LogHelper.Error($"创建下载失败：{ex.Message}");
                    MessageBox.Show($"无法创建下载目录，886~\n{ex.Message}");
                    Application.Exit();
                }
            }

            GS_ASDE = AssemblyHelper.CreateAssembly("GamerSkySADE.dll");
            if (GS_ASDE == null)
            {
                LogHelper.Fatal("创建程序集反射失败，终止");
                MessageBox.Show("创建程序集反射失败，终止");
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshCatalogList();
            return;

            ScannerType = GS_ASDE.GetSubTypes(typeof(Scanner)).FirstOrDefault();
            if (ScannerType == null)
            {
                LogHelper.Fatal("未发现程序集内存在扫描器类型，终止");
                return;
            }

            scanner = GS_ASDE.CreateInstance(ScannerType) as Scanner;
            scanner.ProcessStarted += (s, v) => { this.Invoke(new Action(() => { button1.Enabled = false; button2.Enabled = false; button3.Enabled = false; button4.Enabled = false; })); };
            scanner.ProcessReport += (s, v) => { this.Text = $"已扫描：{v.ProgressPercentage} 篇文章"; };
            scanner.ProcessCompleted += (s, v) => { this.Text = $"{this.Text} - [扫描完成]"; button1.Enabled = true; button2.Enabled = true; };
            scanner.Process();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AnalyzerType = GS_ASDE.GetSubTypes(typeof(Analyzer)).FirstOrDefault();
            if (ScannerType == null)
            {
                LogHelper.Fatal("未发现程序集内存在分析器类型，终止");
                return;
            }

            analyzer = GS_ASDE.CreateInstance(AnalyzerType) as Analyzer;
            analyzer.ProcessStarted += (s, v) => { this.Invoke(new Action(() => { button2.Enabled = false; button3.Enabled = false; button4.Enabled = false; })); };
            analyzer.ProcessReport += (s, v) => { this.Text = $"已分析：{v.ProgressPercentage} 页，{(int)v.UserState} 图"; };
            analyzer.ProcessCompleted += (s, v) => { this.Text = $"{this.Text} - [分析完成]"; button2.Enabled = true; button3.Enabled = true; };
            analyzer.SetTargetURI(@"https://www.gamersky.com/ent/201809/1096176.shtml");
            analyzer.Process();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DownloaderType = GS_ASDE.GetSubTypes(typeof(Downloader)).FirstOrDefault();
            if (DownloaderType == null)
            {
                LogHelper.Fatal("未发现程序集内存在下载器类型，终止");
                return;
            }

            downloader = GS_ASDE.CreateInstance(DownloaderType) as Downloader;
            downloader.ProcessStarted += (s, v) => { this.Invoke(new Action(() => { button2.Enabled = false; button3.Enabled = false; button4.Enabled = false; })); };
            downloader.ProcessReport += (s, v) => { this.Text = $"已下载：{v.ProgressPercentage} 张图片，{(int)v.UserState} 张失败"; };
            downloader.ProcessCompleted += (s, v) => { this.Text = $"{this.Text} - [下载完成]"; button2.Enabled = true; button3.Enabled = true; button4.Enabled = true; };
            downloader.SetTargetURI(@"https://www.gamersky.com/ent/201809/1096176.shtml");
            downloader.Process();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ExporterType = GS_ASDE.GetSubTypes(typeof(Exporter)).FirstOrDefault();
            if (ExporterType == null)
            {
                LogHelper.Fatal("未发现程序集内存在导出器类型，终止");
                return;
            }

            exporter = GS_ASDE.CreateInstance(ExporterType) as Exporter;
            exporter.ProcessStarted += (s, v) => { this.Invoke(new Action(() => { button2.Enabled = false; button3.Enabled = false; button4.Enabled = false; })); };
            exporter.ProcessReport += (s, v) => { this.Text = $"已导出：{v.ProgressPercentage} / {(int)v.UserState} 张图片"; };
            exporter.ProcessCompleted += (s, v) => { this.Text = $"{this.Text} - [导出完成]"; button2.Enabled = true; button3.Enabled = true; button4.Enabled = true; };
            exporter.SetTargetURI(@"https://www.gamersky.com/ent/201809/1096176.shtml");
            exporter.Process();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            WebBrowser browser = new WebBrowser();
            form.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
            browser.Navigate(@"F:\C Sharp\LeonReader\Debug\Articles\201809051640044034\日本30岁的女装大佬 这么娇小可爱竟然是男人.html");
            form.ShowDialog();
        }


        #region 扫描目录

        /// <summary>
        /// 刷新目录列表
        /// </summary>
        private void RefreshCatalogList()
        {
            LogHelper.Info("刷新目录列表...");

            //清空现有目录列表
            while (CatalogLayoutPanel.Controls.Count > 0)
            {
                CatalogLayoutPanel.Controls[0].Dispose();
            }

            //扫描目录
            ScanCatalog(Application.StartupPath);
            //TODO: Scanner.Process() 是异步方法，加载需要等待扫描完成后再进行：Scanner.ProcessCompleted += (s, e) => { //使用选项卡单独加载此扫描器的文章 }
            //加载目录
            foreach (var card in LoadCatalog())
            {
                CatalogLayoutPanel.Controls.Add(card);
            }
        }

        /// <summary>
        /// 使用目录内所有扫描器扫描目录
        /// </summary>
        private void ScanCatalog(string directoryPath)
        {
            LogHelper.Info("使用所有扫描器扫描目录...");
            //遍历符合条件的链接库
            foreach (var assembly in assemblyFactory.CreateAssemblys(
                directoryPath,
                path => path.ToUpper().EndsWith("SADE.DLL")))
            {
                if (assembly == null) continue;

                //遍历程序集内的扫描器
                foreach (var scanner in sadeFactory.CreateScanners(assembly))
                {
                    if (scanner == null) continue;

                    LogHelper.Info($"发现扫描器：{scanner.ASDESource} in {assembly.FullName}");
                    scanner.Process();
                }
            }
        }

        /// <summary>
        /// 加载目录
        /// </summary>
        private IEnumerable<CardContainer> LoadCatalog()
        {
            //巨幅加载新文章
            foreach (var article in articleManager.GetNewArticles())
            {
                if (article == null) continue;

                yield return cardFactory.CreateLargeCard(
                    article.Title, 
                    article.Description,
                    article.PublishTime
                    );
            }

            //foreach (var article in TargetDBContext.Articles.Where(article=>!article.IsNew && article.ExportTime!=null))
            {

            }
        }

        #endregion

    }
}
