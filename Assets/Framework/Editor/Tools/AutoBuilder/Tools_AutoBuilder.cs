
using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Framework.Editor.Tools.AutoBuilder
{
    public class Tools_AutoBuilder : EditorWindow
    {
        private static Tools_AutoBuilder s_instance = null;
        internal static Tools_AutoBuilder instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = GetWindow<Tools_AutoBuilder>();
                return s_instance;
            }
        }
        [MenuItem("Tools/Auto Builder/Windows/32", priority = 2001)]
        internal static void Official_Windows32()
        {
        }
        [MenuItem("Tools/Auto Builder/Windows/64", priority = 2002)]
        internal static void Official_Windows64()
        {
        }
        [MenuItem("Tools/Auto Builder/Android/Apk", priority = 2100)]
        internal static void Official_Android_Apk()
        {
        }
        [MenuItem("Tools/Auto Builder/Android/Grable", priority = 2101)]
        internal static void Official_Android_Grable()
        {
        }
        [MenuItem("Tools/Auto Builder/Web", priority = 2004)]
        internal static void Official_Web()
        {
        }
        [MenuItem("Tools/Auto Builder/iOS", priority = 2005)]
        internal static void Official_IOS()
        {
        }
        [MenuItem("Tools/Auto Builder Buduger/Windows/32", priority = 2006)]
        internal static void Demo_Windows32()
        {
        }
        [MenuItem("Tools/Auto Builder Buduger/Windows/64", priority = 2007)]
        internal static void Demo_Windows64()
        {
        }
        [MenuItem("Tools/Auto Builder Buduger/Android/Apk", priority = 2100)]
        internal static void Demo_Android_Apk()
        {
        }
        [MenuItem("Tools/Auto Builder Buduger/Android/Grable", priority = 2101)]
        internal static void Demo_Android_Grable()
        {
        }
        [MenuItem("Tools/Auto Builder Buduger/Web", priority = 2009)]
        internal static void Demo_Web()
        {
        }
        [MenuItem("Tools/Auto Builder Buduger/iOS", priority = 2010)]
        internal static void Demo_IOS()
        {
        }
    }
}