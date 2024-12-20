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
        if (GUILayout.Button("Retrieve pos data"))
        {
            myTarget.ShowPositionData();
        }
    }
}

#endif