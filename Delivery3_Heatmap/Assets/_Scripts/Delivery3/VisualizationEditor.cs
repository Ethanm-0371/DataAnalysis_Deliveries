using UnityEngine;
using UnityEditor;

#if (UNITY_EDITOR) 

[CustomEditor(typeof(VisualizationLogic))]
public class VisualizationEditor : Editor
{
    VisualizationLogic myTarget;
    bool showButtonsDropdown = false;

    public override void OnInspectorGUI()
    {
        myTarget = (VisualizationLogic)target;

        //ShowSerializationTypes();

        //GUILayout.Space(10);

        //ShowClassSerialization();

        //GUILayout.Space(10);

        //ShowPrintButtons();

        EditorGUILayout.LabelField("Buttons", EditorStyles.boldLabel);

        if (GUILayout.Button("Update Data"))
        {
            myTarget.UpdatePositionData();
        }

        showButtonsDropdown = EditorGUILayout.Foldout(showButtonsDropdown, "Update individual fields");

        if (showButtonsDropdown)
        {
            //Print one
            if (GUILayout.Button("Retrieve position data"))
            {
                myTarget.UpdatePositionData();
            }

            if (GUILayout.Button("Retrieve attacks data"))
            {
                myTarget.UpdateAttackData();
            }

            if (GUILayout.Button("Retrieve deaths data"))
            {
                myTarget.UpdateDeathData();
            }

            if (GUILayout.Button("Retrieve hits data"))
            {
                myTarget.UpdateHitData();
            }

            if (GUILayout.Button("Retrieve kills data"))
            {
                myTarget.UpdateKillData();
            }
        }

        EditorGUI.BeginChangeCheck();

        bool targetValue1 = EditorGUILayout.Toggle("Show Position Data", myTarget.showPosData);
        bool targetValue2 = EditorGUILayout.Toggle("Show Treated Data", myTarget.showTreatedData);
        bool targetValue3 = EditorGUILayout.Toggle("Show Raw Data", myTarget.showRawData);

        if (EditorGUI.EndChangeCheck())
        {
            if (myTarget.showPosData != targetValue1)
            {
                myTarget.showPosData = targetValue1;
            }
            else if (myTarget.showTreatedData != targetValue2)
            {
                myTarget.showTreatedData = targetValue2;
            }
            else if (myTarget.showRawData != targetValue3)
            {
                myTarget.showRawData = targetValue3;
            }
        }

        myTarget.cellSize = EditorGUILayout.FloatField("Cell Size", myTarget.cellSize);

        myTarget.colorGradient = EditorGUILayout.GradientField("Gradient", myTarget.colorGradient);
    }
}

#endif