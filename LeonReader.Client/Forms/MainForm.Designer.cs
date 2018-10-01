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
            this.CatalogTabControl = new MetroFramework.Controls.MetroTabControl();
            this.UnityToolContainer = new LeonReader.Client.DirectUI.Container.ToolContainer();
            this.SuspendLayout();
            // 
            // CatalogTabControl
            // 
            this.CatalogTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CatalogTabControl.ItemSize = new System.Drawing.Size(102, 32);
            this.CatalogTabControl.Location = new System.Drawing.Point(10, 60);
            this.CatalogTabControl.Name = "CatalogTabControl";
            this.CatalogTabControl.Size = new System.Drawing.Size(780, 530);
            this.CatalogTabControl.TabIndex = 0;
            this.CatalogTabControl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CatalogTabControl.UseSelectable = true;
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
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.CatalogTabControl);
            this.Controls.Add(this.UnityToolContainer);
            this.MinimumSize = new System.Drawing.Size(500, 65);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Text = "Leon Reader";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl CatalogTabControl;
        private DirectUI.Container.ToolContainer UnityToolContainer;
    }
}

