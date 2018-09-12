using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeonReader.Client.Controls
{
    /// <summary>
    /// 文章卡片控件
    /// </summary>
    public class ArticleCard : Control
    {
        /// <summary>
        /// 绘制计数器
        /// </summary>
        private int _paintCount = 0;

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

        private CardStyles _style = CardStyles.Normal;

        /// <summary>
        /// 卡片类型
        /// </summary>
        [Browsable(true)]
        [Description("设置卡片类型")]
        [DefaultValue(CardStyles.Normal)]
        public CardStyles Style
        {
            get => _style;
            set
            {
                _style = value;
                ResetLayout(value);
            }
        }

        /// <summary>
        /// 重新布局方法委托
        /// </summary>
        protected delegate void RelayoutDelegate();

        /// <summary>
        /// 重新布局方法委托
        /// </summary>
        protected RelayoutDelegate Relayout;

        #endregion

        #region 交互单元区域

        private Rectangle previewImageRectangle = new Rectangle();
        /// <summary>
        /// 预览图像区域
        /// </summary>
        protected Rectangle PreviewImageRectangle { get => previewImageRectangle; set => previewImageRectangle = value; }

        private Rectangle titleRectangle = new Rectangle();
        /// <summary>
        /// 文章标题区域
        /// </summary>
        protected Rectangle TitleRectangle { get => titleRectangle; set => titleRectangle = value; }

        private Rectangle descriptionRectangle = new Rectangle();
        /// <summary>
        /// 文章描述区域
        /// </summary>
        protected Rectangle DescriptionRectangle { get => descriptionRectangle; set => descriptionRectangle = value; }

        private Rectangle publishTimeRectangle = new Rectangle();
        /// <summary>
        /// 发布时间区域
        /// </summary>
        protected Rectangle PublishTimeRectangle { get => publishTimeRectangle; set => publishTimeRectangle = value; }

        private Rectangle stateRectangle = new Rectangle();
        /// <summary>
        /// 状态区域
        /// </summary>
        protected Rectangle StateRectangle { get => stateRectangle; set => stateRectangle = value; }

        private Rectangle mainButtonRectangle = new Rectangle();
        /// <summary>
        /// 主按钮区域
        /// </summary>
        protected Rectangle MainButtonRectangle { get => mainButtonRectangle; set => mainButtonRectangle = value; }

        private Rectangle readedButtonRectangle = new Rectangle();
        /// <summary>
        /// 已读按钮区域
        /// </summary>
        protected Rectangle ReadedButtonRectangle { get => readedButtonRectangle; set => readedButtonRectangle = value; }

        private Rectangle locationButtonRectangle = new Rectangle();
        /// <summary>
        /// 定位文件夹按钮
        /// </summary>
        protected Rectangle LocationButtonRectangle { get => locationButtonRectangle; set => locationButtonRectangle = value; }

        private Rectangle browserButtonRectangle = new Rectangle();
        /// <summary>
        /// 在浏览器打开按钮
        /// </summary>
        protected Rectangle BrowserButtonRectangle { get => browserButtonRectangle; set => browserButtonRectangle = value; }

        private Rectangle deleteButtonRectangle = new Rectangle();
        /// <summary>
        /// 删除按钮
        /// </summary>
        protected Rectangle DeleteButtonRectangle { get => deleteButtonRectangle; set => deleteButtonRectangle = value; }

        /// <summary>
        /// 区域数组，便于统一管理
        /// </summary>
        protected Rectangle[] UIRectangles;

        #endregion

        #region 属性
        //TODO: 映射为文章标题
        public override string Text { get => base.Text; set => base.Text = value; }
        public override Cursor Cursor { get => base.Cursor; set => base.Cursor = value; }
        public override bool Focused => base.Focused;
        //TODO: 映射为文章标题字体颜色
        public override Color ForeColor { get => base.ForeColor; set => base.ForeColor = value; }
        //TODO: 映射为文章标题字体
        public override Font Font { get => base.Font; set => base.Font = value; }
        protected override bool DoubleBuffered { get => base.DoubleBuffered; set => base.DoubleBuffered = value; }
        public override DockStyle Dock { get => base.Dock; set => base.Dock = value; }
        public override ImageLayout BackgroundImageLayout { get => base.BackgroundImageLayout; set => base.BackgroundImageLayout = value; }
        public override Image BackgroundImage { get => base.BackgroundImage; set => base.BackgroundImage = value; }
        public override Color BackColor { get => base.BackColor; set => base.BackColor = value; }
        public override Size MaximumSize { get => base.MaximumSize; set => base.MaximumSize = value; }
        public override Size MinimumSize { get => base.MinimumSize; set => base.MinimumSize = value; }
        #endregion

        #region 覆写方法

        protected override void OnSizeChanged(EventArgs e)
        {
            //尺寸改变重新布局
            Relayout?.Invoke();
            this.Invalidate();
            base.OnSizeChanged(e);
        }

        protected override void OnClick(EventArgs e)
        {
            //TODO: 先根据鼠标所在子Rectangle，判断触发自定义的卡片事件或普通的 base.OnClick(e);
            base.OnClick(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            //TODO: 刷新文章标题
            base.OnTextChanged(e);
        }

        #endregion

        #region 构造初始化

        public ArticleCard()
        {
            InitializeComponent();

            base.DoubleBuffered = true;
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.ResizeRedraw, false);
            base.SetStyle(ControlStyles.UserPaint, true);

            //注册UI区域
            UIRectangles = new Rectangle[] {
                previewImageRectangle,
                titleRectangle,
                readedButtonRectangle,
                locationButtonRectangle,
                browserButtonRectangle,
                deleteButtonRectangle,
                descriptionRectangle,
                publishTimeRectangle,
                stateRectangle,
                mainButtonRectangle,
            };
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ArticleCard
            // 
            this.MinimumSize = new System.Drawing.Size(100, 28);
            this.Size = new System.Drawing.Size(256, 28);
            this.ResumeLayout(false);

            ResetLayout(_style);
        }

        #endregion

        #region 布局

        /// <summary>
        /// 根据布局样式重新绑定布局委托并立即布局
        /// </summary>
        /// <param name="styles">布局样式</param>
        protected virtual void ResetLayout(CardStyles styles)
        {
            switch (styles)
            {
                case CardStyles.Normal:
                    {
                        Relayout = NormalLayout;
                        break;
                    }
                case CardStyles.Small:
                    {
                        Relayout = SmallLayout;
                        break;
                    }
                case CardStyles.Large:
                    {
                        Relayout = LargeLayout;
                        break;
                    }
            }
            Relayout();
        }

        /// <summary>
        /// 精简布局（允许覆写）
        /// </summary>
        protected virtual void SmallLayout()
        {
            //精简布局
            //        previewImageRectangle.X = previewImageRectangle.Y = previewImageRectangle.Width = previewImageRectangle.Height = 0;

            //        mainButtonRectangle.Width = 90;
            //        mainButtonRectangle.Height = 28;
            //        mainButtonRectangle.X = DisplayRectangle.Width - 90;
            //        mainButtonRectangle.Y = 0;

            //        deleteButtonRectangle.X = mainButtonRectangle.Left - 28;
            //        deleteButtonRectangle.Y = 0;
            //        deleteButtonRectangle.Width = deleteButtonRectangle.Height = 28;

            //        stateRectangle.Width = 0;
            //    }

            //    browserButtonRectangle = new Rectangle(deleteButtonRectangle.Left - 28, 0, 28, 28);
            //    locationButtonRectangle = new Rectangle(browserButtonRectangle.Left - 28, 0, 28, 28);
            //    readedButtonRectangle = new Rectangle(locationButtonRectangle.Left - 28, 0, 28, 28);
            //    titleRectangle = new Rectangle(previewImageRectangle.Right, 0, readedButtonRectangle.Left - previewImageRectangle.Right, 28);
            //    descriptionRectangle = new Rectangle(titleRectangle.Left, titleRectangle.Bottom, mainButtonRectangle.Right - previewImageRectangle.Right, Math.Max(0, mainButtonRectangle.Top - titleRectangle.Bottom));
        }

        /// <summary>
        /// 正常布局（允许覆写）
        /// </summary>
        protected virtual void NormalLayout()
        {
            previewImageRectangle.X = previewImageRectangle.Y = 0;
            previewImageRectangle.Width = Math.Min((int)((25.0 / 14.0) * this.DisplayRectangle.Height), this.DisplayRectangle.Width);
            previewImageRectangle.Height = this.DisplayRectangle.Height;

            deleteButtonRectangle.Y = 0;
            deleteButtonRectangle.Width = Math.Min(this.DisplayRectangle.Width-previewImageRectangle.Right, 28);
            deleteButtonRectangle.Height = 28;
            deleteButtonRectangle.X = this.DisplayRectangle.Width - deleteButtonRectangle.Width;

            browserButtonRectangle.Y = 0;
            browserButtonRectangle.Width = Math.Min(deleteButtonRectangle.Left - previewImageRectangle.Right, 28);
            browserButtonRectangle.Height = 28;
            browserButtonRectangle.X = deleteButtonRectangle.Left - browserButtonRectangle.Width;

            locationButtonRectangle.Y = 0;
            locationButtonRectangle.Width = Math.Min(browserButtonRectangle.Left - previewImageRectangle.Right, 28);
            locationButtonRectangle.Height = 28;
            locationButtonRectangle.X = browserButtonRectangle.Left - locationButtonRectangle.Width;

            readedButtonRectangle.Y = 0;
            readedButtonRectangle.Width = Math.Min(locationButtonRectangle.Left - previewImageRectangle.Right, 28);
            readedButtonRectangle.Height = 28;
            readedButtonRectangle.X = locationButtonRectangle.Left - readedButtonRectangle.Width;

            titleRectangle.X = previewImageRectangle.Right;
            titleRectangle.Y = 0;
            titleRectangle.Width = Math.Max(readedButtonRectangle.Left - previewImageRectangle.Right, 0);
            titleRectangle.Height = 28;

            mainButtonRectangle.Width = Math.Min(this.DisplayRectangle.Width - previewImageRectangle.Width, 112);
            mainButtonRectangle.Height = 28;
            mainButtonRectangle.X = this.DisplayRectangle.Width - mainButtonRectangle.Width;
            mainButtonRectangle.Y = Math.Max(DisplayRectangle.Height - 28, titleRectangle.Bottom);

            stateRectangle.Height = 28;
            stateRectangle.Width = Math.Min(mainButtonRectangle.Left-previewImageRectangle.Right, 180);
            stateRectangle.X = Math.Max(mainButtonRectangle.Left - stateRectangle.Width, 0);
            stateRectangle.Y = mainButtonRectangle.Y;

            publishTimeRectangle.X = previewImageRectangle.Right;
            publishTimeRectangle.Y = mainButtonRectangle.Top;
            publishTimeRectangle.Width = Math.Max(stateRectangle.Left - previewImageRectangle.Width, 0);
            publishTimeRectangle.Height = 28;

            descriptionRectangle = new Rectangle(titleRectangle.Left, titleRectangle.Bottom, mainButtonRectangle.Right - previewImageRectangle.Right, Math.Max(0, mainButtonRectangle.Top - titleRectangle.Bottom));
        }

        /// <summary>
        /// 巨幅布局（允许覆写）
        /// </summary>
        protected virtual void LargeLayout()
        {

        }

        #endregion

        #region 绘制

        protected override void OnPaint(PaintEventArgs e)
        {
            _paintCount++;
            //TODO: 仅绘制与 e.ClipRectangle 相交的区域，提高性能
            //Console.WriteLine($"绘制区域：{e.ClipRectangle.ToString()}");
            //foreach (var rectangle in GetIntersectedRectangles(e.ClipRectangle))
            //{
            //    RePaintRecangle();
            //}

            e.Graphics.FillRectangle(Brushes.Red, previewImageRectangle);
            e.Graphics.FillRectangle(Brushes.Orange, titleRectangle);
            e.Graphics.FillRectangle(Brushes.LightGreen, mainButtonRectangle);
            e.Graphics.FillRectangle(Brushes.LightGray, descriptionRectangle);
            e.Graphics.FillRectangle(Brushes.DodgerBlue, deleteButtonRectangle);
            e.Graphics.FillRectangle(Brushes.BlueViolet, browserButtonRectangle);
            e.Graphics.FillRectangle(Brushes.CadetBlue, locationButtonRectangle);
            e.Graphics.FillRectangle(Brushes.CornflowerBlue, readedButtonRectangle);
            e.Graphics.FillRectangle(Brushes.Pink, stateRectangle);
            e.Graphics.FillRectangle(Brushes.MediumPurple, publishTimeRectangle);

            e.Graphics.DrawRectangle(Pens.DarkRed, previewImageRectangle.Left, previewImageRectangle.Top, previewImageRectangle.Width - 1, previewImageRectangle.Height - 1);
            e.Graphics.DrawRectangle(Pens.DarkOrange, titleRectangle.Left, titleRectangle.Top, titleRectangle.Width - 1, titleRectangle.Height - 1);
            e.Graphics.DrawRectangle(Pens.Green, mainButtonRectangle.Left, mainButtonRectangle.Top, mainButtonRectangle.Width - 1, mainButtonRectangle.Height - 1);
            e.Graphics.DrawRectangle(Pens.Gray, descriptionRectangle.Left, descriptionRectangle.Top, descriptionRectangle.Width - 1, descriptionRectangle.Height - 1);
            e.Graphics.DrawRectangle(Pens.DeepSkyBlue, deleteButtonRectangle.Left, deleteButtonRectangle.Top, deleteButtonRectangle.Width - 1, deleteButtonRectangle.Height - 1);
            e.Graphics.DrawRectangle(Pens.DeepSkyBlue, browserButtonRectangle.Left, browserButtonRectangle.Top, browserButtonRectangle.Width - 1, browserButtonRectangle.Height - 1);
            e.Graphics.DrawRectangle(Pens.DeepSkyBlue, locationButtonRectangle.Left, locationButtonRectangle.Top, locationButtonRectangle.Width - 1, locationButtonRectangle.Height - 1);
            e.Graphics.DrawRectangle(Pens.DeepSkyBlue, readedButtonRectangle.Left, readedButtonRectangle.Top, readedButtonRectangle.Width - 1, readedButtonRectangle.Height - 1);
            e.Graphics.DrawRectangle(Pens.DeepPink, stateRectangle.Left, stateRectangle.Top, stateRectangle.Width - 1, stateRectangle.Height - 1);
            e.Graphics.DrawRectangle(Pens.Purple, publishTimeRectangle.Left, publishTimeRectangle.Top, publishTimeRectangle.Width - 1, publishTimeRectangle.Height - 1);

            e.Graphics.DrawString($"Style: {Style}, delegate: {Relayout.Method.Name}, paintCount: {_paintCount}", this.Font, Brushes.White, Point.Empty);

            //用于向外界广播 Paint 事件，并没有绘制功能，不需要可以不调用
            //base.OnPaint(e);
        }

        #endregion

        #region 区域计算方法

        /// <summary>
        /// 查询与目标区域相交的已注册UI区域
        /// </summary>
        /// <param name="targetRectangle">目标区域</param>
        /// <returns></returns>
        protected IEnumerable<Rectangle> GetIntersectedRectangles(Rectangle targetRectangle)
        {
            return UIRectangles.Where(rectangle => rectangle.IntersectsWith(targetRectangle));
        }

        /// <summary>
        /// 查询包含目标坐标的已注册UI区域
        /// </summary>
        /// <param name="targetPoint">目标坐标</param>
        /// <returns></returns>
        protected IEnumerable<Rectangle> GetContainedRectangles(Point targetPoint)
        {
            return UIRectangles.Where(rectangle => rectangle.Contains(targetPoint));
        }

        #endregion
    }
}
