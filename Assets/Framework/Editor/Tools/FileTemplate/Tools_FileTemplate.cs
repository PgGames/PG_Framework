
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;

namespace Framework.Editor.Tools.FileTemplate
{
    public class Tools_FileTemplate : EditorWindow
    {
        private static Tools_FileTemplate s_instance = null;
        internal static Tools_FileTemplate instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = GetWindow<Tools_FileTemplate>();
                return s_instance;
            }
        }
        [MenuItem("Tools/File Template", priority = 1)]
        internal static void SettingFileTemplate()
        {
            s_instance = null;
            instance.titleContent = new GUIContent("File Template");
            instance.Show();
        }
        
        internal FileTemplateTabData m_TabData;


        private File_Setting m_FileSetting;
        private File_Prevew m_FilePrevew;
        private mode m_Mode = mode.Setting;


        internal Tools_FileTemplate()
        {
            if (m_FileSetting == null)
                m_FileSetting = new File_Setting();
            if (m_FilePrevew == null)
                m_FilePrevew = new File_Prevew();
        }


        internal void OnEnable()
        {
            ReadDate();
            
            if (m_FileSetting != null)
                m_FileSetting.OnEnable(this);
            if (m_FilePrevew != null)
                m_FilePrevew.OnEnable(this);
        }
        internal void OnDisable()
        {
            //存储数据
            var dataPath = System.IO.Path.GetFullPath(".");
            dataPath = dataPath.Replace("\\", "/");
            dataPath += "/Library/Tools/FileTemplate.dat";
            if (!File.Exists(dataPath))
            {
                if (!Directory.Exists(Path.GetDirectoryName(dataPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(dataPath));
                }
            }

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(dataPath);

            bf.Serialize(file, m_TabData);
            file.Close();
        }


        void ReadDate()
        {
            //读取数据
            var dataPath = System.IO.Path.GetFullPath(".");
            dataPath = dataPath.Replace("\\", "/");
            dataPath += "/Library/Tools/FileTemplate.dat";
            if (File.Exists(dataPath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(dataPath, FileMode.Open);
                var data = bf.Deserialize(file) as FileTemplateTabData;
                if (data != null)
                {
                    m_TabData = data;
                }
                else
                {
                    InitDate();
                }
                file.Close();
            }
            else
            {
                InitDate();
            }
        }
        internal void InitDate()
        {
            m_TabData = new FileTemplateTabData();
            m_TabData.m_UsingNamespace = new List<string>();
            m_TabData.m_UsingNamespace.AddRange(new string[2] { "UnityEngine", "System.Collections" });
            m_TabData.m_ModuleInfo = new List<ModuleInfo>{
                new ModuleInfo { Name = "property variable" },
                new ModuleInfo { Name = "Unity Methods", m_Methods = new List<string>{
                    "Awake",
                    "OnEnable",
                    "Start",
                    "Update",
                    "OnDisable",
                } },
                new ModuleInfo { Name = "private Methods" },
                new ModuleInfo { Name = "Protected & Public Methods" },
            };
        }

        






        const float k_ToolbarPadding = 15f;
        const float k_ToolButtonspacing = 5f;

        private Vector2 m_ScrollPosition;
        private void OnGUI()
        {
            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);
            GUILayout.BeginVertical();
            HandToggleUI();
            EditorGUILayout.Space();
            CenterContentUI();
            EditorGUILayout.Space();
            EndButtonUI();
            EditorGUILayout.Space();
            GUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }
        /// <summary>
        /// 头部开关按钮
        /// </summary>
        void HandToggleUI()
        {
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.Space(k_ToolbarPadding);
            float toolbarWidth = (position.width - k_ToolButtonspacing * ((int)mode.Count) - k_ToolbarPadding * 2) / ((int)mode.Count);
            for (int i = 0; i < (int)mode.Count; i++)
            {
                string tempName = ((mode)i).ToString();
                if (GUILayout.Button(tempName, GUILayout.Width(toolbarWidth)))
                {
                    m_Mode = (mode)i;
                }
            }
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// 中部内容
        /// </summary>
        void CenterContentUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(k_ToolbarPadding);
            GUILayout.BeginVertical();
            switch (m_Mode)
            {
                case mode.Setting:
                    m_FileSetting.OnGUI();
                    break;
                case mode.Preview:
                    m_FilePrevew.OnGUI();
                    break;
                default:
                    break;
            }
            GUILayout.EndVertical();
            GUILayout.Space(k_ToolbarPadding);
            GUILayout.EndHorizontal();

        }

        /// <summary>
        /// 底部提交按钮
        /// </summary>
        void EndButtonUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Replace File Template"))
            {
                m_FilePrevew.SaveFileInfo();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private enum mode
        {
            Noll = -1,
            Setting,        //设置
            Preview,        //预览
            Count,
        }




        [System.Serializable]
        internal class FileTemplateTabData
        {
            internal string m_CopyrightName;
            internal bool m_UserCopyright = true;
            internal List<string> m_UsingNamespace;
            internal bool m_Namespace = true;
            internal List<ModuleInfo> m_ModuleInfo;
        }
        [System.Serializable]
        internal class ModuleInfo
        {
            internal string Name;
            internal List<string> m_Methods;
        }
    }
}