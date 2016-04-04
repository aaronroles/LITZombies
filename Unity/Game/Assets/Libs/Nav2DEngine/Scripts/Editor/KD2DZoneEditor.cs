//-------------------------------------------------------------------------------------------------
//  ©Copyright KDToons & Games 2014
//-------------------------------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KD2DZone))]
public class KD2DZoneEditor : KD2DAreaEditor
{
    public override void OnInspectorGUI()
    {
        var zone = (KD2DZone)target;
        zone.LinkedObject = (GameObject)EditorGUILayout.ObjectField("Linked to", zone.LinkedObject, typeof(GameObject), true);
        
        EditorGUILayout.BeginHorizontal();
        {
            zone.PenaltyPercentage = EditorGUILayout.FloatField("Penalty", zone.PenaltyPercentage);
            EditorGUILayout.LabelField("%");    
        }
        EditorGUILayout.EndHorizontal();

        base.OnInspectorGUI();

        if (GUI.changed)
            EditorUtility.SetDirty(zone);
    }
}