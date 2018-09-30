using System.Windows.Forms;

using MetroFramework.Forms;

namespace LeonReader.Client.Factory
{
    /// <summary>
    /// 静态阅读窗口工厂
    /// </summary>
    public static class ReaderFormFactory
    {
        /// <summary>
        /// 创建阅读器窗口
        /// </summary>
        /// <returns></returns>
        public static MetroForm CreateReaderForm(string articlePath)
        {
            MetroForm readerForm = new MetroForm()
            {
                Icon = UnityResource.LeonReader,
                Width = 800,
                Height = 600
            };
            WebBrowser webBrowser = new WebBrowser()
            {
                Parent = readerForm,
                Dock = DockStyle.Fill,
            };

            readerForm.FormClosing += (s, e) => { webBrowser.Dispose(); };
            readerForm.Shown += (s, e) => { webBrowser.Navigate(articlePath); };

            return readerForm;
        }

    }
}
