using System;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Collections;

// Look at https://forum.unity.com/threads/how-to-screenshot-in-a-new-tab-of-the-browser-without-pop-up-blocking.440051/
// look at https://www.youtube.com/watch?v=4OZqY1Ukj8I

public static class SaveResults
{
    public static IEnumerator SaveScreenCapture(string filename)
    {
        Debug.Log($"Starts ScreenShot()");
        // Debug.Log($"Data Path: {Application.dataPath}");
        // Debug.Log($"Persistant Data Path: {Application.dataPath}");
        
        // Disabled screen capture for now
        // ScreenCapture.CaptureScreenshot($"{Application.persistentDataPath}/{filename}", 4);
        
        // The following Disabled code is part of an unfinished solution to the saving bug.
        /*
        int width = Screen.width, height = Screen.height;
        var texture = new Texture2D(width, height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0,0, width, height), 0, 0);
        texture.Apply();
        */

        Debug.LogWarning($"Save functionality has been disabled. This will be improved later.");

        // yield return SaveResultsToDesktop($"{Application.persistentDataPath}/{filename}", filename);
        yield return new WaitForEndOfFrame();
        Debug.Log($"Ends ScreenShot()");
    }

    static IEnumerator SaveResultsToDesktop(string path, string filename)
    {
        Debug.Log($"Starts Coroutine SaveResultsToDesktop(path)");
        using (UnityWebRequest request = UnityWebRequest.Get(path))
        {
            Debug.Log($"Inside Using Statement");
            yield return request.SendWebRequest(); // If this fails, throw exception
            bool success = false;
            if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                throw new Exception($"Network Exception - {request.error}");
            else
                success = WriteFile(request, path, filename);
            if(!success)
                Debug.LogError($"File was unable to be saved");
            Debug.Log($"Exit Using Statement");
        }
        Debug.Log($"Ends Coroutine SaveResultsToDesktop(path)");
    }

    static bool WriteFile(UnityWebRequest request, string path, string filename)
    {
        Debug.Log($"Starts WriteFile(request, path)");
        string userFilePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/{filename}";
        var data = request.downloadHandler.data;
        bool success = false;
        if(request.result == UnityWebRequest.Result.Success)
        {
            Stream stream = File.Open(userFilePath, FileMode.Create);
            stream.Write(data, 0, data.Length);
            stream.Close();
            success = true;
        }
        Debug.Log($"Ends WriteFile(request, path)");
        return success;
    }
}
