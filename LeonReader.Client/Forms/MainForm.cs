using System;
using System.Collections.Generic;
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
        Dictionary<TabPage, FlowLayoutPanel> TabPage_Panel_Rel = new Dictionary<TabPage, FlowLayoutPanel>();

        /// <summary>
        /// 扫描器和TabPage关联字典
        /// </summary>
        Dictionary<Scanner, TabPage> Scanner_TabPage_Rel = new Dictionary<Scanner, TabPage>();

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

    }
}
