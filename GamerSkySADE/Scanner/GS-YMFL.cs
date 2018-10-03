using System;

namespace GamerSkySADE
{

    /// <summary>
    /// 游民娱乐-游民福利
    /// </summary>
    public class GS_YMFL : GamerSkyScanner
    {

        /// <summary>
        /// 文章处理源
        /// </summary>
        public override string SADESource { get; protected set; } = "GamerSky-游民福利";

        /// <summary>
        /// 目标地址
        /// </summary>
        public override Uri TargetCatalogURI { get; protected set; } = new Uri(@"https://www.gamersky.com/ent/xz/");

    }
}
