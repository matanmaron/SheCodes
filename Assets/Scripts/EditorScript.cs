using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelCreator))]
[CanEditMultipleObjects]

public class EditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelCreator lc = (LevelCreator)target;
        
        if (GUILayout.Button("Create Level Grid"))
        {
            lc.CreateLevelGrid();
        }

        if (GUILayout.Button("Clear Level Grid"))
        {
            lc.ClearLevelGrid();
        }

        if (GUILayout.Button("Set Blocks Roles"))
        {
            lc.SetBlocksType();
        }

        if (GUILayout.Button("Set Connection Lights"))
        {
            lc.SetConnectionLights();
        }

        
        if (GUILayout.Button("Clear Blocks Roles"))
        {
            lc.ClearBlocksRoles();
        }

        
    }


}
