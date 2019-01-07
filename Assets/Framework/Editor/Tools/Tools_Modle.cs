
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using UnityEngine.Events;

namespace Framework.Editor.Tools
{
    public class Tools_Model : EditorWindow, IHasCustomMenu, ISerializationCallbackReceiver
    {
        private static Tools_Model s_instance = null;
        internal static Tools_Model instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = GetWindow<Tools_Model>();
                return s_instance;
            }
        }



        [MenuItem("Tools/Create Model", priority = 1)]
        static void CreatorModel()
        {
            s_instance = null;
            m_ModelInfo.Init();
            //m_Path = Application.dataPath;
            //ModelName = null;
            //ModelTips = null;
            instance.titleContent = new GUIContent("Create Model");
            //instance.minSize = new Vector2(800, 300);
            instance.Show();
        }




        public void AddItemsToMenu(GenericMenu menu)
        {
        }

        public void OnAfterDeserialize()
        {
        }

        public void OnBeforeSerialize()
        {
        }


        private void OnGUI()
        {
            if (m_ModelInfo == null)
            {
                m_ModelInfo = new ModelDate();
                m_ModelInfo.Init();
            }
            CreateModelUI();
        }
        internal static ModelDate m_ModelInfo = new ModelDate();
        private Vector2 m_ScrollPosition;
        private void CreateModelUI()
        {
            m_ScrollPosition =  EditorGUILayout.BeginScrollView(m_ScrollPosition);
            GUILayout.BeginVertical();
            EditorGUILayout.Space();
            m_ModelInfo.ModelPath = EditorGUILayout.TextField("Folder Path", m_ModelInfo.ModelPath);  //ShowTextField("Folder Path", m_Path);


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
                        m_ModelInfo.ModelPath = newPath;
                    }
                    else
                    {

                    }
                }
            }
            if (GUILayout.Button("UpDate", GUILayout.MaxWidth(Tools_Const.Tools_MinButtonWidth)))
            {
                m_ModelInfo.Init();
            }
            GUILayout.EndHorizontal();
            EditorGUILayout.Space();
            m_ModelInfo.ModelName = EditorGUILayout.TextField("Model Name", m_ModelInfo.ModelName);
            EditorGUILayout.Space();
            m_ModelInfo.ModelTips = EditorGUILayout.TextField("Model Tips", m_ModelInfo.ModelTips);
            EditorGUILayout.Space();
            CenterButton("Create Model",()=> {
                CreateFolder(m_ModelInfo.ModelPath, m_ModelInfo.ModelName, m_ModelInfo.ModelTips);
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
                CreateFolder(m_ModelInfo.ModelPath, m_ModelInfo.ModelName, m_ModelInfo.ModelTips);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }


        private void CreateFolder(string varPath,string varName,string varTips)
        {

            varName = varName.Replace(" ","");
            if (string.IsNullOrEmpty(varPath) || string.IsNullOrEmpty(varName) || string.IsNullOrEmpty(varTips))
                return;
            string tempPath = varPath + "/" + varName;

            CreateFolderAndTxt(varPath, varName, varTips);

            CreateFolderAndTxt(tempPath, "Animations", "这个文件夹用来放动画文件\n自己做的动画");
            CreateFolderAndTxt(tempPath, "Sprites", "这个文件夹用来放UI图片");
            CreateFolderAndTxt(tempPath, "Materials", "这个文件夹用来放材质球");
            CreateFolderAndTxt(tempPath, "Shader", "这个文件夹用来放Shader文件");
            CreateFolderAndTxt(tempPath, "Texture", "这个文件夹用来放贴图");

            CreateFolderAndTxt(tempPath, "Models", "这个文件夹用来放模型");
            //CreateFolderAndTxt(tempPath, "Models/Texture", "这个文件夹用来放模型贴图");
            //CreateFolderAndTxt(tempPath, "Models/Animation", "这个文件夹用来放模型动画");

            CreateFolderAndTxt(tempPath, "Prefabs", "这个文件夹用来放不需要放在Resources文件加下的预制");
            
            CreateFolderAndTxt(tempPath, "Resources/" + varName + "/Prefabs", null);
            CreateFolderAndTxt(tempPath, "Resources/" + varName + "/UI/Prefabs", null);

            CreateFolderAndTxt(tempPath, "Scripts", "这个文件夹用来放脚本按功能划分文件夹\n脚本统一按模块名称进行设置命名空间");
            CreateFolderAndTxt(tempPath, "Scripts/UI", "这个文件夹用来放模块中的UI脚本");

            CreateFolderAndTxt(tempPath, "Scenes", "这个文件夹用来放场景");
            
            //刷新资源
            AssetDatabase.Refresh();
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


        public class ModelDate
        {
            public string ModelPath;
            public string ModelName;
            public string ModelTips;
            public ModelDate()
            {
                //Init();
            }

            public void Init()
            {
                ModelPath = Application.dataPath;
                ModelName = null;
                ModelTips = null;
            }
        }
    }
}