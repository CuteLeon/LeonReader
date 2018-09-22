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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.cardContainer1 = new LeonReader.Client.DirectUI.Container.CardContainer();
            this.cardContainer2 = new LeonReader.Client.DirectUI.Container.CardContainer();
            this.cardContainer3 = new LeonReader.Client.DirectUI.Container.CardContainer();
            this.cardContainer8 = new LeonReader.Client.DirectUI.Container.CardContainer();
            this.cardContainer5 = new LeonReader.Client.DirectUI.Container.CardContainer();
            this.cardContainer6 = new LeonReader.Client.DirectUI.Container.CardContainer();
            this.cardContainer4 = new LeonReader.Client.DirectUI.Container.CardContainer();
            this.cardContainer7 = new LeonReader.Client.DirectUI.Container.CardContainer();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(115, 10);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(703, 475);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.flowLayoutPanel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(695, 449);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "正常";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel2.Controls.Add(this.cardContainer1);
            this.flowLayoutPanel2.Controls.Add(this.cardContainer2);
            this.flowLayoutPanel2.Controls.Add(this.cardContainer3);
            this.flowLayoutPanel2.Controls.Add(this.cardContainer8);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(689, 443);
            this.flowLayoutPanel2.TabIndex = 7;
            this.flowLayoutPanel2.WrapContents = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.flowLayoutPanel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(695, 449);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "精简";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoScroll = true;
            this.flowLayoutPanel3.Controls.Add(this.cardContainer5);
            this.flowLayoutPanel3.Controls.Add(this.cardContainer6);
            this.flowLayoutPanel3.Controls.Add(this.cardContainer4);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(689, 443);
            this.flowLayoutPanel3.TabIndex = 8;
            this.flowLayoutPanel3.WrapContents = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.flowLayoutPanel4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(695, 449);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "巨幅";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.AutoScroll = true;
            this.flowLayoutPanel4.Controls.Add(this.cardContainer7);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(689, 443);
            this.flowLayoutPanel4.TabIndex = 9;
            this.flowLayoutPanel4.WrapContents = false;
            // 
            // cardContainer1
            // 
            this.cardContainer1.BackColor = System.Drawing.Color.White;
            this.cardContainer1.Description = "文章描述内容";
            this.cardContainer1.Location = new System.Drawing.Point(3, 3);
            this.cardContainer1.MinimumSize = new System.Drawing.Size(212, 62);
            this.cardContainer1.Name = "cardContainer1";
            this.cardContainer1.PreviewImage = ((System.Drawing.Image)(resources.GetObject("cardContainer1.PreviewImage")));
            this.cardContainer1.PublishTime = "2018/9/22 20:30:40";
            this.cardContainer1.Size = new System.Drawing.Size(270, 120);
            this.cardContainer1.StateText = "爸爸，点击右边的按钮开始下载";
            this.cardContainer1.TabIndex = 0;
            this.cardContainer1.Text = "cardContainer1";
            this.cardContainer1.Title = "标题标签";
            // 
            // cardContainer2
            // 
            this.cardContainer2.BackColor = System.Drawing.Color.White;
            this.cardContainer2.Description = "文章描述内容";
            this.cardContainer2.Location = new System.Drawing.Point(3, 129);
            this.cardContainer2.MinimumSize = new System.Drawing.Size(212, 62);
            this.cardContainer2.Name = "cardContainer2";
            this.cardContainer2.PreviewImage = ((System.Drawing.Image)(resources.GetObject("cardContainer2.PreviewImage")));
            this.cardContainer2.PublishTime = "2018/9/22 20:30:41";
            this.cardContainer2.Size = new System.Drawing.Size(212, 62);
            this.cardContainer2.StateText = "爸爸，点击右边的按钮开始下载";
            this.cardContainer2.TabIndex = 1;
            this.cardContainer2.Text = "cardContainer2";
            this.cardContainer2.Title = "标题标签";
            // 
            // cardContainer3
            // 
            this.cardContainer3.BackColor = System.Drawing.Color.White;
            this.cardContainer3.Description = "文章描述内容";
            this.cardContainer3.Location = new System.Drawing.Point(3, 197);
            this.cardContainer3.MinimumSize = new System.Drawing.Size(212, 62);
            this.cardContainer3.Name = "cardContainer3";
            this.cardContainer3.PreviewImage = ((System.Drawing.Image)(resources.GetObject("cardContainer3.PreviewImage")));
            this.cardContainer3.PublishTime = "2018/9/22 20:30:42";
            this.cardContainer3.Size = new System.Drawing.Size(212, 126);
            this.cardContainer3.StateText = "爸爸，点击右边的按钮开始下载";
            this.cardContainer3.TabIndex = 2;
            this.cardContainer3.Text = "cardContainer3";
            this.cardContainer3.Title = "标题标签";
            // 
            // cardContainer8
            // 
            this.cardContainer8.BackColor = System.Drawing.Color.White;
            this.cardContainer8.Description = "文章描述内容";
            this.cardContainer8.Location = new System.Drawing.Point(3, 329);
            this.cardContainer8.MinimumSize = new System.Drawing.Size(212, 62);
            this.cardContainer8.Name = "cardContainer8";
            this.cardContainer8.PreviewImage = ((System.Drawing.Image)(resources.GetObject("cardContainer8.PreviewImage")));
            this.cardContainer8.PublishTime = "2018/9/22 20:30:42";
            this.cardContainer8.Size = new System.Drawing.Size(649, 95);
            this.cardContainer8.StateText = "爸爸，点击右边的按钮开始下载";
            this.cardContainer8.TabIndex = 3;
            this.cardContainer8.Text = "cardContainer8";
            this.cardContainer8.Title = "标题标签";
            // 
            // cardContainer5
            // 
            this.cardContainer5.BackColor = System.Drawing.Color.White;
            this.cardContainer5.Description = "文章描述内容";
            this.cardContainer5.Location = new System.Drawing.Point(3, 3);
            this.cardContainer5.MaximumSize = new System.Drawing.Size(1000, 32);
            this.cardContainer5.MinimumSize = new System.Drawing.Size(112, 32);
            this.cardContainer5.Name = "cardContainer5";
            this.cardContainer5.PreviewImage = null;
            this.cardContainer5.PublishTime = "2018/9/22 20:14:11";
            this.cardContainer5.Size = new System.Drawing.Size(317, 32);
            this.cardContainer5.StateText = "爸爸，点击右边的按钮开始下载";
            this.cardContainer5.Style = LeonReader.Client.DirectUI.Container.CardContainer.CardStyles.Small;
            this.cardContainer5.TabIndex = 1;
            this.cardContainer5.Text = "cardContainer5";
            this.cardContainer5.Title = "标题标签";
            // 
            // cardContainer6
            // 
            this.cardContainer6.BackColor = System.Drawing.Color.White;
            this.cardContainer6.Description = "文章描述内容";
            this.cardContainer6.Location = new System.Drawing.Point(3, 41);
            this.cardContainer6.MaximumSize = new System.Drawing.Size(1000, 32);
            this.cardContainer6.MinimumSize = new System.Drawing.Size(112, 32);
            this.cardContainer6.Name = "cardContainer6";
            this.cardContainer6.PreviewImage = null;
            this.cardContainer6.PublishTime = "2018/9/22 20:14:12";
            this.cardContainer6.Size = new System.Drawing.Size(212, 32);
            this.cardContainer6.StateText = "爸爸，点击右边的按钮开始下载";
            this.cardContainer6.Style = LeonReader.Client.DirectUI.Container.CardContainer.CardStyles.Small;
            this.cardContainer6.TabIndex = 2;
            this.cardContainer6.Text = "cardContainer6";
            this.cardContainer6.Title = "标题标签";
            // 
            // cardContainer4
            // 
            this.cardContainer4.BackColor = System.Drawing.Color.White;
            this.cardContainer4.Description = "文章描述内容";
            this.cardContainer4.Location = new System.Drawing.Point(3, 79);
            this.cardContainer4.MaximumSize = new System.Drawing.Size(1000, 32);
            this.cardContainer4.MinimumSize = new System.Drawing.Size(112, 32);
            this.cardContainer4.Name = "cardContainer4";
            this.cardContainer4.PreviewImage = null;
            this.cardContainer4.PublishTime = "2018/9/22 20:14:10";
            this.cardContainer4.Size = new System.Drawing.Size(473, 32);
            this.cardContainer4.StateText = "爸爸，点击右边的按钮开始下载";
            this.cardContainer4.Style = LeonReader.Client.DirectUI.Container.CardContainer.CardStyles.Small;
            this.cardContainer4.TabIndex = 0;
            this.cardContainer4.Text = "cardContainer4";
            this.cardContainer4.Title = "标题标签";
            // 
            // cardContainer7
            // 
            this.cardContainer7.BackColor = System.Drawing.Color.White;
            this.cardContainer7.Description = "文章描述内容";
            this.cardContainer7.Location = new System.Drawing.Point(3, 3);
            this.cardContainer7.MinimumSize = new System.Drawing.Size(112, 62);
            this.cardContainer7.Name = "cardContainer7";
            this.cardContainer7.PreviewImage = ((System.Drawing.Image)(resources.GetObject("cardContainer7.PreviewImage")));
            this.cardContainer7.PublishTime = "2018/9/22 20:56:01";
            this.cardContainer7.Size = new System.Drawing.Size(434, 346);
            this.cardContainer7.StateText = "爸爸，点击右边的按钮开始下载";
            this.cardContainer7.Style = LeonReader.Client.DirectUI.Container.CardContainer.CardStyles.Large;
            this.cardContainer7.TabIndex = 0;
            this.cardContainer7.Text = "cardContainer7";
            this.cardContainer7.Title = "标题标签";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 495);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Main Form";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private DirectUI.Container.CardContainer cardContainer4;
        private DirectUI.Container.CardContainer cardContainer5;
        private DirectUI.Container.CardContainer cardContainer6;
        private DirectUI.Container.CardContainer cardContainer1;
        private DirectUI.Container.CardContainer cardContainer2;
        private DirectUI.Container.CardContainer cardContainer3;
        private DirectUI.Container.CardContainer cardContainer8;
        private DirectUI.Container.CardContainer cardContainer7;
    }
}

