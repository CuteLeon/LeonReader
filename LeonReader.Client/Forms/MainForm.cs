using System;
using System.Drawing;
using System.Windows.Forms;

using LeonReader.ArticleContentManager;
using LeonReader.Client.DirectUI.Container;
using LeonReader.Client.Factory;
using LeonReader.Common;

using MetroFramework.Forms;

namespace LeonReader.Client
{
    public partial class MainForm : MetroForm
    {

        #region 工厂

        /// <summary>
        /// 反射工厂
        /// </summary>
        AssemblyFactory TargetAssemblyFactory = new AssemblyFactory();

        /// <summary>
        /// SADE 工厂
        /// </summary>
        SADEFactory TargetSADEFactory = new SADEFactory();

        #endregion

        #region 初始化

        public MainForm()
        {
            this.Icon = UnityResource.LeonReader;

            //初始化布局
            this.InitializeComponent();

            //绑定事件
            this.BindEvent();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            //适应工具容器位置
            this.UnityToolContainer.Left = (this.DisplayRectangle.Width - this.UnityToolContainer.Width) / 2;
        }

        /// <summary>
        /// 绑定事件
        /// </summary>
        private void BindEvent()
        {
            this.UnityToolContainer.GoBackClick += this.UnityToolContainer_GoBackClick;
            this.UnityToolContainer.RefreshClick += this.UnityToolContainer_RefreshClick;
            this.UnityToolContainer.LogClick += this.UnityToolContainer_LogClick;
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

            this.RefreshCatalogList();
        }

        #endregion

        #region 工具按钮事件

        /// <summary>
        /// 点击工具箱刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnityToolContainer_RefreshClick(object sender, EventArgs e)
        {
            this.RefreshCatalogList();
        }

        /// <summary>
        /// 点击工具箱日志按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnityToolContainer_LogClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(LogUtils.LogFilePath);
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

        #region 扫描目录

        /// <summary>
        /// 刷新目录列表
        /// </summary>
        private void RefreshCatalogList()
        {
            LogUtils.Info("刷新目录列表...");

            this.ClearCatalogControl();

            this.ScanCatalog(Application.StartupPath);
        }

        /// <summary>
        /// 清空目录控件
        /// </summary>
        private void ClearCatalogControl()
        {
            foreach (TabPage tabPage in this.CatalogTabControl.TabPages)
            {
                if (!(tabPage.Tag is FlowLayoutPanel flowPanel)) continue;
                foreach (Control control in flowPanel.Controls)
                {
                    if (control is CardContainer container)
                        container?.TargetArticleProxy?.Dispose();
                    else
                        control.Dispose();
                }
                flowPanel.Dispose();
                tabPage.Dispose();
            }
        }

        /// <summary>
        /// 使用目录内所有扫描器扫描目录
        /// </summary>
        private void ScanCatalog(string directoryPath)
        {
            LogUtils.Info("使用所有扫描器扫描目录...");

            //遍历符合条件的链接库
            foreach (var assembly in this.TargetAssemblyFactory.CreateAssemblys(
                directoryPath,
                path => path.ToUpper().EndsWith("SADE.DLL")))
            {
                if (assembly == null) continue;

                //遍历程序集内的扫描器
                foreach (var scanner in this.TargetSADEFactory.CreateScanners(assembly))
                {
                    if (scanner == null) continue;
                    LogUtils.Info($"发现扫描器：{scanner.SADESource} in {assembly.FullName}");

                    //每个扫描器对应一个 ACManager（UnityDBContext）
                    scanner.TargetACManager = new ACManager();
                    TabPage tabPage = this.CreateCatalogContainer(scanner.SADESource);

                    try
                    {
                        //Scanner 将在代理工厂内扫描完毕后释放
                        ArticleProxyFactory articleProxyFactory = new ArticleProxyFactory(
                            tabPage,
                            tabPage.Tag as FlowLayoutPanel,
                            assembly,
                            scanner
                            );
                    }
                    catch (Exception ex)
                    {
                        LogUtils.Error($"创建 ArticleProxyFactory 遇到异常：{ex.Message}");
                        using (MessageBoxForm messageBox = new MessageBoxForm("Scanner.Process() 遇到异常：", ex.Message, MessageBoxForm.MessageType.Error))
                            messageBox.ShowDialog();
                    }

                    try
                    {
                        scanner.Process();
                    }
                    catch (Exception ex)
                    {
                        LogUtils.Error($"调用扫描器失败：{ex.Message}");
                        using (MessageBoxForm messageBox = new MessageBoxForm("Scanner.Process() 遇到异常：", ex.Message, MessageBoxForm.MessageType.Error))
                            messageBox.ShowDialog();
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
            this.CatalogTabControl.TabPages.Add(tabPage);

            FlowLayoutPanel flowPanel = new FlowLayoutPanel()
            {
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.White
            };
            tabPage.Controls.Add(flowPanel);
            flowPanel.Dock = DockStyle.Fill;
            //使用 TAG 关联 Page 和 Panel
            tabPage.Tag = flowPanel;
            return tabPage;
        }

        #endregion

    }
}
