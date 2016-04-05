//-------------------------------------------------------------------------------------------------
//  ©Copyright KDToons & Games 2014
//-------------------------------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KDNav2DAgent))]
public class KDNavigator2DAgentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        KDNav2DAgent agent = (KDNav2DAgent)target;
        agent.Speed = EditorGUILayout.FloatField("Speed", agent.Speed);
        agent.RepathThisAgent = EditorGUILayout.Toggle("Repath Agent", agent.RepathThisAgent);

        if (GUI.changed)
            EditorUtility.SetDirty(agent);
    }
}