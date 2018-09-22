using LeonDirectUI.Container;
using LeonDirectUI.DUIControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeonReader.Client.DirectUI.Container
{
    /// <summary>
    /// 卡片控件基础容器
    /// </summary>
    public class CardContainer : ContainerBase
    {

        #region 自定义属性
        //TODO: [提醒] 添加自定义属性到这里

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get => DUITitleLabel.Text; set => DUITitleLabel.Text = value; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get => DUIDescriptionLabel.Text; set => DUIDescriptionLabel.Text = value; }

        /// <summary>
        /// 预览图
        /// </summary>
        public Image PreviewImage
        {
            get => DUIPreviewImageBox.BackgroundImage;
            set => DUIPreviewImageBox.BackgroundImage = value;
        }

        /// <summary>
        /// 发布时间
        /// </summary>
        public string PublishTime { get => DUIPublishTimeLabel.Text; set => DUIPublishTimeLabel.Text = value; }

        /// <summary>
        /// 状态文本
        /// </summary>
        public string StateText { get => DUIStateLabel.Text; set => DUIStateLabel.Text = value; }

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

        #region 样式布局

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
            get => _style;
            set
            {
                if (_style != value)
                {
                    _style = value;
                    //切换布局方法
                    ResetLayout(value);
                    //立即调整布局
                    Relayout?.Invoke(this.DisplayRectangle.Width, this.DisplayRectangle.Height);
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
            this.MouseEnter += (s, e) => { DUISpliteLine.BackColor = Color.Red; };
            this.MouseLeave += (s, e) => { DUISpliteLine.BackColor = Color.DeepSkyBlue; };

            Style = CardStyles.Normal;
            Relayout = NormalLayout;
            Relayout?.Invoke(this.Width, this.Height);
        }

        #region 布局

        /// <summary>
        /// 初始化卡片控件容器布局
        /// </summary>
        public override void InitializeLayout()
        {
            this.MinimumSize = new Size(212, 62);
            this.BackColor = Color.White;

            //创建虚拟控件对象 并 维护子虚拟控件列表
            Add(DUIPublishTimeLabel = new ControlBase());
            Add(DUIDescriptionLabel = new ControlBase());
            Add(DUIPreviewImageBox = new ControlBase());
            Add(DUIStateLabel = new ControlBase());
            Add(DUITitleLabel = new ControlBase());
            Add(DUILocationButton = new ControlBase());
            Add(DUIReadedButton = new ControlBase());
            Add(DUIBrowserButton = new ControlBase());
            Add(DUIDeleteButton = new ControlBase());
            Add(DUIMainButton = new ControlBase());
            Add(DUISpliteLine = new ControlBase());

            this.SuspendPaint();

            #region 发布时间标签

            DUIPublishTimeLabel.Name = "发布时间标签";
            DUIPublishTimeLabel.Text = DateTime.Now.ToString();
            DUIPublishTimeLabel.ForeColor = Color.Gray;
            DUIPublishTimeLabel.Image = UnityResource.ClockIcon;
            DUIPublishTimeLabel.ImageAlign = ContentAlignment.MiddleLeft;
            DUIPublishTimeLabel.ShowEllipsis = true;
            DUIPublishTimeLabel.TextAlign = ContentAlignment.MiddleCenter;
            DUIPublishTimeLabel.MinSize = new Size(0, 28);
            DUIPublishTimeLabel.MaxSize = new Size(0, 28);
            //DUIPublishTimeLabel.BackColor = Color.MediumPurple;
            #endregion

            #region 文章描述标签

            DUIDescriptionLabel.Name = "文章描述标签";
            DUIDescriptionLabel.Text = "文章描述内容";
            DUIDescriptionLabel.TextAlign = ContentAlignment.TopLeft;
            DUIDescriptionLabel.ShowEllipsis = true;
            //DUIDescriptionLabel.BackColor = Color.LightGray;
            #endregion

            #region 预览图相框

            DUIPreviewImageBox.Name = "预览图像框";
            DUIPreviewImageBox.BackgroundImage = UnityResource.BrokenImage; /* TODO: 文章预览图像 */
            DUIPreviewImageBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            DUIPreviewImageBox.BorderColor = Color.WhiteSmoke;
            DUIPreviewImageBox.BorderSize = 1;
            DUIPreviewImageBox.BorderStyle = ButtonBorderStyle.Solid;
            DUIPreviewImageBox.ForeColor = Color.Gray;
            DUIPreviewImageBox.TextAlign = ContentAlignment.BottomLeft;
            //DUIPreviewImageBox.BackColor = Color.Red;
            #endregion

            #region 状态栏标签

            DUIStateLabel.Name = "状态栏标签";
            DUIStateLabel.Text = "爸爸，点击右边的按钮开始下载";
            DUIStateLabel.ForeColor = Color.Gray;
            DUIStateLabel.Image = UnityResource.DownloadIcon;
            DUIStateLabel.ImageAlign = ContentAlignment.MiddleLeft;
            DUIStateLabel.ShowEllipsis = false;
            DUIStateLabel.TextAlign = ContentAlignment.MiddleCenter;
            DUIStateLabel.MinSize = new Size(0, 28);
            DUIStateLabel.MaxSize = new Size(0, 28);
            //DUIStateLabel.BackColor = Color.Pink;
            #endregion

            #region 标题标签

            DUITitleLabel.Name = "标题标签";
            DUITitleLabel.Text = "标题标签";
            DUITitleLabel.ForeColor = Color.Orange;
            DUITitleLabel.ShowEllipsis = true;
            DUITitleLabel.Mouseable = true;
            DUITitleLabel.Font = new Font(DUITitleLabel.Font, FontStyle.Bold);
            DUITitleLabel.Click += (s, e) => { /* TODO: 抛出点击标题事件 */ };
            DUITitleLabel.MaxSize = new Size(0, 28);
            DUITitleLabel.MinSize = new Size(0, 28);
            DUITitleLabel.MouseEnter += (s, e) => { DUITitleLabel.ForeColor = Color.OrangeRed; };
            DUITitleLabel.MouseLeave += (s, e) => { DUITitleLabel.ForeColor = Color.Orange; };
            DUITitleLabel.MouseDown += (s, e) => { DUITitleLabel.ForeColor = Color.Chocolate; };
            DUITitleLabel.MouseUp += (s, e) => { DUITitleLabel.ForeColor = Color.OrangeRed; };
            //DUITitleLabel.BackColor = Color.Orange;
            #endregion

            #region 定位按钮

            DUILocationButton.Name = "定位按钮";
            DUILocationButton.Mouseable = true;
            DUILocationButton.Image = UnityResource.Location_0;
            DUILocationButton.Click += (s, e) => { /* TODO: 抛出点击定位按钮事件 */ };
            DUILocationButton.MouseEnter += (s, e) => { this.Invalidate(DUILocationButton.Rectangle); DUILocationButton.Image = UnityResource.Location_1; };
            DUILocationButton.MouseLeave += (s, e) => { this.Invalidate(DUILocationButton.Rectangle); DUILocationButton.Image = UnityResource.Location_0; };
            DUILocationButton.MaxSize = new Size(28, 28);
            DUILocationButton.MinSize = new Size(0, 28);
            //DUILocationButton.BackColor = Color.CadetBlue;
            #endregion

            #region 置为已读按钮

            DUIReadedButton.Name = "已读按钮";
            DUIReadedButton.Mouseable = true;
            DUIReadedButton.Image = UnityResource.Flag_0;
            DUIReadedButton.Click += (s, e) => { /* TODO: 抛出点击已读按钮事件 */ };
            DUIReadedButton.MouseEnter += (s, e) => { this.Invalidate(DUIReadedButton.Rectangle); DUIReadedButton.Image = UnityResource.Flag_1; };
            DUIReadedButton.MouseLeave += (s, e) => { this.Invalidate(DUIReadedButton.Rectangle); DUIReadedButton.Image = UnityResource.Flag_0; };
            DUIReadedButton.MaxSize = new Size(28, 28);
            DUIReadedButton.MinSize = new Size(0, 28);
            //DUIReadedButton.BackColor = Color.CornflowerBlue;
            #endregion

            #region 浏览按钮

            DUIBrowserButton.Name = "浏览按钮";
            DUIBrowserButton.Mouseable = true;
            DUIBrowserButton.Image = UnityResource.Browser_0;
            DUIBrowserButton.Click += (s, e) => { /* TODO: 抛出点击浏览按钮事件 */ };
            DUIBrowserButton.MouseEnter += (s, e) => { this.Invalidate(DUIBrowserButton.Rectangle); DUIBrowserButton.Image = UnityResource.Browser_1; };
            DUIBrowserButton.MouseLeave += (s, e) => { this.Invalidate(DUIBrowserButton.Rectangle); DUIBrowserButton.Image = UnityResource.Browser_0; };
            DUIBrowserButton.MaxSize = new Size(28, 28);
            DUIBrowserButton.MinSize = new Size(0, 28);
            //DUIBrowserButton.BackColor = Color.BlueViolet;
            #endregion

            #region 删除按钮

            DUIDeleteButton.Name = "删除按钮";
            DUIDeleteButton.Mouseable = true;
            DUIDeleteButton.Image = UnityResource.Delete_0;
            DUIDeleteButton.Click += (s, e) => { /* TODO: 抛出点击删除按钮事件 */ };
            DUIDeleteButton.MouseEnter += (s, e) => { this.Invalidate(DUIDeleteButton.Rectangle); DUIDeleteButton.Image = UnityResource.Delete_1; };
            DUIDeleteButton.MouseLeave += (s, e) => { this.Invalidate(DUIDeleteButton.Rectangle); DUIDeleteButton.Image = UnityResource.Delete_0; };
            DUIDeleteButton.MaxSize = new Size(28, 28);
            DUIDeleteButton.MinSize = new Size(0, 28);
            //DUIDeleteButton.BackColor = Color.DodgerBlue;
            #endregion

            #region 主按钮

            DUIMainButton.Name = "主按钮";
            DUIMainButton.Mouseable = true;
            DUIMainButton.BackgroundImage = UnityResource.Button_0;
            DUIMainButton.BackgroundImageLayout = ImageLayout.Stretch;
            DUIMainButton.Padding = new Padding(6, 0, 6, 0);
            DUIMainButton.Click += (s, e) => { /* TODO: 抛出点击主按钮事件 */ };
            DUIMainButton.MouseEnter += (s, e) => { this.Invalidate(DUIMainButton.Rectangle); DUIMainButton.BackgroundImage = UnityResource.Button_1; };
            DUIMainButton.MouseLeave += (s, e) => { this.Invalidate(DUIMainButton.Rectangle); DUIMainButton.BackgroundImage = UnityResource.Button_0; };
            DUIMainButton.MouseDown += (s, e) => { this.Invalidate(DUIMainButton.Rectangle); DUIMainButton.BackgroundImage = UnityResource.Button_2; };
            DUIMainButton.MouseUp += (s, e) => { this.Invalidate(DUIMainButton.Rectangle); DUIMainButton.BackgroundImage = UnityResource.Button_1; };
            DUIMainButton.MaxSize = new Size(112, 28);
            DUIMainButton.MinSize = new Size(0, 28);
            //DUIMainButton.BackColor = Color.LightGreen;
            #endregion

            #region 分割线

            DUISpliteLine.Name = "分割线";
            DUISpliteLine.BackColor = Color.DeepSkyBlue;
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
            Relayout?.Invoke(width, height);

            this.ResumePaint();
        }

        /// <summary>
        /// 根据布局样式重新绑定布局委托
        /// </summary>
        /// <param name="styles">布局样式</param>
        protected virtual void ResetLayout(CardStyles styles)
        {
            //TODO: [提醒] 这里放置切换布局模式时调用一次即可而不用在响应布局时频繁调用的代码。
            this.SuspendPaint();

            switch (styles)
            {
                case CardStyles.Normal:
                    {
                        DUIDescriptionLabel.Visible = true;
                        DUIPreviewImageBox.Visible = true;
                        DUIPublishTimeLabel.Visible = true;
                        DUIStateLabel.Visible = true;

                        this.MaximumSize = new Size(0, 0);
                        this.MinimumSize = new Size(212, 62);
                        Relayout = NormalLayout;
                        break;
                    }
                case CardStyles.Small:
                    {
                        DUIDescriptionLabel.Visible = false;
                        DUIPreviewImageBox.Visible = false;
                        DUIPublishTimeLabel.Visible = false;
                        DUIStateLabel.Visible = false;

                        this.MaximumSize = new Size(1000, 32);
                        this.MinimumSize = new Size(112, 32);
                        Relayout = SmallLayout;
                        break;
                    }
                case CardStyles.Large:
                    {
                        DUIDescriptionLabel.Visible = false;
                        DUIPreviewImageBox.Visible = true;
                        DUIPublishTimeLabel.Visible = true;
                        DUIStateLabel.Visible = true;
                        DUIPreviewImageBox.Text = DUIDescriptionLabel.Text;

                        this.MaximumSize = new Size(0, 0);
                        this.MinimumSize = new Size(112, 62);
                        Relayout = LargeLayout;
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

            DUISpliteLine.SetBounds(0, 0, width, 3);

            DUIMainButton.SetLocation(width - 112, 4);

            DUIDeleteButton.Width = DUIMainButton.Left - 28;
            DUIDeleteButton.SetLocation(DUIMainButton.Left - DUIDeleteButton.Width, 4);

            DUIBrowserButton.Width = DUIDeleteButton.Left - 28;
            DUIBrowserButton.SetLocation(DUIDeleteButton.Left - DUIBrowserButton.Width, 4);

            DUILocationButton.Width = DUIBrowserButton.Left - 28;
            DUILocationButton.SetLocation(DUIBrowserButton.Left - DUILocationButton.Width, 4);

            DUIReadedButton.Width = DUILocationButton.Left - 28;
            DUIReadedButton.SetLocation(DUILocationButton.Left - DUIReadedButton.Width, 4);

            DUITitleLabel.SetLocation(0, 0);
            DUITitleLabel.SetSize(DUIReadedButton.Left, 28);

            this.ResumePaint();
        }

        /// <summary>
        /// 正常布局
        /// </summary>
        protected virtual void NormalLayout(int width, int height)
        {
            this.SuspendPaint();

            DUISpliteLine.SetBounds(0, 0, width, 6);

            DUIPreviewImageBox.SetBounds(0, 6, Math.Min((int)((25.0 / 14.0) * (height - 6)), width), height - 6);

            DUIDeleteButton.Width = width - DUIPreviewImageBox.Right;
            DUIDeleteButton.SetLocation(width - DUIDeleteButton.Width, 6);

            DUIBrowserButton.Width = DUIDeleteButton.Left - DUIPreviewImageBox.Right;
            DUIBrowserButton.SetLocation(DUIDeleteButton.Left - DUIBrowserButton.Width, 6);

            DUILocationButton.Width = DUIBrowserButton.Left - DUIPreviewImageBox.Right;
            DUILocationButton.SetLocation(DUIBrowserButton.Left - DUILocationButton.Width, 6);

            DUIReadedButton.Width = DUILocationButton.Left - DUIPreviewImageBox.Right;
            DUIReadedButton.SetLocation(DUILocationButton.Left - DUIReadedButton.Width, 6);

            DUITitleLabel.SetLocation(DUIPreviewImageBox.Right, 6);
            DUITitleLabel.Width = DUIReadedButton.Left - DUIPreviewImageBox.Right;

            DUIMainButton.Width = width - DUIPreviewImageBox.Width;
            DUIMainButton.Left = width - DUIMainButton.Width;
            DUIMainButton.Top = Math.Max(height - 28, DUITitleLabel.Bottom);

            DUIStateLabel.Width = Math.Min(DUIMainButton.Left - DUIPreviewImageBox.Right, 240);
            DUIStateLabel.Left = DUIMainButton.Left - DUIStateLabel.Width;
            DUIStateLabel.Top = DUIMainButton.Top;

            DUIPublishTimeLabel.SetLocation(DUIPreviewImageBox.Right, DUIMainButton.Top);
            DUIPublishTimeLabel.Width = DUIStateLabel.Left - DUIPreviewImageBox.Width;

            DUIDescriptionLabel.SetBounds(
                DUITitleLabel.Left,
                DUITitleLabel.Bottom,
                DUIMainButton.Right - DUIPreviewImageBox.Right,
                DUIMainButton.Top - DUITitleLabel.Bottom
                );

            this.ResumePaint();
        }

        /// <summary>
        /// 巨幅布局
        /// </summary>
        protected virtual void LargeLayout(int width, int height)
        {
            this.SuspendPaint();

            DUISpliteLine.SetBounds(0, 0, width, 6);

            DUIPreviewImageBox.SetBounds(0, 34, width, height - 62);

            DUIDeleteButton.Width = width;
            DUIDeleteButton.SetLocation(width - DUIDeleteButton.Width, 6);

            DUIBrowserButton.Width = DUIDeleteButton.Left;
            DUIBrowserButton.SetLocation(DUIDeleteButton.Left - DUIBrowserButton.Width, 6);

            DUILocationButton.Width = DUIBrowserButton.Left;
            DUILocationButton.SetLocation(DUIBrowserButton.Left - DUILocationButton.Width, 6);

            DUIReadedButton.Width = DUILocationButton.Left;
            DUIReadedButton.SetLocation(DUILocationButton.Left - DUIReadedButton.Width, 6);

            DUITitleLabel.SetBounds(0, 6, width - DUIReadedButton.Left, 28);

            DUIMainButton.Width = width;
            DUIMainButton.Left = width - DUIMainButton.Width;
            DUIMainButton.Top = height - 28;

            DUIStateLabel.Width = Math.Min(DUIMainButton.Left, 200);
            DUIStateLabel.SetLocation(DUIMainButton.Left - DUIStateLabel.Width,DUIMainButton.Top);

            DUIPublishTimeLabel.SetLocation(0, DUIMainButton.Top);
            DUIPublishTimeLabel.Width = DUIStateLabel.Left;

            this.ResumePaint();
        }

        #endregion

        //TODO: 调试代码，记得删掉哈
        public override void Add(ControlBase control)
        {
            base.Add(control);
            return;
            control.Mouseable = true;
            control.MouseEnter += (s, e) => { (s as ControlBase).Text = (s as ControlBase).Name; };
            control.MouseLeave += (s, e) => { (s as ControlBase).Text = string.Empty; };
        }

    }
}
