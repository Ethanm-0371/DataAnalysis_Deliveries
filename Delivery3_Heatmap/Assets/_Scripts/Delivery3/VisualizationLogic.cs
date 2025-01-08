using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

#if (UNITY_EDITOR)

public class VisualizationLogic : MonoBehaviour
{
    List<Vector3> data;

    public void ShowPositionData()
    {
        StartCoroutine(RetrieveData("positionDownloader"));
    }

    public void ShowAttackData()
    {
        StartCoroutine(RetrieveData("attackDownloader"));
    }

    public void ShowDeathData()
    {
        StartCoroutine(RetrieveData("deathDownloader"));
    }

    public void ShowHitData()
    {
        StartCoroutine(RetrieveData("hitDownloader"));
    }

    public void ShowKillData()
    {
        StartCoroutine(RetrieveData("killDownloader"));
    }

    public IEnumerator RetrieveData(string fileName)
    {
        WWWForm form = new WWWForm();
        //form.AddField("x", position.x.ToString(CultureInfo.InvariantCulture));
        //form.AddField("y", position.y.ToString(CultureInfo.InvariantCulture));
        //form.AddField("z", position.z.ToString(CultureInfo.InvariantCulture));

        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~jonathancl1/" + fileName + ".php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log($"Succcess: {www.downloadHandler.text}");
            //lastPlayerID = uint.Parse(www.downloadHandler.text);
            //callback.Invoke(lastPlayerID);
        }
        yield return null;
    }

}
#endif