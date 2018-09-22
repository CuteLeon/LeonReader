using LeonReader.Client.DirectUI.Container;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonReader.Client.Factory
{
    /// <summary>
    /// 卡片容器工厂
    /// </summary>
    public class CardContainerFactory
    {

        #region 创建正常卡片

        /// <summary>
        /// 创建正常卡片
        /// </summary>
        /// <returns></returns>
        private CardContainer CreateNormalCard()
            => new CardContainer() { Style = CardContainer.CardStyles.Normal };

        /// <summary>
        /// 创建正常卡片
        /// </summary>
        /// <param name="title">文章标题</param>
        /// <returns></returns>
        public CardContainer CreateNormalCard(string title)
        {
            CardContainer cardContainer = CreateNormalCard();
            cardContainer.Title = title;

            return cardContainer;
        }

        /// <summary>
        /// 创建正常卡片
        /// </summary>
        /// <param name="title">文章标题</param>
        /// <param name="descript">文章描述</param>
        /// <returns></returns>
        public CardContainer CreateNormalCard(string title, string descript)
        {
            CardContainer cardContainer = CreateNormalCard(title);
            cardContainer.Description = descript;

            return cardContainer;
        }

        /// <summary>
        /// 创建正常卡片
        /// </summary>
        /// <param name="title">文章标题</param>
        /// <param name="descript">文章描述</param>
        /// <param name="publishTime">发布时间</param>
        /// <returns></returns>
        public CardContainer CreateNormalCard(string title, string descript, string publishTime)
        {
            CardContainer cardContainer = CreateNormalCard(title, descript);
            cardContainer.PublishTime = publishTime;

            return cardContainer;
        }

        /// <summary>
        /// 创建正常卡片
        /// </summary>
        /// <param name="title">文章标题</param>
        /// <param name="descript">文章描述</param>
        /// <param name="publishTime">发布时间</param>
        /// <param name="previewImage">预览图像</param>
        /// <returns></returns>
        public CardContainer CreateNormalCard(string title, string descript, string publishTime, Image previewImage)
        {
            CardContainer cardContainer = CreateNormalCard(title, descript, publishTime);
            cardContainer.PreviewImage = previewImage;

            return cardContainer;
        }

        #endregion

        #region 创建精简卡片

        /// <summary>
        /// 创建精简卡片
        /// </summary>
        /// <returns></returns>
        private CardContainer CreateSmallCard()
            => new CardContainer() { Style = CardContainer.CardStyles.Small };

        /// <summary>
        /// 创建精简卡片
        /// </summary>
        /// <param name="title">文章标题</param>
        /// <returns></returns>
        public CardContainer CreateSmallCard(string title)
        {
            CardContainer cardContainer = CreateSmallCard();
            cardContainer.Title = title;

            return cardContainer;
        }

        /// <summary>
        /// 创建精简卡片
        /// </summary>
        /// <param name="title">文章标题</param>
        /// <param name="descript">文章描述</param>
        /// <returns></returns>
        public CardContainer CreateSmallCard(string title, string descript)
        {
            CardContainer cardContainer = CreateSmallCard(title);
            cardContainer.Description = descript;

            return cardContainer;
        }

        /// <summary>
        /// 创建精简卡片
        /// </summary>
        /// <param name="title">文章标题</param>
        /// <param name="descript">文章描述</param>
        /// <param name="publishTime">发布时间</param>
        /// <returns></returns>
        public CardContainer CreateSmallCard(string title, string descript, string publishTime)
        {
            CardContainer cardContainer = CreateSmallCard(title, descript);
            cardContainer.PublishTime = publishTime;

            return cardContainer;
        }

        /// <summary>
        /// 创建精简卡片
        /// </summary>
        /// <param name="title">文章标题</param>
        /// <param name="descript">文章描述</param>
        /// <param name="publishTime">发布时间</param>
        /// <param name="previewImage">预览图像</param>
        /// <returns></returns>
        public CardContainer CreateSmallCard(string title, string descript, string publishTime, Image previewImage)
        {
            CardContainer cardContainer = CreateSmallCard(title, descript, publishTime);
            cardContainer.PreviewImage = previewImage;

            return cardContainer;
        }

        #endregion

        #region 创建巨幅卡片

        /// <summary>
        /// 创建巨幅卡片
        /// </summary>
        /// <returns></returns>
        private CardContainer CreateLargeCard()
            => new CardContainer() { Style = CardContainer.CardStyles.Large };

        /// <summary>
        /// 创建巨幅卡片
        /// </summary>
        /// <param name="title">文章标题</param>
        /// <returns></returns>
        public CardContainer CreateLargeCard(string title)
        {
            CardContainer cardContainer = CreateLargeCard();
            cardContainer.Title = title;

            return cardContainer;
        }

        /// <summary>
        /// 创建巨幅卡片
        /// </summary>
        /// <param name="title">文章标题</param>
        /// <param name="descript">文章描述</param>
        /// <returns></returns>
        public CardContainer CreateLargeCard(string title, string descript)
        {
            CardContainer cardContainer = CreateLargeCard(title);
            cardContainer.Description = descript;

            return cardContainer;
        }

        /// <summary>
        /// 创建巨幅卡片
        /// </summary>
        /// <param name="title">文章标题</param>
        /// <param name="descript">文章描述</param>
        /// <param name="publishTime">发布时间</param>
        /// <returns></returns>
        public CardContainer CreateLargeCard(string title, string descript, string publishTime)
        {
            CardContainer cardContainer = CreateLargeCard(title, descript);
            cardContainer.PublishTime = publishTime;

            return cardContainer;
        }

        /// <summary>
        /// 创建巨幅卡片
        /// </summary>
        /// <param name="title">文章标题</param>
        /// <param name="descript">文章描述</param>
        /// <param name="publishTime">发布时间</param>
        /// <param name="previewImage">预览图像</param>
        /// <returns></returns>
        public CardContainer CreateLargeCard(string title, string descript, string publishTime, Image previewImage)
        {
            CardContainer cardContainer = CreateLargeCard(title, descript, publishTime);
            cardContainer.PreviewImage = previewImage;

            return cardContainer;
        }

        #endregion

    }
}
