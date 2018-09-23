namespace LeonReader.Client
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.SADETabControl = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.UnityToolContainer = new LeonReader.Client.DirectUI.Container.ToolContainer();
            this.SADETabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // SADETabControl
            // 
            this.SADETabControl.Controls.Add(this.metroTabPage1);
            this.SADETabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SADETabControl.ItemSize = new System.Drawing.Size(102, 32);
            this.SADETabControl.Location = new System.Drawing.Point(10, 60);
            this.SADETabControl.Name = "SADETabControl";
            this.SADETabControl.SelectedIndex = 0;
            this.SADETabControl.Size = new System.Drawing.Size(808, 425);
            this.SADETabControl.TabIndex = 0;
            this.SADETabControl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SADETabControl.UseSelectable = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 36);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(800, 385);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "metroTabPage1";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // UnityToolContainer
            // 
            this.UnityToolContainer.Location = new System.Drawing.Point(318, 17);
            this.UnityToolContainer.MaximumSize = new System.Drawing.Size(150, 40);
            this.UnityToolContainer.MinimumSize = new System.Drawing.Size(150, 40);
            this.UnityToolContainer.Name = "UnityToolContainer";
            this.UnityToolContainer.Size = new System.Drawing.Size(150, 40);
            this.UnityToolContainer.TabIndex = 1;
            this.UnityToolContainer.Text = "toolContainer1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 495);
            this.Controls.Add(this.SADETabControl);
            this.Controls.Add(this.UnityToolContainer);
            this.MinimumSize = new System.Drawing.Size(500, 65);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Text = "Leon Reader";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SADETabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl SADETabControl;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private DirectUI.Container.ToolContainer UnityToolContainer;
    }
}

