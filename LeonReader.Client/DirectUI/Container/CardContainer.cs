using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using LeonDirectUI.Container;
using LeonDirectUI.DUIControl;

using LeonReader.AbstractSADE;
using LeonReader.Common;
using LeonReader.Model;

namespace LeonReader.Client.DirectUI.Container
{
    /// <summary>
    /// 卡片控件基础容器
    /// </summary>
    public class CardContainer : ContainerBase
    {

        #region 状态

        /// <summary>
        /// 文章状态枚举
        /// </summary>
        public enum ArticleStates
        {
            /// <summary>
            /// 新文章
            /// </summary>
            New = 0,
            /// <summary>
            /// 正在取消
            /// </summary>
            Cancelling = 1,
            /// <summary>
            /// 正在分析
            /// </summary>
            Analyzing = 2,
            /// <summary>
            /// 分析完毕
            /// </summary>
            Analyzed = 3,
            /// <summary>
            /// 正在下载
            /// </summary>
            Downloading = 4,
            /// <summary>
            /// 下载完成
            /// </summary>
            Downloaded = 5,
            /// <summary>
            /// 正在导出
            /// </summary>
            Exporting = 6,
            /// <summary>
            /// 导出完成
            /// </summary>
            Exported = 7,
            /// <summary>
            /// 正在阅读
            /// </summary>
            Reading = 8,
            /// <summary>
            /// 已读
            /// </summary>
            Readed = 9,
            /// <summary>
            /// 正在删除
            /// </summary>
            Deleting = 10,
            /// <summary>
            /// 已经删除
            /// </summary>
            Deleted = 11,
        }

        private ArticleStates _articleState;
        /// <summary>
        /// 文章状态
        /// </summary>
        public ArticleStates ArticleState
        {
            get => this._articleState;
            set
            {
                if (this._articleState != value)
                {
                    this._articleState = value;

                    this.SwitchUI(value);
                    this.ArticleStateChanged?.Invoke(this, value);
                }
            }
        }

        /// <summary>
        /// 切换文章状态
        /// </summary>
        /// <param name="articleState"></param>
        protected virtual void SwitchUI(ArticleStates articleState)
        {
            //TODO: [提醒] 状态改变切换界面显示的代码放在这里（DUIStateLabel由Processer控制，在这里排除）
            switch (articleState)
            {
                case ArticleStates.New:
                    {
                        this.DUIMainButton.Text = "分析";
                        break;
                    }
                case ArticleStates.Analyzing:
                    {
                        this.DUIMainButton.Text = "取消";
                        break;
                    }
                case ArticleStates.Analyzed:
                    {
                        this.DUIMainButton.Text = "下载";
                        break;
                    }
                case ArticleStates.Downloading:
                    {
                        this.DUIMainButton.Text = "取消";
                        break;
                    }
                case ArticleStates.Downloaded:
                    {
                        this.DUIMainButton.Text = "导出";
                        break;
                    }
                case ArticleStates.Exporting:
                    {
                        this.DUIMainButton.Text = "取消";
                        break;
                    }
                case ArticleStates.Exported:
                    {
                        this.DUIMainButton.Text = "阅读";
                        break;
                    }
                case ArticleStates.Reading:
                    {
                        this.DUIMainButton.Text = "正在阅读";
                        break;
                    }
                case ArticleStates.Readed:
                    {
                        this.DUIMainButton.Text = "阅读";
                        break;
                    }
                default:
                    {
                        LogUtils.Warn($"CardContainer 切换到未支持的文章状态：{articleState}");
                        break;
                    }
            }
        }

        #endregion

        #region 关联对象 [代理模式]

        //TODO: 注入文章依赖对象后根据文章状态修改此控件状态并相应界面（显示进度）

        /// <summary>
        /// 文章对象
        /// </summary>
        public Article TargetArticle { get; set; }

        private Analyzer _analyzer;
        /// <summary>
        /// 关联分析器
        /// </summary>
        public Analyzer Analyzer
        {
            get => this._analyzer;
            set
            {
                if (this._analyzer == value) return;

                if (this._analyzer != null)
                {
                    this._analyzer.ProcessCompleted -= this.Analyzer_ProcessCompleted;
                    this._analyzer.ProcessReport -= this.Analyzer_ProcessReport;
                    this._analyzer.ProcessStarted -= this.Analyzer_ProcessStarted;
                }

                this._analyzer = value;

                if (this._analyzer != null)
                {
                    this._analyzer.ProcessCompleted += this.Analyzer_ProcessCompleted;
                    this._analyzer.ProcessReport += this.Analyzer_ProcessReport;
                    this._analyzer.ProcessStarted += this.Analyzer_ProcessStarted;
                }
            }
        }

