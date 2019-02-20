
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Framework.Editor.Tools.Language
{
    public class ExcelWindows : WindowBaseEditor
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

            Tools_Public.EnumButton<ExcelType>(ref m_type, position.width);

            Tools_Public.CenterLabel("---->" + m_type.ToString() + "<----");

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
                default:
                    break;
            }
            EditorGUILayout.EndVertical();
        }

        private enum ExcelType
        {
            ImportExcel,
            ExportExcel,
        }
    }
}