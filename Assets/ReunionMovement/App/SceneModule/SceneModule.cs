using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameLogic
{
    /// <summary>
    /// 场景管理
    /// </summary>
    public class SceneModule : CustommModuleInitialize
    {
        #region 实例与初始化
        //实例
        public static SceneModule Instance = new SceneModule();
        //是否初始化完成
        public bool IsInited { get; private set; }
        //初始化进度
        private double _initProgress = 0;
        public double InitProgress { get { return _initProgress; } }
        #endregion

        #region 
        private Action m_onSceneLoaded = null;   // 场景加载完成回调
        private string m_strTargetSceneName = null;  // 将要加载的场景名
        private string m_strCurSceneName = null;   // 当前场景名，如若没有场景，则默认返回 Login
        private string m_strPreSceneName = null;   // 上一个场景名
        private bool m_bLoading = false;     // 是否正在加载中
        private const string m_strLoadSceneName = "LoadingScene";  // 加载场景名字
        private GameObject m_objLoadProgress = null;               // 加载进度显示对象

        private string m_showUI = "";  // 将要显示的UI

        private Sprite m_sprite;  //

        //获取当前场景名
        public string s_strLoadedSceneName => Instance.m_strCurSceneName;

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public IEnumerator Init()
        {
            if (Tools.IsDebug())
            {
                Debug.Log("SceneModule 初始化");
            }
            _initProgress = 0;
            Instance = this;

            Instance.m_strCurSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            yield return null;

            _initProgress = 100;
            IsInited = true;
        }
        /// <summary>
        /// 清除数据
        /// </summary>
        public void ClearData()
        {
            if (Tools.IsDebug())
            {
                Debug.Log("SceneModule 清除数据");
            }
        }

        public void SetSprite(Sprite sprite)
        {
            m_sprite = sprite;
        }

        /// <summary>
        /// 返回上一场景
        /// </summary>
        public void LoadPreScene()
        {
            if (string.IsNullOrEmpty(Instance.m_strPreSceneName))
            {
                return;
            }
            LoadScene(Instance.m_strPreSceneName);
        }

        /// <summary>
        /// 清除被观察者
        /// </summary>
        void Clear()
        {
            LanguagesModule.Instance.GetLanguageSubject().Clear();
        }

        /// <summary>
        /// 加载场景 (不带回调)
        /// </summary>
        /// <param name="strLevelName"></param>
        public void LoadScene(string strLevelName,UnityAction unityAction = null, string showUI = "null")
        {
            m_showUI = showUI;

            if (unityAction != null)
            {
                unityAction.Invoke();
            }

            Instance.LoadLevel(strLevelName, null);
        }


        /// <summary>
        /// 加载场景（带回调）
        /// </summary>
        /// <param name="strLevelName"></param>
        /// <param name="onSecenLoaded"></param>
        public void LoadScene(string strLevelName, Action onSecenLoaded)
        {
            Instance.LoadLevel(strLevelName, onSecenLoaded);
        }

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="strLevelName"></param>
        /// <param name="onSecenLoaded"></param>
        /// <param name="isDestroyAuto"></param>
        private void LoadLevel(string strLevelName, Action onSecenLoaded, bool isDestroyAuto = true)
        {
            if (m_bLoading || m_strCurSceneName == strLevelName)
            {
                return;
            }

            m_bLoading = true;  // 锁屏
                                // *开始加载    
            m_onSceneLoaded = onSecenLoaded;
            m_strTargetSceneName = strLevelName;
            m_strPreSceneName = m_strCurSceneName;
            m_strCurSceneName = m_strLoadSceneName;

            //先异步加载 Loading 界面
            SceneSpriteMgr.Instance.StartCoroutine(OnLoadingScene(OnLoadingSceneLoaded, LoadSceneMode.Single));
        }

        /// <summary>
        /// 加载-加载场景
        /// </summary>
        /// <param name="OnSecenLoaded"></param>
        /// <param name="OnSceneProgress"></param>
        /// <param name="loadSceneMode"></param>
        /// <returns></returns>
        private IEnumerator OnLoadingScene(Action OnSecenLoaded, LoadSceneMode loadSceneMode)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(m_strLoadSceneName, loadSceneMode);
            if (null == async)
            {
                yield break;
            }

            while (!async.isDone)
            {
                float fProgressValue;
                if (async.progress < 0.9f)
                {
                    fProgressValue = async.progress;
                }
                else
                {
                    fProgressValue = 1.0f;
                }
                yield return null;
            }

            OnProgress(0);
            OnLoadingSceneLoaded();
        }


        /// <summary>
        /// 过渡场景加载完成回调
        /// </summary>
        private void OnLoadingSceneLoaded()
        {
            // 过渡场景加载完成后加载下一个场景
            SceneSpriteMgr.Instance.StartCoroutine(OnLoadTargetScene(m_strTargetSceneName, LoadSceneMode.Single));
        }

        private IEnumerator OnLoadTargetScene(string strLevelName, LoadSceneMode loadSceneMode)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(strLevelName, loadSceneMode);
            async.allowSceneActivation = false;
            if (null == async)
            {
                yield break;
            }

            OnProgress(0.15f);

            yield return new WaitForSeconds(0.2f);

            //*加载进度
            while (!async.isDone)
            {
                float fProgressValue;

                if (async.progress < 0.9f)
                    fProgressValue = async.progress;
                else
                    fProgressValue = 1.0f;

                OnProgress(fProgressValue);

                if (fProgressValue >= 0.9)
                {
                    yield return new WaitForSeconds(0.2f);
                    async.allowSceneActivation = true;
                    OnTargetSceneLoaded();
                    if (m_showUI != "null")
                    {
                        UIModule.Instance.OpenWindow(m_showUI);
                    }
                }
            }

            
        }

        /// <summary>
        /// 加载下一场景完成回调
        /// </summary>
        private void OnTargetSceneLoaded()
        {
            m_bLoading = false;
            m_strCurSceneName = m_strTargetSceneName;
            m_strTargetSceneName = null;
            m_onSceneLoaded?.Invoke();
        }

        /// <summary>
        /// 进度
        /// </summary>
        /// <param name="fProgress"></param>
        private void OnProgress(float fProgress)
        {
            if (m_objLoadProgress == null)
            {
                m_objLoadProgress = GameObject.Find("LoadingObj");

                Image bg = GameObject.Find("BG").GetComponent<Image>();
                bg.sprite = m_sprite;

                if (m_objLoadProgress == null)
                {
                    Debug.Log(s_strLoadedSceneName);
                    return;
                }
            }

            Text textLoadProgress = m_objLoadProgress.transform.Find("TextLoadProgress").GetComponent<Text>();
            textLoadProgress.text = (fProgress * 100).ToString() + "%";

            if (textLoadProgress == null)
            {
                return;
            }
        }
    }
}