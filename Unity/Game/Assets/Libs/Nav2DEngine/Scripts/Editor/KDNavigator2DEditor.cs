//-------------------------------------------------------------------------------------------------
//  ©Copyright KDToons & Games 2014
//-------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using KDInteractive.PathFinder;
using UnityEditorInternal;
using Object = UnityEngine.Object;

[CustomEditor(typeof(KDNav2D))]
public class KDNavigator2DEditor : Editor
{
    private bool m_showZones;
    private bool m_showObstacles;
    private static Texture m_aTree;

    protected void GuiLine()
    {
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
    }

    public void OnEnable()
    {
        m_aTree = (Texture)Resources.Load("FloodFillSeed");
    }

    public override void OnInspectorGUI()
    {
        var nav2D = (KDNav2D)target;

        Properties(nav2D);
        MainButtons(nav2D);

        if (nav2D.Areas != null)
        {
            Action<KD2DObstacle> action = a_obstacle => 
            a_obstacle.IsDynamic = GUILayout.Toggle(a_obstacle.IsDynamic, "Dynamic");

            Grid<KD2DObstacle>
            (
                nav2D, 
                action, 
                ref m_showObstacles, 
                "Hide Obstacles", 
                "Show Obstacles", 
                "_showObstacles", 
                Color.red
            );

            Grid<KD2DZone>
            (
                nav2D, 
                null, 
                ref m_showZones, 
                "Hide Zones", 
                "Show Zones", 
                "_showZones", 
                Color.yellow
            );
        }
        EditorUtility.SetDirty(nav2D);
    }
    //----------------------------------------------------------------------------------------------

    private static void Properties(KDNav2D a_navigator)
    {
        int falseNavigatorIndex = string.IsNullOrEmpty(a_navigator.TileLayerName) ?
        0 : InternalEditorUtility.layers.ToList().IndexOf(a_navigator.TileLayerName);

        a_navigator.TileLayerIndex = EditorGUILayout.Popup
        (
            "Layer Tiles", 
            falseNavigatorIndex, 
            InternalEditorUtility.layers.Where
            (
                a_layer => a_layer != LayerMask.LayerToName(a_navigator.gameObject.layer)
            ).ToArray()
        );

        a_navigator.TileLayerName = InternalEditorUtility.layers[a_navigator.TileLayerIndex];
        a_navigator.TileLayerIndex = LayerMask.NameToLayer(a_navigator.TileLayerName);

        a_navigator.Resolution = 1 / EditorGUILayout.Slider
        (
            "Flood Resolution", 
            1 / a_navigator.Resolution, 
            1 / KDPathFinder.MAX_SIZE, 
            1 / KDPathFinder.MIN_SIZE
        );

        a_navigator.SeparationFactor = EditorGUILayout.Slider
        (
            "Tile Separation", 
            a_navigator.SeparationFactor, 
            KDPathFinder.MIN_SEPARATION, 
            KDPathFinder.MAX_SEPARATION
        );

        a_navigator.Seed = (GameObject)EditorGUILayout.ObjectField
        (
            "The Seed", 
            a_navigator.Seed, 
            typeof(GameObject), 
            true
        );

        a_navigator.RepathAfterTile = EditorGUILayout.Toggle
        (
            "Path After Step", 
            a_navigator.RepathAfterTile
        );

        a_navigator.Areas.RemoveAll(a_obstacle => a_obstacle == null);
    }
    //----------------------------------------------------------------------------------------------

