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
        public CardContainer CreateCardContainer(Article article)
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

            return cardContainer;
        }
        
    }
}
