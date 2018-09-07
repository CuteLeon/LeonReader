using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LeonReader.Common
{
    /// <summary>
    /// IO助手
    /// </summary>
    public static class IOHelper
    {

        /// <summary>
        /// 安全的合并路径
        /// </summary>
        /// <param name="DirectoryPath"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string PathCombine(string DirectoryPath, string FileName) => Path.Combine(DirectoryPath, FileName);

        /// <summary>
        /// 返回指定路径字符串的文件名和扩展名
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <returns></returns>
        public static string GetFileName(string FilePath) => Path.GetFileName(FilePath);

        /// <summary>
        /// 返回指定路径字符串的文件名（不包括扩展名）
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <returns></returns>
        public static string GetFileNameWithoutExtension(string FilePath) => Path.GetFileNameWithoutExtension(FilePath);

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool FileExists(string path) => File.Exists(path);
        
        /// <summary>
        /// 检查目录是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool DirectoryExists(string path) => Directory.Exists(path);

        /// <summary>
        /// 通过流读取图像文件（避免文件占用）
        /// </summary>
        /// <param name="imagePath">图像路径</param>
        /// <returns></returns>
        public static Image ReadeImageByStream(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath)) throw new Exception("无法通过空路径流读取图像文件的流。");

            try
            {
                using (FileStream ImageStream = new FileStream(imagePath, FileMode.Open))
                {
                    return Image.FromStream(ImageStream);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error($"通过流读取图像文件失败：{imagePath}，{ex.Message}");
                return null;
            }
        }

    }
}
