using System;
using System.Drawing;

using LeonDirectUI.Container;
using LeonDirectUI.DUIControl;

namespace LeonReader.Client.DirectUI.Container
{
    public class ToolContainer : ContainerBase
    {
        #region 虚拟容器

        /// <summary>
        /// 后退按钮
        /// </summary>
        ControlBase GoBackButton;

        /// <summary>
        /// 刷新按钮
        /// </summary>
        ControlBase RefreshButton;

        /// <summary>
        /// 日志按钮
        /// </summary>
        ControlBase LogButton;

        #endregion

        #region 事件

        /// <summary>
        /// 点击后退按钮
        /// </summary>
        public event EventHandler GoBackClick;

        /// <summary>
        /// 点击刷新按钮
        /// </summary>
        public event EventHandler RefreshClick;

        /// <summary>
        /// 点击日志按钮
        /// </summary>
        public event EventHandler LogClick;

        #endregion

        #region 初始化布局

        /// <summary>
        /// 初始化布局
        /// </summary>
        public override void InitializeLayout()
        {
            this.MaximumSize = new Size(150, 40);
            this.MinimumSize = new Size(150, 40);

            this.Add(this.GoBackButton = new ControlBase());
            this.Add(this.RefreshButton = new ControlBase());
            this.Add(this.LogButton = new ControlBase());

            this.GoBackButton.SetLocation(5, 0);
            this.GoBackButton.Mouseable = true;
            this.GoBackButton.MaxSize = new Size(40, 40);
            this.GoBackButton.MinSize = new Size(40, 40);
            this.GoBackButton.Image = UnityResource.GoBack_0;
            this.GoBackButton.MouseEnter += (s, e) => { this.Invalidate(this.GoBackButton.Rectangle); this.GoBackButton.Image = UnityResource.GoBack_1; };
            this.GoBackButton.MouseLeave += (s, e) => { this.Invalidate(this.GoBackButton.Rectangle); this.GoBackButton.Image = UnityResource.GoBack_0; };
            this.GoBackButton.Click += (s, e) => { GoBackClick?.Invoke(this, EventArgs.Empty); };

            this.RefreshButton.SetLocation(55, 0);
            this.RefreshButton.Mouseable = true;
            this.RefreshButton.MaxSize = new Size(40, 40);
            this.RefreshButton.MinSize = new Size(40, 40);
            this.RefreshButton.Image = UnityResource.Refresh_0;
            this.RefreshButton.MouseEnter += (s, e) => { this.Invalidate(this.RefreshButton.Rectangle); this.RefreshButton.Image = UnityResource.Refresh_1; };
            this.RefreshButton.MouseLeave += (s, e) => { this.Invalidate(this.RefreshButton.Rectangle); this.RefreshButton.Image = UnityResource.Refresh_0; };
            this.RefreshButton.Click += (s, e) => { RefreshClick?.Invoke(this, EventArgs.Empty); };

            this.LogButton.SetLocation(105, 0);
            this.LogButton.Mouseable = true;
            this.LogButton.MaxSize = new Size(40, 40);
            this.LogButton.MinSize = new Size(40, 40);
            this.LogButton.Image = UnityResource.Log_0;
            this.LogButton.MouseEnter += (s, e) => { this.Invalidate(this.LogButton.Rectangle); this.LogButton.Image = UnityResource.Log_1; };
            this.LogButton.MouseLeave += (s, e) => { this.Invalidate(this.LogButton.Rectangle); this.LogButton.Image = UnityResource.Log_0; };
            this.LogButton.Click += (s, e) => { LogClick?.Invoke(this, EventArgs.Empty); };
        }

        /// <summary>
        /// 响应式布局
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public override void ResetSize(int width, int height) { }

        #endregion

    }
}