    private static void MainButtons(KDNav2D a_navigator)
    {
        GUILayout.BeginHorizontal("", GUILayout.Height(25));
        {
            GUILayout.Label("A* Seed");
            if (GUILayout.Button("Adjust/Move", EditorStyles.miniButton))
            {
                a_navigator.Remove();
                Selection.objects = new Object[] { a_navigator.Seed };
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("", GUILayout.Height(25));
        {
            GUILayout.Label("Navigation");
            if (GUILayout.Button("Bake", EditorStyles.miniButtonLeft))
            {
                int desnity = a_navigator.Bake();
                Debug.Log("Baked Finished with " + desnity + " nodes");
                Selection.objects = new Object[] { null };
            }

            if (GUILayout.Button("Clear", EditorStyles.miniButtonRight))
                a_navigator.Remove();
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("", GUILayout.Height(25));
        {
            GUILayout.Label("Add");

            if (GUILayout.Button("Obstacle", EditorStyles.miniButtonLeft))
                a_navigator.AddArea<KD2DObstacle>("Obstacle 2D");

            if (GUILayout.Button("Penalty Zone", EditorStyles.miniButtonRight))
                a_navigator.AddArea<KD2DZone>("Zone 2D");
        }
        GUILayout.EndHorizontal();
    }
    //----------------------------------------------------------------------------------------------

    private void Grid<T>
    (
        KDNav2D a_nav2D, 
        Action<T> a_action, 
        ref bool a_enabled, 
        string a_typeOn, 
        string a_typeOff, 
        string a_key, 
        Color a_color
    ) where T : KD2DArea
    {
        a_enabled = DrawHeader(a_enabled ? a_typeOn : a_typeOff, a_key, a_enabled, a_color);
        if (a_enabled)
        {
            BeginContents();
            if (a_nav2D.Areas.Exists(o => o.GetComponent<KD2DArea>() is T))
                DrawNavigatorAreaGrid<T>(a_nav2D, a_action);
            else
            {
                GUILayout.Label
                (
                    "Not Areas added yet", 
                    GUILayout.ExpandWidth(true), 
                    GUILayout.Height(50)
                );
            }
            EndContents();
        }
    }
    //----------------------------------------------------------------------------------------------

    protected void DrawNavigatorAreaGrid<T>
    (
        KDNav2D a_nav2D, 
        Action<T> a_action
    ) where T : KD2DArea
    {
        GUILayout.Space(10f);
        GameObject removed = null;

        var typeAreas = a_nav2D.Areas.Where
        (
            o => o.GetComponent<KD2DArea>() is T
        ).ToArray();
        
        var i = 0;
        foreach (var obstacle in typeAreas)
        {
            var obstacleComponent = obstacle.GetComponent<T>();
            GUILayout.BeginHorizontal();
            {
                obstacleComponent.LinkedObject = (GameObject)EditorGUILayout.ObjectField
                (
                    "Linked To", 
                    obstacleComponent.LinkedObject, 
                    typeof(GameObject), 
                    true
                );
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("", GUILayout.Height(25));
            {
                if (a_action != null)
                    a_action(obstacleComponent);

                if (GUILayout.Button("Edit", EditorStyles.miniButtonLeft))
                    Selection.objects = new Object[] { obstacle };

                if (obstacleComponent.LinkedObject != null &&
                    GUILayout.Button("Select Linked", EditorStyles.miniButtonMid))
                {
                    Selection.objects = new Object[] { obstacleComponent.LinkedObject };
                }

                if (GUILayout.Button("Delete", EditorStyles.miniButtonRight))
                    removed = obstacle.gameObject;
            }
            GUILayout.EndHorizontal();

            if (i++ < typeAreas.Length - 1)
                GuiLine();
        }

        if (removed)
        {
            a_nav2D.Areas.Remove(removed);
            Undo.DestroyObjectImmediate(removed);
        }
    }
    //----------------------------------------------------------------------------------------------

    public static bool DrawHeader(string a_text, string a_key, bool a_forceOn, Color a_color)
    {
        bool state = EditorPrefs.GetBool(a_key, true);

        GUILayout.Space(3f);
        if (!a_forceOn && !state)
            GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
        else
            GUI.backgroundColor = a_color;

        GUILayout.BeginHorizontal();
        GUILayout.Space(3f);

        GUI.changed = false;
#if UNITY_3_5
		if (state) 
            p_text = "\u25B2 " + p_text;
		else 
            p_text = "\u25BC " + p_text;
		if (!GUILayout.Toggle(true, p_text, "dragtab", GUILayout.MinWidth(20f))) 
            state = !state;
#else
        a_text = "<b><size=11>" + a_text + "</size></b>";
        a_text = (state ? "\u25B2 " : "\u25BC ") + a_text;

        if (!GUILayout.Toggle(true, a_text, "dragtab", GUILayout.MinWidth(20f)))
            state = !state;
#endif
        if (GUI.changed)
            EditorPrefs.SetBool(a_key, state);

        GUILayout.Space(2f);
        GUILayout.EndHorizontal();

        GUI.backgroundColor = Color.white;
        if (!a_forceOn && !state)
            GUILayout.Space(3f);
        return state;
    }
    //----------------------------------------------------------------------------------------------

    public static void BeginContents()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(4f);
        EditorGUILayout.BeginHorizontal("AS TextArea", GUILayout.MinHeight(10f));
        GUILayout.BeginVertical();
        GUILayout.Space(2f);
    }
    //----------------------------------------------------------------------------------------------

    public static void EndContents()
    {
        GUILayout.Space(3f);
        GUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(3f);
        GUILayout.EndHorizontal();
        GUILayout.Space(3f);
    }
    //----------------------------------------------------------------------------------------------
}