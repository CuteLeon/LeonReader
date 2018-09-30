using System;
using System.Data.Entity;
using System.Linq;

using LeonReader.Model;

using SQLite.CodeFirst;

namespace LeonReader.DataAccess
{
    /// <summary>
    /// 数据种子
    /// </summary>
    public class SampleDataSeed : SqliteDropCreateDatabaseWhenModelChanges<UnityDBContext>
    {
        /// <summary>
        /// 构造数据种子
        /// </summary>
        /// <param name="modelBuilder"></param>
        public SampleDataSeed(DbModelBuilder modelBuilder)
            : base(modelBuilder) { }

        //覆写此方法，用于初始化数据种子
        protected override void Seed(UnityDBContext context)
        {
            context.Articles.Add(
                new Article()
                {
                    ArticleID = "10000",
                    Title = "种子文章",
                    ArticleLink = "http://www.cuteleon.com",
                    Description = "种子文章",
                    PublishTime = DateTime.Now.ToString(),
                    SADESource = "DataSeed",
                    ScanTime = DateTime.Now,
                    Contents = new ContentItem[] {
                        new ContentItem("种子文章"),
                        new ContentItem("欢迎使用 Leon Reader."),
                        new ContentItem("Best Wishes !")
                    }.ToList(),
                    ArticleFileName = "种子文章文件",
                    DownloadDirectoryName = "种子文章下载目录",
                }
            );

            context.SaveChanges();
            base.Seed(context);
        }

    }
}
