using LeonReader.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LeonReader.Client.Factory
{
    /// <summary>
    /// 程序集工厂
    /// </summary>
    public class AssemblyFactory
    {
        /// <summary>
        /// 创建程序集
        /// </summary>
        /// <param name="filePath">可执行文件路径</param>
        /// <returns></returns>
        public Assembly CreateAssembly(string filePath)
        {
            Assembly assembly = AssemblyHelper.CreateAssembly(filePath);
            return assembly;
        }

        /// <summary>
        /// 扫描目录并创建其内所有程序集
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="pathPredicate">路径筛选条件</param>
        /// <returns></returns>
        public IEnumerable<Assembly> CreateAssemblys(string directoryPath, Predicate<string> pathPredicate)
        {
            foreach (var filePath in IOHelper.GetChildrenFiles(
                directoryPath,
                pathPredicate
            ))
            {
                yield return CreateAssembly(filePath);
            }
        }

    }
}
