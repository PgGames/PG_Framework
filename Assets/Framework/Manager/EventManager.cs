﻿
using System;
using UnityEngine;

namespace Framework.Event
{
    /// <summary>
    /// 临时的事件管理器（切换场景时事件会被清空）
    /// </summary>
    public class EventManager : AbstractEvent<CurrentEventHead, EventManager.CurrentEvent>
    {
        private static EventManager m_instance;
        /// <summary>
        /// 
        /// </summary>
        public static EventManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    var go = new GameObject("CurrentSceneEventManager");
                    m_instance = go.AddComponent<EventManager>();
                }
                return m_instance;
            }
        }
        private void OnDestroy()
        {
            m_instance = null;
        }
        /// <summary>
        /// 添加事件监听
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback"></param>
        /// <returns></returns>
        public CurrentEvent RigistEvent<T>(Action<T> callback) where T : CurrentEventHead
        {
            CurrentEvent current = new CurrentEvent();
            current.m_Type = typeof(T);
            current.CallBack = callback;

            base.RigistEvent(current);      //注册
            return current;
        }
        /// <summary>
        /// 解除事件监听
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback"></param>
        public void UnrigistEvent<T>(Action<T> callback) where T : CurrentEventHead
        {
            CurrentEvent current = new CurrentEvent();
            current.m_Type = typeof(T);
            current.CallBack = callback;

            base.UnregistEvent(current);    //解除注册
        }

        /// <summary>
        /// 事件广播回调
        /// </summary>
        /// <param name="varhead"></param>
        /// <param name="varevent"></param>
        protected override void CallEventDelegate(CurrentEventHead varhead, CurrentEvent varevent)
        {
            varevent.CallBack.DynamicInvoke(varhead);
        }
        /// <summary>
        /// 事件类
        /// </summary>
        public class CurrentEvent : EventClass
        { }
    }
    /// <summary>
    /// 事件定义必须继承该类(用作传输数据内容)
    /// </summary>
    public class CurrentEventHead
    { }
}
