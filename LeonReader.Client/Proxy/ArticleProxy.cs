using System;
using System.ComponentModel;

using LeonReader.AbstractSADE;
using LeonReader.ArticleContentManager;
using LeonReader.Client.DirectUI.Container;
using LeonReader.Model;

using static LeonReader.Model.Article;

namespace LeonReader.Client.Proxy
{
    /// <summary>
    /// 文章代理
    /// </summary>
    public sealed class ArticleProxy : IDisposable
    {

        /// <summary>
        /// 构造文章代理器
        /// </summary>
        /// <param name="manager">AC管理器</param>
        /// <param name="articleID">文章ID</param>
        /// <param name="articleSource">文章处理源</param>
        /// <param name="cardContainer">卡片控件</param>
        /// <param name="analyzer">分析器</param>
        /// <param name="downloader">下载器</param>
        /// <param name="exporter">导出器</param>
        public ArticleProxy(
            ACManager manager,
            string articleID,
            string articleSource,
            CardContainer cardContainer,
            Analyzer analyzer,
            Downloader downloader,
            Exporter exporter
            )
        {
            //赋值顺序有要求
            this.TargetACManager = manager ?? throw new ArgumentNullException(nameof(manager));

            this.TargetCardContainer = cardContainer ?? throw new ArgumentNullException(nameof(cardContainer));

            if (string.IsNullOrEmpty(articleID) || string.IsNullOrEmpty(articleSource))
                throw new ArgumentNullException($"{nameof(articleID)}, {nameof(articleSource)}");
            this.TargetArticle = this.TargetACManager.GetArticle(articleID, articleSource);
            if (this.TargetArticle == null)
                throw new ArgumentException($"{nameof(articleID)}, {nameof(articleSource)}");

            this.TargetAnalyzer = analyzer ?? throw new ArgumentNullException(nameof(analyzer));

            this.TargetDownloader = downloader ?? throw new ArgumentNullException(nameof(downloader));

            this.TargetExporter = exporter ?? throw new ArgumentNullException(nameof(exporter));
        }

        #region 代理AC管理器

        /// <summary>
        /// 文章对象
        /// </summary>
        public ACManager TargetACManager { get; }

        #endregion

        #region 代理文章

        private Article _targetArticle;
        /// <summary>
        /// 文章对象
        /// </summary>
        public Article TargetArticle
        {
            get => this._targetArticle;
            private set
            {
                //这里没有验证，只允许在构造函数调用
                this._targetArticle = value;
                //TODO: 注入文章对象后根据文章状态设置卡片控件布局并显示此状态进度
                Console.WriteLine(this._targetArticle.State.ToString());
            }
        }
        #endregion

        #region 代理卡片容器

        private CardContainer _targetCardContainer;
        /// <summary>
        /// 卡片容器
        /// </summary>
        public CardContainer TargetCardContainer
        {
            get => this._targetCardContainer;
            private set
            {
                //这里没有验证，只允许在构造函数调用
                this._targetCardContainer = value;

                this._targetCardContainer.TitleClick += this.CardContainer_TitleClick;
                this._targetCardContainer.LocationClick += this.CardContainer_LocationClick;
                this._targetCardContainer.BrowserClick += this.CardContainer_BrowserClick;
                this._targetCardContainer.ReadedClick += this.CardContainer_ReadedClick;
                this._targetCardContainer.DeleteClick += this.CardContainer_DeleteClick;
                this._targetCardContainer.MainButtonClick += this.CardContainer_MainButtonClick;
            }
        }

        #endregion

        #region 关联处理器

        private Analyzer _targetAnalyzer;
        /// <summary>
        /// 关联分析器
        /// </summary>
        public Analyzer TargetAnalyzer
        {
            get => this._targetAnalyzer;
            private set
            {
                //这里没有验证，只允许在构造函数调用
                this._targetAnalyzer = value;
                this._targetAnalyzer.TargetACManager = this.TargetACManager;
                this._targetAnalyzer.TargetArticle = this.TargetArticle;

                this._targetAnalyzer.ProcessCompleted += this.Analyzer_ProcessCompleted;
                this._targetAnalyzer.ProcessReport += this.Analyzer_ProcessReport;
                this._targetAnalyzer.ProcessStarted += this.Analyzer_ProcessStarted;
            }
        }

