
#region 版权信息
/*
 * -----------------------------------------------------------
 *  Copyright (c) KeJun All rights reserved.
 * -----------------------------------------------------------
 *		描述: 
 *      创建者：
 *      创建时间: 2019-01-03 10:55:21
 *  
 */
#endregion


using UnityEngine;
using System.Collections;
using Framework.Manager;

namespace Name.UI
{
    public class WindowsInfo : MonoBehaviour
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Unity Messages
        //    void Awake()
        //    {
        //
        //    }
        //    void OnEnable()
        //    {
        //
        //    }
        //
        //    void Start() 
        //    {
        //    
        //    }
        //    
        //    void Update() 
        //    {
        //    
        //    }
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

        #endregion

        #region Private Methods

        #endregion

        #region Protected & Public Methods


        public void OpenWindows(string WindowsName)
        {
            WindowsManager.GetManager.OpenWindows(WindowsName);
        }
        public void CloseWindows(string WindowsName)
        {
            WindowsManager.GetManager.CloseWindows(WindowsName);
        }
        public void CloseWindows()
        {
            WindowsManager.GetManager.CloseWindows(this.transform);
        }
        public void DestroyWindows(string WindowsName)
        {
            WindowsManager.GetManager.DestroyWindows(WindowsName);
        }

        public void DestroyWindows()
        {
            WindowsManager.GetManager.DestroyWindows(this.transform);
        }

        #endregion
    }
}