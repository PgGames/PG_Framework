
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Framework.Editor.Tools.Language
{
    public class ExcelWindows : EditorWindow
    {

        [MenuItem("Tools/Language", priority = 2000)]
        static void OpenWindows()
        {
            s_instance = null;
            instance.titleContent = new GUIContent("Language Excel");
            instance.Show();
        }

        //[MenuItem("Tools/Language/Import Excel", priority = 2000)]
        //static void ImportExcels()
        //{
        //}
        //[MenuItem("Tools/Language/Export Excel", priority = 2000)]
        //static void ExportExcels()
        //{
        //}


        /// <summary>
        /// 读取数据
        /// </summary>
        void ReadDate()
        {
            //读取数据
            var dataPath = System.IO.Path.GetFullPath(".");
            dataPath = dataPath.Replace("\\", "/");
            dataPath += "/Library/Tools/LanguageDate.dat";
            if (File.Exists(dataPath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(dataPath, FileMode.Open);
                var data = bf.Deserialize(file) as LanguageDate;
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
        /// <summary>
        /// 存储数据
        /// </summary>
        void SaveDate()
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
        /// <summary>
        /// 初始化数据
        /// </summary>
        void InitDate()
        {
            m_TabData.excelpath = "Ces";
        }

        private static ExcelWindows s_instance = null;

        internal static ExcelWindows instance {
            get {
                if (s_instance == null)
                {
                    s_instance = GetWindow<ExcelWindows>();
                }
                return s_instance;
            }
        }
        internal ExcelWindows()
        {
            if (m_Export == null)
                m_Export = new ExportExcel();
            if (m_Import == null)
                m_Import = new ImportExcel();
        }

        private ImportExcel m_Import;
        private ExportExcel m_Export;

        private ExcelType m_type = ExcelType.ImportExcel;
        private LanguageDate m_TabData;



        private void OnEnable()
        {

            if (m_Import != null)
            {
                m_Import.OnEnable(this);
            }
            if (m_Export != null)
            {
                m_Export.OnEnable(this);
            }
        }
        private void OnDisable()
        {
            if (m_Import != null)
            {
                m_Import.OnDisable();
            }
            if (m_Export != null)
            {
                m_Export.OnDisable();
            }
        }


        const float k_ToolbarPadding = 15f;
        const float k_ToolButtonspacing = 5f;
        private Vector2 m_ScrollPosition;
        private void OnGUI()
        {
            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);
            EditorGUILayout.BeginVertical();
            HandToggleUI();
            EditorGUILayout.Space();
            CententUI();


            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }


        private void HandToggleUI()
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();

            GUILayout.Space(k_ToolbarPadding);
            float toolbarWidth = (position.width - k_ToolButtonspacing * ((int)ExcelType.Count) - k_ToolbarPadding * 2) / ((int)ExcelType.Count);
            for (int i = 0; i < (int)ExcelType.Count; i++)
            {
                string tempName = ((ExcelType)i).ToString();
                if (GUILayout.Button(tempName, GUILayout.Width(toolbarWidth)))
                {
                    m_type = (ExcelType)i;
                }
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("----> " + m_type.ToString() + " <----");
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }

        private void CententUI()
        {
            EditorGUILayout.BeginVertical();
            switch (m_type)
            {
                case ExcelType.ImportExcel:
                    if (m_Import != null)
                        m_Import.OnGUI();
                    break;
                case ExcelType.ExportExcel:
                    if (m_Export != null)
                        m_Export.OnGUI();
                    break;
                case ExcelType.Count:
                default:
                    break;
            }
            EditorGUILayout.EndVertical();
        }

        private enum ExcelType
        {
            ImportExcel,
            ExportExcel,
            Count
        }
        internal class LanguageDate
        {
            internal string excelpath;
        }
    }
}