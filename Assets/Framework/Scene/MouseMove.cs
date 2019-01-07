using UnityEngine;
using System.Collections;

namespace Framework.Scene
{
    public class MouseMove : MonoBehaviour
    {
        #region Fields

        public MoveState m_State;



        Vector3 screenSpace;
        //Vector3 initPos;
        Vector3 offset;
        bool IsOnclick = false;

        #endregion

        #region Properties

        #endregion

        #region Unity Messages
        //    void Awake()
        //    {
        //
        //    }
        void OnEnable()
        {
            if (m_State == MoveState.NotClick)
            {
                //将物体由世界坐标系转换为屏幕坐标系
                screenSpace = Camera.main.WorldToScreenPoint(transform.position);//三维物体坐标转屏幕坐标
                //initPos = transform.position;
                //完成两个步骤 
                //1.由于鼠标的坐标系是2维，需要转换成3维的世界坐标系 
                //2.只有3维坐标情况下才能来计算鼠标位置与物理的距离，offset即是距离
                //将鼠标屏幕坐标转为三维坐标，再算出物体位置与鼠标之间的距离
                offset = Vector3.zero;
            }
            IsOnclick = false;
        }
        //
        //    void Start() 
        //    {
        //    
        //    }
        //    
        void Update()
        {
            if (m_State == MoveState.NotClick)
            {
                ModelMove();
            }
            else
            {
                if (IsOnclick)
                {
                    if (Input.GetMouseButton(0))
                    {
                        ModelMove();
                    }
                    else
                    {
                        IsOnclick = false;
                    }
                }
            }
        }
        //
        //    void OnDisable()
        //    {
        //
        //    }
        //
        //    void OnDestroy()
        //    {
        //
        //    }

        private void OnMouseDown()
        {
            if (m_State == MoveState.OnClick)
            {
                //将物体由世界坐标系转换为屏幕坐标系
                screenSpace = Camera.main.WorldToScreenPoint(transform.position);//三维物体坐标转屏幕坐标
                //initPos = transform.position;
                //完成两个步骤 
                //1.由于鼠标的坐标系是2维，需要转换成3维的世界坐标系 
                //2.只有3维坐标情况下才能来计算鼠标位置与物理的距离，offset即是距离
                //将鼠标屏幕坐标转为三维坐标，再算出物体位置与鼠标之间的距离
                offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
                //更新点击
                IsOnclick = true;
            }
        }


        #endregion

        #region Private Methods


        private void ModelMove()
        {
            //得到现在鼠标的2维坐标系位置
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
            //将当前鼠标的2维位置转换成3维位置，再加上鼠标的移动量
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
            //更新位置
            transform.position = curPosition;
        }

        #endregion

        #region Protected & Public Methods

        #endregion


        public enum MoveState
        {
            OnClick,
            NotClick
        }
    }
}