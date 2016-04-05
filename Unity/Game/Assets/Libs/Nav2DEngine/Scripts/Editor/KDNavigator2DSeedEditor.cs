//-------------------------------------------------------------------------------------------------
//  ©Copyright KDToons & Games 2014
//-------------------------------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KDNav2DSeed))]
public class KDNavigator2DSeedEditor : Editor
{
    private KDNav2DSeed m_seed;

    public void OnEnable()
    {
        if (m_seed == null)
            m_seed = (KDNav2DSeed)target;
    }

    public override void OnInspectorGUI()
    {
        //m_seed.Navigator = (KDNavigator2D)EditorGUILayout.ObjectField(m_seed.Navigator, typeof(KDNavigator2D), true);

        if (GUILayout.Button("Up to Navigator 2D", EditorStyles.miniButton))
            Selection.objects = new Object[] { ((Component)target).transform.parent.gameObject };

        if (GUI.changed)
            EditorUtility.SetDirty(target);
    }
}