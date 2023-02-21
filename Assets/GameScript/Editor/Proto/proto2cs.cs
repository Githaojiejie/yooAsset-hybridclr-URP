using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
 
public class proto2cs
{
 
    [MenuItem("Tools/proto2cs")]
    private static void Run()
    {
        // 执行bat脚本       
        RunMyBat("proto.bat", Application.dataPath.Replace("Assets", "") + @"\Proto\");
    }
    private static void RunMyBat(string batFile, string workingDir)
    {
        var path = FormatPath(workingDir + batFile);
        if (!System.IO.File.Exists(path))
        {
            Debug.LogError("bat文件不存在：" + path);
        }
        else
        {
            System.Diagnostics.Process proc = null;
            try
            {
                proc = new System.Diagnostics.Process();
                proc.StartInfo.WorkingDirectory = workingDir;
                proc.StartInfo.FileName = batFile;
                proc.Start();
                proc.WaitForExit();
                proc.Close();
            }
            catch (System.Exception ex)
            {
                Debug.LogFormat("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }
        }
    }
    static string FormatPath(string path)
    {
        path = path.Replace("/", "\\");
        if (Application.platform == RuntimePlatform.OSXEditor)
            path = path.Replace("\\", "/");
        return path;
    }
}