        private Downloader _targetDownloader;
        /// <summary>
        /// 关联下载器
        /// </summary>
        public Downloader TargetDownloader
        {
            get => this._targetDownloader;
            private set
            {
                //这里没有验证，只允许在构造函数调用
                this._targetDownloader = value;
                this._targetDownloader.TargetACManager = this.TargetACManager;
                this._targetDownloader.TargetArticle = this.TargetArticle;

                this._targetDownloader.ProcessCompleted += this.Downloader_ProcessCompleted;
                this._targetDownloader.ProcessReport += this.Downloader_ProcessReport;
                this._targetDownloader.ProcessStarted += this.Downloader_ProcessStarted;
            }
        }

        private Exporter _targetExporter;
        /// <summary>
        /// 关联导出器
        /// </summary>
        public Exporter TargetExporter
        {
            get => this._targetExporter;
            private set
            {
                //这里没有验证，只允许在构造函数调用
                this._targetExporter = value;
                this._targetExporter.TargetACManager = this.TargetACManager;
                this._targetExporter.TargetArticle = this.TargetArticle;

                this._targetExporter.ProcessCompleted += this.Exporter_ProcessCompleted;
                this._targetExporter.ProcessReport += this.Exporter_ProcessReport;
                this._targetExporter.ProcessStarted += this.Exporter_ProcessStarted;
            }
        }

        #endregion

        #region 卡片事件

        private void CardContainer_TitleClick(object sender, EventArgs e)
        {
        }

        private void CardContainer_LocationClick(object sender, EventArgs e)
        {
        }

        private void CardContainer_ReadedClick(object sender, EventArgs e)
        {
        }

        private void CardContainer_MainButtonClick(object sender, EventArgs e)
        {
            //TODO: 根据文章状态跳过已经完成的步骤，将界面直接置为下一步操作的布局
        }

        private void CardContainer_DeleteClick(object sender, EventArgs e)
        {
        }

        private void CardContainer_BrowserClick(object sender, EventArgs e)
        {
        }

        #endregion

        #region 分析器事件

        /// <summary>
        /// 开始分析文章
        /// </summary>
        public void OnAnalyze()
        {
            if (this.TargetArticle.State < ArticleStates.New ||
                this.TargetArticle.State == ArticleStates.Cancelling ||
                this.TargetArticle.State == ArticleStates.Analyzing ||
                this.TargetArticle.State == ArticleStates.Downloading ||
                this.TargetArticle.State == ArticleStates.Exporting ||
                this.TargetArticle.State == ArticleStates.Reading ||
                this.TargetArticle.State == ArticleStates.Deleting
                )
                throw new InvalidOperationException($"无法在 {this.TargetArticle.State} 状态下进行此操作");

            this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Analyzing);
            this.TargetAnalyzer.Process();
        }

        /// <summary>
        /// 分析器开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Analyzer_ProcessStarted(object sender, DoWorkEventArgs e)
        {
            this.TargetCardContainer.OnAnalyze();
        }

        /// <summary>
        /// 分析器报告进度事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Analyzer_ProcessReport(object sender, ProgressChangedEventArgs e)
        {
            this.TargetCardContainer.OnAnalyzeReport(e.ProgressPercentage, Convert.ToInt32(e.UserState));
        }