        private Downloader _downloader;
        /// <summary>
        /// 关联下载器
        /// </summary>
        public Downloader Downloader
        {
            get => this._downloader;
            set
            {
                if (this._downloader == value) return;

                if (this._downloader != null)
                {
                    this._downloader.ProcessCompleted -= this.Downloader_ProcessCompleted;
                    this._downloader.ProcessReport -= this.Downloader_ProcessReport;
                    this._downloader.ProcessStarted -= this.Downloader_ProcessStarted;
                }

                this._downloader = value;

                if (this._downloader != null)
                {
                    this._downloader.ProcessCompleted += this.Downloader_ProcessCompleted;
                    this._downloader.ProcessReport += this.Downloader_ProcessReport;
                    this._downloader.ProcessStarted += this.Downloader_ProcessStarted;
                }
            }
        }

        private Exporter _exporter;
        /// <summary>
        /// 关联导出器
        /// </summary>
        public Exporter Exporter
        {
            get => this._exporter;
            set
            {
                if (this._exporter == value) return;

                if (this._exporter != null)
                {
                    this._exporter.ProcessCompleted -= this.Exporter_ProcessCompleted;
                    this._exporter.ProcessReport -= this.Exporter_ProcessReport;
                    this._exporter.ProcessStarted -= this.Exporter_ProcessStarted;
                }

                this._exporter = value;

                if (this._exporter != null)
                {
                    this._exporter.ProcessCompleted += this.Exporter_ProcessCompleted;
                    this._exporter.ProcessReport += this.Exporter_ProcessReport;
                    this._exporter.ProcessStarted += this.Exporter_ProcessStarted;
                }
            }
        }

        #endregion

        #region 自定义事件

        /// <summary>
        /// 文章状态变化
        /// </summary>
        public event EventHandler<ArticleStates> ArticleStateChanged;

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

            this.ArticleState = ArticleStates.New;
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
            this.DUIMainButton.Click += (s, e) =>
            {
                try
                {
                    this.SwitchProcesser();
                }
                catch (Exception ex)
                {
                    this.DUIStateLabel.Text = ex.Message;
                }
                MainButtonClick?.Invoke(this, EventArgs.Empty);
            };
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

        #region 分析器事件

        /// <summary>
        /// 开始分析文章
        /// </summary>
        public void OnAnalyze()
        {
            if (this.ArticleState < ArticleStates.New) throw new Exception("文章状态需要至少为 New 才允许分析");
            if (this.TargetArticle == null) throw new ArgumentNullException($"当前卡片控件({this.Title})关联的文章实体为空");
            if (this._analyzer == null) throw new ArgumentNullException($"当前文章({this.TargetArticle.Title})关联的分析器为空");
            if (this.ArticleState == ArticleStates.Cancelling) return;

            this.ArticleState = ArticleStates.Analyzing;
            this._analyzer.TargetArticle = this.TargetArticle;
            this._analyzer.Process();
        }

        /// <summary>
        /// 取消分析
        /// </summary>
        public void OnCancelAnalyze()
        {
            if (this._analyzer == null) return;
            if (this.ArticleState == ArticleStates.Cancelling) return;

            this.ArticleState = ArticleStates.Cancelling;
            this._analyzer.Cancle();
        }

        /// <summary>
        /// 分析器开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Analyzer_ProcessStarted(object sender, DoWorkEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() => this.DUIStateLabel.Text = "开始分析文章..."));
            else
                this.DUIStateLabel.Text = "开始分析文章...";
        }

