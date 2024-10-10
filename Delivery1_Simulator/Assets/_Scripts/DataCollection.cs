using System;
using System.Collections;
using UnityEngine;

public class DataCollection : MonoBehaviour
{
    private void OnEnable()
    {
        Simulator.OnNewPlayer += SendNewPlayer;
        Simulator.OnNewSession += SendNewSession;
        Simulator.OnEndSession += SendEndSession;
        Simulator.OnBuyItem += SendItemBought;
    }

    void SendNewPlayer(string name, string country, int age, float gender, DateTime date)
    {
        BBDDManager.UploadPlayer(name, country, age, gender, date, CallbackEvents.OnAddPlayerCallback);
    }

    void SendNewSession(DateTime time, uint playerID)
    {
        BBDDManager.UploadNewSession(playerID, time, CallbackEvents.OnNewSessionCallback);
        Debug.Log("New session with player ID: " + playerID);

        //uint sessionID = 4; //received from server

        //StartCoroutine(SendNewSessionDelay(sessionID));
    }
    //IEnumerator SendNewSessionDelay(uint sessionID)
    //{
    //    yield return null;
    //    CallbackEvents.OnNewSessionCallback.Invoke(sessionID);
    //}



    void SendEndSession(DateTime time, uint sessionID)
    {
        //Send info to server

        uint playerID = 45; //received from server
        StartCoroutine(SendEndSessionDelay(playerID));

    }
    IEnumerator SendEndSessionDelay(uint sessionID)
    {
        yield return null;
        CallbackEvents.OnEndSessionCallback.Invoke(sessionID);
    }

    void SendItemBought(int itemID, DateTime time, uint sessionID)
    {
        //Send info to server

        //no need? sessionID = 7; //received from server
        StartCoroutine(SendItemBoughtDelay(2));
    }

    IEnumerator SendItemBoughtDelay(uint sessionID)
    {
        yield return null;
            CallbackEvents.OnItemBuyCallback.Invoke(sessionID);
        
    }
}
