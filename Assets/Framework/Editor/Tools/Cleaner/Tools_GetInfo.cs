
using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Framework.Editor.Tools.Cleaner
{
    public class Tools_GetInfo : MonoBehaviour
    {
        internal bool _IgnoreScripts = false;

        internal void GetAllFile()
        {
            //get project all file ,ignore（.meta .dll .js）file
            var files = Directory.GetFiles("Assets", "*.*", SearchOption.AllDirectories)
                .Where(item => Path.GetExtension(item) != ".meta")
                .Where(item => Path.GetExtension(item) != ".js")
                .Where(item => Path.GetExtension(item) != ".dll")
                .Where(item => Regex.IsMatch(item, "[\\/\\\\]Gizmos[\\/\\\\]") == false)
                .Where(item => Regex.IsMatch(item, "[\\/\\\\]Plugins[\\/\\\\]Android[\\/\\\\]") == false)
                .Where(item => Regex.IsMatch(item, "[\\/\\\\]Plugins[\\/\\\\]iOS[\\/\\\\]") == false)
                .Where(item => Regex.IsMatch(item, "[\\/\\\\]Resources[\\/\\\\]") == false);


            if (_IgnoreScripts)
            {
                files = files.Where(item => Path.GetExtension(item) != ".cs");
            }


            List<string> templist = new List<string>(files);

            for (int i = 0; i < templist.Count; i++)
            {
                Debug.LogError(templist[i]);
            }
        }
        internal void GetAllTexture()
        {
            List<string> templist = new List<string>();
            var filespng = GetFile("*.png");
            templist.AddRange(filespng);
            var filesjpg = GetFile("*.jpg"); 
            templist.AddRange(filesjpg);


            for (int i = 0; i < templist.Count; i++)
            {
                Debug.LogError(templist[i]);
            }
        }

        /// <summary>
        /// find file path
        /// </summary>
        /// <param name="searchPattern">find file suffix</param>
        /// <param name="Wherefile">ignore file suffix</param>
        /// <returns></returns>
        private IEnumerable<string> GetFile(string searchPattern,params string[] Wherefile)
        {
            var files = Directory.GetFiles("Assets", searchPattern, SearchOption.AllDirectories)
                .Where(item => Regex.IsMatch(item, "[\\/\\\\]Gizmos[\\/\\\\]") == false)
                .Where(item => Regex.IsMatch(item, "[\\/\\\\]Plugins[\\/\\\\]Android[\\/\\\\]") == false)
                .Where(item => Regex.IsMatch(item, "[\\/\\\\]Plugins[\\/\\\\]iOS[\\/\\\\]") == false)
                .Where(item => Regex.IsMatch(item, "[\\/\\\\]Resources[\\/\\\\]") == false)
                .Where(item => Regex.IsMatch(item, "[\\/\\\\]Editor[\\/\\\\]") == false);
            if (Wherefile != null)
            {
                for (int i = 0; i < Wherefile.Length; i++)
                {
                    files = files.Where(item =>Path.GetExtension(item)== Wherefile[i]);
                }
            }
            return files;
        }
    }
}