using System;
using System.Diagnostics;

using LeonReader.Client.DirectUI.Container;
using LeonReader.Common;
using LeonReader.Model;

namespace LeonReader.Client
{
    public partial class MainForm
    {

        #region 卡片控件事件

        /// <summary>
        /// 点击卡片标题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardContainer_TitleClick(object sender, EventArgs e)
        {
            //TODO: 判断文章状态，并阅读文章
        }

        /// <summary>
        /// 点击卡片已读按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardContainer_ReadedClick(object sender, EventArgs e)
        {
            CardContainer cardContainer = sender as CardContainer;
            this.articleManager.SetArticleReaded(cardContainer.Article);
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

            Article article = cardContainer.Article;
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
            //TODO: 点击卡片删除按钮
        }

        /// <summary>
        /// 点击卡片浏览按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardContainer_BrowserClick(object sender, EventArgs e)
        {
            Article article = (sender as CardContainer)?.Article;
            if (article == null) return;

            Process.Start(article.ArticleLink);
        }

        #endregion
    }
}
