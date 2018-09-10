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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.articleCard5 = new LeonReader.Client.Controls.ArticleCard();
            this.articleCard1 = new LeonReader.Client.Controls.ArticleCard();
            this.articleCard2 = new LeonReader.Client.Controls.ArticleCard();
            this.articleCard3 = new LeonReader.Client.Controls.ArticleCard();
            this.articleCard4 = new LeonReader.Client.Controls.ArticleCard();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 62);
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
            this.button2.Size = new System.Drawing.Size(102, 62);
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
            this.button3.Size = new System.Drawing.Size(102, 62);
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
            this.button4.Size = new System.Drawing.Size(102, 62);
            this.button4.TabIndex = 3;
            this.button4.Text = "GS 导出";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(3, 275);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(102, 62);
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
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.Controls.Add(this.articleCard5);
            this.flowLayoutPanel2.Controls.Add(this.articleCard1);
            this.flowLayoutPanel2.Controls.Add(this.articleCard2);
            this.flowLayoutPanel2.Controls.Add(this.articleCard3);
            this.flowLayoutPanel2.Controls.Add(this.articleCard4);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(115, 10);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(703, 475);
            this.flowLayoutPanel2.TabIndex = 7;
            this.flowLayoutPanel2.WrapContents = false;
            // 
            // articleCard5
            // 
            this.articleCard5.Location = new System.Drawing.Point(3, 3);
            this.articleCard5.MinimumSize = new System.Drawing.Size(100, 28);
            this.articleCard5.Name = "articleCard5";
            this.articleCard5.Size = new System.Drawing.Size(642, 112);
            this.articleCard5.TabIndex = 12;
            this.articleCard5.Text = "articleCard5";
            // 
            // articleCard1
            // 
            this.articleCard1.Location = new System.Drawing.Point(3, 121);
            this.articleCard1.MinimumSize = new System.Drawing.Size(100, 28);
            this.articleCard1.Name = "articleCard1";
            this.articleCard1.Size = new System.Drawing.Size(642, 72);
            this.articleCard1.TabIndex = 13;
            this.articleCard1.Text = "articleCard1";
            // 
            // articleCard2
            // 
            this.articleCard2.Location = new System.Drawing.Point(3, 199);
            this.articleCard2.MinimumSize = new System.Drawing.Size(100, 28);
            this.articleCard2.Name = "articleCard2";
            this.articleCard2.Size = new System.Drawing.Size(642, 28);
            this.articleCard2.TabIndex = 14;
            this.articleCard2.Text = "articleCard2";
            // 
            // articleCard3
            // 
            this.articleCard3.Location = new System.Drawing.Point(3, 233);
            this.articleCard3.MinimumSize = new System.Drawing.Size(100, 28);
            this.articleCard3.Name = "articleCard3";
            this.articleCard3.Size = new System.Drawing.Size(642, 56);
            this.articleCard3.TabIndex = 15;
            this.articleCard3.Text = "articleCard3";
            // 
            // articleCard4
            // 
            this.articleCard4.Location = new System.Drawing.Point(3, 295);
            this.articleCard4.MinimumSize = new System.Drawing.Size(100, 28);
            this.articleCard4.Name = "articleCard4";
            this.articleCard4.Size = new System.Drawing.Size(287, 177);
            this.articleCard4.TabIndex = 16;
            this.articleCard4.Text = "articleCard4";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 495);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Main Form";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Controls.ArticleCard articleCard5;
        private Controls.ArticleCard articleCard1;
        private Controls.ArticleCard articleCard2;
        private Controls.ArticleCard articleCard3;
        private Controls.ArticleCard articleCard4;
    }
}

