using System;
using System.Diagnostics;

using LeonReader.Client.DirectUI.Container;
using LeonReader.Client.Factory;
using LeonReader.Common;
using LeonReader.Model;

using MetroFramework.Forms;

namespace LeonReader.Client
{
    public partial class MainForm
    {
        //TODO: 卡片的事件不要由 Form 处理了，将卡片关联的Manager、文章、处理器解耦，由关联对象的管理器处理卡片的事件

        #region 卡片控件事件

        /// <summary>
        /// 点击卡片标题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardContainer_TitleClick(object sender, EventArgs e)
        {
            CardContainer cardContainer = sender as CardContainer ?? throw new ArgumentNullException(nameof(sender));
            Article article = cardContainer.TargetArticle;
            if (article == null) return;

            string ArticleFilePath = IOUtils.PathCombine(
                ConfigHelper.GetConfigHelper.DownloadDirectory,
                article.DownloadDirectoryName,
                string.Format("{0}.{1}", article.ArticleFileName, ConfigHelper.GetConfigHelper.Extension)
                );

            MetroForm readerForm = ReaderFormFactory.CreateReaderForm(ArticleFilePath);
            readerForm.FormClosed += (s, v) => { cardContainer.ArticleState = CardContainer.ArticleStates.Exported; };
            readerForm.Show(this);
        }

        /// <summary>
        /// 点击卡片已读按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardContainer_ReadedClick(object sender, EventArgs e)
        {
            CardContainer cardContainer = sender as CardContainer;
            this.TargetArticleManager.SetArticleState(cardContainer.TargetArticle, Article.ArticleStates.Readed);
            cardContainer.Style = CardContainer.CardStyles.Small;
        }

        /// <summary>
        /// 点击卡片主按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardContainer_MainButtonClick(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 点击卡片定位按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardContainer_LocationClick(object sender, EventArgs e)
        {
            CardContainer cardContainer = sender as CardContainer ?? throw new ArgumentNullException(nameof(sender));

            Article article = cardContainer.TargetArticle;
            if (article == null) return;

            string ArticleFilePath = IOUtils.PathCombine(
                ConfigHelper.GetConfigHelper.DownloadDirectory,
                article.DownloadDirectoryName,
                string.Format("{0}.{1}", article.ArticleFileName, ConfigHelper.GetConfigHelper.Extension)
                );

            if (IOUtils.FileExists(ArticleFilePath))
            {
                IOUtils.SelectFile(ArticleFilePath);
            }
            else
            {
                string ArticleDirectory = IOUtils.PathCombine(
                    ConfigHelper.GetConfigHelper.DownloadDirectory,
                    article.DownloadDirectoryName
                    );
                IOUtils.SelectDirectory(ArticleDirectory);
            }
        }

        /// <summary>
        /// 点击卡片删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardContainer_DeleteClick(object sender, EventArgs e)
        {
            //TODO: 点击卡片删除按钮，文章状态置为 Deleting 和 Deleted
        }

        /// <summary>
        /// 点击卡片浏览按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardContainer_BrowserClick(object sender, EventArgs e)
        {
            Article article = (sender as CardContainer)?.TargetArticle;
            if (article == null) return;

            Process.Start(article.ArticleLink);
        }

        #endregion
    }
}
