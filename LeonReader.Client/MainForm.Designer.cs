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
            this.SuspendLayout();
            // 
            // SADETabControl
            // 
            this.SADETabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SADETabControl.ItemSize = new System.Drawing.Size(102, 32);
            this.SADETabControl.Location = new System.Drawing.Point(10, 60);
            this.SADETabControl.Name = "SADETabControl";
            this.SADETabControl.Size = new System.Drawing.Size(808, 425);
            this.SADETabControl.TabIndex = 0;
            this.SADETabControl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SADETabControl.UseSelectable = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 495);
            this.Controls.Add(this.SADETabControl);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Text = "Leon Reader";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl SADETabControl;
    }
}

