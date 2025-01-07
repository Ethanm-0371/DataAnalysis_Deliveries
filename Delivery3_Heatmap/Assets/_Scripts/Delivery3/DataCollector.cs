using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;
using Gamekit3D;

public class DataCollector : MonoBehaviour
{
    [SerializeField] float sendFrequency = 3.0f;
    [SerializeField] float attackDelay = 1.0f;
    [SerializeField] float hitDelay = 1.0f;
    [SerializeField] float deathDelay = 1.0f;
    [SerializeField] float killDelay = 1.0f;

    [SerializeField] GameObject player;

    private void Awake()
    {
        StartCoroutine(SendPlayerPos());
        StartCoroutine(SendAttackPos());
        StartCoroutine(SendHitPos());
        StartCoroutine(SendDeathPos());
        StartCoroutine(SendKillPos());
    }

    IEnumerator SendPlayerPos()
    {
        while(true)
        {
            yield return new WaitForSeconds(sendFrequency);

            Debug.Log("Position: " + player.transform.position);
            StartCoroutine(UploadPos(player.transform.position));
        }
    }

    IEnumerator SendAttackPos()
    {
        while (true)
        {
            yield return null;

            if (PlayerInput.Instance.Attack)
            {
                Debug.Log("Attack Position: " + player.transform.position);
                StartCoroutine(UploadAttack(player.transform.position));
                yield return new WaitForSeconds(attackDelay);
            }
        }
    }

    IEnumerator SendHitPos()
    {
        while (true)
        {
            yield return null;

            if (PlayerController.instance.hurtAudioPlayer.audioSource.isPlaying)
            {
                Debug.Log("Death Position: " + player.transform.position);
                StartCoroutine(UploadHit(player.transform.position));
                yield return new WaitForSeconds(hitDelay);
            }

        }
    }

    IEnumerator SendDeathPos()
    {
        while (true)
        {
            yield return null;

            if (PlayerController.instance.emoteDeathPlayer.audioSource.isPlaying)
            {
                Debug.Log("Death Position: " + player.transform.position);
                StartCoroutine(UploadDeath(player.transform.position));
                yield return new WaitForSeconds(deathDelay);
            }
           
        }
    }

    IEnumerator SendKillPos()
    {
        while (true)
        {
            yield return null;

            if (PlayerController.instance.emoteDeathPlayer.audioSource.isPlaying)
            {
                Debug.Log("Kill Position: " + player.transform.position);
                StartCoroutine(UploadKill(player.transform.position));
                yield return new WaitForSeconds(killDelay);
            }

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
            Debug.Log($"Sent Position: {www.downloadHandler.text}");
            //lastPlayerID = uint.Parse(www.downloadHandler.text);
            //callback.Invoke(lastPlayerID);
        }
    }

    IEnumerator UploadAttack(Vector3 position)
    {
        WWWForm form = new WWWForm();
        form.AddField("x", position.x.ToString(CultureInfo.InvariantCulture));
        form.AddField("y", position.y.ToString(CultureInfo.InvariantCulture));
        form.AddField("z", position.z.ToString(CultureInfo.InvariantCulture));


        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~jonathancl1/attackUploader.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log($"Sent Attack: {www.downloadHandler.text}");
            //lastPlayerID = uint.Parse(www.downloadHandler.text);
            //callback.Invoke(lastPlayerID);
        }
    }

    IEnumerator UploadHit(Vector3 position)
    {
        WWWForm form = new WWWForm();
        form.AddField("x", position.x.ToString(CultureInfo.InvariantCulture));
        form.AddField("y", position.y.ToString(CultureInfo.InvariantCulture));
        form.AddField("z", position.z.ToString(CultureInfo.InvariantCulture));


        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~jonathancl1/hitUploader.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log($"Sent Hurt: {www.downloadHandler.text}");
            //lastPlayerID = uint.Parse(www.downloadHandler.text);
            //callback.Invoke(lastPlayerID);
        }
    }

    IEnumerator UploadDeath(Vector3 position)
    {
        WWWForm form = new WWWForm();
        form.AddField("x", position.x.ToString(CultureInfo.InvariantCulture));
        form.AddField("y", position.y.ToString(CultureInfo.InvariantCulture));
        form.AddField("z", position.z.ToString(CultureInfo.InvariantCulture));


        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~jonathancl1/deathUploader.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log($"Sent Death: {www.downloadHandler.text}");
            //lastPlayerID = uint.Parse(www.downloadHandler.text);
            //callback.Invoke(lastPlayerID);
        }
    }

    IEnumerator UploadKill(Vector3 position)
    {
        WWWForm form = new WWWForm();
        form.AddField("x", position.x.ToString(CultureInfo.InvariantCulture));
        form.AddField("y", position.y.ToString(CultureInfo.InvariantCulture));
        form.AddField("z", position.z.ToString(CultureInfo.InvariantCulture));


        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~jonathancl1/killUploader.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log($"Sent Kill: {www.downloadHandler.text}");
            //lastPlayerID = uint.Parse(www.downloadHandler.text);
            //callback.Invoke(lastPlayerID);
        }
    }
}
