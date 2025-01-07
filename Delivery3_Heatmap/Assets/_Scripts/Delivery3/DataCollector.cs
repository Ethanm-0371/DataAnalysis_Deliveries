using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;
using Gamekit3D;
using Gamekit3D.Message;

public class DataCollector : MonoBehaviour, IMessageReceiver
{
    [SerializeField] float sendFrequency = 3.0f;
    [SerializeField] float attackDelay = 1.0f;

    [SerializeField] GameObject player;

    private void Awake()
    {
        StartCoroutine(SendPlayerPos());
        StartCoroutine(SendAttackPos());
        AddEnemyReceiver();
    }

    void AddEnemyReceiver()
    {
        Damageable[] dmgGOs = FindObjectsOfType<Damageable>();

        foreach (Damageable GO in dmgGOs)
        {
            if (GO.name.Equals("Spitter") || GO.name.Equals("Chomper"))
            {
                GO.onDamageMessageReceivers.Add(this);
            }
        }
    }

    public void OnReceiveMessage(MessageType type, object sender, object msg)
    {
        switch (type)
        {
            case MessageType.DAMAGED:
                
                if (sender.ToString().Contains("Ellen")) SendHitPos();

                break;
            case MessageType.DEAD:
                if (sender.ToString().Contains("Ellen")) SendDeathPos();
                else
                {
                    Damageable GO = (Damageable) sender;
                    SendKillPos(GO.transform.position);
                }

                break;
        }
    }

    IEnumerator SendPlayerPos()
    {
        while (true)
        {
            yield return new WaitForSeconds(sendFrequency);

            Debug.Log("Position: " + player.transform.position);
            StartCoroutine(UploadData(player.transform.position, "positionUploader"));
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
                StartCoroutine(UploadData(player.transform.position, "attackUploader"));
                yield return new WaitForSeconds(attackDelay);
            }
        }
    }

    void SendHitPos()
    {
        Debug.Log("Hit Position: " + player.transform.position);
        StartCoroutine(UploadData(player.transform.position, "hitUploader"));
    }

    void SendDeathPos()
    {

        if (PlayerController.instance.emoteDeathPlayer.audioSource.isPlaying)
        {
            Debug.Log("Death Position: " + player.transform.position);
            StartCoroutine(UploadData(player.transform.position, "deathUploader"));
        }

    }

    void SendKillPos(Vector3 enemyPosition)
    {
        Debug.Log("Kill Position: " + enemyPosition);
        StartCoroutine(UploadData(enemyPosition, "killUploader"));
    }

    IEnumerator UploadData(Vector3 position, string fileName)
    {
        WWWForm form = new WWWForm();
        form.AddField("x", position.x.ToString(CultureInfo.InvariantCulture));
        form.AddField("y", position.y.ToString(CultureInfo.InvariantCulture));
        form.AddField("z", position.z.ToString(CultureInfo.InvariantCulture));


        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~jonathancl1/" + fileName + ".php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log($"Sent Data: {www.downloadHandler.text}");
            //lastPlayerID = uint.Parse(www.downloadHandler.text);
            //callback.Invoke(lastPlayerID);
        }
    }
}
