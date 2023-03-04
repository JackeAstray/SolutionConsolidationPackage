using GameLogic.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.Events
{
    /// <summary>
    /// 事件管理器
    /// </summary>
    public class EventModule : CustommModuleInitialize
    {
        #region 实例与初始化
        //实例
        public static EventModule Instance = new EventModule();
        //是否初始化完成
        public bool IsInited { get; private set; }
        //初始化进度
        private double _initProgress = 0;
        public double InitProgress { get { return _initProgress; } }
        #endregion

        /// <summary>
        /// 事件监听池
        /// </summary>
        private Dictionary<EventType, DelegateEvent> eventTypeListeners = new Dictionary<EventType, DelegateEvent>();

        public IEnumerator Init()
        {
            if (Tools.IsDebug())
            {
                Debug.Log("EventModule 初始化");
            }
            _initProgress = 0;
            Instance = this;

            yield return null;

            _initProgress = 100;
            IsInited = true;
        }

        public void ClearData()
        {
            if (Tools.IsDebug())
            {
                Debug.Log("EventModule 清除数据");
            }
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="listenerFunc">监听函数</param>
        public void AddEventListener(EventType type, DelegateEvent.EventHandler listenerFunc)
        {
            DelegateEvent delegateEvent;
            if (eventTypeListeners.ContainsKey(type))
            {
                delegateEvent = eventTypeListeners[type];
            }
            else
            {
                delegateEvent = new DelegateEvent();
                eventTypeListeners[type] = delegateEvent;
            }
            delegateEvent.AddListener(listenerFunc);
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="listenerFunc">监听函数</param>
        public void RemoveEventListener(EventType type, DelegateEvent.EventHandler listenerFunc)
        {
            if (listenerFunc == null)
            {
                return;
            }
            if (!eventTypeListeners.ContainsKey(type))
            {
                return;
            }
            DelegateEvent delegateEvent = eventTypeListeners[type];
            delegateEvent.RemoveListener(listenerFunc);
        }

        /// <summary>
        /// 触发某一类型的事件  并传递数据
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="data">事件的数据(可为null)</param>
        public void DispatchEvent(EventType type, object data)
        {
            if (!eventTypeListeners.ContainsKey(type))
            {
                return;
            }
            //创建事件数据
            EventData eventData = new EventData();
            eventData.type = type;
            eventData.data = data;

            DelegateEvent delegateEvent = eventTypeListeners[type];
            delegateEvent.Handle(eventData);
        }
    }
}

