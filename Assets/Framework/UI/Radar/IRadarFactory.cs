using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Framework.UI.Radar
{
    // 装配操作,将上述分布建造的内容合并
    public interface IRadarFactory : IRadarBase, IRadarline
    {
        //VertexHelper DrawRadar(VertexHelper vh, Rect rect, RadarBaseData radardata);
    }
}