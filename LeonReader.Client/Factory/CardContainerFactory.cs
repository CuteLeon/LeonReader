using System;
using System.Drawing;

using LeonReader.Client.DirectUI.Container;
using LeonReader.Common;
using LeonReader.Model;

namespace LeonReader.Client.Factory
{
    /// <summary>
    /// 卡片容器工厂
    /// </summary>
    public class CardContainerFactory
    {
        /// <summary>
        /// 创建卡片控件
        /// </summary>
        /// <returns></returns>
        private CardContainer CreateCardContainer(Article article)
        {
            CardContainer cardContainer = new CardContainer();
            try
            {
                cardContainer.Title = article.Title;
                cardContainer.Description = article.Description;
                cardContainer.PublishTime = article.PublishTime;
                cardContainer.PreviewImage = IOUtils.ReadeImageWithoutDispose(
                    IOUtils.PathCombine(
                        ConfigHelper.GetConfigHelper.DownloadDirectory,
                        article.ImageFileName
                        )
                    );
            }
            catch (Exception ex)
            {
                LogUtils.Error($"根据文章实体创建卡片时遇到错误：{ex.Message}");
            }

            cardContainer.Article = article;

            return cardContainer;
        }

        #region 创建正常卡片

        /// <summary>
        /// 创建正常卡片
        /// </summary>
        /// <returns></returns>
        public CardContainer CreateNormalCard(Article article)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));

            CardContainer cardContainer = this.CreateCardContainer(article);
            cardContainer.Size = new Size(675, 118);
            cardContainer.Style = CardContainer.CardStyles.Normal;

            return cardContainer;
        }

        #endregion

        #region 创建精简卡片

        /// <summary>
        /// 创建精简卡片
        /// </summary>
        /// <returns></returns>
        public CardContainer CreateSmallCard(Article article)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));

            CardContainer cardContainer = this.CreateCardContainer(article);
            cardContainer.Size = new Size(675, 32);
            cardContainer.Style = CardContainer.CardStyles.Small;

            return cardContainer;
        }

        #endregion

        #region 创建巨幅卡片

        /// <summary>
        /// 创建巨幅卡片
        /// </summary>
        /// <returns></returns>
        public CardContainer CreateLargeCard(Article article)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));

            CardContainer cardContainer = this.CreateCardContainer(article);
            cardContainer.Size = new Size(675, 180);
            cardContainer.Style = CardContainer.CardStyles.Large;

            return cardContainer;
        }

        #endregion

    }
}
