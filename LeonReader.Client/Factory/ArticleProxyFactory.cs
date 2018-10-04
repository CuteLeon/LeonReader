using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

using LeonReader.AbstractSADE;
using LeonReader.ArticleContentManager;
using LeonReader.Client.DirectUI.Container;
using LeonReader.Client.Proxy;
using LeonReader.Common;
using LeonReader.Model;

namespace LeonReader.Client.Factory
{
    /// <summary>
    /// 文章代理类工厂
    /// </summary>
    public class ArticleProxyFactory : IDisposable
    {
        #region 关联对象

        /// <summary>
        /// 卡片工厂
        /// </summary>
        private readonly CardContainerFactory TargetCardFactory = new CardContainerFactory();

        /// <summary>
        /// 选项卡页面
        /// </summary>
        private TabPage TargetTabPage { get; set; }

        /// <summary>
        /// 流式布局容器
        /// </summary>
        private FlowLayoutPanel TargetFlowPanel { get; set; }

        /// <summary>
        /// 扫描器
        /// </summary>
        private Scanner TargetScanner { get; set; }

        /// <summary>
        /// 程序集
        /// </summary>
        private Assembly TargetAssembly { get; set; }

        #endregion

        /// <summary>
        /// 文章代理器工厂
        /// </summary>
        /// <param name="tabPage">选项卡容器</param>
        /// <param name="flowPanel">流式布局容器</param>
        /// <param name="assembly">程序集</param>
        /// <param name="scanner">扫描器</param>
        public ArticleProxyFactory(TabPage tabPage, FlowLayoutPanel flowPanel, Assembly assembly, Scanner scanner)
        {
            this.TargetTabPage = tabPage ?? throw new ArgumentNullException(nameof(tabPage));
            this.TargetFlowPanel = flowPanel ?? throw new ArgumentNullException(nameof(flowPanel));
            this.TargetAssembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
            this.TargetScanner = scanner ?? throw new ArgumentNullException(nameof(scanner));

            if (this.TargetScanner.AnalyzerType == null ||
                this.TargetScanner.DownloaderType == null ||
                this.TargetScanner.ExportedType == null
                )
                throw new ArgumentNullException("扫描器关联的ADE类型为空");

            this.TargetScanner.ProcessReport += this.TargetScanner_ProcessReport;
            this.TargetScanner.ProcessCompleted += this.TargetScanner_ProcessCompleted;
        }

        /// <summary>
        /// 扫描器报告进度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetScanner_ProcessReport(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            //e.UserState 即为文章实体对象，但如果此时加入界面因为预览图像尚未下载成功而导致无法显示预览图的可能性更大，而且无法对新文章、缓存文章、已读文章分组显示
            this.TargetTabPage.Text = $"{this.TargetScanner.SADESource}-发现：{e.ProgressPercentage}";
            Application.DoEvents();
        }

        /// <summary>
        /// 扫描器处理完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetScanner_ProcessCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            LogUtils.Info($"扫描器处理完成：{this.TargetScanner.SADESource} {(e.Cancelled ? "(手动取消)" : "")}");

            this.LoadArticleCatalog();
            this.TargetTabPage.Text = $"{this.TargetScanner.SADESource} ({ACManager.GetACManager.GetArticleCount(this.TargetScanner.SADESource)} 篇)";

            this.Dispose(true);
        }

        /// <summary>
        /// 加载文章目录
        /// </summary>
        private void LoadArticleCatalog()
        {
            List<CardContainer> cardContainers = new List<CardContainer>();

            //创建卡片控件
            foreach (var article in ACManager.GetACManager.GetArticles(this.TargetScanner.SADESource))
            {
                if (article == null) continue;

                CardContainer cardContainer = this.TargetCardFactory.CreateCardContainer(article);
                this.CreateArticleProxy(article, cardContainer);
                cardContainers.Add(cardContainer);

                //显示控件
                this.TargetFlowPanel.Controls.Add(cardContainer);
                Application.DoEvents();
            }
        }

        /// <summary>
        /// 创建文章代理
        /// </summary>
        /// <returns></returns>
        private void CreateArticleProxy(Article article, CardContainer cardContainer)
        {
            try
            {
                ArticleProxy articleProxy = new ArticleProxy(
                    article.ArticleID,
                    article.SADESource,
                    cardContainer,
                    this.TargetAssembly.CreateInstance(this.TargetScanner.AnalyzerType) as Analyzer,
                    this.TargetAssembly.CreateInstance(this.TargetScanner.DownloaderType) as Downloader,
                    this.TargetAssembly.CreateInstance(this.TargetScanner.ExportedType) as Exporter
                    );
            }
            catch (Exception ex)
            {
                using (MessageBoxForm messageBox = new MessageBoxForm("CreateArticleProxy 遇到异常：", ex.Message, MessageBoxForm.MessageType.Error))
                    messageBox.ShowDialog();
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.TargetScanner.Dispose();
                    this.TargetAssembly = null;
                }

                this.disposedValue = true;
            }
        }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
