using UnityEditor;
using UnityEngine;
using System.Collections;

namespace Framework.Editor.Tools.AutoBuilder
{
    public class AutoBuilder_Ios : BaseBuilder
    {
        internal void Ios()
        {
            Debug.Log("�����Խ׶�");
           // SwitchApplication(BuildTarget.iOS);
        }
        internal void MacOSX()
        {
            SwitchApplication(BuildTarget.StandaloneOSX);
        }



        protected override void SetBuildSetting(BuildTarget varTarget)
        {
            string filename = "";
            int index = (int)BuildOptions.None;

            //if (varTarget == BuildTarget.StandaloneOSX)
            //{
            //}
            //else if (varTarget == BuildTarget.iOS)
            //{
                filename = OutBackPath + "/"+ Application.productName;
                index = (int)BuildOptions.AcceptExternalModificationsToPlayer;
            //index += (int)BuildOptions.ShowBuiltPlayer;
            //index += (int)BuildOptions.SymlinkLibraries;
            //}
            BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, filename, EditorUserBuildSettings.activeBuildTarget, (BuildOptions)index);
            //Building(filename, (BuildOptions)index);
        }
    }
}