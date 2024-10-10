using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Globalization;

public class BBDDManager : MonoBehaviour
{
    public static BBDDManager Singleton;

    private void Awake()
    {
        Singleton = this;
    }

    public static void UploadPlayer(string name, string country, int age, float gender, DateTime date)
    {
        Singleton.StartCoroutine(Singleton._UploadPlayer(name,country,age,gender,date));
    }

    IEnumerator _UploadPlayer(string name, string country, int age, float gender, DateTime date)
    {
        WWWForm form = new WWWForm();
        form.AddField("playername", name);
        form.AddField("country", country);
        form.AddField("age", age);
        form.AddField("gender", gender.ToString(CultureInfo.InvariantCulture));
        form.AddField("date", date.ToString("yyyy-MM-dd HH:mm:ss"));

        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~ethanmp/DBUploader.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);
        }
    }
}
