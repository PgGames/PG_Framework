
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using UnityEngine.Events;

namespace Framework.Editor.Tools
{
    public class Tools_Module : EditorWindow
    {
        private static Tools_Module s_instance = null;
        internal static Tools_Module instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = GetWindow<Tools_Module>();
                return s_instance;
            }
        }



        [MenuItem("Tools/Create Module", priority = 8)]
        static void CreatorModule()
        {
            s_instance = null;
            m_ModuleInfo.Init();
            instance.titleContent = new GUIContent("Create Module");
            instance.Show();
        }
        


        private void OnGUI()
        {
            if (m_ModuleInfo == null)
            {
                m_ModuleInfo = new ModuleDate();
                m_ModuleInfo.Init();
            }
            CreateModuleUI();
        }
        internal static ModuleDate m_ModuleInfo = new ModuleDate();
        private Vector2 m_ScrollPosition;
        private void CreateModuleUI()
        {
            if (string.IsNullOrEmpty(m_ModuleInfo.ModulePath))
            {
                m_ModuleInfo.Init();
            }
            m_ScrollPosition =  EditorGUILayout.BeginScrollView(m_ScrollPosition);
            GUILayout.BeginVertical();
            EditorGUILayout.Space();
            m_ModuleInfo.ModulePath = EditorGUILayout.TextField("Folder Path", m_ModuleInfo.ModulePath);  //ShowTextField("Folder Path", m_Path);


            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("OpenFile", GUILayout.MaxWidth(Tools_Const.Tools_MinButtonWidth)))
            {
                var newPath = EditorUtility.OpenFolderPanel("OpenFile", Application.dataPath, "");
                if (!string.IsNullOrEmpty(newPath))
                {
                    newPath = newPath.Replace("\\", "/");
                    var temppath = Application.dataPath.Replace("\\", "/");
                    if (newPath.Contains(temppath))
                    {
                        m_ModuleInfo.ModulePath = newPath;
                    }
                    else
                    {

                    }
                }
            }
            if (GUILayout.Button("UpDate", GUILayout.MaxWidth(Tools_Const.Tools_MinButtonWidth)))
            {
                m_ModuleInfo.Init();
            }
            GUILayout.EndHorizontal();
            EditorGUILayout.Space();
            m_ModuleInfo.ModuleName = EditorGUILayout.TextField("Module Name", m_ModuleInfo.ModuleName);
            EditorGUILayout.Space();
            m_ModuleInfo.ModuleTips = EditorGUILayout.TextField("Module Tips", m_ModuleInfo.ModuleTips);
            EditorGUILayout.Space();
            CenterButton("Create Module", ()=> {
                CreateFolder(m_ModuleInfo.ModulePath, m_ModuleInfo.ModuleName, m_ModuleInfo.ModuleTips);
            }, GUILayout.MaxWidth(Tools_Const.Tools_MaxButtonWidth));
            GUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }

        private void CenterButton(string text,UnityAction action,params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(text, options))
            {
                action.Invoke();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }


        private void CreateFolder(string varPath,string varName,string varTips)
        {
            string message = null;
            try
            {
                if (string.IsNullOrEmpty(varPath))
                {
                    message = "Create Module Fail, Folder Path Can't Null";
                    return;
                }
                if (string.IsNullOrEmpty(varName))
                {
                    message = "Create Module Fail, Module Name Can't Null";
                    return;
                }
                else
                {
                    varName = varName.Replace(" ", "");
                }
                if (string.IsNullOrEmpty(varTips))
                {
                    message = "Create Module Fail, Module Tips Can't Null";
                    return;
                }
                string tempPath = varPath + "/" + varName;

                CreateFolderAndTxt(varPath, varName, varTips);

                CreateFolderAndTxt(tempPath, "Animations", "这个文件夹用来放动画文件\n自己做的动画");
                CreateFolderAndTxt(tempPath, "Sprites", "这个文件夹用来放UI图片");
                CreateFolderAndTxt(tempPath, "Materials", "这个文件夹用来放材质球");
                CreateFolderAndTxt(tempPath, "Shader", "这个文件夹用来放Shader文件");
                CreateFolderAndTxt(tempPath, "Texture", "这个文件夹用来放贴图");

                CreateFolderAndTxt(tempPath, "AudioClip", "这个文件夹用来放音频文件");
                CreateFolderAndTxt(tempPath, "VideoPlayer", "这个文件夹用来放视频文件");

                CreateFolderAndTxt(tempPath, "Models", "这个文件夹用来放模型");
                //CreateFolderAndTxt(tempPath, "Model/Texture", "这个文件夹用来放模型贴图");
                //CreateFolderAndTxt(tempPath, "Model/Animation", "这个文件夹用来放模型动画");

                CreateFolderAndTxt(tempPath, "Prefabs", "这个文件夹用来放不需要放在Resources文件加下的预制");

                CreateFolderAndTxt(tempPath, "Resources/" + varName + "/Prefabs", null);
                CreateFolderAndTxt(tempPath, "Resources/" + varName + "/UI/Prefabs", null);

                CreateFolderAndTxt(tempPath, "Scripts", "这个文件夹用来放脚本按功能划分文件夹\n脚本统一按模块名称进行设置命名空间");
                CreateFolderAndTxt(tempPath, "Scripts/UI", "这个文件夹用来放模块中的UI脚本");

                CreateFolderAndTxt(tempPath, "Scenes", "这个文件夹用来放场景");

                //刷新资源
                AssetDatabase.Refresh();
                message = "Create " + varName + " Module Success";
            }
            catch
            { }
            finally
            {
                EditorUtility.DisplayDialog("Create Module", message, "Clsoe");
            }
        }
        private void CreateFolderAndTxt(string varPath,string varName,string varContent)
        {
            if (string.IsNullOrEmpty(varPath) || string.IsNullOrEmpty(varName))
                return;
            Directory.CreateDirectory(varPath+"/"+varName);
            if (!string.IsNullOrEmpty(varContent))
            {
                var tempstream = File.CreateText(varPath + "/" + varName + "/explain.txt");
                tempstream.Write(varContent);
                tempstream.Close();
            }
        }


        public class ModuleDate
        {
            public string ModulePath;
            public string ModuleName;
            public string ModuleTips;
            public ModuleDate()
            {
                //Init();
            }

            public void Init()
            {
                ModulePath = Application.dataPath+ "/Project Module";
                ModuleName = null;
                ModuleTips = null;
            }
        }
    }
}