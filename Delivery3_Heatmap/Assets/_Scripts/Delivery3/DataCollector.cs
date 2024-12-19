using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }
}
