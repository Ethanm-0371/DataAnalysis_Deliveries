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

        if (myTarget.tables == null) { return; }

        GUILayout.Space(10);

        PositionDropdown();
        AttackDropdown();
        DeathDropdown();
        HitDropdown();
        KillDropdown();

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

    void PositionDropdown()
    {
        positionDropdown = EditorGUILayout.Foldout(positionDropdown, "Position data");
        if (positionDropdown)
        {
            GUILayout.Space(5);

            if (GUILayout.Button("Retrieve position data")) { myTarget.UpdatePositionData(); }

            GUILayout.Space(5);

            myTarget.positionTable.showHeatmapData = EditorGUILayout.Toggle("Show Heatmap Data", myTarget.positionTable.showHeatmapData);
            myTarget.positionTable.showRawData = EditorGUILayout.Toggle("Show Raw Data", myTarget.positionTable.showRawData);

            myTarget.positionTable.gradient = EditorGUILayout.GradientField("Gradient", myTarget.positionTable.gradient);

            myTarget.positionTable.resolution = EditorGUILayout.Slider("Heatmap Resolution", myTarget.positionTable.resolution, 0.5f, 10.0f);

            GUILayout.Space(10);
        }
    }
    void AttackDropdown()
    {
        attackDropdown = EditorGUILayout.Foldout(attackDropdown, "Attack data");
        if (attackDropdown)
        {
            GUILayout.Space(5);

            if (GUILayout.Button("Retrieve attack data")) { myTarget.UpdateAttackData(); }

            GUILayout.Space(5);

            myTarget.attackTable.showHeatmapData = EditorGUILayout.Toggle("Show Heatmap Data", myTarget.attackTable.showHeatmapData);
            myTarget.attackTable.showRawData = EditorGUILayout.Toggle("Show Raw Data", myTarget.attackTable.showRawData);

            myTarget.attackTable.gradient = EditorGUILayout.GradientField("Gradient", myTarget.attackTable.gradient);

            myTarget.attackTable.resolution = EditorGUILayout.FloatField("Heatmap resolution", myTarget.attackTable.resolution);

            GUILayout.Space(10);
        }
    }
    void DeathDropdown()
    {
        deathDropdown = EditorGUILayout.Foldout(deathDropdown, "Death data");
        if (deathDropdown)
        {
            GUILayout.Space(5);

            if (GUILayout.Button("Retrieve death data")) { myTarget.UpdateDeathData(); }

            GUILayout.Space(5);

            myTarget.deathTable.showHeatmapData = EditorGUILayout.Toggle("Show Heatmap Data", myTarget.deathTable.showHeatmapData);
            myTarget.deathTable.showRawData = EditorGUILayout.Toggle("Show Raw Data", myTarget.deathTable.showRawData);

            myTarget.deathTable.gradient = EditorGUILayout.GradientField("Gradient", myTarget.deathTable.gradient);

            myTarget.deathTable.resolution = EditorGUILayout.FloatField("Heatmap resolution", myTarget.deathTable.resolution);

            GUILayout.Space(10);
        }
    }
    void HitDropdown()
    {
        hitDropdown = EditorGUILayout.Foldout(hitDropdown, "Hit data");
        if (hitDropdown)
        {
            GUILayout.Space(5);

            if (GUILayout.Button("Retrieve hit data")) { myTarget.UpdateHitData(); }

            GUILayout.Space(5);

            myTarget.hitTable.showHeatmapData = EditorGUILayout.Toggle("Show Heatmap Data", myTarget.hitTable.showHeatmapData);
            myTarget.hitTable.showRawData = EditorGUILayout.Toggle("Show Raw Data", myTarget.hitTable.showRawData);

            myTarget.hitTable.gradient = EditorGUILayout.GradientField("Gradient", myTarget.hitTable.gradient);

            myTarget.hitTable.resolution = EditorGUILayout.FloatField("Heatmap resolution", myTarget.hitTable.resolution);

            GUILayout.Space(10);
        }
    }
    void KillDropdown()
    {
        killDropdown = EditorGUILayout.Foldout(killDropdown, "Kill data");
        if (killDropdown)
        {
            GUILayout.Space(5);

            if (GUILayout.Button("Retrieve kill data")) { myTarget.UpdateKillData(); }

            GUILayout.Space(5);

            myTarget.killTable.showHeatmapData = EditorGUILayout.Toggle("Show Heatmap Data", myTarget.killTable.showHeatmapData);
            myTarget.killTable.showRawData = EditorGUILayout.Toggle("Show Raw Data", myTarget.killTable.showRawData);

            myTarget.killTable.gradient = EditorGUILayout.GradientField("Gradient", myTarget.killTable.gradient);

            myTarget.killTable.resolution = EditorGUILayout.FloatField("Heatmap resolution", myTarget.killTable.resolution);

            GUILayout.Space(10);
        }
    }
}

#endif