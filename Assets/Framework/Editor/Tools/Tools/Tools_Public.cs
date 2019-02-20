using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Framework.Editor.Tools
{
    public sealed class Tools_Public
    {
        private Tools_Public()
        { }


        public const float Tools_BoxWidth = 150f;
        public const float Tools_FieldWidth = 400f;
        public const float Tools_Space = 18f;
        public const float Tools_barPadding = 15f;

        public const float Tools_MinButtonWidth = 75f;
        public const float Tools_MaxButtonWidth = 150f;
        public const float Tools_Buttonspacing = 5f;

        /// <summary>
        /// 枚举按钮
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varEnum"></param>
        /// <param name="width"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool EnumButton<T>(ref T varEnum, float width, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal();
            string[] tringname = System.Enum.GetNames(varEnum.GetType());
            int count = tringname.Length;
            float bottonwidth = (width - Tools_barPadding * 2 - Tools_Buttonspacing * count) / count;

            GUILayout.Space(Tools_barPadding);

            for (int i = 0; i < count; i++)
            {
                string tempName = tringname[i];
                if (GUILayout.Button(tempName, GUILayout.Width(bottonwidth)))
                {
                    object temp = (System.Enum.Parse(varEnum.GetType(), tempName));
                    varEnum = (T)temp;
                    //varEnum = (varEnum.GetType())(System.Enum.Parse(varEnum.GetType(), tempName));
                    return true;
                }
            }
            EditorGUILayout.EndHorizontal();
            return false;
        }

        /// <summary>
        /// 居中按钮
        /// </summary>
        /// <param name="text"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool CenterButton(string text, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(text, options))
            {
                return true;
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            return false;
        }
        /// <summary>
        /// 居中文字
        /// </summary>
        /// <param name="text"></param>
        /// <param name="options"></param>
        public static void CenterLabel(string text, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(text, options);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }



        /// <summary>
        /// 读取数据
        /// </summary>
        public static T ReadDate<T>(string path) where T : BaseDate
        {
            T date = null;
            //读取数据
            var dataPath = System.IO.Path.GetFullPath(".");
            dataPath = dataPath.Replace("\\", "/");
            dataPath += "/Library/Tools/" + path + ".dat";
            if (File.Exists(dataPath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(dataPath, FileMode.Open);
                date = bf.Deserialize(file) as T;
                file.Close();
            }
            return date;
        }

        /// <summary>
        /// 存储数据
        /// </summary>
        public static void SaveDate<T>(string path, T date) where T : BaseDate
        {
            //存储数据
            var dataPath = System.IO.Path.GetFullPath(".");
            dataPath = dataPath.Replace("\\", "/");
            dataPath += "/Library/Tools/" + path + ".dat";
            if (!File.Exists(dataPath))
            {
                if (!Directory.Exists(Path.GetDirectoryName(dataPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(dataPath));
                }
            }

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(dataPath);

            bf.Serialize(file, date);
            file.Close();
        }

    }
    [System.Serializable]
    public class BaseDate
    {
    }
}