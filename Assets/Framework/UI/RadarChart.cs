using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Framework.UI
{
    public class RadarChart : Graphic
    {
        public RectTransform[] maxPoints;
        //分别为攻击，防御，闪避，穿透，治疗数值的上限
        private float[] maxValue = new float[5] { 100, 100, 100, 100, 100 };
        //分别为攻击，防御，闪避，穿透，治疗当前数值
        private float[] values = new float[5];
        private Vector3[] vertexes = new Vector3[5];
        private bool isDirty = true;


        protected override void Start()
        {
            UpdateDate(70, 60, 80, 50, 80);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attackPercent">攻击</param>
        /// <param name="defensePercent">防御</param>
        /// <param name="parryPercent">闪避</param>
        /// <param name="penetratePercent">穿透</param>
        /// <param name="curePerfent">治疗</param>
        public void UpdateDate(float attack, float defense, float parry, float penetrate, float cure)
        {
            values[0] = attack;
            values[1] = defense;
            values[2] = parry;
            values[3] = penetrate;
            values[4] = cure;
            refresh();
        }

        public void refresh()
        {
            for (int i = 0; i < maxPoints.Length; i++)
            {
                float percent = values[i] / maxValue[i];
                vertexes[i] = maxPoints[i].anchoredPosition * percent;
                Debug.Log("i " + i + " " + maxPoints[i].anchoredPosition);
            }
            SetAllDirty();
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
            vh.AddVert(vertexes[0], Color.red, Vector2.zero);
            vh.AddVert(vertexes[1], Color.black, Vector2.zero);
            vh.AddVert(vertexes[2], Color.yellow, Vector2.zero);
            vh.AddVert(vertexes[3], Color.blue, Vector2.zero);
            vh.AddVert(vertexes[4], Color.green, Vector2.zero);

            vh.AddTriangle(0, 1, 2);
            vh.AddTriangle(0, 2, 3);
            vh.AddTriangle(0, 3, 4);
        }
//--------------------- 
//作者：攻城狮一叶秋
//来源：CSDN
//原文：https://blog.csdn.net/jk823394954/article/details/53876546 
//版权声明：本文为博主原创文章，转载请附上博文链接！
    }
}