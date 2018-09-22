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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.CatalogLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.cardContainer1 = new LeonReader.Client.DirectUI.Container.CardContainer();
            this.cardContainer2 = new LeonReader.Client.DirectUI.Container.CardContainer();
            this.cardContainer3 = new LeonReader.Client.DirectUI.Container.CardContainer();
            this.cardContainer4 = new LeonReader.Client.DirectUI.Container.CardContainer();
            this.cardContainer5 = new LeonReader.Client.DirectUI.Container.CardContainer();
            this.cardContainer6 = new LeonReader.Client.DirectUI.Container.CardContainer();
            this.cardContainer7 = new LeonReader.Client.DirectUI.Container.CardContainer();
            this.cardContainer8 = new LeonReader.Client.DirectUI.Container.CardContainer();
            this.cardContainer9 = new LeonReader.Client.DirectUI.Container.CardContainer();
            this.flowLayoutPanel1.SuspendLayout();
            this.CatalogLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(3, 275);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(100, 62);
            this.button5.TabIndex = 4;
            this.button5.Text = "阅读";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(3, 207);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 62);
            this.button4.TabIndex = 3;
            this.button4.Text = "GS 导出";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(3, 139);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 62);
            this.button3.TabIndex = 2;
            this.button3.Text = "GS 下载";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(3, 71);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 62);
            this.button2.TabIndex = 1;
            this.button2.Text = "GS 分析";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 62);
            this.button1.TabIndex = 0;
            this.button1.Text = "GS 扫描";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Controls.Add(this.button3);
            this.flowLayoutPanel1.Controls.Add(this.button4);
            this.flowLayoutPanel1.Controls.Add(this.button5);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(105, 475);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // CatalogLayoutPanel
            // 
            this.CatalogLayoutPanel.AutoScroll = true;
            this.CatalogLayoutPanel.Controls.Add(this.cardContainer1);
            this.CatalogLayoutPanel.Controls.Add(this.cardContainer2);
            this.CatalogLayoutPanel.Controls.Add(this.cardContainer3);
            this.CatalogLayoutPanel.Controls.Add(this.cardContainer4);
            this.CatalogLayoutPanel.Controls.Add(this.cardContainer5);
            this.CatalogLayoutPanel.Controls.Add(this.cardContainer6);
            this.CatalogLayoutPanel.Controls.Add(this.cardContainer7);
            this.CatalogLayoutPanel.Controls.Add(this.cardContainer8);
            this.CatalogLayoutPanel.Controls.Add(this.cardContainer9);
            this.CatalogLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CatalogLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.CatalogLayoutPanel.Location = new System.Drawing.Point(115, 10);
            this.CatalogLayoutPanel.Name = "CatalogLayoutPanel";
            this.CatalogLayoutPanel.Size = new System.Drawing.Size(703, 475);
            this.CatalogLayoutPanel.TabIndex = 7;
            // 
            // cardContainer1
            // 
            this.cardContainer1.BackColor = System.Drawing.Color.White;
            this.cardContainer1.Description = "文章描述内容";
            this.cardContainer1.Location = new System.Drawing.Point(3, 3);
            this.cardContainer1.MinimumSize = new System.Drawing.Size(112, 62);
            this.cardContainer1.Name = "cardContainer1";
            this.cardContainer1.PreviewImage = ((System.Drawing.Image)(resources.GetObject("cardContainer1.PreviewImage")));
            this.cardContainer1.PublishTime = "2018/9/23 0:37:06";
            this.cardContainer1.Size = new System.Drawing.Size(313, 198);
            this.cardContainer1.Style = LeonReader.Client.DirectUI.Container.CardContainer.CardStyles.Large;
            this.cardContainer1.TabIndex = 0;
            this.cardContainer1.Text = "cardContainer1";
            this.cardContainer1.Title = "标题标签";
            // 
            // cardContainer2
            // 
            this.cardContainer2.BackColor = System.Drawing.Color.White;
            this.cardContainer2.Description = "文章描述内容";
            this.cardContainer2.Location = new System.Drawing.Point(3, 207);
            this.cardContainer2.MinimumSize = new System.Drawing.Size(112, 62);
            this.cardContainer2.Name = "cardContainer2";
            this.cardContainer2.PreviewImage = ((System.Drawing.Image)(resources.GetObject("cardContainer2.PreviewImage")));
            this.cardContainer2.PublishTime = "2018/9/23 0:37:06";
            this.cardContainer2.Size = new System.Drawing.Size(313, 198);
            this.cardContainer2.Style = LeonReader.Client.DirectUI.Container.CardContainer.CardStyles.Large;
            this.cardContainer2.TabIndex = 1;
            this.cardContainer2.Text = "cardContainer2";
            this.cardContainer2.Title = "标题标签";
            // 
            // cardContainer3
            // 
            this.cardContainer3.BackColor = System.Drawing.Color.White;
            this.cardContainer3.Description = "文章描述内容";
            this.cardContainer3.Location = new System.Drawing.Point(322, 3);
            this.cardContainer3.MinimumSize = new System.Drawing.Size(112, 62);
            this.cardContainer3.Name = "cardContainer3";
            this.cardContainer3.PreviewImage = ((System.Drawing.Image)(resources.GetObject("cardContainer3.PreviewImage")));
            this.cardContainer3.PublishTime = "2018/9/23 0:37:06";
            this.cardContainer3.Size = new System.Drawing.Size(313, 198);
            this.cardContainer3.Style = LeonReader.Client.DirectUI.Container.CardContainer.CardStyles.Large;
            this.cardContainer3.TabIndex = 2;
            this.cardContainer3.Text = "cardContainer3";
            this.cardContainer3.Title = "标题标签";
            // 
            // cardContainer4
            // 
            this.cardContainer4.BackColor = System.Drawing.Color.White;
            this.cardContainer4.Description = "文章描述内容";
            this.cardContainer4.Location = new System.Drawing.Point(322, 207);
            this.cardContainer4.MinimumSize = new System.Drawing.Size(212, 62);
            this.cardContainer4.Name = "cardContainer4";
            this.cardContainer4.PreviewImage = ((System.Drawing.Image)(resources.GetObject("cardContainer4.PreviewImage")));
            this.cardContainer4.PublishTime = "2018/9/23 0:37:06";
            this.cardContainer4.Size = new System.Drawing.Size(443, 62);
            this.cardContainer4.TabIndex = 3;
            this.cardContainer4.Text = "cardContainer4";
            this.cardContainer4.Title = "标题标签";
            // 
            // cardContainer5
            // 
            this.cardContainer5.BackColor = System.Drawing.Color.White;
            this.cardContainer5.Description = "文章描述内容";
            this.cardContainer5.Location = new System.Drawing.Point(322, 275);
            this.cardContainer5.MinimumSize = new System.Drawing.Size(212, 62);
            this.cardContainer5.Name = "cardContainer5";
            this.cardContainer5.PreviewImage = ((System.Drawing.Image)(resources.GetObject("cardContainer5.PreviewImage")));
            this.cardContainer5.PublishTime = "2018/9/23 0:37:06";
            this.cardContainer5.Size = new System.Drawing.Size(443, 62);
            this.cardContainer5.TabIndex = 4;
            this.cardContainer5.Text = "cardContainer5";
            this.cardContainer5.Title = "标题标签";
            // 
            // cardContainer6
            // 
            this.cardContainer6.BackColor = System.Drawing.Color.White;
            this.cardContainer6.Description = "文章描述内容";
            this.cardContainer6.Location = new System.Drawing.Point(322, 343);
            this.cardContainer6.MinimumSize = new System.Drawing.Size(212, 62);
            this.cardContainer6.Name = "cardContainer6";
            this.cardContainer6.PreviewImage = ((System.Drawing.Image)(resources.GetObject("cardContainer6.PreviewImage")));
            this.cardContainer6.PublishTime = "2018/9/23 0:37:06";
            this.cardContainer6.Size = new System.Drawing.Size(443, 62);
            this.cardContainer6.TabIndex = 5;
            this.cardContainer6.Text = "cardContainer6";
            this.cardContainer6.Title = "标题标签";
            // 
            // cardContainer7
            // 
            this.cardContainer7.BackColor = System.Drawing.Color.White;
            this.cardContainer7.Description = "文章描述内容";
            this.cardContainer7.Location = new System.Drawing.Point(322, 411);
            this.cardContainer7.MaximumSize = new System.Drawing.Size(1000, 32);
            this.cardContainer7.MinimumSize = new System.Drawing.Size(112, 32);
            this.cardContainer7.Name = "cardContainer7";
            this.cardContainer7.PreviewImage = ((System.Drawing.Image)(resources.GetObject("cardContainer7.PreviewImage")));
            this.cardContainer7.PublishTime = "2018/9/23 0:37:06";
            this.cardContainer7.Size = new System.Drawing.Size(443, 32);
            this.cardContainer7.Style = LeonReader.Client.DirectUI.Container.CardContainer.CardStyles.Small;
            this.cardContainer7.TabIndex = 6;
            this.cardContainer7.Text = "cardContainer7";
            this.cardContainer7.Title = "标题标签";
            // 
            // cardContainer8
            // 
            this.cardContainer8.BackColor = System.Drawing.Color.White;
            this.cardContainer8.Description = "文章描述内容";
            this.cardContainer8.Location = new System.Drawing.Point(771, 3);
            this.cardContainer8.MaximumSize = new System.Drawing.Size(1000, 32);
            this.cardContainer8.MinimumSize = new System.Drawing.Size(112, 32);
            this.cardContainer8.Name = "cardContainer8";
            this.cardContainer8.PreviewImage = ((System.Drawing.Image)(resources.GetObject("cardContainer8.PreviewImage")));
            this.cardContainer8.PublishTime = "2018/9/23 0:37:06";
            this.cardContainer8.Size = new System.Drawing.Size(443, 32);
            this.cardContainer8.Style = LeonReader.Client.DirectUI.Container.CardContainer.CardStyles.Small;
            this.cardContainer8.TabIndex = 7;
            this.cardContainer8.Text = "cardContainer8";
            this.cardContainer8.Title = "标题标签";
            // 
            // cardContainer9
            // 
            this.cardContainer9.BackColor = System.Drawing.Color.White;
            this.cardContainer9.Description = "文章描述内容";
            this.cardContainer9.Location = new System.Drawing.Point(771, 41);
            this.cardContainer9.MaximumSize = new System.Drawing.Size(1000, 32);
            this.cardContainer9.MinimumSize = new System.Drawing.Size(112, 32);
            this.cardContainer9.Name = "cardContainer9";
            this.cardContainer9.PreviewImage = ((System.Drawing.Image)(resources.GetObject("cardContainer9.PreviewImage")));
            this.cardContainer9.PublishTime = "2018/9/23 0:37:06";
            this.cardContainer9.Size = new System.Drawing.Size(443, 32);
            this.cardContainer9.Style = LeonReader.Client.DirectUI.Container.CardContainer.CardStyles.Small;
            this.cardContainer9.TabIndex = 8;
            this.cardContainer9.Text = "cardContainer9";
            this.cardContainer9.Title = "标题标签";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 495);
            this.Controls.Add(this.CatalogLayoutPanel);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Main Form";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.CatalogLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel CatalogLayoutPanel;
        private DirectUI.Container.CardContainer cardContainer1;
        private DirectUI.Container.CardContainer cardContainer2;
        private DirectUI.Container.CardContainer cardContainer3;
        private DirectUI.Container.CardContainer cardContainer4;
        private DirectUI.Container.CardContainer cardContainer5;
        private DirectUI.Container.CardContainer cardContainer6;
        private DirectUI.Container.CardContainer cardContainer7;
        private DirectUI.Container.CardContainer cardContainer8;
        private DirectUI.Container.CardContainer cardContainer9;
    }
}