        /// <summary>
        /// 分析器报告进度事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Analyzer_ProcessReport(object sender, ProgressChangedEventArgs e)
        {
            this.DUIStateLabel.Text = $"分析进度：{e.ProgressPercentage} 页 {e.UserState} 图";
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
                this.DUIStateLabel.Text = $"用户取消了分析";
                this.ArticleState = ArticleStates.New;
            }
            else if (e.Error != null)
            {
                this.DUIStateLabel.Text = $"分析遇到错误：{e.Error.Message}";
                this.ArticleState = ArticleStates.New;
            }
            else
            {
                this.DUIStateLabel.Text = $"文章分析完成：{e.Result}";
                this.ArticleState = ArticleStates.Analyzed;
            }
        }

        #endregion

        #region 下载器事件

        /// <summary>
        /// 开始下载文章
        /// </summary>
        public void OnDownload()
        {
            if (this.ArticleState < ArticleStates.Analyzed) throw new Exception("文章状态需要至少为 Analyzed 才允许分析");
            if (this.TargetArticle == null) throw new ArgumentNullException($"当前卡片控件({this.Title})关联的文章实体为空");
            if (this._downloader == null) throw new ArgumentNullException($"当前文章({this.TargetArticle.Title})关联的分析器为空");
            if (this.ArticleState == ArticleStates.Cancelling) return;

            this.ArticleState = ArticleStates.Downloading;
            this._downloader.TargetArticle = this.TargetArticle;
            this._downloader.Process();
        }

        /// <summary>
        /// 取消下载
        /// </summary>
        public void OnCancelDownload()
        {
            if (this._downloader == null) return;
            if (this.ArticleState == ArticleStates.Cancelling) return;

            this.ArticleState = ArticleStates.Cancelling;
            this._downloader.Cancle();
        }

        /// <summary>
        /// 下载器开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Downloader_ProcessStarted(object sender, DoWorkEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() => this.DUIStateLabel.Text = "开始下载文章..."));
            else
                this.DUIStateLabel.Text = "开始下载文章...";
        }

        /// <summary>
        /// 下载器报告进度事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Downloader_ProcessReport(object sender, ProgressChangedEventArgs e)
        {
            this.DUIStateLabel.Text = $"下载进度：{e.ProgressPercentage} 个成功, {e.UserState} 个失败";
        }

        /// <summary>
        /// 下载器报告进度事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Downloader_ProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.DUIStateLabel.Text = $"用户取消了下载";
                this.ArticleState = ArticleStates.Analyzed;
            }
            else if (e.Error != null)
            {
                this.DUIStateLabel.Text = $"下载遇到错误：{e.Error.Message}";
                this.ArticleState = ArticleStates.Analyzed;
            }
            else
            {
                this.DUIStateLabel.Text = $"文章下载完成：{e.Result}";
                this.ArticleState = ArticleStates.Downloaded;
            }
        }

        #endregion

        #region 导出器事件

        /// <summary>
        /// 开始导出文章
        /// </summary>
        public void OnExport()
        {
            if (this.ArticleState < ArticleStates.Downloaded) throw new Exception("文章状态需要至少为 Downloaded 才允许分析");
            if (this.TargetArticle == null) throw new ArgumentNullException($"当前卡片控件({this.Title})关联的文章实体为空");
            if (this._exporter == null) throw new ArgumentNullException($"当前文章({this.TargetArticle.Title})关联的分析器为空");
            if (this.ArticleState == ArticleStates.Cancelling) return;

            this.ArticleState = ArticleStates.Exporting;
            this._exporter.TargetArticle = this.TargetArticle;
            this._exporter.Process();
        }

        /// <summary>
        /// 取消导出
        /// </summary>
        public void OnCancelExport()
        {
            if (this._exporter == null) return;
            if (this.ArticleState == ArticleStates.Cancelling) return;

            this.ArticleState = ArticleStates.Cancelling;
            this._exporter.Cancle();
        }

        /// <summary>
        /// 导出器开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exporter_ProcessStarted(object sender, DoWorkEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() => this.DUIStateLabel.Text = "开始导出文章..."));
            else
                this.DUIStateLabel.Text = "开始导出文章...";
        }

        /// <summary>
        /// 导出器报告进度事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exporter_ProcessReport(object sender, ProgressChangedEventArgs e)
        {
            this.DUIStateLabel.Text = $"导出进度：{e.ProgressPercentage}";
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
                this.DUIStateLabel.Text = $"用户取消了导出";
                this.ArticleState = ArticleStates.Downloaded;
            }
            else if (e.Error != null)
            {
                this.DUIStateLabel.Text = $"导出遇到错误：{e.Error.Message}";
                this.ArticleState = ArticleStates.Downloaded;
            }
            else
            {
                this.DUIStateLabel.Text = $"文章导出完成：{e.Result}";
                this.ArticleState = ArticleStates.Exported;
            }
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            this.Analyzer?.Dispose();
            this.Downloader?.Dispose();
            this.Exporter?.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>
        /// 切换处理器 (业务流程)
        /// </summary>
        protected virtual void SwitchProcesser()
        {
            switch (this.ArticleState)
            {
                case ArticleStates.New:
                    {
                        this.OnAnalyze();
                        break;
                    }
                case ArticleStates.Analyzing:
                    {
                        this.OnCancelAnalyze();
                        break;
                    }
                case ArticleStates.Analyzed:
                    {
                        this.OnDownload();
                        break;
                    }
                case ArticleStates.Downloading:
                    {
                        this.OnCancelDownload();
                        break;
                    }
                case ArticleStates.Downloaded:
                    {
                        this.OnExport();
                        break;
                    }
                case ArticleStates.Exporting:
                    {
                        this.OnCancelExport();
                        break;
                    }
                case ArticleStates.Exported:
                    {
                        this.TitleClick?.Invoke(this, EventArgs.Empty);
                        this.ArticleState = ArticleStates.Reading;
                        break;
                    }
                case ArticleStates.Reading:
                    {
                        break;
                    }
                case ArticleStates.Readed:
                    {
                        break;
                    }
                case ArticleStates.Cancelling:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
