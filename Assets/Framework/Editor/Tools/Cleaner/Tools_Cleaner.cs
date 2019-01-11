using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Framework.Editor.Tools.Cleaner
{
    public class Tools_Cleaner : EditorWindow
    {
        AssetCollector collection = new AssetCollector();
        List<DeleteAsset> deleteAssets = new List<DeleteAsset>();       //��Ч����Դ·����Ϣ
        Vector2 scroll;

        [MenuItem("Tools/Delete Unused Assets/only resource", priority = 1000)]
        static void InitWithoutCode()
        {
            var window = Tools_Cleaner.CreateInstance<Tools_Cleaner>();
            window.collection._IgnoreScripts = false;
            window.collection.saveEditorExtensions = true;
            window.collection.GetAllFile();
            window.CopyDeleteFileList(window.collection.deleteFileList);

            window.Show();
        }

        [MenuItem("Tools/Delete Unused Assets/unused by editor", priority = 1001)]
        static void InitWithout()
        {
            var window = Tools_Cleaner.CreateInstance<Tools_Cleaner>();
            window.collection.saveEditorExtensions = false;
            window.collection._IgnoreScripts = false;
            window.collection.GetAllFile();
            window.CopyDeleteFileList(window.collection.deleteFileList);

            window.Show();
        }

        [MenuItem("Tools/Delete Unused Assets/unused by game", priority = 1002)]
        static void Init()
        {
            var window = Tools_Cleaner.CreateInstance<Tools_Cleaner>();
            window.collection.saveEditorExtensions = false;
            window.collection._IgnoreScripts = true;
            window.collection.GetAllFile();
            window.CopyDeleteFileList(window.collection.deleteFileList);

            window.Show();
        }


        void CopyDeleteFileList(IEnumerable<string> deleteFileList)
        {
            foreach (var asset in deleteFileList)
            {
                var filePath = AssetDatabase.GUIDToAssetPath(asset);
                if (!string.IsNullOrEmpty(filePath))
                {
                    deleteAssets.Add(new DeleteAsset() { path = filePath });
                }
            }
        }
        void CleanDir()
        {
            RemoveEmptyDirectry("Assets");
            AssetDatabase.Refresh();
        }

        void OnGUI()
        {
            using (var horizonal = new EditorGUILayout.HorizontalScope("box"))
            {
                EditorGUILayout.LabelField("delete unreference assets from buildsettings and resources");

                if (GUILayout.Button("Delete", GUILayout.Width(120), GUILayout.Height(40)) && deleteAssets.Count != 0)
                {
                    RemoveFiles();
                    Close();
                }
            }

            using (var scrollScope = new EditorGUILayout.ScrollViewScope(scroll))
            {
                scroll = scrollScope.scrollPosition;
                foreach (var asset in deleteAssets)
                {
                    if (string.IsNullOrEmpty(asset.path))
                    {
                        continue;
                    }

                    using (var horizonal = new EditorGUILayout.HorizontalScope())
                    {
                        asset.isDelete = EditorGUILayout.Toggle(asset.isDelete, GUILayout.Width(20));
                        var icon = AssetDatabase.GetCachedIcon(asset.path);
                        GUILayout.Label(icon, GUILayout.Width(20), GUILayout.Height(20));
                        if (GUILayout.Button(asset.path, EditorStyles.largeLabel))
                        {
                            Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(asset.path);
                        }
                    }
                }
            }

        }




        void RemoveFiles()
        {
            try
            {
                string exportDirectry = "BackupUnusedAssets";
                Directory.CreateDirectory(exportDirectry);
                //��ȡ�����ļ���Ҫɾ�����ļ�
                var files = deleteAssets.Where(item => item.isDelete == true).Select(item => item.path).ToArray();

                string backupPackageName = exportDirectry + "/package" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".unitypackage";
                //�򿪽�����
                EditorUtility.DisplayProgressBar("export package", backupPackageName, 0);
                //��Ҫɾ�����ļ������package�����һ�
                AssetDatabase.ExportPackage(files, backupPackageName);

                int i = 0;
                int length = deleteAssets.Count;

                foreach (var assetPath in files)
                {
                    i++;
                    //���ý�������Ϣ
                    EditorUtility.DisplayProgressBar("delete unused assets", assetPath, (float)i / length);
                    //ɾ���ļ�
                    AssetDatabase.DeleteAsset(assetPath);
                }

                EditorUtility.DisplayProgressBar("clean directory", "", 1);
                foreach (var dir in Directory.GetDirectories("Assets"))
                {
                    RemoveEmptyDirectry(dir);
                }

                System.Diagnostics.Process.Start(exportDirectry);
                //ˢ����Դ
                AssetDatabase.Refresh();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
            finally
            {
                //�رս�����
                EditorUtility.ClearProgressBar();
            }
        }
        /// <summary>
        /// ���·���µĿ��ļ���
        /// </summary>
        /// <param name="path"></param>
        void RemoveEmptyDirectry(string path)
        {
            var dirs = Directory.GetDirectories(path);
            foreach (var dir in dirs)
            {
                RemoveEmptyDirectry(dir);
            }

            var files = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly).Where(item => Path.GetExtension(item) != ".meta");
            if (files.Count() == 0 && Directory.GetDirectories(path).Count() == 0)
            {
                var metaFile = AssetDatabase.GetTextMetaFilePathFromAssetPath(path);
                UnityEditor.FileUtil.DeleteFileOrDirectory(path);
                UnityEditor.FileUtil.DeleteFileOrDirectory(metaFile);
            }
        }




        class DeleteAsset
        {
            public bool isDelete = true;
            public string path;
        }
    }
}