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

            Add(GoBackButton = new ControlBase());
            Add(RefreshButton = new ControlBase());
            Add(LogButton = new ControlBase());

            GoBackButton.SetLocation(5, 0);
            GoBackButton.Mouseable = true;
            GoBackButton.MaxSize = new Size(40, 40);
            GoBackButton.MinSize = new Size(40, 40);
            GoBackButton.Image = UnityResource.GoBack_0;
            GoBackButton.MouseEnter += (s, e) => { this.Invalidate(GoBackButton.Rectangle); GoBackButton.Image = UnityResource.GoBack_1; };
            GoBackButton.MouseLeave += (s, e) => { this.Invalidate(GoBackButton.Rectangle); GoBackButton.Image = UnityResource.GoBack_0; };
            GoBackButton.Click += (s, e) => { GoBackClick?.Invoke(this, EventArgs.Empty); };

            RefreshButton.SetLocation(55, 0);
            RefreshButton.Mouseable = true;
            RefreshButton.MaxSize = new Size(40, 40);
            RefreshButton.MinSize = new Size(40, 40);
            RefreshButton.Image = UnityResource.Refresh_0;
            RefreshButton.MouseEnter += (s, e) => { this.Invalidate(RefreshButton.Rectangle); RefreshButton.Image = UnityResource.Refresh_1; };
            RefreshButton.MouseLeave += (s, e) => { this.Invalidate(RefreshButton.Rectangle); RefreshButton.Image = UnityResource.Refresh_0; };
            RefreshButton.Click += (s, e) => { RefreshClick?.Invoke(this, EventArgs.Empty); };

            LogButton.SetLocation(105, 0);
            LogButton.Mouseable = true;
            LogButton.MaxSize = new Size(40, 40);
            LogButton.MinSize = new Size(40, 40);
            LogButton.Image = UnityResource.Log_0;
            LogButton.MouseEnter += (s, e) => { this.Invalidate(LogButton.Rectangle); LogButton.Image = UnityResource.Log_1; };
            LogButton.MouseLeave += (s, e) => { this.Invalidate(LogButton.Rectangle); LogButton.Image = UnityResource.Log_0; };
            LogButton.Click += (s, e) => { LogClick?.Invoke(this, EventArgs.Empty); };
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
