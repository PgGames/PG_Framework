using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Framework.UI.Radar
{
    public class RadarProperty : BaseMeshEffect
    {
        public List<uint> m_MaxValue = new List<uint>();
        public List<float> m_CurrValue = new List<float>();
        public uint m_Sum = 6;
        public float m_Size = 10;
        public Color m_Color = Color.white;


        public override void ModifyMesh(VertexHelper vh)
        {
            if (!IsActive())
            {
                return;
            }
            if (m_Sum < 3)
            {
                m_Sum = 3;
                //return;
            }
            vh.Clear();
            var vertexList = new List<UIVertex>();
            ApplyGradient(ref vertexList);
            vh.AddUIVertexTriangleStream(vertexList);
        }
        private void ApplyGradient(ref List<UIVertex> vertices)
        {
            List<Vector3> tempvector =  GetDrawUIVertex(m_Sum, m_Size);

            //bg
            for (int i = 0; i < m_Sum; i++)
            {
                for (int j = 0; j < 3; j++)
                {

                    UIVertex tempver = new UIVertex();
                    tempver.color = m_Color;
                    if (j == 0)
                    {
                        tempver.position = tempvector[i];
                    }
                    else if (j == 1)
                    {
                        tempver.position = Vector3.zero;
                    }
                    else if (j == 2)
                    {
                        int index = (int)((i + 1) % m_Sum);
                        tempver.position = tempvector[index];
                    }
                    vertices.Add(tempver);
                }
            }
        }
        private List<Vector3> GetDrawUIVertex(uint count, float r)
        {
            List<Vector3> tempvector = new List<Vector3>();

            for (int i = 0; i < count; i++)
            {
                float temp_x = Mathf.Sin((360f * i / count) * Mathf.Deg2Rad) * r;//横坐标
                float temp_y = Mathf.Cos((360f * i / count) * Mathf.Deg2Rad) * r;//纵坐标
                tempvector.Add(new Vector3(temp_x, temp_y, 0));
            }
            return tempvector;
        }
    }
}