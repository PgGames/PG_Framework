
using UnityEngine;
using System.Collections;
using UnityEditor;

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
        //[MenuItem("Tools/File Template", priority = 0)]
        //internal static void FileTemplate()
        //{
        //}
        [MenuItem("Tools/File Template/Replace File Template", priority = 0)]
        internal static void ReplaceFileTemplate()
        {
        }
        [MenuItem("Tools/File Template/Setting File Template", priority = 1)]
        internal static void SettingFileTemplate()
        {
            s_instance = null;
            instance.titleContent = new GUIContent("Setting File Template");
            instance.Show();
        }
    }
}