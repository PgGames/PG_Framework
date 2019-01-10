
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;

namespace Framework.Editor.Tools
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

        [SerializeField]
        private FileTemplateTabData m_TabData;

        private mode m_Mode = mode.Setting;


        internal Tools_FileTemplate()
        {
            m_TabData = new FileTemplateTabData();
            m_TabData.m_UsingNamespace = new List<string>();
            if (m_TabData.m_UsingNamespace.Count == 0)
            {
                m_TabData.m_UsingNamespace.AddRange(new string[2] { "UnityEngine", "System.Collections" });
            }
            m_TabData.m_ModuleInfo = new List<ModuleInfo>();
            if (m_TabData.m_ModuleInfo.Count == 0)
            {
                m_TabData.m_ModuleInfo.AddRange(new ModuleInfo[4] {
                    new ModuleInfo { Name = "property variable" },
                    new ModuleInfo { Name = "Unity Methods" },
                    new ModuleInfo { Name = "private Methods" },
                    new ModuleInfo { Name = "Protected & Public Methods" },
                });
            }
        }


        internal void OnEnable()
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
                    m_TabData = data;
                file.Close();
            }
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
        
        const float k_ToolbarPadding = 15f;
        const float k_ToolButtonspacing = 5f;
        const float k_ButtonSingleWidth = 20f;
        const float k_AddButtonWidth = 100f;

        private Vector2 m_ScrollPosition;
        private void OnGUI()
        {
            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);
            GUILayout.BeginVertical();
            ModeToggleGUI();
            GUILayout.BeginHorizontal();
            GUILayout.Space(k_ToolbarPadding);
            GUILayout.BeginVertical();
            switch (m_Mode)
            {
                case mode.Setting:
                    SettingInfoGUI();
                    break;
                case mode.Preview:
                    PreviewInfoGUI();
                    break;
                default:
                    break;
            }
            GUILayout.EndVertical();
            GUILayout.Space(k_ToolbarPadding);
            GUILayout.EndHorizontal();

            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Replace File Template"))
            {
                ReplaceFile();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            EditorGUILayout.Space();
            GUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }
        private void ModeToggleGUI()
        {
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.Space(k_ToolbarPadding);
            float toolbarWidth = (position.width - k_ToolButtonspacing * ((int)mode.Count) - k_ToolbarPadding * 2) / ((int)mode.Count);
            for (int i = 0; i < (int)mode.Count; i++)
            {
                string tempName = ((mode)i).ToString().ToUpper();
                if (GUILayout.Button(tempName, GUILayout.Width(toolbarWidth)))
                {
                    m_Mode = (mode)i;
                }
            }
            GUILayout.EndHorizontal();
        }
        private void SettingInfoGUI()
        {
            EditorGUILayout.Space();
            //Copyright
            using (new EditorGUI.DisabledScope())
            {
                m_TabData.m_CopyrightName = EditorGUILayout.TextField("CopyrightName", m_TabData.m_CopyrightName);

                m_TabData.m_UserCopyright = EditorGUILayout.Toggle("IsCopyright", m_TabData.m_UserCopyright);
            }
            //using
            using (new EditorGUI.DisabledScope())
            {
                EditorGUILayout.Space();
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Using Namespace");
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("+", GUILayout.Width(k_ButtonSingleWidth)))
                {
                    m_TabData.m_UsingNamespace.Add("");
                }
                GUILayout.EndHorizontal();
                for (int i = 0; i < m_TabData.m_UsingNamespace.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    m_TabData.m_UsingNamespace[i] = EditorGUILayout.TextField(m_TabData.m_UsingNamespace[i]);
                    if (GUILayout.Button("-", GUILayout.Width(k_ButtonSingleWidth)))
                    {
                        Debug.Log("Remove " + m_TabData.m_UsingNamespace[i]);
                        m_TabData.m_UsingNamespace.RemoveAt(i);
                    }
                    GUILayout.EndHorizontal();
                }
            }
            //namespace
            using (new EditorGUI.DisabledScope())
            {
                EditorGUILayout.Space();
                m_TabData.m_Namespace = EditorGUILayout.Toggle("NameSpace", m_TabData.m_Namespace);
            }
            //module
            using (new EditorGUI.DisabledScope())
            {
                EditorGUILayout.Space();
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("module info");
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("+", GUILayout.Width(k_ButtonSingleWidth)))
                {
                    m_TabData.m_ModuleInfo.Add(new ModuleInfo());
                }
                GUILayout.EndHorizontal();
                for (int i = 0; i < m_TabData.m_ModuleInfo.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    var tempModuleinfo = m_TabData.m_ModuleInfo[i];
                    if (tempModuleinfo.m_Methods == null)
                    {
                        tempModuleinfo.m_Methods = new List<string>();
                    }
                    tempModuleinfo.Name = EditorGUILayout.TextField("\u3000module Name", tempModuleinfo.Name);
                    if (GUILayout.Button("Add Method", GUILayout.Width(k_AddButtonWidth)))
                    {
                        tempModuleinfo.m_Methods.Add("");
                    }
                    if (GUILayout.Button("-", GUILayout.Width(k_ButtonSingleWidth)))
                    {
                        m_TabData.m_ModuleInfo.RemoveAt(i);
                    }
                    GUILayout.EndHorizontal();
                    for (int j = 0; j < tempModuleinfo.m_Methods.Count; j++)
                    {
                        GUILayout.BeginHorizontal();
                        tempModuleinfo.m_Methods[j] = EditorGUILayout.TextField("\u3000\u3000method Name", tempModuleinfo.m_Methods[j]);
                        if (GUILayout.Button("-", GUILayout.Width(k_ButtonSingleWidth)))
                        {
                            m_TabData.m_ModuleInfo[i].m_Methods.RemoveAt(j);
                        }
                        GUILayout.EndHorizontal();
                    }
                }
            }

        }
        private void PreviewInfoGUI()
        {
            EditorGUILayout.Space();
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.UpperLeft;
            GUILayout.Label(GetPreviewInfo(), style);

        }
        private void ReplaceFile()
        {
            string content = GetPreviewInfo();
            var path = EditorApplication.applicationContentsPath + "/Resources/ScriptTemplates/81-C# Script-NewBehaviourScript.cs.txt";
            if (File.Exists(path))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(content);
                FileStream file = File.Create(path);
                file.Write(bytes,0,bytes.Length);
                file.Close();
            }
        }
        private string GetPreviewInfo()
        {
            string tempPreviewInfo = "";
            //Copyright
            if (m_TabData.m_UserCopyright)
            {
                tempPreviewInfo = string.Format("{0}\n{1}", tempPreviewInfo, "#region Copyright");
                tempPreviewInfo = string.Format("{0}\n{1}", tempPreviewInfo, "/* ----------------------------------------------------------- ");
                tempPreviewInfo = string.Format("{0}\n{1}", tempPreviewInfo, " *   Copyright (c) {0} All rights reserved.");
                if (m_TabData.m_CopyrightName != null)
                {
                    tempPreviewInfo = string.Format(tempPreviewInfo, m_TabData.m_CopyrightName);
                }
                else
                {
                    tempPreviewInfo = string.Format(tempPreviewInfo, "***");
                }
                tempPreviewInfo = string.Format("{0}\n{1}", tempPreviewInfo, " * ----------------------------------------------------------- ");
                //tempPreviewInfo = string.Format("{0}\n{1}", tempPreviewInfo, " * Creater--------: #CREATORNAME#");
                //tempPreviewInfo = string.Format("{0}\n{1}", tempPreviewInfo, " * CreateTime-----: #CREATETIME# ");
                tempPreviewInfo = string.Format("{0}\n{1}", tempPreviewInfo, " * Creater--------: #DEVELOPERNAME#");
                tempPreviewInfo = string.Format("{0}\n{1}", tempPreviewInfo, " * CreateTime-----: #CREATIONDATE# ");
                tempPreviewInfo = string.Format("{0}\n{1}", tempPreviewInfo, " * Msg------------: ");
                tempPreviewInfo = string.Format("{0}\n{1}", tempPreviewInfo, " * ---------------------------------------------------------*/ ");
                tempPreviewInfo = string.Format("{0}\n{1}", tempPreviewInfo, "#endregion Copyright\n");
            }
            //using
            if (m_TabData.m_UsingNamespace != null)
            {
                for (int i = 0; i < m_TabData.m_UsingNamespace.Count; i++)
                {
                    tempPreviewInfo = string.Format("{0}\nusing {1};", tempPreviewInfo, m_TabData.m_UsingNamespace[i]);
                }
                tempPreviewInfo = string.Format("{0}\n\n", tempPreviewInfo);
            }
            string templinehand = "";

            //namespace
            if (m_TabData.m_Namespace)
            {
                //tempPreviewInfo = string.Format("{0}\n{1}", tempPreviewInfo, "namespace #NAMESPACE#  \n{");
                tempPreviewInfo = string.Format("{0}\n{1}", tempPreviewInfo, "namespace #ROOTNAMESPACE#  \n{");
                templinehand = string.Format("{0}{1}", templinehand, "\u3000\u3000");
            }
            tempPreviewInfo = string.Format("{0}\n{1}{2}", tempPreviewInfo, templinehand, "public class #SCRIPTNAME# : MonoBehaviour");
            tempPreviewInfo = string.Format("{0}\n{1}{2}", tempPreviewInfo, templinehand, "{");

            //module
            if (m_TabData.m_ModuleInfo != null)
            {
                var templinehandtwo = string.Format("{0}{1}", templinehand, "\u3000\u3000");
                for (int i = 0; i < m_TabData.m_ModuleInfo.Count; i++)
                {
                    ModuleInfo tempmodule = m_TabData.m_ModuleInfo[i];

                    if (string.IsNullOrEmpty(tempmodule.Name))
                    {
                        continue;
                    }
                    tempPreviewInfo = string.Format("{0}\n{1}#region {2}", tempPreviewInfo, templinehandtwo, tempmodule.Name);
                    if (tempmodule.m_Methods != null)
                    {
                        for (int j = 0; j < tempmodule.m_Methods.Count; j++)
                        {
                            if (string.IsNullOrEmpty(tempmodule.m_Methods[j]))
                            {
                                continue;
                            }
                            tempPreviewInfo = string.Format("{0}\n{1}private void {2}", tempPreviewInfo, templinehandtwo, tempmodule.m_Methods[j]);
                            tempPreviewInfo = string.Format("{0}\n{1}{2}", tempPreviewInfo, templinehandtwo, "{");
                            tempPreviewInfo = string.Format("{0}\n{1}{2}", tempPreviewInfo, templinehandtwo, "}");
                        }
                    }
                    tempPreviewInfo = string.Format("{0}\n{1}#endregion\n", tempPreviewInfo, templinehandtwo);
                }
            }

            tempPreviewInfo = string.Format("{0}\n{1}{2}", tempPreviewInfo, templinehand, "}");
            if (m_TabData.m_Namespace)
            {
                tempPreviewInfo = string.Format("{0}\n{1}", tempPreviewInfo, "}");
            }

            tempPreviewInfo = string.Format("{0}\n\n", tempPreviewInfo);
            return tempPreviewInfo;
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