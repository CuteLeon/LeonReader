using System;
using System.Collections.Generic;
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
        [Key, Column(Order = 1)]
        [Required]
        [DisplayName("文章ID"), DataType(DataType.Text)]
        public string ArticleID { get; set; }

        /// <summary>
        /// 文章处理源
        /// </summary>
        [Key, Column(Order = 2)]
        [Required]
        [DisplayName("文章处理源"), DataType(DataType.Text)]
        public string ASDESource { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [DisplayName("标题"), DataType(DataType.Text)]
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        [DisplayName("发布时间"), DataType(DataType.Text)]
        public string PublishTime { get; set; }

        /// <summary>
        /// 图像文件名称
        /// </summary>
        [DisplayName("图像文件名称"), DataType(DataType.Text)]
        public string ImageFileName { get; set; }

        /// <summary>
        /// 图像链接
        /// </summary>
        [DisplayName("图像链接"), DataType(DataType.ImageUrl)]
        public string ImageLink { get; set; }

        /// <summary>
        /// 是否为新文章
        /// </summary>
        [DisplayName("是否为新文章")]
        public bool IsNew { get; set; }

        /// <summary>
        /// 文章链接
        /// </summary>
        [DisplayName("文章链接"), DataType(DataType.Url)]
        public string ArticleLink { get; set; }

        /// <summary>
        /// 扫描时间
        /// </summary>
        [DisplayName("扫描时间"), DataType(DataType.DateTime)]
        public DateTime? ScanTime { get; set; }

        /// <summary>
        /// 分析时间
        /// </summary>
        [DisplayName("分析时间"), DataType(DataType.DateTime)]
        public DateTime? AnalyzeTime { get; set; }

        /// <summary>
        /// 下载时间
        /// </summary>
        [DisplayName("下载时间"), DataType(DataType.DateTime)]
        public DateTime? DownloadTime { get; set; }

        /// <summary>
        /// 导出时间
        /// </summary>
        [DisplayName("导出时间"), DataType(DataType.DateTime)]
        public DateTime? ExportTime { get; set; }

        /// <summary>
        /// 文章文件名称（相对）
        /// </summary>
        //[NotMapped]
        [Required]
        [DisplayName("文章文件名称（相对）")]
        public string ArticleFileName { get; set; }

        /// <summary>
        /// 下载目录名称（相对）
        /// </summary>
        //[NotMapped]
        [Required]
        [DisplayName("下载目录名称（相对）")]
        public string DownloadDirectoryName { get; set; }

        #endregion

        /// <summary>
        /// 文章内容集合
        /// </summary>
        [DisplayName("文章内容集合")]
        public virtual List<ContentItem> Contents { get; set; }

        public Article() { }

    }
}
