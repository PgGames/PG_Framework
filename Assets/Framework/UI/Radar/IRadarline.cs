
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Framework.UI.Radar
{
    // 绘制数据线段
    public interface IRadarline
    {
        VertexHelper DrawLine(VertexHelper vh);
    }
}