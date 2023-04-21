using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using LitJson;

namespace GameLogic
{
    /// <summary>
    /// 数据库模块
    /// </summary>
    public class DatabaseModule : CustommModuleInitialize 
    {
        #region 实例与初始化
        public static DatabaseModule Instance = new DatabaseModule();

        public bool IsInited { get; private set; }
        private double _initProgress = 0;
        public double InitProgress { get { return _initProgress; } }
        #endregion

        // 游戏配置表
        Dictionary<int, GameConfig> configs = new Dictionary<int, GameConfig>();

        string filePath = AppConfig.DatabasePath;
        string fileName = "GameConfig.json";

        public IEnumerator Init()
        {
            if(Tools.IsDebug())
            {
                Debug.Log("DataBaseModule 初始化");
            }
            Instance = this;
            InitGameConfig();

            yield return null;
            IsInited = true;
        }

        public void ClearData()
        {
            if (Tools.IsDebug())
            {
                Debug.Log("DataBaseModule 清除数据");
            }
            configs.Clear();
        }

        ////-------------------------------------
        void InitGameConfig()
        {
            List<GameConfig> gameConfigs = new List<GameConfig>();
            //获取完整路径
            string fullPath;
            bool exists = PathUtils.GetFullPath(filePath + fileName, out fullPath);
            if (exists)
            {
                string content = PathUtils.ReadFile(filePath, fileName);
                gameConfigs = JsonMapper.ToObject<List<GameConfig>>(content);
            }
            else
            {
                TextAsset json = ResourcesModule.Instance.LoadAsset<TextAsset>("AutoDatabase/GameConfig");
                PathUtils.WriteFile(json.text, filePath, fileName);
                gameConfigs = JsonMapper.ToObject<List<GameConfig>>(json.text);
            }

            foreach (GameConfig tempData in gameConfigs)
            {
                configs.Add(tempData.Id, tempData);
            }

            //Debug.Log(configs.Count);

            if (Tools.IsDebug())
            {
                Debug.Log("DataBaseModule 完成GameConfig加载");
            }
        }

        /// <summary>
        /// 获取游戏配置表数据（已从数据库读取）
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, GameConfig> GetConfig()
        {
            return configs;
        }
        
        public void SaveConfig()
        {
            string jsonStr = JsonMapper.ToJson(configs.Values.ToList(), true);
            PathUtils.WriteFile(jsonStr, filePath, fileName);
        }
    }
}