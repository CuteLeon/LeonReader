using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LeonReader.Model
{
    /// <summary>
    /// 全局数据库交互类
    /// </summary>
    public class UnityDBContext : DbContext
    {
        /// <summary>
        /// 文章表
        /// </summary>
        public DbSet<Article> Articles { get; set; }

        /// <summary>
        /// 构造全局数据库交互类
        /// </summary>
        public UnityDBContext() : base("LeonReaderDataBase") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //必须的代码
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(typeof(UnityDBContext).Assembly);

            //初始化数据种子，用于CodeFirst模式自动创建或修改数据库
            Database.SetInitializer(new SampleDataSeed(modelBuilder));
        }

    }
}
