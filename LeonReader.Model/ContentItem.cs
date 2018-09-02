using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using LeonReader.Common;

namespace LeonReader.Model
{

    /// <summary>
    /// 文章内容类
    /// </summary>
    [Table("Contents")]
    public class ContentItem
    {
        #region 数据库字段

        /// <summary>
        /// 文章内容ID
        /// </summary>
        [Key]
        [Required]
        [DisplayName("文章内容ID")]
        public int ID { get; set; }

        /// <summary>
        /// 图像链接
        /// </summary>
        [DisplayName("图像链接"), DataType(DataType.ImageUrl)]
        public string ImageLink { get; set; }

        /// <summary>
        /// 图像描述
        /// </summary>
        [DisplayName("图像描述"), DataType(DataType.MultilineText)]
        public string ImageDescription { get; set; }

        /// <summary>
        /// 图像文件名称
        /// </summary>
        [DisplayName("图像文件名称"), DataType(DataType.Text)]
        public string ImageFileName { get; set; }

        #endregion

        public ContentItem() { }

        public ContentItem(string description) : this()
        {
            ImageDescription = description;
        }

        public ContentItem(string description, string link) : this(description)
        {
            ImageLink = link;
            //ImageFileName = IOHelper.GetFileName(link);
        }

        public ContentItem(string description, string link, string filename) : this(description)
        {
            ImageLink = link;
            ImageFileName = filename;
        }

    }
}
