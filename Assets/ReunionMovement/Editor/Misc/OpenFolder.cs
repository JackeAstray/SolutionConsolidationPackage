using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace GameLogic.Editor
{
    /// <summary>
    /// 打开文件夹相关的实用函数。
    /// </summary>
    public static class OpenFolder
    {
        /// <summary>
        /// 打开 Data Path 文件夹。
        /// </summary>
        [MenuItem("GameLogic/打开文件夹/Data Path", false, 1)]
        public static void OpenFolderDataPath()
        {
            Execute(Application.dataPath);
        }

        /// <summary>
        /// 打开 Persistent Data Path 文件夹。
        /// </summary>
        [MenuItem("GameLogic/打开文件夹/Persistent Data Path", false, 2)]
        public static void OpenFolderPersistentDataPath()
        {
            Execute(Application.persistentDataPath);
        }

        /// <summary>
        /// 打开 Streaming Assets Path 文件夹。
        /// </summary>
        [MenuItem("GameLogic/打开文件夹/Streaming Assets Path", false, 3)]
        public static void OpenFolderStreamingAssetsPath()
        {
            Execute(Application.streamingAssetsPath);
        }

        /// <summary>
        /// 打开 Temporary Cache Path 文件夹。
        /// </summary>
        [MenuItem("GameLogic/打开文件夹/Temporary Cache Path", false, 4)]
        public static void OpenFolderTemporaryCachePath()
        {
            Execute(Application.temporaryCachePath);
        }

#if UNITY_2018_3_OR_NEWER

        /// <summary>
        /// 打开 Console Log Path 文件夹。
        /// </summary>
        [MenuItem("GameLogic/打开文件夹/Console Log Path", false, 5)]
        public static void OpenFolderConsoleLogPath()
        {
            Execute(System.IO.Path.GetDirectoryName(Application.consoleLogPath));
        }

#endif

        /// <summary>
        /// 打开 Excel 所在文件夹。
        /// </summary>
        [MenuItem("GameLogic/打开文件夹/Excel Tools Path", false, 6)]
        public static void OpenFolderExcelToolsPath()
        {
            Execute(System.IO.Path.GetDirectoryName("./Config/ExcelTools"));
        }

        
        /// <summary>
        /// 打开指定路径的文件夹。
        /// </summary>
        /// <param name="folder">要打开的文件夹的路径。</param>
        public static void Execute(string folder)
        {
            folder = Utility.Text.Format("\"{0}\"", folder);
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                    Process.Start("Explorer.exe", folder.Replace('/', '\\'));
                    break;

                case RuntimePlatform.OSXEditor:
                    Process.Start("open", folder);
                    break;

                default:
                    throw new GameLogicException(Utility.Text.Format("Not support 打开文件夹 on '{0}' platform.", Application.platform));
            }
        }
    }
}