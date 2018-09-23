using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LeonReader.Common
{

    /// <summary>
    /// 程序集反射助手
    /// </summary>
    public static class AssemblyUtils
    {
        /// <summary>
        /// 加载程序集
        /// </summary>
        /// <param name="AssemblyPath">程序集路径</param>
        /// <param name="DynamicLoad">在内存中动态加载</param>
        /// <returns>程序集</returns>
        public static Assembly CreateAssembly(string AssemblyPath, bool DynamicLoad = true)
        {
            LogUtils.Debug("开始{0}加载程序集路径：{1} ...", (DynamicLoad ? "动态" : string.Empty), AssemblyPath);

            Assembly PluginAssembly = null;
            try
            {
                if (!DynamicLoad)
                {
                    // 从链接库文件路径加载
                    PluginAssembly = Assembly.LoadFrom(AssemblyPath);
                }
                else
                {
                    // 把链接库文件读入内存后从内存加载，允许程序在运行时更新链接库
                    using (FileStream AssemblyStream = new FileStream(AssemblyPath, FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader AssemblyReader = new BinaryReader(AssemblyStream))
                        {
                            byte[] AssemblyBuffer = AssemblyReader.ReadBytes((int)AssemblyStream.Length);
                            PluginAssembly = Assembly.Load(AssemblyBuffer);
                            AssemblyReader.Close();
                        }
                        AssemblyStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtils.Error("创建程序集遇到异常：{0}", ex.Message);
                throw;
            }

            return PluginAssembly;
        }

        /// <summary>
        /// 在程序集获取指定基类的子类型（扩展方法）
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="baseType">基类</param>
        /// <returns></returns>
        public static Type[] GetSubTypes(this Assembly assembly, Type baseType)
        {
            if (assembly == null)
            {
                LogUtils.Warn($"获取 {baseType.FullName} 的子类型时遇到错误：空的程序集。");
                return new Type[] { };
            }

            LogUtils.Debug($"在程序集 {assembly.FullName} 中获取 {baseType.Name} 的子类型...");
            return assembly.GetTypes().Where(
                            type =>
                            type.IsSubclassOf(baseType)
                        ).ToArray();
        }

        /// <summary>
        /// 创建类型实例（扩展方法）
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static object CreateInstance(this Assembly assembly, Type type)
        {
            LogUtils.Debug($"在程序集 {assembly.FullName} 中创建 {type.Name} 的实例...");
            try
            {
                return assembly.CreateInstance(type.FullName);
            }
            catch (Exception ex)
            {
                LogUtils.Error($"在程序集 {assembly.FullName} 中创建 {type.Name} 的实例遇到异常：{ex.Message}");
                return null;
            }
        }

    }
}
