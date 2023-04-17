using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace GameLogic
{
    public static class Tools
    {
        #region object转换
        /// <summary>
        /// object转int
        /// </summary>
        /// <param name="o"></param>
        /// <returns>int</returns>
        public static int Int(object o)
        {
            return Convert.ToInt32(o);
        }

        /// <summary>
        /// object转float
        /// </summary>
        /// <param name="o"></param>
        /// <returns>float</returns>
        public static float Float(object o)
        {
            return (float)Math.Round(Convert.ToSingle(o), 2);
        }

        /// <summary>
        /// object转long
        /// </summary>
        /// <param name="o"></param>
        /// <returns>long</returns>
        public static long Long(object o)
        {
            return Convert.ToInt64(o);
        }
        /// <summary>
        /// object转string
        /// </summary>
        /// <param name="o"></param>
        /// <returns>string</returns>
        public static string String(object o)
        {
            return Convert.ToString(o);
        }
        #endregion

        #region 随机
        /// <summary>
        /// 随机数（int）
        /// </summary>
        /// <param name="min">最小数</param>
        /// <param name="max">最大数</param>
        /// <returns>随机数</returns>
        public static int Random(int min, int max)
        {
            return UnityEngine.Random.Range(min, max + 1);
        }
        /// <summary>
        /// 随机数（float）
        /// </summary>
        /// <param name="min">最小数</param>
        /// <param name="max">最大数</param>
        /// <returns>随机数</returns>
        public static float Random(float min, float max)
        {
            return UnityEngine.Random.Range(min, max + 1);
        }
        #endregion

        #region 时间
        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// 获得增加时间后的时间
        /// </summary>
        /// <param name="type">1、秒 2、分钟 3、小时 4、天</param>
        /// <param name="value">索要添加的数值</param>
        /// <returns></returns>
        public static DateTime GetAddTime(int type,int value)
        {
            DateTime time = DateTime.Now;
            DateTime result = DateTime.Now;

            switch (type)
            {
                case 1:
                    result = time.AddSeconds(value);
                    break;
                case 2:
                    result = time.AddMinutes(value);
                    break;
                case 3:
                    result = time.AddHours(value);
                    break;
                case 4:
                    result = time.AddDays(value);
                    break;
            }

            return result;
        }

        /// <summary>
        /// 获取目标时间与当前时间的时间差
        /// </summary>
        /// <param name="target"></param>
        /// <returns>可转换为天、小时、秒等</returns>
        public static TimeSpan GetTime(DateTime target)
        {
            DateTime current = DateTime.Now;	//获取当前时间
            TimeSpan ts = current - target;	    //计算时间差
            return ts;
        }

        /// <summary>
        /// 秒数据换算为日期
        /// </summary>
        /// <param name="tick"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(long tick, bool totalMilliseconds = false)
        {
            if (totalMilliseconds == false)
            {
                //秒
                return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(tick).ToLocalTime();
            }
            else
            {
                //毫秒
                return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(tick).ToLocalTime();
            }
        }

        /// <summary>
        /// 获取1970-01-01至dateTime0 - 毫秒
        /// </summary>
        public static long GetTimestamp(DateTime dateTime, bool totalMilliseconds = false)
        {
            if (totalMilliseconds == false)
            {
                //秒
                DateTime dt1970 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();
                return (dateTime.Ticks - dt1970.Ticks) / 10000000;
            }
            else
            {
                //毫秒
                DateTime dt1970 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();
                return (dateTime.Ticks - dt1970.Ticks) / 10000;
            }
        }

        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetCurrentTimestamp(bool totalMilliseconds = false)
        {
            if (totalMilliseconds == false)
            {
                //秒
                DateTime dt1970 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();
                return (DateTime.Now.Ticks - dt1970.Ticks) / 10000000;
            }
            else
            {
                //毫秒
                DateTime dt1970 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();
                return (DateTime.Now.Ticks - dt1970.Ticks) / 10000;
            }
        }

        /// <summary>
        /// yyyy-MM-dd HH:MM:SS 格式的日期string 转换为 DateTime
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeByString(string dateTime)
        {
            string[] tmp = dateTime.Split(' ');
            string[] tmpDate = tmp[0].Split('-');
            string[] tmpTime = tmp[1].Split(':');
            int year = int.Parse(tmpDate[0]);
            int month = int.Parse(tmpDate[1]);
            int date = int.Parse(tmpDate[2]);
            int hours = int.Parse(tmpTime[0]);
            int minutes = int.Parse(tmpTime[1]);
            int seconds = int.Parse(tmpTime[2]);

            return new DateTime(year, month, date, hours, minutes, seconds, 0, DateTimeKind.Utc);
        }
        #endregion

        #region Unity
        /// <summary>
        /// 获取当前平台
        /// </summary>
        /// <returns>平台</returns>
        public static RuntimePlatform GetCurrentPlatform()
        {
            return Application.platform;
        }
        /// <summary>
        /// 判断是否是编辑模式
        /// </summary>
        /// <returns>是/否</returns>
        public static bool IsDebug()
        {
            return Application.isEditor;
        }

        /// <summary>
        /// 搜索子物体组件-GameObject版
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <param name="subnode"></param>
        /// <returns></returns>
        public static T Get<T>(GameObject go, string subnode) where T : Component
        {
            if (go != null)
            {
                Transform sub = go.transform.Find(subnode);
                if (sub != null) return sub.GetComponent<T>();
            }
            return null;
        }

        /// <summary>
        /// 设置屏幕分辨率
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="fullScreen"></param>
        public static void SetScreen(int width, int height, bool fullScreen)
        {
            Screen.SetResolution(/*屏幕宽度*/ width,/*屏幕高度*/ height, /*是否全屏显示*/fullScreen);
        }

        /// <summary>
        /// 搜索子物体组件-Transform版
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <param name="subnode"></param>
        /// <returns></returns>
        public static T Get<T>(Transform go, string subnode) where T : Component
        {
            if (go != null)
            {
                Transform sub = go.Find(subnode);
                if (sub != null) return sub.GetComponent<T>();
            }
            return null;
        }

        /// <summary>
        /// 搜索子物体组件-Component版
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <param name="subnode"></param>
        /// <returns></returns>
        public static T Get<T>(Component go, string subnode) where T : Component
        {
            return go.transform.Find(subnode).GetComponent<T>();
        }
        //----------------------------------------
        /// <summary>
        /// 添加组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        public static T Add<T>(GameObject go) where T : Component
        {
            if (go != null)
            {
                T[] ts = go.GetComponents<T>();
                for (int i = 0; i < ts.Length; i++)
                {
                    if (ts[i] != null) GameObject.Destroy(ts[i]);
                }
                return go.gameObject.AddComponent<T>();
            }
            return null;
        }

        /// <summary>
        /// 添加组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        public static T Add<T>(Transform go) where T : Component
        {
            return Add<T>(go.gameObject);
        }
        //----------------------------------------
        /// <summary>
        /// 查找子对象
        /// </summary>
        /// <param name="go"></param>
        /// <param name="subnode"></param>
        /// <returns></returns>
        public static GameObject Child(GameObject go, string subnode)
        {
            return Child(go.transform, subnode);
        }

        /// <summary>
        /// 查找子对象
        /// </summary>
        /// <param name="go"></param>
        /// <param name="subnode"></param>
        /// <returns></returns>
        public static GameObject Child(Transform go, string subnode)
        {
            Transform tran = go.Find(subnode);
            if (tran == null) return null;
            return tran.gameObject;
        }

        //----------------------------------------

        /// <summary>
        /// 取平级对象
        /// </summary>
        /// <param name="go"></param>
        /// <param name="subnode"></param>
        /// <returns></returns>
        public static GameObject Peer(GameObject go, string subnode)
        {
            return Peer(go.transform, subnode);
        }

        /// <summary>
        /// 取平级对象
        /// </summary>
        /// <param name="go"></param>
        /// <param name="subnode"></param>
        /// <returns></returns>
        public static GameObject Peer(Transform go, string subnode)
        {
            Transform tran = go.parent.Find(subnode);
            if (tran == null) return null;
            return tran.gameObject;
        }

        //----------------------------------------

        /// <summary>
        /// 清除所有子节点
        /// </summary>
        /// <param name="go"></param>
        public static void ClearChild(Transform go)
        {
            if (go == null) return;
            for (int i = go.childCount - 1; i >= 0; i--)
            {
                GameObject.Destroy(go.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// 清除所有子节点
        /// </summary>
        /// <param name="go"></param>
        public static void ClearChild(GameObject go)
        {
            var tran = go.transform;

            while (tran.childCount > 0)
            {
                var child = tran.GetChild(0);

                if (Application.isEditor && !Application.isPlaying)
                {
                    child.parent = null; // 清空父, 因为.Destroy非同步的
                    GameObject.DestroyImmediate(child.gameObject);
                }
                else
                {
                    GameObject.Destroy(child.gameObject);
                    // 预防触发对象的OnEnable，先Destroy
                    child.parent = null; // 清空父, 因为.Destroy非同步的
                }
            }
        }

        /// <summary>
        /// 模仿 NGUISelectionTool的同名方法，将位置旋转缩放清零
        /// </summary>
        /// <param name="t"></param>
        public static void ResetLocalTransform(Transform t)
        {
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
        }
        #endregion

        #region 字符串
        /// <summary>
        /// 得到字符串长度，一个汉字长度为2
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static int StrLength(string inputString)
        {
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            int tempLen = 0;
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;
            }
            return tempLen;
        }

        /// <summary>
        /// 计算字符串的MD5值
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string md5(string source)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(source);
            byte[] md5Data = md5.ComputeHash(data, 0, data.Length);
            md5.Clear();

            string destString = "";
            for (int i = 0; i < md5Data.Length; i++)
            {
                destString += System.Convert.ToString(md5Data[i], 16).PadLeft(2, '0');
            }
            destString = destString.PadLeft(32, '0');
            return destString;
        }
        /// <summary>
        /// 检查类名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CheckClassName(string str)
        {
            return Regex.IsMatch(str, @"^[A-Z][A-Za-z0-9_]*$");
        }
        /// <summary>
        /// 检查字段名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool CheckFieldName(string name)
        {
            return Regex.IsMatch(name, @"^[A-Za-z_][A-Za-z0-9_]*$");
        }

        /// <summary>
        /// 第一个字符大写
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CapitalFirstChar(string str)
        {
            return str[0].ToString().ToUpper() + str.Substring(1);
        }
        #endregion

        #region File
        /// <summary>
        /// MD5Encrypt
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string str)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes;
            hashedDataBytes = md5Hasher.ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(str));
            StringBuilder tmp = new StringBuilder();
            foreach (byte i in hashedDataBytes)
            {
                tmp.Append(i.ToString("x2"));
            }
            return tmp.ToString();
        }

        /// <summary>
        /// 无视锁文件，直接读bytes  读取（加载）数据
        /// </summary>
        /// <param name="resPath"></param>
        /// <returns></returns>
        public static byte[] ReadAllBytes(string resPath)
        {
            byte[] bytes;
            using (FileStream fs = File.Open(resPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                bytes = new byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);
            }
            return bytes;
        }
        /// <summary>
        /// 获取文件协议
        /// </summary>
        /// <returns></returns>
        public static string GetFileProtocol()
        {
            string fileProtocol = "file://";
            if (Application.platform == RuntimePlatform.WindowsEditor ||
                Application.platform == RuntimePlatform.WindowsPlayer
#if !UNITY_5_4_OR_NEWER
                || Application.platform == RuntimePlatform.WindowsWebPlayer
#endif
)
                fileProtocol = "file:///";

            return fileProtocol;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fullpath">完整路径</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static async Task SaveFile(string fullpath, string content)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            await SaveFileAsync(fullpath, buffer);
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fullpath"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static async Task<int> SaveFileAsync(string fullpath, byte[] content)
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (content == null)
                    {
                        content = new byte[0];
                    }

                    string dir = PathUtils.GetParentDir(fullpath);

                    if (!Directory.Exists(dir))
                    {
                        try
                        {
                            Directory.CreateDirectory(dir);
                        }
                        catch (Exception e)
                        {
                            Debug.LogError(string.Format("SaveFile() CreateDirectory Error! Dir:{0}, Error:{1}", dir, e.Message));
                            return -1;
                        }
                    }

                    FileStream fs = null;
                    try
                    {
                        fs = new FileStream(fullpath, FileMode.Create, FileAccess.Write);
                        fs.Write(content, 0, content.Length);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(string.Format("SaveFile() Path:{0}, Error:{1}", fullpath, e.Message));
                        fs.Close();
                        return -1;
                    }

                    fs.Close();
                    return content.Length;
                });
            }
            catch (Exception ex)
            {
                Debug.LogError(ex + " SaveFile");
                throw;
            }
        }

        /// <summary>
        /// 加载Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T LoadJson<T>(string fileName)
        {
            string fileAbslutePath = Application.persistentDataPath + "/Json/" + fileName + ".json";
            object value = null;
            if (File.Exists(fileAbslutePath))
            {
                FileStream fs = new FileStream(fileAbslutePath, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string tempStr = sr.ReadToEnd();
                value = JsonMapper.ToObject<T>(tempStr);

                sr.Close();
                if (fs != null)
                {
                    fs.Close();
                }
            }

            return (T)value;
        }

        /// <summary>
        /// 保存Json
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static IEnumerator SaveJson(string jsonStr, string fileName)
        {
            string filePath = Application.persistentDataPath + "/Json";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string fileAbslutePath = filePath + "/" + fileName + ".json";

            byte[] bts = System.Text.Encoding.UTF8.GetBytes(jsonStr);
            File.WriteAllBytes(fileAbslutePath, bts);

            yield return null;
        }
        #endregion

        #region Color
        /// <summary>
        /// color 转换hex
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ColorToHex(Color color)
        {
            int r = Mathf.RoundToInt(color.r * 255.0f);
            int g = Mathf.RoundToInt(color.g * 255.0f);
            int b = Mathf.RoundToInt(color.b * 255.0f);
            int a = Mathf.RoundToInt(color.a * 255.0f);
            string hex = string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", r, g, b, a);
            return hex;
        }

        /// <summary>
        /// hex转换到color
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static Color HexToColor(string hex)
        {
            byte br = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte bg = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte bb = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            byte cc = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            float r = br / 255f;
            float g = bg / 255f;
            float b = bb / 255f;
            float a = cc / 255f;
            return new Color(r, g, b, a);
        }
        #endregion

        #region 计算公式
        /// <summary>
        /// 计算中心点
        /// </summary>
        /// <param name="Points"></param>
        /// <returns></returns>
        public static Vector3 CalculateCenterPoint(List<Transform> Points)
        {
            Vector3 centerPoint = Vector3.zero;
            foreach (Transform p in Points)
            {
                centerPoint += p.position;
            }
            centerPoint /= Points.Count;
            return centerPoint;
        }

        /// <summary>
        /// 计算AB与CD两条线段的交点.
        /// </summary>
        /// <param name="a">A点</param>
        /// <param name="b">B点</param>
        /// <param name="c">C点</param>
        /// <param name="d">D点</param>
        /// <param name="intersectPos">AB与CD的交点</param>
        /// <returns>是否相交 true:相交 false:未相交</returns>
        public static bool TryGetIntersectPoint(Vector3 a, Vector3 b, Vector3 c, Vector3 d, out Vector3 intersectPos)
        {
            intersectPos = Vector3.zero;

            Vector3 ab = b - a;
            Vector3 ca = a - c;
            Vector3 cd = d - c;

            Vector3 v1 = Vector3.Cross(ca, cd);

            if (Mathf.Abs(Vector3.Dot(v1, ab)) > 1e-6)
            {
                // 不共面
                return false;
            }

            if (Vector3.Cross(ab, cd).sqrMagnitude <= 1e-6)
            {
                // 平行
                return false;
            }

            Vector3 ad = d - a;
            Vector3 cb = b - c;
            // 快速排斥
            if (Mathf.Min(a.x, b.x) > Mathf.Max(c.x, d.x) || Mathf.Max(a.x, b.x) < Mathf.Min(c.x, d.x)
               || Mathf.Min(a.y, b.y) > Mathf.Max(c.y, d.y) || Mathf.Max(a.y, b.y) < Mathf.Min(c.y, d.y)
               || Mathf.Min(a.z, b.z) > Mathf.Max(c.z, d.z) || Mathf.Max(a.z, b.z) < Mathf.Min(c.z, d.z)
            )
                return false;

            // 跨立试验
            if (Vector3.Dot(Vector3.Cross(-ca, ab), Vector3.Cross(ab, ad)) > 0
                && Vector3.Dot(Vector3.Cross(ca, cd), Vector3.Cross(cd, cb)) > 0)
            {
                Vector3 v2 = Vector3.Cross(cd, ab);
                float ratio = Vector3.Dot(v1, v2) / v2.sqrMagnitude;
                intersectPos = a + ab * ratio;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取两点之间距离一定百分比的一个点
        /// </summary>
        /// <param name="start">起始点</param>
        /// <param name="end">结束点</param>
        /// <param name="distance">起始点到目标点距离百分比</param>
        /// <returns></returns>
        public static Vector3 GetBetweenPoint1(Vector3 start, Vector3 end, float percent)
        {
            Vector3 normal = (end - start).normalized;
            float distance = Vector3.Distance(start, end);
            return normal * (distance * percent) + start;
        }

        /// <summary>
        /// 获取两点之间一定距离的点
        /// </summary>
        /// <param name="start">起始点</param>
        /// <param name="end">结束点</param>
        /// <param name="distance">距离</param>
        /// <returns></returns>
        public static Vector3 GetBetweenPoint2(Vector3 start, Vector3 end, float distance)
        {
            Vector3 normal = (end - start).normalized;
            return normal * distance + start;
        }

        /// <summary>
        /// 获取角度
        /// </summary>
        /// <param name="var1"></param>
        /// <param name="var2"></param>
        /// <returns></returns>
        public static float GetAngel(Transform var1, Transform var2)
        {
            //注意角度测量一定要用对象的正方向
            float angel = Vector3.Angle(var1.forward, var2.forward);
            return angel;
        }

        /// <summary>
        /// 判断物体左右转转
        /// </summary>
        /// <param name="var1"></param>
        /// <param name="var2"></param>
        /// <returns></returns>
        public static int RotationDirection(Transform var1, Transform var2)
        {
            int direction = 0;

            Vector2 v1 = new Vector2(var1.forward.x, var1.forward.z); //旋转前的前方
            Vector2 v2 = new Vector2(var2.forward.x, var2.forward.z); //旋转后的前方

            float rightFloat = v1.x * v2.y - v2.x * v1.y;

            if (rightFloat < 0)
            {
                //向右转了
                direction = 1;
            }
            else if (rightFloat > 0)
            {
                //向左转了
                direction = -1;
            }
            else
            {
                //没转
                direction = 0;
            }

            return direction;
        }

        /// <summary>
        /// 车轮 - PID控制器
        /// </summary>
        /// <param name="wheel"></param>
        /// <param name="var2"></param>
        public static float PIDController(WheelCollider wheel, 
                                         float targetSpeed,
                                         float kp,
                                         float ki,
                                         float kd,
                                         float integral,
                                         float prevError)
        {
            // 获取当前车轮的转速
            float currentSpeed = wheel.rpm * (Mathf.PI / 30.0f * wheel.radius);

            // 计算速度误差
            float error = targetSpeed - currentSpeed;

            // 计算PID控制器的控制力
            float proportional = kp * error;
            integral += ki * error * Time.deltaTime;
            float derivative = kd * (error - prevError) / Time.deltaTime;
            float control = proportional + integral + derivative;

            // 应用控制力到车轮
            if (control > 0)
            {
                wheel.brakeTorque = 0.0f;
                wheel.motorTorque = control;
            }
            else
            {
                wheel.brakeTorque = -control;
                wheel.motorTorque = 0.0f;
            }

            // 更新速度误差和控制参数
            prevError = error;

            return integral;
        }
        #endregion
    }
}