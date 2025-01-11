using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

#if (UNITY_EDITOR)

public class VisualizationLogic : MonoBehaviour
{
    public bool showPosData = false;
    public bool showTreatedData = false;
    public bool showRawData = false;

    List<Vector3> posData = new List<Vector3>();
    List<Vector3> attackData = new List<Vector3>();
    List<Vector3> deathData = new List<Vector3>();
    List<Vector3> hitData = new List<Vector3>();
    List<Vector3> killData = new List<Vector3>();

    Dictionary<Vector3Int, int> positionsDictionary = new Dictionary<Vector3Int, int>();

    public float cellSize = 1f;

    public Gradient colorGradient = new Gradient();

    #region Data retrieval

    public void UpdateAllData()
    {
        StartCoroutine(RetrieveData("positionDownloader", posData));
        StartCoroutine(RetrieveData("attackDownloader", attackData));
        StartCoroutine(RetrieveData("deathDownloader", deathData));
        StartCoroutine(RetrieveData("hitDownloader", hitData));
        StartCoroutine(RetrieveData("killDownloader", killData));
    }

    public void UpdatePositionData()
    {
        StartCoroutine(RetrieveData("positionDownloader", posData));
    }
    public void UpdateAttackData()
    {
        StartCoroutine(RetrieveData("attackDownloader", attackData));
    }
    public void UpdateDeathData()
    {
        StartCoroutine(RetrieveData("deathDownloader", deathData));
    }
    public void UpdateHitData()
    {
        StartCoroutine(RetrieveData("hitDownloader", hitData));
    }
    public void UpdateKillData()
    {
        StartCoroutine(RetrieveData("killDownloader", killData));
    }

    public IEnumerator RetrieveData(string fileName, List<Vector3> data)
    {
        WWWForm form = new WWWForm();

        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~jonathancl1/" + fileName + ".php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log($"Succcess: {www.downloadHandler.text}");
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

    #endregion

    private void OnDrawGizmos()
    {
        if (showPosData)
        {
            if (showTreatedData)
            {
                positionsDictionary.Clear();

                foreach (var pos in posData)
                {
                    var gridPos = GetGridPos(pos);

                    if (positionsDictionary.ContainsKey(gridPos))
                    {
                        positionsDictionary[gridPos]++;
                    }
                    else
                    {
                        positionsDictionary.Add(gridPos, 1);
                    }
                }

                foreach (var item in positionsDictionary)
                {
                    Gizmos.color = GetColor(item.Value);

                    if (cellSize > .5f)
                        Gizmos.DrawCube(GetWorldPos(item.Key), Vector3.one * cellSize);
                }
            }

            if (showRawData)
            {
                Gizmos.color = Color.white;

                foreach (var item in posData)
                {
                    Gizmos.DrawSphere(item, 0.1f);
                }
            }
        }
    }

    Vector3Int GetGridPos(Vector3 worldPosition)
    {
        Vector3 relativePosition = worldPosition;

        Vector3Int gridIndex = new Vector3Int(
            Mathf.FloorToInt(relativePosition.x / cellSize),
            Mathf.FloorToInt(relativePosition.y / cellSize),
            Mathf.FloorToInt(relativePosition.z / cellSize)
        );

        return gridIndex;
    }

    Vector3 GetWorldPos(Vector3Int gridPos)
    {
        return new Vector3(
            gridPos.x * cellSize + cellSize / 2f,
            gridPos.y * cellSize + cellSize / 2f,
            gridPos.z * cellSize + cellSize / 2f
        );
    }

    Color GetColor(int instances)
    {
        return colorGradient.Evaluate((float)instances / positionsDictionary.Values.Max());
    }
}
#endif