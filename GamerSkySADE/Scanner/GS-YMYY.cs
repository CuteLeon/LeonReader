using System;

namespace GamerSkySADE
{
    /// <summary>
    /// 游民娱乐-游民影院
    /// </summary>
    public class GS_YMYY : GamerSkyScanner
    {

        /// <summary>
        /// 文章处理源
        /// </summary>
        public override string SADESource { get; protected set; } = "GamerSky-游民影院";

        /// <summary>
        /// 目标地址
        /// </summary>
        public override Uri TargetCatalogURI { get; protected set; } = new Uri(@"https://www.gamersky.com/wenku/movie/");

    }
}
