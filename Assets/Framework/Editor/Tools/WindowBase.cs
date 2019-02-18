

using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Framework.Editor.Tools
{
    public class WindowBase
    {
        internal const float _CenterButton_Width = 150f;

        /// <summary>
        /// 读取数据
        /// </summary>
        internal T ReadDate<T>(string path)where T: BaseDate
        {
            T date = null;
            //读取数据
            var dataPath = System.IO.Path.GetFullPath(".");
            dataPath = dataPath.Replace("\\", "/");
            dataPath += "/Library/Tools/"+path+".dat";
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
        internal void SaveDate<T>(string path,T date) where T : BaseDate
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

        internal virtual void OnEnable(EditorWindow editor)
        {
        }
        internal virtual void OnDisable()
        {
        }
        internal virtual void OnGUI()
        {
        }




        internal bool CenterButton(string text,params GUILayoutOption[] options)
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







        [System.Serializable]
        internal class BaseDate
        {
        }
    }
}