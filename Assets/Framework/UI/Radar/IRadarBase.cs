using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Framework.UI.Radar
{
    public interface IRadarBase
    {
        /// 绘制底图线框
        VertexHelper DrawBase(VertexHelper vh);
        /// 绘制xy坐标
        VertexHelper DrawAxis(VertexHelper vh);
    }
}