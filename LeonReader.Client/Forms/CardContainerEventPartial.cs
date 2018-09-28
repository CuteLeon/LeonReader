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
            //TODO: 点击卡片主按钮
        }

        /// <summary>
        /// 点击卡片定位按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardContainer_LocationClick(object sender, EventArgs e)
        {
            CardContainer cardContainer = sender as CardContainer ?? throw new ArgumentNullException(nameof(sender));
            if (cardContainer.ArticleState < CardContainer.ArticleStates.Downloading) return;

            Article article = cardContainer.Article;
            if (article == null) return;

            if (cardContainer.ArticleState < CardContainer.ArticleStates.Exporting)
            {
                //TODO: 使用 explorer.exe 浏览此文章下载目录（由IOUtils提供方法）
            }
            else
            {
                string ArticleFileName = IOUtils.PathCombine(
                    ConfigHelper.GetConfigHelper.DownloadDirectory,
                    article.DownloadDirectoryName,
                    article.ArticleFileName,
                    ConfigHelper.GetConfigHelper.Extension
                    );

                if (IOUtils.FileExists(ArticleFileName))
                {
                    //TODO: 使用 explorer.exe 浏览此文章导出的文件
                }
                else
                {
                    //TODO: 使用 explorer.exe 浏览此文章下载目录（由IOUtils提供方法）
                }
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
