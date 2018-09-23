using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using LeonReader.AbstractSADE;
using LeonReader.ArticleContentManager;
using LeonReader.Client.Factory;
using LeonReader.Common;

namespace LeonReader.Client
{
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {

        #region 变量

        /// <summary>
        /// 文章管理器
        /// </summary>
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

        /// <summary>
        /// 目录Tab容器与内流式布局容器对应关系
        /// </summary>
        Dictionary<TabPage, FlowLayoutPanel> PanelInTabPage = new Dictionary<TabPage, FlowLayoutPanel>();

        #endregion

        #region 初始化

        public MainForm()
        {
            this.Icon = UnityResource.LeonReader;

            //初始化布局
            InitializeComponent();

            //绑定事件
            BindEvent();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            //适应工具容器位置
            UnityToolContainer.Left = (this.DisplayRectangle.Width - UnityToolContainer.Width) / 2;
        }

        /// <summary>
        /// 绑定事件
        /// </summary>
        private void BindEvent()
        {
            UnityToolContainer.GoBackClick += UnityToolContainer_GoBackClick;
            UnityToolContainer.RefreshClick += UnityToolContainer_RefreshClick;
            UnityToolContainer.LogClick += UnityToolContainer_LogClick;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string DownloadDirectory = ConfigHelper.GetConfigHelper.DownloadDirectory;
            if (!IOUtils.DirectoryExists(DownloadDirectory))
            {
                LogUtils.Info($"正在创建下载目录：{DownloadDirectory}");
                try
                {
                    IOUtils.CreateDirectory(DownloadDirectory);
                }
                catch (Exception ex)
                {
                    LogUtils.Error($"创建下载失败：{ex.Message}");
                    MessageBox.Show($"无法创建下载目录，886~\n{ex.Message}");
                    Application.Exit();
                }
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            RefreshCatalogList();
        }

        #endregion

        #region 扫描目录

        /// <summary>
        /// 刷新目录列表
        /// </summary>
        private void RefreshCatalogList()
        {
            LogUtils.Info("刷新目录列表...");

            ClearCatalog();

            ScanCatalog(Application.StartupPath);
        }

        /// <summary>
        /// 清空目录
        /// </summary>
        private void ClearCatalog()
        {
            foreach (var flowPanel in PanelInTabPage.Values)
            {
                if (flowPanel != null)
                {
                    while (flowPanel.Controls.Count > 0)
                    {
                        flowPanel.Controls[0].Dispose();
                    }
                    flowPanel.Dispose();
                }
            }
            foreach (var tabPage in PanelInTabPage.Keys)
            {
                tabPage.Dispose();
            }
            PanelInTabPage.Clear();
        }

        /// <summary>
        /// 使用目录内所有扫描器扫描目录
        /// </summary>
        private void ScanCatalog(string directoryPath)
        {
            LogUtils.Info("使用所有扫描器扫描目录...");

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

                    LogUtils.Info($"发现扫描器：{scanner.ASDESource} in {assembly.FullName}");

                    TabPage tabPage = CreateCatalogContainer(scanner.ASDESource);
                    try
                    {
                        scanner.ProcessCompleted += Scanner_ProcessCompleted;
                        scanner.Process(tabPage);
                    }
                    catch (Exception ex)
                    {
                        LogUtils.Error($"调用扫描器失败：{ex.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// 创建容器
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private TabPage CreateCatalogContainer(string source)
        {
            TabPage tabPage = new TabPage()
            {
                Text = $"{source} - 正在扫描...",
            };
            CatalogTabControl.TabPages.Add(tabPage);

            FlowLayoutPanel flowPanel = new FlowLayoutPanel()
            {
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false
            };
            tabPage.Controls.Add(flowPanel);
            flowPanel.Dock = DockStyle.Fill;

            PanelInTabPage.Add(tabPage, flowPanel);
            return tabPage;
        }

        /// <summary>
        /// 扫描器处理完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Scanner_ProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LogUtils.Info($"扫描器处理完成：{(sender as Scanner).ASDESource} {(e.Cancelled ? "(手动取消)" : "")}");

            Scanner scanner = sender as Scanner;
            TabPage tabPage = scanner.Argument as TabPage;

            LoadCatalog(PanelInTabPage[tabPage], scanner.ASDESource);
            tabPage.Text = scanner.ASDESource;
            
            //扫描完成释放扫描器
            scanner.Dispose();
        }

        /// <summary>
        /// 加载目录
        /// </summary>
        private void LoadCatalog(FlowLayoutPanel flowPanel, string source)
        {
            if (flowPanel == null) throw new ArgumentNullException("flowPanel");

            //巨幅加载新文章
            foreach (var article in articleManager.GetNewArticles(source))
            {
                if (article == null) continue;

                flowPanel.Controls.Add(
                    cardFactory.CreateLargeCard(
                        article.Title,
                        article.Description,
                        article.PublishTime
                        )
                    );

                Application.DoEvents();
            }

            //foreach (var article in TargetDBContext.Articles.Where(article=>!article.IsNew && article.ExportTime!=null))
            {

            }
        }

        #endregion

        #region 工具按钮事件

        /// <summary>
        /// 点击工具箱日志按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnityToolContainer_LogClick(object sender, EventArgs e)
        {
            //TODO: 点击日志按钮
        }

        /// <summary>
        /// 点击工具箱刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnityToolContainer_RefreshClick(object sender, EventArgs e)
        {
            RefreshCatalogList();
        }

        /// <summary>
        /// 点击工具箱后退按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnityToolContainer_GoBackClick(object sender, EventArgs e)
        {
            //TODO: 点击后退按钮
        }

        #endregion

    }
}
