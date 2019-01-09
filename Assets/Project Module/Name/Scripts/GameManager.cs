
#region 版权信息
/*
 * -----------------------------------------------------------
 *  Copyright (c) KeJun All rights reserved.
 * -----------------------------------------------------------
 *		描述: 
 *      创建者：
 *      创建时间: 2019-01-03 10:50:32
 *  
 */
#endregion


using UnityEngine;
using System.Collections;
using Framework.Manager;

namespace Name
{
    public class GameManager : MonoBehaviour
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Unity Messages
        void Awake()
        {
            WindowsManager.GetManager.AddResourcesWindows("0", "Name/UI/Prefabs/Button");

            WindowsManager.GetManager.AddResourcesWindows("1", "Name/UI/Prefabs/Toggle");

            WindowsManager.GetManager.Init();

            WindowsManager.GetManager.OpenWindows("0");
        }
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

        #endregion
    }
}