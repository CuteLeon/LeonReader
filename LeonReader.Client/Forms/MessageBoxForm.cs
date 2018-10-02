using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace LeonReader.Client
{
    public sealed partial class MessageBoxForm : MetroForm
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public enum MessageType
        {
            /// <summary>
            /// 消息
            /// </summary>
            Info,
            /// <summary>
            /// 询问
            /// </summary>
            Question,
            /// <summary>
            /// 警告
            /// </summary>
            Warning,
            /// <summary>
            /// 错误
            /// </summary>
            Error
        }

        private MessageBoxForm()
        {
            this.InitializeComponent();
        }

        public MessageBoxForm(string caption, string message, MessageType type) : this()
        {
            this.Text = caption;
            this.MessageLabel.Text = message;

            ImageList ButtonImageList = new ImageList() {
                ColorDepth = ColorDepth.Depth24Bit,
                ImageSize = new Size(112, 28),
            };
            ButtonImageList.Images.Add(UnityResource.Button_0);
            ButtonImageList.Images.Add(UnityResource.Button_1);
            ButtonImageList.Images.Add(UnityResource.Button_2);
            this.MSAcceptButton.ImageList = ButtonImageList;
            this.MSCancelButton.ImageList = ButtonImageList;
            this.MSAcceptButton.ImageIndex = 0;
            this.MSCancelButton.ImageIndex = 0;

            this.MSAcceptButton.MouseEnter += (s, e) => this.MSAcceptButton.ImageIndex = 1;
            this.MSAcceptButton.MouseLeave += (s, e) => this.MSAcceptButton.ImageIndex = 0;
            this.MSAcceptButton.MouseDown += (s, e) => this.MSAcceptButton.ImageIndex = 2;
            this.MSAcceptButton.MouseUp += (s, e) => this.MSAcceptButton.ImageIndex = 1;
            this.MSCancelButton.MouseEnter += (s, e) => this.MSCancelButton.ImageIndex = 1;
            this.MSCancelButton.MouseLeave += (s, e) => this.MSCancelButton.ImageIndex = 0;
            this.MSCancelButton.MouseDown += (s, e) => this.MSCancelButton.ImageIndex = 2;
            this.MSCancelButton.MouseUp += (s, e) => this.MSCancelButton.ImageIndex = 1;

            this.Icon = UnityResource.LeonReader;
            switch (type)
            {
                case MessageType.Info:
                    {
                        this.Style = MetroFramework.MetroColorStyle.Green;
                        this.IconLabel.Image = UnityResource.InfoIcon;
                        break;
                    }
                case MessageType.Question:
                    {
                        this.Style = MetroFramework.MetroColorStyle.Blue;
                        this.IconLabel.Image = UnityResource.QuestionIcon;
                        this.MSCancelButton.Show();
                        this.MSAcceptButton.Left = (this.ButtonsPanel.Width - this.MSAcceptButton.Width - this.MSCancelButton.Width - 20) / 2;
                        this.MSCancelButton.Left = this.MSAcceptButton.Right + 20;
                        break;
                    }
                case MessageType.Warning:
                    {
                        this.Style = MetroFramework.MetroColorStyle.Orange;
                        this.IconLabel.Image = UnityResource.WarningIcon;

                        break;
                    }
                case MessageType.Error:
                    {
                        this.Style = MetroFramework.MetroColorStyle.Red;
                        this.IconLabel.Image = UnityResource.ErrorIcon;

                        break;
                    }
            }
        }

    }
}
