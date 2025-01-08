using UnityEngine;
using UnityEditor;

#if (UNITY_EDITOR) 

[CustomEditor(typeof(VisualizationLogic))]
public class VisualizationEditor : Editor
{
    VisualizationLogic myTarget;

    public override void OnInspectorGUI()
    {
        myTarget = (VisualizationLogic)target;

        //ShowSerializationTypes();

        //GUILayout.Space(10);

        //ShowClassSerialization();

        //GUILayout.Space(10);

        //ShowPrintButtons();

        EditorGUILayout.LabelField("Buttons", EditorStyles.boldLabel);

        //Print one
        if (GUILayout.Button("Retrieve position data"))
        {
            myTarget.ShowPositionData();
        }

        if (GUILayout.Button("Retrieve attacks data"))
        {
            myTarget.ShowAttackData();
        }

        if (GUILayout.Button("Retrieve deaths data"))
        {
            myTarget.ShowDeathData();
        }

        if (GUILayout.Button("Retrieve hits data"))
        {
            myTarget.ShowHitData();
        }

        if (GUILayout.Button("Retrieve kills data"))
        {
            myTarget.ShowKillData();
        }
    }
}

#endif