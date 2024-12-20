using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

public class DataCollector : MonoBehaviour
{
    [SerializeField] float sendFrequency = 3.0f;
    [SerializeField] GameObject player;

    private void Awake()
    {
        StartCoroutine(SendPlayerPos());
    }

    IEnumerator SendPlayerPos()
    {
        while(true)
        {
            yield return new WaitForSeconds(sendFrequency);

            Debug.Log(player.transform.position);
            StartCoroutine(UploadPos(player.transform.position));
        }
    }


    IEnumerator UploadPos(Vector3 position)
    {
        WWWForm form = new WWWForm();
        form.AddField("x", position.x.ToString(CultureInfo.InvariantCulture));
        form.AddField("y", position.y.ToString(CultureInfo.InvariantCulture));
        form.AddField("z", position.z.ToString(CultureInfo.InvariantCulture));


        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~jonathancl1/positionUploader.php", form);
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
    }
}
