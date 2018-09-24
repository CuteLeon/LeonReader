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
            cardContainer.Title = article.Title;
            cardContainer.Description = article.Description;
            cardContainer.PublishTime = article.PublishTime;
            cardContainer.PreviewImage = IOUtils.ReadeImageByStream(
                IOUtils.PathCombine(
                    ConfigHelper.GetConfigHelper.DownloadDirectory,
                    article.ImageFileName
                    )
                );
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
                throw new System.ArgumentNullException(nameof(article));

            CardContainer cardContainer = CreateCardContainer(article);
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
                throw new System.ArgumentNullException(nameof(article));

            CardContainer cardContainer = CreateCardContainer(article);
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
                throw new System.ArgumentNullException(nameof(article));

            CardContainer cardContainer = CreateCardContainer(article);
            cardContainer.Size = new Size(675, 180);
            cardContainer.Style = CardContainer.CardStyles.Large;

            return cardContainer;
        }

        #endregion

    }
}
