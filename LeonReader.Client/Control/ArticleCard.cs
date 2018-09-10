using System;
using System.Collections.Generic;
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

        #region Rectangle
        private Rectangle previewImageRectangle;
        /// <summary>
        /// 预览图像区域
        /// </summary>
        protected Rectangle PreviewImageRectangle { get => previewImageRectangle; set => previewImageRectangle = value; }
       
        private Rectangle titleRectangle;
        /// <summary>
        /// 文章标题区域
        /// </summary>
        protected Rectangle TitleRectangle { get => titleRectangle; set => titleRectangle = value; }
        
        private Rectangle descriptionRectangle;
        /// <summary>
        /// 文章描述区域
        /// </summary>
        protected Rectangle DescriptionRectangle { get => descriptionRectangle; set => descriptionRectangle = value; }
        
        private Rectangle publishTimeRectangle;
        /// <summary>
        /// 发布时间区域
        /// </summary>
        protected Rectangle PublishTimeRectangle { get => publishTimeRectangle; set => publishTimeRectangle = value; }
        
        private Rectangle stateRectangle;
        /// <summary>
        /// 状态区域
        /// </summary>
        protected Rectangle StateRectangle { get => stateRectangle; set => stateRectangle = value; }
        
        private Rectangle mainButtonRectangle;
        /// <summary>
        /// 主按钮区域
        /// </summary>
        protected Rectangle MainButtonRectangle { get => mainButtonRectangle; set => mainButtonRectangle = value; }
        
        private Rectangle readedButtonRectangle;
        /// <summary>
        /// 已读按钮区域
        /// </summary>
        protected Rectangle ReadedButtonRectangle { get => readedButtonRectangle; set => readedButtonRectangle = value; }
        
        private Rectangle locationButtonRectangle;
        /// <summary>
        /// 定位文件夹按钮
        /// </summary>
        protected Rectangle LocationButtonRectangle { get => locationButtonRectangle; set => locationButtonRectangle = value; }
        
        private Rectangle browserButtonRectangle;
        /// <summary>
        /// 在浏览器打开按钮
        /// </summary>
        protected Rectangle BrowserButtonRectangle { get => browserButtonRectangle; set => browserButtonRectangle = value; }
        
        private Rectangle deleteButtonRectangle;
        /// <summary>
        /// 删除按钮
        /// </summary>
        protected Rectangle DeleteButtonRectangle { get => deleteButtonRectangle; set => deleteButtonRectangle = value; }
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
            ResetRectangle();
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

        public ArticleCard()
        {
            InitializeComponent();

            base.DoubleBuffered = true;
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.ResizeRedraw, false);
            base.SetStyle(ControlStyles.UserPaint, true);
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

            ResetRectangle();
        }

        protected virtual void ResetRectangle()
        {
            if (this.DisplayRectangle.Height > this.MinimumSize.Height)
            {
                //正常布局
                previewImageRectangle = new Rectangle(this.DisplayRectangle.Location,
                    new Size(Math.Min((int)((25.0 / 14.0) * this.DisplayRectangle.Height), this.DisplayRectangle.Width),
                    this.DisplayRectangle.Height));
                deleteButtonRectangle = new Rectangle(this.DisplayRectangle.Width - 28, 0, 28, 28);
                mainButtonRectangle.Size = new Size(Math.Min(this.DisplayRectangle.Width - previewImageRectangle.Width, 112), 28);
                mainButtonRectangle.Location = new Point(DisplayRectangle.Width - mainButtonRectangle.Width, Math.Max(DisplayRectangle.Height - 28, titleRectangle.Bottom));
            }
            else
            {
                //精简布局
                previewImageRectangle = new Rectangle(Point.Empty,Size.Empty);
                mainButtonRectangle = new Rectangle(DisplayRectangle.Width - 90, 0, 90, 28);
                deleteButtonRectangle = new Rectangle(mainButtonRectangle.Left - 28, 0, 28, 28);
                titleRectangle = new Rectangle(previewImageRectangle.Right, 0, mainButtonRectangle.Left - previewImageRectangle.Right, 28);
            }
            browserButtonRectangle = new Rectangle(deleteButtonRectangle.Left - 28, 0, 28, 28);
            locationButtonRectangle = new Rectangle(browserButtonRectangle.Left - 28, 0, 28, 28);
            readedButtonRectangle = new Rectangle(locationButtonRectangle.Left - 28, 0, 28, 28);
            titleRectangle = new Rectangle(previewImageRectangle.Right, 0, readedButtonRectangle.Left - previewImageRectangle.Right, 28);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.FillRectangle(Brushes.Red, previewImageRectangle);
            e.Graphics.DrawRectangle(Pens.DarkRed, previewImageRectangle.Left, previewImageRectangle.Top, previewImageRectangle.Width-1, previewImageRectangle.Height-1);

            e.Graphics.FillRectangle(Brushes.Orange, titleRectangle);
            e.Graphics.DrawRectangle(Pens.DarkOrange, titleRectangle.Left, titleRectangle.Top, titleRectangle.Width - 1, titleRectangle.Height - 1);

            e.Graphics.FillRectangle(Brushes.LightGreen,mainButtonRectangle);
            e.Graphics.DrawRectangle(Pens.Green, mainButtonRectangle.Left, mainButtonRectangle.Top, mainButtonRectangle.Width-1, mainButtonRectangle.Height-1);

            e.Graphics.FillRectangle(Brushes.DodgerBlue, deleteButtonRectangle);
            e.Graphics.DrawRectangle(Pens.DeepSkyBlue, deleteButtonRectangle.Left, deleteButtonRectangle.Top, deleteButtonRectangle.Width - 1, deleteButtonRectangle.Height - 1);
            e.Graphics.FillRectangle(Brushes.BlueViolet, browserButtonRectangle);
            e.Graphics.DrawRectangle(Pens.DeepSkyBlue, browserButtonRectangle.Left, browserButtonRectangle.Top, browserButtonRectangle.Width - 1, browserButtonRectangle.Height - 1);
            e.Graphics.FillRectangle(Brushes.CadetBlue, locationButtonRectangle);
            e.Graphics.DrawRectangle(Pens.DeepSkyBlue, locationButtonRectangle.Left, locationButtonRectangle.Top, locationButtonRectangle.Width - 1, locationButtonRectangle.Height - 1);
            e.Graphics.FillRectangle(Brushes.CornflowerBlue, readedButtonRectangle);
            e.Graphics.DrawRectangle(Pens.DeepSkyBlue, readedButtonRectangle.Left, readedButtonRectangle.Top, readedButtonRectangle.Width - 1, readedButtonRectangle.Height - 1);
        }

    }
}