        /// <summary>
        /// 取消分析
        /// </summary>
        public void OnCancelAnalyze()
        {
            if (this.TargetArticle.State != ArticleStates.Analyzing)
                throw new InvalidOperationException($"无法在 {this.TargetArticle.State} 状态下进行此操作");

            this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Cancelling);
            this.TargetCardContainer.OnCancle();
            this.TargetAnalyzer.Cancle();
        }

        /// <summary>
        /// 分析器完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Analyzer_ProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.New);
                this.TargetCardContainer.OnAnalyzeCanceled();
            }
            else if (e.Error != null)
            {
                this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.New);
                this.TargetCardContainer.OnAnalyzeError(e.Error);
            }
            else
            {
                this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Analyzed);
                this.TargetCardContainer.OnAnalyzed(Convert.ToInt32(e.Result));
            }
        }

        #endregion

        #region 下载器事件

        /// <summary>
        /// 开始下载文章
        /// </summary>
        public void OnDownload()
        {
            if (this.TargetArticle.State < ArticleStates.Analyzed ||
                this.TargetArticle.State == ArticleStates.Cancelling ||
                this.TargetArticle.State == ArticleStates.Downloading ||
                this.TargetArticle.State == ArticleStates.Exporting ||
                this.TargetArticle.State == ArticleStates.Reading ||
                this.TargetArticle.State == ArticleStates.Deleting
                )
                throw new InvalidOperationException($"无法在 {this.TargetArticle.State} 状态下进行此操作");

            this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Downloading);
            this.TargetDownloader.Process();
        }

        /// <summary>
        /// 下载器开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Downloader_ProcessStarted(object sender, DoWorkEventArgs e)
        {
            this.TargetCardContainer.OnDownload();
        }

        /// <summary>
        /// 下载器报告进度事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Downloader_ProcessReport(object sender, ProgressChangedEventArgs e)
        {
            this.TargetCardContainer.OnDownloadReport(e.ProgressPercentage, Convert.ToInt32(e.UserState));
        }

        /// <summary>
        /// 取消下载
        /// </summary>
        public void OnCancelDownload()
        {
            if (this.TargetArticle.State != ArticleStates.Downloading)
                throw new InvalidOperationException($"无法在 {this.TargetArticle.State} 状态下进行此操作");

            this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Cancelling);
            this.TargetCardContainer.OnCancle();
            this.TargetDownloader.Cancle();
        }

        /// <summary>
        /// 下载器完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Downloader_ProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Analyzed);
                this.TargetCardContainer.OnDownloadCanceled();
            }
            else if (e.Error != null)
            {
                this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Analyzed);
                this.TargetCardContainer.OnDownloadError(e.Error);
            }
            else
            {
                this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Downloaded);
                this.TargetCardContainer.OnDownloaded(e.Result as Tuple<int, int>);
            }
        }

        #endregion

        #region 导出器事件

        /// <summary>
        /// 开始导出文章
        /// </summary>
        public void OnExport()
        {
            if (this.TargetArticle.State < ArticleStates.Downloaded ||
                this.TargetArticle.State == ArticleStates.Cancelling ||
                this.TargetArticle.State == ArticleStates.Exporting ||
                this.TargetArticle.State == ArticleStates.Reading ||
                this.TargetArticle.State == ArticleStates.Deleting
                )
                throw new InvalidOperationException($"无法在 {this.TargetArticle.State} 状态下进行此操作");

            this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Exporting);
            this.TargetExporter.Process();
        }

        /// <summary>
        /// 导出器开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exporter_ProcessStarted(object sender, DoWorkEventArgs e)
        {
            this.TargetCardContainer.OnExport();
        }

        /// <summary>
        /// 导出器报告进度事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exporter_ProcessReport(object sender, ProgressChangedEventArgs e)
        {
            this.TargetCardContainer.OnExportReport(e.ProgressPercentage, e.UserState);
        }

        /// <summary>
        /// 取消导出
        /// </summary>
        public void OnCancelExport()
        {
            if (this.TargetArticle.State != ArticleStates.Exporting)
                throw new InvalidOperationException($"无法在 {this.TargetArticle.State} 状态下进行此操作");

            this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Cancelling);
            this.TargetCardContainer.OnCancle();
            this.TargetExporter.Cancle();
        }

        /// <summary>
        /// 导出器完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exporter_ProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Downloaded);
                this.TargetCardContainer.OnExportCanceled();
            }
            else if (e.Error != null)
            {
                this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Downloaded);
                this.TargetCardContainer.OnExportError(e.Error);
            }
            else
            {
                this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Exported);
                this.TargetCardContainer.OnExported(e.Result as string);
            }
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    //TODO: [提醒] 对象要专用，因为会被释放的哦~
                    this.TargetAnalyzer.Dispose();
                    this.TargetDownloader.Dispose();
                    this.TargetExporter.Dispose();
                    this.TargetACManager.Dispose();
                    this.TargetCardContainer.Dispose();
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
