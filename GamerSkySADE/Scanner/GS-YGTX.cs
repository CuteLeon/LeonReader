using System;

namespace GamerSkySADE
{

    /// <summary>
    /// 游民娱乐-游观天下
    /// </summary>
    public class GS_YGTX : GamerSkyScanner
    {

        /// <summary>
        /// 文章处理源
        /// </summary>
        public override string SADESource { get; protected set; } = "GamerSky-游观天下";

        /// <summary>
        /// 目标地址
        /// </summary>
        public override Uri TargetCatalogURI { get; protected set; } = new Uri(@"https://www.gamersky.com/ent/discovery/");

    }
}
