using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Framework.Editor.Tools.Cleaner
{
    public class Tools_Cleaner : MonoBehaviour
    {

        static Tools_GetInfo m_Info;
        [MenuItem("Tools/Cleaner/only resources",priority = 2000)]
        internal static void onlyresources()
        {
            if (m_Info == null)
                m_Info = new Tools_GetInfo();
            m_Info._IgnoreScripts = true;
            m_Info.GetAllFile();
        }
        [MenuItem("Tools/Cleaner/only resources texture", priority = 2000)]
        internal static void onlyresourcestexture()
        {
            if (m_Info == null)
                m_Info = new Tools_GetInfo();
            m_Info._IgnoreScripts = true;
            m_Info.GetAllTexture();
        }
    }
}