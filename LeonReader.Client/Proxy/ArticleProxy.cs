using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

using LeonReader.AbstractSADE;
using LeonReader.ArticleContentManager;
using LeonReader.Client.DirectUI.Container;
using LeonReader.Client.Factory;
using LeonReader.Common;
using LeonReader.Model;

using MetroFramework.Forms;

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
            string articleID,
            string articleSource,
            CardContainer cardContainer,
            Analyzer analyzer,
            Downloader downloader,
            Exporter exporter
            )
        {
            //赋值顺序有要求

            this.TargetCardContainer = cardContainer ?? throw new ArgumentNullException(nameof(cardContainer));
            try
            {
                this.TargetCardContainer.TargetArticleProxy = this;
            }
            catch (Exception)
            {
                throw;
            }

            if (string.IsNullOrEmpty(articleID) || string.IsNullOrEmpty(articleSource))
                throw new ArgumentNullException($"{nameof(articleID)}, {nameof(articleSource)}");
            //这里文章实体必须与ACManager内的DBContext同源，否则无法正常更新数据库
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
        public ACManager TargetACManager { get; } = new ACManager();

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
                this.InitUI(this._targetArticle.State);
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

        /// <summary>
        /// 初始化文章状态显示布局
        /// </summary>
        /// <param name="articleState"></param>
        private void InitUI(ArticleStates articleState)
        {
            switch (articleState)
            {
                case ArticleStates.Analyzing:
                case ArticleStates.CancelAnalyze:
                case ArticleStates.New:
                    {
                        this.TargetArticle.State = ArticleStates.New;
                        this.TargetCardContainer.OnNew();
                        break;
                    }
                case ArticleStates.Downloading:
                case ArticleStates.CancelDownload:
                case ArticleStates.Analyzed:
                    {
                        this.TargetArticle.State = ArticleStates.Analyzed;
                        this.TargetCardContainer.OnAnalyzed(new Tuple<int, int>(
                            this.TargetACManager.GetPageCountAnalyzed(this.TargetArticle),
                            this.TargetArticle.Contents.Count
                            ));
                        break;
                    }
                case ArticleStates.Exporting:
                case ArticleStates.CancelExport:
                case ArticleStates.Downloaded:
                    {
                        this.TargetArticle.State = ArticleStates.Downloaded;
                        this.TargetCardContainer.OnDownloaded(new Tuple<int, int>(
                            this.TargetArticle.Contents.FindAll(content => content.State == ContentItem.ContentStates.Downloaded).Count,
                            this.TargetArticle.Contents.FindAll(content => content.State != ContentItem.ContentStates.Downloaded).Count
                            ));
                        break;
                    }
                case ArticleStates.Reading:
                case ArticleStates.Exported:
                    {
                        this.TargetArticle.State = ArticleStates.Exported;
                        this.TargetCardContainer.OnExported(
                                string.Format("{0}.{1}", this.TargetArticle.ArticleFileName, ConfigHelper.GetConfigHelper.Extension)
                                );
                        break;
                    }
                case ArticleStates.Readed:
                    {
                        this.TargetCardContainer.OnReaded();
                        break;
                    }
                case ArticleStates.Deleting:
                case ArticleStates.Deleted:
                    {
                        this.TargetArticle.State = ArticleStates.Deleted;
                        this.TargetCardContainer.OnDeleted();
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// 点击标题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardContainer_TitleClick(object sender, EventArgs e)
        {
            this.DoRead();
        }

        /// <summary>
        /// 点击定位按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardContainer_LocationClick(object sender, EventArgs e)
        {
            string ArticleFilePath = IOUtils.PathCombine(
                ConfigHelper.GetConfigHelper.DownloadDirectory,
                this.TargetArticle.DownloadDirectoryName,
                string.Format("{0}.{1}", this.TargetArticle.ArticleFileName, ConfigHelper.GetConfigHelper.Extension)
                );

            if (IOUtils.FileExists(ArticleFilePath))
            {
                IOUtils.SelectFile(ArticleFilePath);
            }
            else
            {
                string ArticleDirectory = IOUtils.PathCombine(
                    ConfigHelper.GetConfigHelper.DownloadDirectory,
                    this.TargetArticle.DownloadDirectoryName
                    );
                IOUtils.SelectDirectory(ArticleDirectory);
            }
        }

        /// <summary>
        /// 点击已读按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardContainer_ReadedClick(object sender, EventArgs e)
        {
            if (this.TargetArticle.State == ArticleStates.New ||
                this.TargetArticle.State == ArticleStates.Analyzed ||
                this.TargetArticle.State == ArticleStates.Downloaded ||
                this.TargetArticle.State == ArticleStates.Exported
                )
            {
                //置为已读
                this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Readed);
                this.TargetCardContainer.OnReaded();
            }
            else if (this.TargetArticle.State == ArticleStates.Readed ||
                this.TargetArticle.State == ArticleStates.Deleted
                )
            {
                this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.New);
                this.TargetCardContainer.OnNew();
            }
            else
            {
                using (MessageBoxForm messageBox = new MessageBoxForm("警告：", $"无法在 {this.TargetArticle.State} 状态下进行此操作", MessageBoxForm.MessageType.Warning))
                    messageBox.ShowDialog();
            }
        }

        /// <summary>
        /// 点击主按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardContainer_MainButtonClick(object sender, EventArgs e)
        {
            switch (this.TargetArticle.State)
            {
                case ArticleStates.Deleted:
                case ArticleStates.New:
                    {
                        this.DoAnalyze();
                        break;
                    }
                case ArticleStates.Analyzing:
                    {
                        this.DoCancelAnalyze();
                        break;
                    }
                case ArticleStates.Analyzed:
                    {
                        this.DoDownload();
                        break;
                    }
                case ArticleStates.Downloading:
                    {
                        this.DoCancelDownload();
                        break;
                    }
                case ArticleStates.Downloaded:
                    {
                        this.DoExport();
                        break;
                    }
                case ArticleStates.Exporting:
                    {
                        this.DoCancelExport();
                        break;
                    }
                case ArticleStates.Exported:
                case ArticleStates.Readed:
                    {
                        this.DoRead();
                        break;
                    }
                case ArticleStates.CancelAnalyze:
                case ArticleStates.CancelDownload:
                case ArticleStates.CancelExport:
                case ArticleStates.Reading:
                case ArticleStates.Deleting:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 点击删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardContainer_DeleteClick(object sender, EventArgs e)
        {
            if (this.TargetArticle.State == ArticleStates.New ||
                this.TargetArticle.State == ArticleStates.Analyzed ||
                this.TargetArticle.State == ArticleStates.Downloaded ||
                this.TargetArticle.State == ArticleStates.Exported ||
                this.TargetArticle.State == ArticleStates.Readed ||
                this.TargetArticle.State == ArticleStates.Deleted
                )
            {
                this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Deleting);
                this.TargetCardContainer.OnDeleting();

                string ArticleDirectory = IOUtils.PathCombine(
                    ConfigHelper.GetConfigHelper.DownloadDirectory,
                    this.TargetArticle.DownloadDirectoryName
                    );

                //异步清理文章目录
                ThreadPool.QueueUserWorkItem(
                    new WaitCallback(
                        dirpath =>
                        {
                            if (this.TargetArticle.Contents.Count > 0)
                            {
                                //清理文章
                                this.TargetACManager.SetAnalyzeTime(this.TargetArticle, null);
                                this.TargetACManager.SetDownloadTime(this.TargetArticle, null);
                                this.TargetACManager.SetExportTime(this.TargetArticle, null);
                                this.TargetACManager.ClearContents(this.TargetArticle);

                                this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Deleted);
                                this.TargetCardContainer.OnDeleted();
                            }
                            else
                            {
                                //不存在内容的文章删除文章，释放卡片代理
                                this.TargetACManager.RemoveArticle(this.TargetArticle);
                                this.Dispose();
                                return;
                            }

                            IOUtils.ClearDirectory(dirpath as string);
                        }),
                    ArticleDirectory
                    );
            }
            else
            {
                using (MessageBoxForm messageBox = new MessageBoxForm("警告：", $"无法在 {this.TargetArticle.State} 状态下进行此操作", MessageBoxForm.MessageType.Warning))
                    messageBox.ShowDialog();
            }
        }

        /// <summary>
        /// 点击浏览按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardContainer_BrowserClick(object sender, EventArgs e)
        {
            Process.Start(this.TargetArticle.ArticleLink);
        }

        #endregion

        #region 分析器事件

        /// <summary>
        /// 开始分析文章
        /// </summary>
        public void DoAnalyze()
        {
            if (this.TargetArticle.State == ArticleStates.New ||
                this.TargetArticle.State == ArticleStates.Analyzed ||
                this.TargetArticle.State == ArticleStates.Downloaded ||
                this.TargetArticle.State == ArticleStates.Exported ||
                this.TargetArticle.State == ArticleStates.Readed ||
                this.TargetArticle.State == ArticleStates.Deleted
                )
            {
                if (this.TargetArticle.State == ArticleStates.Deleted)
                    this.TargetCardContainer.OnNew();

                this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Analyzing);
                this.TargetAnalyzer.Process();
            }
            else
            {
                using (MessageBoxForm messageBox = new MessageBoxForm("警告：", $"无法在 {this.TargetArticle.State} 状态下进行此操作", MessageBoxForm.MessageType.Warning))
                    messageBox.ShowDialog();
            }
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
        public void DoCancelAnalyze()
        {
            if (this.TargetArticle.State != ArticleStates.Analyzing)
                using (MessageBoxForm messageBox = new MessageBoxForm("警告：", $"无法在 {this.TargetArticle.State} 状态下进行此操作", MessageBoxForm.MessageType.Warning))
                    messageBox.ShowDialog();

            this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.CancelAnalyze);
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

                using (MessageBoxForm messageBox = new MessageBoxForm("Analyze Process Completed Error :", e.Error.Message, MessageBoxForm.MessageType.Error))
                    messageBox.ShowDialog();
            }
            else
            {
                this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Analyzed);
                this.TargetCardContainer.OnAnalyzed(e.Result as Tuple<int, int>);
            }
        }

        #endregion

        #region 下载器事件

        /// <summary>
        /// 开始下载文章
        /// </summary>
        public void DoDownload()
        {
            if (this.TargetArticle.State == ArticleStates.Analyzed ||
                this.TargetArticle.State == ArticleStates.Downloaded ||
                this.TargetArticle.State == ArticleStates.Exported ||
                this.TargetArticle.State == ArticleStates.Readed
                )
            {
                this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Downloading);
                this.TargetDownloader.Process();
            }
            else
            {
                using (MessageBoxForm messageBox = new MessageBoxForm("警告：", $"无法在 {this.TargetArticle.State} 状态下进行此操作", MessageBoxForm.MessageType.Warning))
                    messageBox.ShowDialog();
            }
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
        public void DoCancelDownload()
        {
            if (this.TargetArticle.State != ArticleStates.Downloading)
                using (MessageBoxForm messageBox = new MessageBoxForm("警告：", $"无法在 {this.TargetArticle.State} 状态下进行此操作", MessageBoxForm.MessageType.Warning))
                    messageBox.ShowDialog();

            this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.CancelDownload);
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

                using (MessageBoxForm messageBox = new MessageBoxForm("Download Process Completed Error :", e.Error.Message, MessageBoxForm.MessageType.Error))
                    messageBox.ShowDialog();
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
        public void DoExport()
        {
            if (this.TargetArticle.State == ArticleStates.Downloaded ||
                this.TargetArticle.State == ArticleStates.Exported ||
                this.TargetArticle.State == ArticleStates.Readed
                )
            {
                this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Exporting);
                this.TargetExporter.Process();
            }
            else
            {
                using (MessageBoxForm messageBox = new MessageBoxForm("警告：", $"无法在 {this.TargetArticle.State} 状态下进行此操作", MessageBoxForm.MessageType.Warning))
                    messageBox.ShowDialog();
            }
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
        public void DoCancelExport()
        {
            if (this.TargetArticle.State != ArticleStates.Exporting)
                using (MessageBoxForm messageBox = new MessageBoxForm("警告：", $"无法在 {this.TargetArticle.State} 状态下进行此操作", MessageBoxForm.MessageType.Warning))
                    messageBox.ShowDialog();

            this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.CancelExport);
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

                using (MessageBoxForm messageBox = new MessageBoxForm("Export Process Completed Error :", e.Error.Message, MessageBoxForm.MessageType.Error))
                    messageBox.ShowDialog();
            }
            else
            {
                this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Exported);
                this.TargetCardContainer.OnExported(IOUtils.GetFileName(e.Result as string));
            }
        }

        #endregion

        #region 阅读

        /// <summary>
        /// 开始阅读
        /// </summary>
        private void DoRead()
        {
            string ArticleFilePath = IOUtils.PathCombine(
                ConfigHelper.GetConfigHelper.DownloadDirectory,
                this.TargetArticle.DownloadDirectoryName,
                string.Format("{0}.{1}", this.TargetArticle.ArticleFileName, ConfigHelper.GetConfigHelper.Extension)
                );

            if (IOUtils.FileExists(ArticleFilePath) &&
                (this.TargetArticle.State == ArticleStates.Exported ||
                this.TargetArticle.State == ArticleStates.Readed)
                )
            {
                MetroForm readerForm = ReaderFormFactory.CreateReaderForm(ArticleFilePath);
                readerForm.FormClosed += (s, v) =>
                {
                    this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Readed);
                    this.TargetCardContainer.OnReaded();
                };

                this.TargetACManager.SetArticleState(this.TargetArticle, ArticleStates.Reading);
                this.TargetCardContainer.OnReading();
                readerForm.Show(this.TargetCardContainer.FindForm());
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
                    //对象要专用，因为会被释放的哦~
                    this.TargetAnalyzer.Dispose();
                    this.TargetDownloader.Dispose();
                    this.TargetExporter.Dispose();
                    this.TargetACManager.Dispose();

                    if (this.TargetCardContainer.InvokeRequired)
                    {
                        this.TargetCardContainer.Invoke(new Action(() => { this.TargetCardContainer.Dispose(); }));
                    }
                    else
                    {
                        this.TargetCardContainer.Dispose();
                    }
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
