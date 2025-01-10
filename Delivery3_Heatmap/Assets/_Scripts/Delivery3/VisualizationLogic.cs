using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

#if (UNITY_EDITOR)

public class VisualizationLogic : MonoBehaviour
{
    List<Vector3> posData;
    List<Vector3> attackData;
    List<Vector3> deathData;
    List<Vector3> hitData;
    List<Vector3> killData;

    public void ShowPositionData()
    {
        StartCoroutine(RetrieveData("positionDownloader", posData));
    }

    public void ShowAttackData()
    {
        StartCoroutine(RetrieveData("attackDownloader", attackData));
    }

    public void ShowDeathData()
    {
        StartCoroutine(RetrieveData("deathDownloader", deathData));
    }

    public void ShowHitData()
    {
        StartCoroutine(RetrieveData("hitDownloader", hitData));
    }

    public void ShowKillData()
    {
        StartCoroutine(RetrieveData("killDownloader", killData));
    }

    public IEnumerator RetrieveData(string fileName, List<Vector3> data)
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

            StoreData(www.downloadHandler.text, data);
        }
        yield return null;
    }

    private void StoreData(string json, List<Vector3> data)
    {
        data.Clear();
        
        string formattedJson = json.TrimStart('[').TrimEnd(']');

        string[] jsonOBJs = formattedJson.Split(new[] { "}," }, StringSplitOptions.None);

        foreach (string jsonOBJ in jsonOBJs)
        {
            try
            {
                string fixedJson = jsonOBJ.Trim();
                if (!fixedJson.EndsWith("}")) fixedJson += "}";

                Vector3 pos = JsonUtility.FromJson<Vector3>(fixedJson);
                data.Add(pos);

                //Debug.Log(pos);
            }
            catch (ArgumentException e)
            {
                Debug.LogError($"Failed: {jsonOBJ}\nException: {e.Message}");
            }
        }
    }
}
#endif