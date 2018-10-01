using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeonReader.Model
{

    /// <summary>
    /// 文章内容类
    /// </summary>
    [Table("Contents")]
    public class ContentItem
    {
        #region 内容状态

        /// <summary>
        /// 内容状态
        /// </summary>
        public enum ContentStates
        {
            /// <summary>
            /// 新内容
            /// </summary>
            New = 0,
            /// <summary>
            /// 已下载
            /// </summary>
            Downloaded = 1,
        }

        #endregion

        #region 数据库字段

        /// <summary>
        /// 文章内容ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        /// <summary>
        /// 页面链接地址
        /// </summary>
        [DisplayName("页面链接地址"), DataType(DataType.Url)]
        public string PageLink { get; set; }

        /// <summary>
        /// 内容状态
        /// </summary>
        [Required]
        [DisplayName("内容状态")]
        public ContentStates State { get; set; } = ContentStates.New;

        #endregion

        public ContentItem() { }

        public ContentItem(string description) : this()
        {
            this.ImageDescription = description;
        }

        public ContentItem(string description, string link) : this(description)
        {
            this.ImageLink = link;
        }

        public ContentItem(string description, string link, string filename) : this(description, link)
        {
            this.ImageFileName = filename;
        }

    }
}
