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
    }

    void SendEndSession(DateTime time, uint sessionID)
    {
        BBDDManager.UploadEndSession(sessionID, time, CallbackEvents.OnEndSessionCallback);
    }

    void SendItemBought(int itemID, DateTime time, uint sessionID)
    {
        BBDDManager.UploadPurchase((uint)itemID, time, sessionID, CallbackEvents.OnItemBuyCallback);
    }
}
