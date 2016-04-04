//-------------------------------------------------------------------------------------------------
//  ©Copyright KDToons & Games 2014
//-------------------------------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KD2DObstacle))]
public class KD2DObstacleEditor : KD2DAreaEditor
{
    public override void OnInspectorGUI()
    {
        KD2DObstacle obstacleComponent = (KD2DObstacle)target;
        obstacleComponent.LinkedObject = (GameObject)EditorGUILayout.ObjectField("Linked to", obstacleComponent.LinkedObject, typeof(GameObject), true);
        obstacleComponent.IsDynamic = EditorGUILayout.Toggle("Dynamic", obstacleComponent.IsDynamic);
        base.OnInspectorGUI();

        if (GUI.changed)
            EditorUtility.SetDirty(obstacleComponent);
    }
}