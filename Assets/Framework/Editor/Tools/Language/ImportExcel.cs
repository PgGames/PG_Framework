using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Framework.Editor.Tools.Language
{
    public class ImportExcel : WindowBase
    {
        internal LanguageDate m_date = new LanguageDate();
        private string filepath = "Excels/Imports";

        internal override void OnEnable(EditorWindow editor)
        {
            m_date = ReadDate<LanguageDate>(filepath);
            if (m_date == null)
            {
                m_date = new LanguageDate();
                m_date.languagepath = "";
                m_date.excelpath = "";
            }
            if (m_date.languagepath == null)
            {
                m_date.languagepath = "";
            }
            if (m_date.excelpath == null)
            {
                m_date.excelpath = "";
            }
        }
        internal override void OnDisable()
        {
            SaveDate(filepath, m_date);
        }



        internal override void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            //Excel 路径
            using (new EditorGUI.DisabledScope())
            {
                m_date.excelpath = EditorGUILayout.TextField("Excel Path", m_date.excelpath);
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("OpenFile", GUILayout.MaxWidth(Tools_Const.Tools_MinButtonWidth)))
                {
                    var newPath = EditorUtility.OpenFilePanel("OpenFile", Application.dataPath, "xlsx,xls");
                    if (!string.IsNullOrEmpty(newPath))
                    {
                        newPath = newPath.Replace("\\", "/");
                        m_date.excelpath = newPath;
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            //Language 路径
            using (new EditorGUI.DisabledScope())
            {
                m_date.languagepath = EditorGUILayout.TextField("Language Path", m_date.languagepath);
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("OpenFile", GUILayout.MaxWidth(Tools_Const.Tools_MinButtonWidth)))
                {
                    var newPath = EditorUtility.OpenFolderPanel("OpenFile", Application.dataPath + "/" + m_date.languagepath, "");
                    if (!string.IsNullOrEmpty(newPath))
                    {
                        newPath = newPath.Replace("\\", "/");
                        var temppath = Application.dataPath.Replace("\\", "/");
                        newPath = newPath.Replace(temppath + "/", "");
                        m_date.languagepath = newPath;
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            //导入 import
            using (new EditorGUI.DisabledScope())
            {
                if (CenterButton("Import Excel", GUILayout.Width(_CenterButton_Width)))
                {

                }
            }
            EditorGUILayout.EndVertical();
        }

        [System.Serializable]
        internal class LanguageDate :BaseDate
        {
            internal string excelpath;
            internal string languagepath;
        }
    }
}