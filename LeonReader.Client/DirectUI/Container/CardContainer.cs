using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using LeonDirectUI.Container;
using LeonDirectUI.DUIControl;

using LeonReader.Client.Proxy;

namespace LeonReader.Client.DirectUI.Container
{

    //TODO: 检查各状态下 StateText 能否显示全，调整状态文本或控件大小

    /// <summary>
    /// 卡片控件基础容器
    /// </summary>
    public class CardContainer : ContainerBase
    {
        #region 切换界面状态

        /// <summary>
        /// 新建
        /// </summary>
        public void OnNew()
        {
            this.DUIMainButton.Text = "分析";
            this.Style = CardStyles.Large;
        }

        #region 分析

        /// <summary>
        /// 开始分析
        /// </summary>
        public void OnAnalyze()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => this.DUIStateLabel.Text = "开始分析文章..."));
                this.DUIMainButton.Text = "取消";
            }
            else
            {
                this.DUIStateLabel.Text = "开始分析文章...";
                this.DUIMainButton.Text = "取消";
            }
        }

        /// <summary>
        /// 报告分析进度
        /// </summary>
        /// <param name="pageCount">页数</param>
        /// <param name="imageCount">图数</param>
        public void OnAnalyzeReport(int pageCount, int imageCount)
        {
            this.DUIStateLabel.Text = $"分析进度：{pageCount} 页 {imageCount} 图";
        }

        /// <summary>
        /// 取消了分析任务
        /// </summary>
        public void OnAnalyzeCanceled()
        {
            this.DUIStateLabel.Text = "用户取消了分析...";
            this.DUIMainButton.Text = "分析";
        }

        /// <summary>
        /// 分析遇到错误
        /// </summary>
        /// <param name="ex"></param>
        public void OnAnalyzeError(Exception ex)
        {
            this.DUIStateLabel.Text = $"分析异常: {ex.Message}";
            this.DUIMainButton.Text = "重新分析";
        }

        /// <summary>
        /// 分析完成
        /// </summary>
        /// <param name="contentCount">内容总数</param>
        public void OnAnalyzed(int contentCount)
        {
            this.DUIStateLabel.Text = $"分析完成：{contentCount} 个内容";
            this.DUIMainButton.Text = "下载";
            this.Style = CardStyles.Large;
        }

        #endregion

        #region 下载

        /// <summary>
        /// 开始下载
        /// </summary>
        public void OnDownload()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => this.DUIStateLabel.Text = "开始下载文章..."));
                this.DUIMainButton.Text = "取消";
            }
            else
            {
                this.DUIStateLabel.Text = "开始下载文章...";
                this.DUIMainButton.Text = "取消";
            }
        }

        /// <summary>
        /// 报告下载进度
        /// </summary>
        /// <param name="successCount">成功个数</param>
        /// <param name="failedCount">失败个数</param>
        public void OnDownloadReport(int successCount, int failedCount)
        {
            this.DUIStateLabel.Text = $"下载中，成功: {successCount}, 失败: {failedCount}";
        }

        /// <summary>
        /// 取消了下载任务
        /// </summary>
        public void OnDownloadCanceled()
        {
            this.DUIStateLabel.Text = "用户取消了下载...";
            this.DUIMainButton.Text = "下载";
        }

        /// <summary>
        /// 下载遇到错误
        /// </summary>
        /// <param name="ex"></param>
        public void OnDownloadError(Exception ex)
        {
            this.DUIStateLabel.Text = $"下载异常: {ex.Message}";
            this.DUIMainButton.Text = "重新下载";
        }

        /// <summary>
        /// 下载完成
        /// </summary>
        /// <param name="result">[成功总数, 失败总数]</param>
        public void OnDownloaded(Tuple<int, int> result)
        {
            this.DUIStateLabel.Text = $"下载完成，成功: {result.Item1}, 失败: {result.Item2}";
            this.DUIMainButton.Text = "导出";
            this.Style = CardStyles.Large;
        }

        #endregion

        #region 导出

        /// <summary>
        /// 开始导出
        /// </summary>
        public void OnExport()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => this.DUIStateLabel.Text = "开始导出文章..."));
                this.DUIMainButton.Text = "取消";
            }
            else
            {
                this.DUIStateLabel.Text = "开始导出文章...";
                this.DUIMainButton.Text = "取消";
            }
        }

        /// <summary>
        /// 报告导出进度
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="userstate"></param>
        public void OnExportReport(int progress, object userstate)
        {
            this.DUIStateLabel.Text = $"导出中，进度: {progress}, 状态: {userstate}";
        }

        /// <summary>
        /// 取消了导出任务
        /// </summary>
        public void OnExportCanceled()
        {
            this.DUIStateLabel.Text = "用户取消了导出...";
            this.DUIMainButton.Text = "导出";
        }

        /// <summary>
        /// 导出遇到错误
        /// </summary>
        /// <param name="ex"></param>
        public void OnExportError(Exception ex)
        {
            this.DUIStateLabel.Text = $"导出异常: {ex.Message}";
            this.DUIMainButton.Text = "重新导出";
        }

        /// <summary>
        /// 导出完成
        /// </summary>
        /// <param name="path">导出路径</param>
        public void OnExported(string path)
        {
            this.DUIStateLabel.Text = $"导出完成: {path}";
            this.DUIMainButton.Text = "阅读";
            this.Style = CardStyles.Normal;
        }

        #endregion

        /// <summary>
        /// 开始取消
        /// </summary>
        public void OnCancle()
        {
            this.DUIMainButton.Text = "正在取消";
        }

        /// <summary>
        /// 开始阅读
        /// </summary>
        public void OnReading()
        {
            this.DUIMainButton.Text = "正在阅读";
        }

        /// <summary>
        /// 阅读完成
        /// </summary>
        public void OnReaded()
        {
            this.DUIMainButton.Text = "阅读";
            this.Style = CardStyles.Small;
        }

        /// <summary>
        /// 开始删除
        /// </summary>
        public void OnDeleting()
        {
            this.DUIMainButton.Text = "正在清理";
            this.DUIStateLabel.Text = "正在清理此文章...";
        }

        /// <summary>
        /// 清理完成
        /// </summary>
        public void OnDeleted()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>{
                    this.DUIMainButton.Text = "已清理";
                    this.DUIStateLabel.Text = "已经清理此文章";
                    this.Style = CardStyles.Small;
                }));
            }
            else
            {
                this.DUIMainButton.Text = "已清理";
                this.DUIStateLabel.Text = "已经清理此文章";
                this.Style = CardStyles.Small;
            }
        }

        #endregion

        #region 自定义事件

        /// <summary>
        /// 点击标题
        /// </summary>
        public event EventHandler TitleClick;

        /// <summary>
        /// 点击位置按钮
        /// </summary>
        public event EventHandler LocationClick;

        /// <summary>
        /// 点击已读按钮
        /// </summary>
        public event EventHandler ReadedClick;

        /// <summary>
        /// 点击浏览按钮
        /// </summary>
        public event EventHandler BrowserClick;

        /// <summary>
        /// 点击删除按钮
        /// </summary>
        public event EventHandler DeleteClick;

        /// <summary>
        /// 点击主按钮
        /// </summary>
        public event EventHandler MainButtonClick;

        #endregion

        #region 自定义属性

        private ArticleProxy _targetArticleProxy;
        /// <summary>
        /// 关联的文章代理
        /// </summary>
        public ArticleProxy TargetArticleProxy
        {
            get => this._targetArticleProxy;
            set
            {
                if (this._targetArticleProxy != null) throw new InvalidOperationException("已经设置关联的对象，禁止修改");
                this._targetArticleProxy = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get => this.DUITitleLabel.Text; set => this.DUITitleLabel.Text = value; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get => this.DUIDescriptionLabel.Text;
            set
            {
                this.DUIDescriptionLabel.Text = value;

                if (this.Style == CardStyles.Large)
                    this.DUIPreviewImageBox.Text = value;
            }
        }

        /// <summary>
        /// 预览图
        /// </summary>
        public Image PreviewImage
        {
            get => this.DUIPreviewImageBox.BackgroundImage;
            set => this.DUIPreviewImageBox.BackgroundImage = value;
        }

        /// <summary>
        /// 发布时间
        /// </summary>
        public string PublishTime { get => this.DUIPublishTimeLabel.Text; set => this.DUIPublishTimeLabel.Text = value; }

        /// <summary>
        /// 状态文本
        /// </summary>
        public string StateText { get => this.DUIStateLabel.Text; set => this.DUIStateLabel.Text = value; }

        #endregion

        #region 虚拟控件

        /// <summary>
        /// 预览图像区域
        /// </summary>
        protected ControlBase DUIPreviewImageBox;

        /// <summary>
        /// 文章标题区域
        /// </summary>
        protected ControlBase DUITitleLabel;

        /// <summary>
        /// 文章描述区域
        /// </summary>
        protected ControlBase DUIDescriptionLabel;

        /// <summary>
        /// 发布时间区域
        /// </summary>
        protected ControlBase DUIPublishTimeLabel;

        /// <summary>
        /// 状态区域
        /// </summary>
        protected ControlBase DUIStateLabel;

        /// <summary>
        /// 主按钮区域
        /// </summary>
        protected ControlBase DUIMainButton;

        /// <summary>
        /// 已读按钮区域
        /// </summary>
        protected ControlBase DUIReadedButton;

        /// <summary>
        /// 定位文件夹按钮
        /// </summary>
        protected ControlBase DUILocationButton;

        /// <summary>
        /// 在浏览器打开按钮
        /// </summary>
        protected ControlBase DUIBrowserButton;

        /// <summary>
        /// 删除按钮
        /// </summary>
        protected ControlBase DUIDeleteButton;

        /// <summary>
        /// 分割线
        /// </summary>
        protected ControlBase DUISpliteLine;

        #endregion

        #region 切换布局样式

        /// <summary>
        /// 卡片样式
        /// </summary>
        public enum CardStyles
        {
            /// <summary>
            /// 精简
            /// </summary>
            Small = 0,
            /// <summary>
            /// 正常
            /// </summary>
            Normal = 1,
            /// <summary>
            /// 巨幅
            /// </summary>
            Large = 2,
        }

        protected CardStyles _style;
        /// <summary>
        /// 卡片类型
        /// </summary>
        [DefaultValue(CardStyles.Normal)]
        public CardStyles Style
        {
            get => this._style;
            set
            {
                if (this._style != value)
                {
                    this._style = value;
                    //切换布局方法
                    this.ResetLayout(value);
                    //立即调整布局
                    this.Relayout?.Invoke(this.DisplayRectangle.Width, this.DisplayRectangle.Height);
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// 重新布局方法委托
        /// </summary>
        protected delegate void RelayoutDelegate(int width, int height);

        /// <summary>
        /// 重新布局方法委托
        /// </summary>
        protected RelayoutDelegate Relayout;

        #endregion

        public CardContainer() : base()
        {
            this.MouseEnter += (s, e) => { this.DUISpliteLine.BackColor = Color.Red; };
            this.MouseLeave += (s, e) => { this.DUISpliteLine.BackColor = Color.DeepSkyBlue; };

            this.Style = CardStyles.Normal;
            this.Relayout = this.NormalLayout;
            this.Relayout?.Invoke(this.Width, this.Height);
        }

        #region 布局逻辑

        /// <summary>
        /// 初始化卡片控件容器布局
        /// </summary>
        public override void InitializeLayout()
        {
            this.MinimumSize = new Size(212, 62);
            this.BackColor = Color.White;

            //创建虚拟控件对象 并 维护子虚拟控件列表
            this.Add(this.DUIPublishTimeLabel = new ControlBase());
            this.Add(this.DUIDescriptionLabel = new ControlBase());
            this.Add(this.DUIPreviewImageBox = new ControlBase());
            this.Add(this.DUIStateLabel = new ControlBase());
            this.Add(this.DUITitleLabel = new ControlBase());
            this.Add(this.DUILocationButton = new ControlBase());
            this.Add(this.DUIReadedButton = new ControlBase());
            this.Add(this.DUIBrowserButton = new ControlBase());
            this.Add(this.DUIDeleteButton = new ControlBase());
            this.Add(this.DUIMainButton = new ControlBase());
            this.Add(this.DUISpliteLine = new ControlBase());

            this.SuspendPaint();

            #region 发布时间标签

            this.DUIPublishTimeLabel.Name = "发布时间标签";
            this.DUIPublishTimeLabel.Text = DateTime.Now.ToString();
            this.DUIPublishTimeLabel.ForeColor = Color.Gray;
            this.DUIPublishTimeLabel.Image = UnityResource.ClockIcon;
            this.DUIPublishTimeLabel.ImageAlign = ContentAlignment.MiddleLeft;
            this.DUIPublishTimeLabel.ShowEllipsis = true;
            this.DUIPublishTimeLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.DUIPublishTimeLabel.MinSize = new Size(0, 28);
            this.DUIPublishTimeLabel.MaxSize = new Size(0, 28);
            //DUIPublishTimeLabel.BackColor = Color.MediumPurple;
            #endregion

            #region 文章描述标签

            this.DUIDescriptionLabel.Name = "文章描述标签";
            this.DUIDescriptionLabel.Text = "文章描述内容";
            this.DUIDescriptionLabel.TextAlign = ContentAlignment.TopLeft;
            this.DUIDescriptionLabel.ShowEllipsis = true;
            //DUIDescriptionLabel.BackColor = Color.LightGray;
            #endregion

            #region 预览图相框

            this.DUIPreviewImageBox.Name = "预览图像框";
            this.DUIPreviewImageBox.BackgroundImage = UnityResource.BrokenImage;
            this.DUIPreviewImageBox.BackgroundImageLayout = ImageLayout.Zoom;
            this.DUIPreviewImageBox.BorderColor = Color.WhiteSmoke;
            this.DUIPreviewImageBox.BorderSize = 1;
            this.DUIPreviewImageBox.BorderStyle = ButtonBorderStyle.Solid;
            this.DUIPreviewImageBox.ForeColor = Color.Gray;
            this.DUIPreviewImageBox.TextAlign = ContentAlignment.BottomLeft;
            //DUIPreviewImageBox.BackColor = Color.Red;
            #endregion

            #region 状态栏标签

            this.DUIStateLabel.Name = "状态栏标签";
            this.DUIStateLabel.Text = "爸爸，点击右边的按钮开始下载";
            this.DUIStateLabel.ForeColor = Color.Gray;
            this.DUIStateLabel.BackColor = Color.White;
            this.DUIStateLabel.Image = UnityResource.DownloadIcon;
            this.DUIStateLabel.ImageAlign = ContentAlignment.MiddleLeft;
            this.DUIStateLabel.ShowEllipsis = false;
            this.DUIStateLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.DUIStateLabel.MinSize = new Size(0, 28);
            this.DUIStateLabel.MaxSize = new Size(0, 28);
            //DUIStateLabel.BackColor = Color.Pink;
            #endregion

            #region 标题标签

            this.DUITitleLabel.Name = "标题标签";
            this.DUITitleLabel.Text = "标题标签";
            this.DUITitleLabel.ForeColor = Color.Orange;
            this.DUITitleLabel.ShowEllipsis = true;
            this.DUITitleLabel.Mouseable = true;
            this.DUITitleLabel.Font = new Font(this.DUITitleLabel.Font, FontStyle.Bold);
            this.DUITitleLabel.Click += (s, e) => TitleClick?.Invoke(this, EventArgs.Empty);
            this.DUITitleLabel.MaxSize = new Size(0, 28);
            this.DUITitleLabel.MinSize = new Size(0, 28);
            this.DUITitleLabel.MouseEnter += (s, e) => { this.DUITitleLabel.ForeColor = Color.OrangeRed; };
            this.DUITitleLabel.MouseLeave += (s, e) => { this.DUITitleLabel.ForeColor = Color.Orange; };
            this.DUITitleLabel.MouseDown += (s, e) => { this.DUITitleLabel.ForeColor = Color.Chocolate; };
            this.DUITitleLabel.MouseUp += (s, e) => { this.DUITitleLabel.ForeColor = Color.OrangeRed; };
            //DUITitleLabel.BackColor = Color.Orange;
            #endregion

            #region 定位按钮

            this.DUILocationButton.Name = "定位按钮";
            this.DUILocationButton.Mouseable = true;
            this.DUILocationButton.Image = UnityResource.Location_0;
            this.DUILocationButton.Click += (s, e) => LocationClick?.Invoke(this, EventArgs.Empty);
            this.DUILocationButton.MouseEnter += (s, e) => { this.Invalidate(this.DUILocationButton.Rectangle); this.DUILocationButton.Image = UnityResource.Location_1; };
            this.DUILocationButton.MouseLeave += (s, e) => { this.Invalidate(this.DUILocationButton.Rectangle); this.DUILocationButton.Image = UnityResource.Location_0; };
            this.DUILocationButton.MaxSize = new Size(28, 28);
            this.DUILocationButton.MinSize = new Size(0, 28);
            //DUILocationButton.BackColor = Color.CadetBlue;
            #endregion

            #region 置为已读按钮

            this.DUIReadedButton.Name = "已读按钮";
            this.DUIReadedButton.Mouseable = true;
            this.DUIReadedButton.Image = UnityResource.Flag_0;
            this.DUIReadedButton.Click += (s, e) => ReadedClick?.Invoke(this, EventArgs.Empty);
            this.DUIReadedButton.MouseEnter += (s, e) => { this.Invalidate(this.DUIReadedButton.Rectangle); this.DUIReadedButton.Image = UnityResource.Flag_1; };
            this.DUIReadedButton.MouseLeave += (s, e) => { this.Invalidate(this.DUIReadedButton.Rectangle); this.DUIReadedButton.Image = UnityResource.Flag_0; };
            this.DUIReadedButton.MaxSize = new Size(28, 28);
            this.DUIReadedButton.MinSize = new Size(0, 28);
            //DUIReadedButton.BackColor = Color.CornflowerBlue;
            #endregion

            #region 浏览按钮

            this.DUIBrowserButton.Name = "浏览按钮";
            this.DUIBrowserButton.Mouseable = true;
            this.DUIBrowserButton.Image = UnityResource.Browser_0;
            this.DUIBrowserButton.Click += (s, e) => BrowserClick?.Invoke(this, EventArgs.Empty);
            this.DUIBrowserButton.MouseEnter += (s, e) => { this.Invalidate(this.DUIBrowserButton.Rectangle); this.DUIBrowserButton.Image = UnityResource.Browser_1; };
            this.DUIBrowserButton.MouseLeave += (s, e) => { this.Invalidate(this.DUIBrowserButton.Rectangle); this.DUIBrowserButton.Image = UnityResource.Browser_0; };
            this.DUIBrowserButton.MaxSize = new Size(28, 28);
            this.DUIBrowserButton.MinSize = new Size(0, 28);
            //DUIBrowserButton.BackColor = Color.BlueViolet;
            #endregion

            #region 删除按钮

            this.DUIDeleteButton.Name = "删除按钮";
            this.DUIDeleteButton.Mouseable = true;
            this.DUIDeleteButton.Image = UnityResource.Delete_0;
            this.DUIDeleteButton.Click += (s, e) => DeleteClick?.Invoke(this, EventArgs.Empty);
            this.DUIDeleteButton.MouseEnter += (s, e) => { this.Invalidate(this.DUIDeleteButton.Rectangle); this.DUIDeleteButton.Image = UnityResource.Delete_1; };
            this.DUIDeleteButton.MouseLeave += (s, e) => { this.Invalidate(this.DUIDeleteButton.Rectangle); this.DUIDeleteButton.Image = UnityResource.Delete_0; };
            this.DUIDeleteButton.MaxSize = new Size(28, 28);
            this.DUIDeleteButton.MinSize = new Size(0, 28);
            //DUIDeleteButton.BackColor = Color.DodgerBlue;
            #endregion

            #region 主按钮

            this.DUIMainButton.Name = "主按钮";
            this.DUIMainButton.Mouseable = true;
            this.DUIMainButton.TextAlign = ContentAlignment.MiddleCenter;
            this.DUIMainButton.ForeColor = Color.OrangeRed;
            this.DUIMainButton.ShowEllipsis = false;
            this.DUIMainButton.Text = "爆发吧";
            this.DUIMainButton.BackgroundImage = UnityResource.Button_0;
            this.DUIMainButton.BackgroundImageLayout = ImageLayout.Stretch;
            this.DUIMainButton.Padding = new Padding(0, 0, 0, 0);
            this.DUIMainButton.Click += (s, e) => { MainButtonClick?.Invoke(this, EventArgs.Empty); };
            this.DUIMainButton.MouseEnter += (s, e) => { this.Invalidate(this.DUIMainButton.Rectangle); this.DUIMainButton.BackgroundImage = UnityResource.Button_1; };
            this.DUIMainButton.MouseLeave += (s, e) => { this.Invalidate(this.DUIMainButton.Rectangle); this.DUIMainButton.BackgroundImage = UnityResource.Button_0; };
            this.DUIMainButton.MouseDown += (s, e) => { this.Invalidate(this.DUIMainButton.Rectangle); this.DUIMainButton.BackgroundImage = UnityResource.Button_2; };
            this.DUIMainButton.MouseUp += (s, e) => { this.Invalidate(this.DUIMainButton.Rectangle); this.DUIMainButton.BackgroundImage = UnityResource.Button_1; };
            this.DUIMainButton.MaxSize = new Size(112, 28);
            this.DUIMainButton.MinSize = new Size(0, 28);
            //DUIMainButton.BackColor = Color.LightGreen;
            #endregion

            #region 分割线

            this.DUISpliteLine.Name = "分割线";
            this.DUISpliteLine.BackColor = Color.DeepSkyBlue;
            #endregion

            this.ResumePaint();
        }

        /// <summary>
        /// 响应卡片控件容器布局
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public override void ResetSize(int width, int height)
        {
            this.SuspendPaint();

            //通过委托调用对应样式的绘制方法
            this.Relayout?.Invoke(width, height);

            this.ResumePaint();
        }

        /// <summary>
        /// 根据布局样式重新绑定布局委托
        /// </summary>
        /// <param name="styles">布局样式</param>
        /// <remarks>这里放置切换布局模式时调用一次即可而不用在响应布局时频繁调用的代码</remarks>
        protected virtual void ResetLayout(CardStyles styles)
        {
            this.SuspendPaint();

            switch (styles)
            {
                case CardStyles.Normal:
                    {
                        this.DUIDescriptionLabel.Visible = true;
                        this.DUIPreviewImageBox.Visible = true;
                        this.DUIPublishTimeLabel.Visible = true;
                        this.DUIStateLabel.Visible = true;
                        this.DUIPreviewImageBox.Text = string.Empty;
                        this.DUIPreviewImageBox.BackgroundImageLayout = ImageLayout.Zoom;
                        this.MaximumSize = new Size(0, 0);
                        this.MinimumSize = new Size(212, 62);
                        this.Size = new Size(675, 118);
                        this.Relayout = this.NormalLayout;
                        break;
                    }
                case CardStyles.Small:
                    {
                        this.DUITitleLabel.SetLocation(0, 4);
                        this.DUIDescriptionLabel.Visible = false;
                        this.DUIPreviewImageBox.Visible = false;
                        this.DUIPublishTimeLabel.Visible = false;
                        this.DUIStateLabel.Visible = false;
                        this.DUIPreviewImageBox.Text = string.Empty;
                        this.MaximumSize = new Size(1000, 32);
                        this.MinimumSize = new Size(112, 32);
                        this.Size = new Size(675, 32);
                        this.Relayout = this.SmallLayout;
                        break;
                    }
                case CardStyles.Large:
                    {
                        this.DUITitleLabel.SetLocation(0, 6);
                        this.DUIDescriptionLabel.Visible = false;
                        this.DUIPreviewImageBox.Visible = true;
                        this.DUIPublishTimeLabel.Visible = true;
                        this.DUIStateLabel.Visible = true;
                        this.DUIPreviewImageBox.Text = this.DUIDescriptionLabel.Text;
                        this.DUIPreviewImageBox.BackgroundImageLayout = ImageLayout.Tile;
                        this.MaximumSize = new Size(0, 0);
                        this.MinimumSize = new Size(112, 62);
                        this.Size = new Size(675, 175);
                        this.Relayout = this.LargeLayout;
                        break;
                    }
            }

            this.ResumePaint();
        }

        /// <summary>
        /// 精简布局
        /// </summary>
        protected virtual void SmallLayout(int width, int height)
        {
            //修改Style同时收到SizeChanged消息会导致一次性的布局混乱
            if (this.Style != CardStyles.Small) return;

            this.SuspendPaint();

            this.DUISpliteLine.SetBounds(0, 0, width, 3);

            this.DUIMainButton.SetLocation(width - 112, 4);

            this.DUIDeleteButton.Width = this.DUIMainButton.Left - 28;
            this.DUIDeleteButton.SetLocation(this.DUIMainButton.Left - this.DUIDeleteButton.Width, 4);

            this.DUIBrowserButton.Width = this.DUIDeleteButton.Left - 28;
            this.DUIBrowserButton.SetLocation(this.DUIDeleteButton.Left - this.DUIBrowserButton.Width, 4);

            this.DUILocationButton.Width = this.DUIBrowserButton.Left - 28;
            this.DUILocationButton.SetLocation(this.DUIBrowserButton.Left - this.DUILocationButton.Width, 4);

            this.DUIReadedButton.Width = this.DUILocationButton.Left - 28;
            this.DUIReadedButton.SetLocation(this.DUILocationButton.Left - this.DUIReadedButton.Width, 4);

            this.DUITitleLabel.SetSize(this.DUIReadedButton.Left, 28);

            this.ResumePaint();
        }

        /// <summary>
        /// 正常布局
        /// </summary>
        protected virtual void NormalLayout(int width, int height)
        {
            //修改Style同时收到SizeChanged消息会导致一次性的布局混乱
            if (this.Style != CardStyles.Normal) return;

            this.SuspendPaint();

            this.DUISpliteLine.SetBounds(0, 0, width, 6);

            this.DUIPreviewImageBox.SetBounds(0, 6, Math.Min((int)((25.0 / 14.0) * (height - 6)), width), height - 6);

            this.DUIDeleteButton.Width = width - this.DUIPreviewImageBox.Right;
            this.DUIDeleteButton.SetLocation(width - this.DUIDeleteButton.Width, 6);

            this.DUIBrowserButton.Width = this.DUIDeleteButton.Left - this.DUIPreviewImageBox.Right;
            this.DUIBrowserButton.SetLocation(this.DUIDeleteButton.Left - this.DUIBrowserButton.Width, 6);

            this.DUILocationButton.Width = this.DUIBrowserButton.Left - this.DUIPreviewImageBox.Right;
            this.DUILocationButton.SetLocation(this.DUIBrowserButton.Left - this.DUILocationButton.Width, 6);

            this.DUIReadedButton.Width = this.DUILocationButton.Left - this.DUIPreviewImageBox.Right;
            this.DUIReadedButton.SetLocation(this.DUILocationButton.Left - this.DUIReadedButton.Width, 6);

            this.DUITitleLabel.SetLocation(this.DUIPreviewImageBox.Right, 6);
            this.DUITitleLabel.Width = this.DUIReadedButton.Left - this.DUIPreviewImageBox.Right;

            this.DUIMainButton.Width = width - this.DUIPreviewImageBox.Width;
            this.DUIMainButton.Left = width - this.DUIMainButton.Width;
            this.DUIMainButton.Top = Math.Max(height - 28, this.DUITitleLabel.Bottom);

            this.DUIStateLabel.Width = Math.Min(this.DUIMainButton.Left - this.DUIPreviewImageBox.Right, 240);
            this.DUIStateLabel.Left = this.DUIMainButton.Left - this.DUIStateLabel.Width;
            this.DUIStateLabel.Top = this.DUIMainButton.Top;

            this.DUIPublishTimeLabel.SetLocation(this.DUIPreviewImageBox.Right, this.DUIMainButton.Top);
            this.DUIPublishTimeLabel.Width = this.DUIStateLabel.Left - this.DUIPreviewImageBox.Width;

            this.DUIDescriptionLabel.SetBounds(
                this.DUITitleLabel.Left,
                this.DUITitleLabel.Bottom,
                this.DUIMainButton.Right - this.DUIPreviewImageBox.Right,
                this.DUIMainButton.Top - this.DUITitleLabel.Bottom
                );

            this.ResumePaint();
        }

        /// <summary>
        /// 巨幅布局
        /// </summary>
        protected virtual void LargeLayout(int width, int height)
        {
            //修改Style同时收到SizeChanged消息会导致一次性的布局混乱
            if (this.Style != CardStyles.Large) return;

            this.SuspendPaint();

            this.DUISpliteLine.SetBounds(0, 0, width, 6);

            this.DUIPreviewImageBox.SetBounds(0, 34, width, height - 62);

            this.DUIDeleteButton.Width = width;
            this.DUIDeleteButton.SetLocation(width - this.DUIDeleteButton.Width, 6);

            this.DUIBrowserButton.Width = this.DUIDeleteButton.Left;
            this.DUIBrowserButton.SetLocation(this.DUIDeleteButton.Left - this.DUIBrowserButton.Width, 6);

            this.DUILocationButton.Width = this.DUIBrowserButton.Left;
            this.DUILocationButton.SetLocation(this.DUIBrowserButton.Left - this.DUILocationButton.Width, 6);

            this.DUIReadedButton.Width = this.DUILocationButton.Left;
            this.DUIReadedButton.SetLocation(this.DUILocationButton.Left - this.DUIReadedButton.Width, 6);

            this.DUITitleLabel.SetSize(this.DUIReadedButton.Left, 28);

            this.DUIMainButton.Width = width;
            this.DUIMainButton.Left = width - this.DUIMainButton.Width;
            this.DUIMainButton.Top = height - 28;

            this.DUIStateLabel.Width = Math.Min(this.DUIMainButton.Left, 200);
            this.DUIStateLabel.SetLocation(this.DUIMainButton.Left - this.DUIStateLabel.Width, this.DUIMainButton.Top);

            this.DUIPublishTimeLabel.SetLocation(0, this.DUIMainButton.Top);
            this.DUIPublishTimeLabel.Width = this.DUIStateLabel.Left;

            this.ResumePaint();
        }

        #endregion

    }
}
