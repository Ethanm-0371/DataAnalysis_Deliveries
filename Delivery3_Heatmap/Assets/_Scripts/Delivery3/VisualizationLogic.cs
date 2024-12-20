
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

#if (UNITY_EDITOR)

public class VisualizationLogic : MonoBehaviour
{
    public void ShowPositionData()
    {
        StartCoroutine(RetrievePositionData());
    }

    public IEnumerator RetrievePositionData()
    {
        WWWForm form = new WWWForm();
        //form.AddField("x", position.x.ToString(CultureInfo.InvariantCulture));
        //form.AddField("y", position.y.ToString(CultureInfo.InvariantCulture));
        //form.AddField("z", position.z.ToString(CultureInfo.InvariantCulture));


        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~jonathancl1/positionDownloader.php", form);
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
