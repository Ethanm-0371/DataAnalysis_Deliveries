using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

#if (UNITY_EDITOR)

public class VisualizationLogic : MonoBehaviour
{
    public class TableData
    {
        public List<Vector3> data { get; private set; }
        public Dictionary<Vector3Int, int> grid;

        public bool showHeatmapData;
        public bool showRawData;
        public Gradient gradient;
        public float resolution;

        public TableData()
        {
            data = new List<Vector3>();
            grid = new Dictionary<Vector3Int, int>();
            showHeatmapData = false;
            showRawData = false;
            resolution = 1.0f;
            gradient = new Gradient()
            {
                colorKeys = new GradientColorKey[5] {
                    // Add your colour and specify the stop point
                    new GradientColorKey(new Color(0, 0.1666665f, 1), 0),
                    new GradientColorKey(new Color(0, 1, 0.8833334f), 0.25f),
                    new GradientColorKey(new Color(0.08333325f, 1, 0), 0.5f),
                    new GradientColorKey(new Color(1, 0.9583334f, 0), 0.75f),
                    new GradientColorKey(new Color(1, 0, 0), 1)
                },
                alphaKeys = new GradientAlphaKey[2] {
                    // This sets the alpha to 1 at both ends of the gradient
                    new GradientAlphaKey(1, 0),
                    new GradientAlphaKey(1, 1)
    }
            };
        }

        public bool UpdateData(string newDataJson)
        {
            data.Clear();

            string formattedJson = newDataJson.TrimStart('[').TrimEnd(']');

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
                    return false;
                }
            }
            return true;
        }
    }

    public TableData positionTable = new TableData();
    public TableData attackTable = new TableData();
    public TableData deathTable = new TableData();
    public TableData hitTable = new TableData();
    public TableData killTable = new TableData();

    public List<TableData> tables;

    public void InitTableList()
    {
        tables = new List<TableData> { positionTable, attackTable, deathTable, hitTable, killTable };
    }

    #region Data retrieval

    public void UpdateAllData()
    {
        StartCoroutine(RetrieveData("positionDownloader", positionTable));
        StartCoroutine(RetrieveData("attackDownloader", attackTable));
        StartCoroutine(RetrieveData("deathDownloader", deathTable));
        StartCoroutine(RetrieveData("hitDownloader", hitTable));
        StartCoroutine(RetrieveData("killDownloader", killTable));
    }

    public void UpdatePositionData()
    {
        StartCoroutine(RetrieveData("positionDownloader", positionTable));
    }
    public void UpdateAttackData()
    {
        StartCoroutine(RetrieveData("attackDownloader", attackTable));
    }
    public void UpdateDeathData()
    {
        StartCoroutine(RetrieveData("deathDownloader", deathTable));
    }
    public void UpdateHitData()
    {
        StartCoroutine(RetrieveData("hitDownloader", hitTable));
    }
    public void UpdateKillData()
    {
        StartCoroutine(RetrieveData("killDownloader", killTable));
    }

    public IEnumerator RetrieveData(string fileName, TableData tableToFill)
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
            tableToFill.UpdateData(www.downloadHandler.text);
        }
        yield return null;
    }

    #endregion

    private void OnDrawGizmos()
    {
        if (tables == null) { return; }

        foreach (var table in tables)
        {
            if (table.showHeatmapData)
            {
                table.grid.Clear();

                foreach (var pos in table.data)
                {
                    var gridPos = GetGridPos(pos, table.resolution);

                    if (table.grid.ContainsKey(gridPos))
                    {
                        table.grid[gridPos]++;
                    }
                    else
                    {
                        table.grid.Add(gridPos, 1);
                    }
                }

                int maxInstance = table.grid.Values.Max();
                foreach (var item in table.grid)
                {
                    Gizmos.color = GetColor(item.Value, maxInstance, table.gradient);

                    if (table.resolution >= .5f)
                        Gizmos.DrawCube(GetWorldPos(item.Key, table.resolution), Vector3.one * table.resolution);
                }
            }

            if (table.showRawData)
            {
                Gizmos.color = Color.white;

                foreach (var position in table.data)
                {
                    Gizmos.DrawSphere(position, 0.1f);
                }
            }
        }
    }

    Vector3Int GetGridPos(Vector3 worldPosition, float cellSize)
    {
        Vector3 relativePosition = worldPosition;

        Vector3Int gridIndex = new Vector3Int(
            Mathf.FloorToInt(relativePosition.x / cellSize),
            Mathf.FloorToInt(relativePosition.y / cellSize),
            Mathf.FloorToInt(relativePosition.z / cellSize)
        );

        return gridIndex;
    }

    Vector3 GetWorldPos(Vector3Int gridPos, float cellSize)
    {
        return new Vector3(
            gridPos.x * cellSize + cellSize / 2f,
            gridPos.y * cellSize + cellSize / 2f,
            gridPos.z * cellSize + cellSize / 2f
        );
    }

    Color GetColor(int instances, int highestInstance, Gradient colorGradient)
    {
        return colorGradient.Evaluate((float)instances / highestInstance);
    }
}
#endif