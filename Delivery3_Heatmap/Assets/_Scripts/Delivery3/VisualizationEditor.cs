using UnityEngine;
using UnityEditor;

#if (UNITY_EDITOR) 

[CustomEditor(typeof(VisualizationLogic))]
public class VisualizationEditor : Editor
{
    VisualizationLogic myTarget;

    bool positionDropdown = false;
    bool attackDropdown = false;
    bool deathDropdown = false;
    bool hitDropdown = false;
    bool killDropdown = false;
    
    public override void OnInspectorGUI()
    {
        myTarget = (VisualizationLogic)target;

        GUILayout.Space(10);

        GetAllDataButton();

        if (myTarget.tables == null) { return; }

        GUILayout.Space(10);

        TableDropdown(myTarget.positionTable, ref positionDropdown, "Position", myTarget.UpdatePositionData);
        TableDropdown(myTarget.attackTable, ref attackDropdown, "Attack", myTarget.UpdateAttackData);
        TableDropdown(myTarget.deathTable, ref deathDropdown, "Death", myTarget.UpdateDeathData);
        TableDropdown(myTarget.hitTable, ref hitDropdown, "Hit", myTarget.UpdateHitData);
        TableDropdown(myTarget.killTable, ref killDropdown, "Kill", myTarget.UpdateKillData);

        GUILayout.Space(5);

        if (GUILayout.Button("Collapse all", GUILayout.Width(75))) 
        {
            positionDropdown = false;
            attackDropdown = false;
            deathDropdown = false;
            hitDropdown = false;
            killDropdown = false;
        }
    }

    void GetAllDataButton()
    {
        if (myTarget.tables != null)
        {
            if (GUILayout.Button("Update all data", GUILayout.Height(30))) { myTarget.UpdateAllData(); }
        }
        else
        {
            if (GUILayout.Button("Get data", GUILayout.Height(30)))
            {
                myTarget.InitTableList();
                myTarget.UpdateAllData();
            }
        }
    }

    void TableDropdown(VisualizationLogic.TableData tableToRender, ref bool dropdown, string tableName, System.Action functionToCall)
    {
        dropdown = EditorGUILayout.Foldout(dropdown, $"{tableName} Data");
        if (dropdown)
        {
            GUILayout.Space(5);

            if (GUILayout.Button($"Retrieve {tableName.ToLower()} data")) { functionToCall.Invoke(); }

            GUILayout.Space(5);

            tableToRender.showHeatmapData = EditorGUILayout.Toggle("Show Heatmap Data", tableToRender.showHeatmapData);
            tableToRender.showRawData = EditorGUILayout.Toggle("Show Raw Data", tableToRender.showRawData);

            tableToRender.gradient = EditorGUILayout.GradientField("Gradient", tableToRender.gradient);

            tableToRender.resolution = EditorGUILayout.Slider("Heatmap Resolution", tableToRender.resolution, 0.5f, 10.0f);

            GUILayout.Space(10);
        }
    }
}

#endif