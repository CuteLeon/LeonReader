namespace LeonReader.Client
{
    partial class MessageBoxForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ButtonsPanel = new MetroFramework.Controls.MetroPanel();
            this.MSAcceptButton = new System.Windows.Forms.Button();
            this.MSCancelButton = new System.Windows.Forms.Button();
            this.MainPanel = new MetroFramework.Controls.MetroPanel();
            this.MessageLabel = new MetroFramework.Controls.MetroLabel();
            this.IconLabel = new System.Windows.Forms.Label();
            this.ButtonsPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonsPanel
            // 
            this.ButtonsPanel.Controls.Add(this.MSAcceptButton);
            this.ButtonsPanel.Controls.Add(this.MSCancelButton);
            this.ButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonsPanel.HorizontalScrollbarBarColor = true;
            this.ButtonsPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.ButtonsPanel.HorizontalScrollbarSize = 10;
            this.ButtonsPanel.Location = new System.Drawing.Point(0, 78);
            this.ButtonsPanel.Name = "ButtonsPanel";
            this.ButtonsPanel.Size = new System.Drawing.Size(280, 32);
            this.ButtonsPanel.TabIndex = 1;
            this.ButtonsPanel.VerticalScrollbarBarColor = true;
            this.ButtonsPanel.VerticalScrollbarHighlightOnWheel = false;
            this.ButtonsPanel.VerticalScrollbarSize = 10;
            // 
            // MSAcceptButton
            // 
            this.MSAcceptButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.MSAcceptButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.MSAcceptButton.FlatAppearance.BorderSize = 0;
            this.MSAcceptButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.MSAcceptButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.MSAcceptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MSAcceptButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MSAcceptButton.Image = global::LeonReader.Client.UnityResource.Button_0;
            this.MSAcceptButton.Location = new System.Drawing.Point(84, 1);
            this.MSAcceptButton.Margin = new System.Windows.Forms.Padding(0);
            this.MSAcceptButton.Name = "MSAcceptButton";
            this.MSAcceptButton.Size = new System.Drawing.Size(114, 30);
            this.MSAcceptButton.TabIndex = 2;
            this.MSAcceptButton.Text = "确认";
            this.MSAcceptButton.UseVisualStyleBackColor = true;
            // 
            // MSCancelButton
            // 
            this.MSCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MSCancelButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.MSCancelButton.FlatAppearance.BorderSize = 0;
            this.MSCancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.MSCancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.MSCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MSCancelButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MSCancelButton.Image = global::LeonReader.Client.UnityResource.Button_0;
            this.MSCancelButton.Location = new System.Drawing.Point(84, 1);
            this.MSCancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.MSCancelButton.Name = "MSCancelButton";
            this.MSCancelButton.Size = new System.Drawing.Size(114, 30);
            this.MSCancelButton.TabIndex = 3;
            this.MSCancelButton.Text = "取消";
            this.MSCancelButton.UseVisualStyleBackColor = true;
            this.MSCancelButton.Visible = false;
            // 
            // MainPanel
            // 
            this.MainPanel.AutoSize = true;
            this.MainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MainPanel.Controls.Add(this.MessageLabel);
            this.MainPanel.Controls.Add(this.IconLabel);
            this.MainPanel.Controls.Add(this.ButtonsPanel);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.HorizontalScrollbarBarColor = true;
            this.MainPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.MainPanel.HorizontalScrollbarSize = 10;
            this.MainPanel.Location = new System.Drawing.Point(20, 60);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(280, 110);
            this.MainPanel.TabIndex = 2;
            this.MainPanel.VerticalScrollbarBarColor = true;
            this.MainPanel.VerticalScrollbarHighlightOnWheel = false;
            this.MainPanel.VerticalScrollbarSize = 10;
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.MessageLabel.LabelMode = MetroFramework.Controls.MetroLabelMode.Selectable;
            this.MessageLabel.Location = new System.Drawing.Point(50, 0);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(121, 19);
            this.MessageLabel.TabIndex = 7;
            this.MessageLabel.Text = "测试弹出消息内容";
            this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // IconLabel
            // 
            this.IconLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.IconLabel.Location = new System.Drawing.Point(0, 0);
            this.IconLabel.Margin = new System.Windows.Forms.Padding(0);
            this.IconLabel.MaximumSize = new System.Drawing.Size(50, 0);
            this.IconLabel.MinimumSize = new System.Drawing.Size(50, 50);
            this.IconLabel.Name = "IconLabel";
            this.IconLabel.Size = new System.Drawing.Size(50, 78);
            this.IconLabel.TabIndex = 6;
            // 
            // MessageBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(320, 180);
            this.Controls.Add(this.MainPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(320, 180);
            this.Name = "MessageBoxForm";
            this.Padding = new System.Windows.Forms.Padding(20, 60, 20, 10);
            this.Resizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "测试弹窗";
            this.ButtonsPanel.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroPanel ButtonsPanel;
        private MetroFramework.Controls.MetroPanel MainPanel;
        private System.Windows.Forms.Button MSCancelButton;
        private System.Windows.Forms.Button MSAcceptButton;
        private System.Windows.Forms.Label IconLabel;
        private MetroFramework.Controls.MetroLabel MessageLabel;
    }
}