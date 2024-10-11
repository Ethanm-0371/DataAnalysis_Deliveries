using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Globalization;

public class BBDDManager : MonoBehaviour
{
    public static BBDDManager Singleton;

    public uint lastPlayerID;
    public uint lastSessionID;

    private void Awake()
    {
        Singleton = this;
    }

    public static void UploadPlayer(string name, string country, int age, float gender, DateTime date, Action<uint> callback)
    {
        Singleton.StartCoroutine(Singleton._UploadPlayer(name,country,age,gender,date, callback));
    }
    IEnumerator _UploadPlayer(string name, string country, int age, float gender, DateTime date, Action<uint> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("playername", name);
        form.AddField("country", country);
        form.AddField("age", age);
        form.AddField("gender", gender.ToString(CultureInfo.InvariantCulture));
        form.AddField("date", date.ToString("yyyy-MM-dd HH:mm:ss"));

        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~ethanmp/playerUploader.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log("Form upload complete!");
            //Debug.Log(www.downloadHandler.text);

            lastPlayerID = uint.Parse(www.downloadHandler.text);
            callback.Invoke(lastPlayerID);
        }
    }

    public static void UploadNewSession(uint playerID, DateTime time, Action<uint> callback)
    {
        Singleton.StartCoroutine(Singleton._UploadNewSession(playerID, time, callback));
    }
    IEnumerator _UploadNewSession(uint playerID, DateTime time, Action<uint> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerID", (int)playerID);
        form.AddField("loginDate", time.ToString("yyyy-MM-dd HH:mm:ss"));

        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~ethanmp/newSessionUploader.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log("Form upload complete!");
            //Debug.Log(www.downloadHandler.text);

            lastSessionID = uint.Parse(www.downloadHandler.text);
            callback.Invoke(lastSessionID);
        }
    }

    public static void UploadEndSession(uint sessionID, DateTime time, Action<uint> callback)
    {
        Singleton.StartCoroutine(Singleton._UploadEndSession(sessionID, time, callback));
    }
    IEnumerator _UploadEndSession(uint sessionID, DateTime time, Action<uint> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("sessionID", (int)sessionID);
        form.AddField("logoutDate", time.ToString("yyyy-MM-dd HH:mm:ss"));

        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~ethanmp/endSessionUploader.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log("Form upload complete!");
            //Debug.Log(www.downloadHandler.text);

            callback.Invoke(lastPlayerID);
        }
    }
}
