﻿using UnityEditor;
using UnityEngine;

using System.Collections;
using System.IO;

namespace Framework.Editor.Tools
{
    public class Tools_Path 
    {
        [MenuItem("Tools/Open Folder Path/Data Path", priority = 1)]
        static void OpenDataPath()
        {
            Application.OpenURL(Application.dataPath);
            //Application.persistentDataPath
        }
        [MenuItem("Tools/Open Folder Path/Persistent Data Path", priority = 0)]
        static void OpenPersistentDataPath()
        {
            Application.OpenURL(Application.persistentDataPath);
        }
        [MenuItem("Tools/Open Folder Path/Streaming Assets Path", priority = 2)]
        static void OpenStreamingAssetsPath()
        {
            Application.OpenURL(Application.streamingAssetsPath);
        }
        [MenuItem("Tools/Open Folder Path/Temporary Cache Path", priority = 3)]
        static void OpenTemporaryCachePath()
        {
            Application.OpenURL(Application.temporaryCachePath);
        }
        [MenuItem("Tools/Open Folder Path/Unity Path", priority = 4)]
        static void OpenUnityPath()
        {
            FileInfo varInfo = new FileInfo(EditorApplication.applicationPath);
            if (varInfo.Exists)
            {
                Application.OpenURL(varInfo.DirectoryName);
            }
        }
        [MenuItem("Tools/Open Folder Path/Unity Script Templates Path", priority = 5)]
        static void OpenUnityScriptTemplatesPath()
        {
            var path = EditorApplication.applicationContentsPath + "/Resources/ScriptTemplates";
            if (Directory.Exists(path))
            {
                Application.OpenURL(path);
            }
        }
    }
}