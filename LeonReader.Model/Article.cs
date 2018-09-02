using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeonReader.Model
{
    /// <summary>
    /// 文章类
    /// </summary>
    [Table("Articles")]
    public class Article
    {
        #region 数据库字段
        
        /// <summary>
        /// 文章ID
        /// </summary>
        [Key]
        [Required]
        [DisplayName("文章ID"), DataType(DataType.Text)]
        public string ArticleID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [DisplayName("标题"), DataType(DataType.Text)]
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Required]
        [DisplayName("描述"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        [Required]
        [DisplayName("发布时间"), DataType(DataType.Text)]
        public string PublishTime { get; set; }

        /// <summary>
        /// 图像文件名称
        /// </summary>
        [Required]
        [DisplayName("图像文件名称"), DataType(DataType.Text)]
        public string ImageFileName { get; set; }

        /// <summary>
        /// 图像链接
        /// </summary>
        [Required]
        [DisplayName("图像链接"), DataType(DataType.ImageUrl)]
        public string ImageLink { get; set; }

        /// <summary>
        /// 是否为新文章
        /// </summary>
        [Required]
        [DisplayName("是否为新文章")]
        public bool IsNew { get; set; }

        /// <summary>
        /// 文章链接
        /// </summary>
        [Required]
        [DisplayName("文章链接"), DataType(DataType.Url)]
        public string ArticleLink { get; set; }

        #endregion
        
        /// <summary>
        /// 文章内容集合
        /// </summary>
        [Required]
        [DisplayName("文章内容集合")]
        public virtual List<ContentItem> Contents { get; set; }

        /// <summary>
        /// 文章文件名称
        /// </summary>
        public string ArticleFileName
        {
            get
            {
                if (string.IsNullOrEmpty(Title))
                    throw new Exception("空的文章ID，无法获得文章文件名称。");
                return Title;
            }
        }

        /// <summary>
        /// 下载目录名称
        /// </summary>
        public virtual string DownloadDirectoryName
        {
            get
            {
                if (string.IsNullOrEmpty(ArticleID))
                    throw new Exception("空的文章ID，无法获得文章下载目录名称");
                return ArticleID;
            }
        }

        public Article()
        {
            
        }

    }
}
