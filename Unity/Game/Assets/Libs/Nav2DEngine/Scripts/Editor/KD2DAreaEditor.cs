//-------------------------------------------------------------------------------------------------
//  ©Copyright KDToons & Games 2014
//-------------------------------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KD2DArea))]
public class KD2DAreaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Color backgroundCacheColor = GUI.color;
        EditorGUILayout.BeginHorizontal();
        {
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Up to Navigator 2D", EditorStyles.miniButtonLeft))
                Selection.objects = new Object[] { ((Component)target).transform.parent.gameObject };

            GUI.backgroundColor = Color.blue;
            if (GUILayout.Button("Select Linked", EditorStyles.miniButtonRight))
                Selection.objects = new Object[] { ((KD2DArea)target).LinkedObject };
            GUI.backgroundColor = backgroundCacheColor;    
        }
        EditorGUILayout.EndHorizontal();
    }
}