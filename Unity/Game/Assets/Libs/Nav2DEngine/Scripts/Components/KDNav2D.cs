//-------------------------------------------------------------------------------------------------
//  ©Copyright KDToons & Games 2014
//-------------------------------------------------------------------------------------------------

using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Linq;
using KDInteractive.PathFinder;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// KDNavigator2D is the administrator of the 2D Path Engine, using "GeneratePaths" coroutine, this class
/// provides routes to the Agents than has demanded a destination using FIFO method.
/// </summary>
[Serializable]
[RequireComponent(typeof(PolygonCollider2D))]
public class KDNav2D : MonoBehaviour
{
    #region "Fields"
    private KDPathFinder m_pathFinder;
    private List<KDNav2DAgent> m_agents;
    private const string SEED_NAME = "|Seed|";
    //----------------------------------------------------------------------------------------------

    [SerializeField] private float m_resolution;
    [SerializeField] private GameObject m_seed;
    [SerializeField] private List<GameObject> m_areas;
    [SerializeField] private string m_tileLayerName;
    [SerializeField] private int m_tileLayerIndex;
    [SerializeField] private float m_separationFactor;
    [SerializeField] private int m_navigatorLayerIndex;
    [SerializeField] private string m_navigatorLayerName;
    [SerializeField] private bool m_repathAfterTile;
    //----------------------------------------------------------------------------------------------
    #endregion //Fields

    #region "Properties"
    /// <summary>
    /// Is the amount of separation between tiles on the path finder grid
    /// </summary>
    public float SeparationFactor { get { return m_separationFactor; } set { m_separationFactor = value; } }
    //----------------------------------------------------------------------------------------------

    /// <summary>
    /// Indez of the physic layer where tiles are placed
    /// </summary>
    public int TileLayerIndex { get { return m_tileLayerIndex; } set { m_tileLayerIndex = value; } }
    //----------------------------------------------------------------------------------------------

    /// <summary>
    /// Name of the physic layer where tiles are placed
    /// </summary>
    public string TileLayerName { get { return m_tileLayerName; } set { m_tileLayerName = value; } }
    //----------------------------------------------------------------------------------------------

    /// <summary>
    /// Detail level of flood fill inside the navigation polygon, 
    /// small resolution generate path faster but are not very accurate.
    /// Increase flood resolution if you need to obtain best routes (This will affect game performace)
    /// </summary>
    public float Resolution
    {
        get { return m_resolution; } 
        set { m_resolution = Mathf.Clamp(value, KDPathFinder.MIN_SIZE, KDPathFinder.MAX_SIZE); }
    }
    //----------------------------------------------------------------------------------------------

    /// <summary>
    /// Seed is the origin of the tile grid, put the seed in a strategic place to obtain best
    /// results of flood fill
    /// </summary>
    public GameObject Seed { get { return m_seed; } set { m_seed = value; } }
    //----------------------------------------------------------------------------------------------

    /// <summary>
    /// All areas inside of the polygon
    /// </summary>
    public List<GameObject> Areas { get { return m_areas ?? (m_areas = new List<GameObject>()); } }
    //----------------------------------------------------------------------------------------------

    /// <summary>
    /// Name of the navigation polygon
    /// </summary>
    public string NavigatorLayerName
    {
        get { return m_navigatorLayerName; } 
        set { m_navigatorLayerName = value; }
    }
    //----------------------------------------------------------------------------------------------

    /// <summary>
    /// Index of the navigator layer
    /// </summary>
    public int NavigatorLayerIndex
    {
        get { return m_navigatorLayerIndex; } 
        set { m_navigatorLayerIndex = value; }
    }
    //----------------------------------------------------------------------------------------------

    /// <summary>
    /// Density
    /// </summary>
    public int Density
    {
        get { return m_pathFinder == null ? 0 : m_pathFinder.Density; }
    }
    //----------------------------------------------------------------------------------------------

    /// <summary>
    /// Path Every Frame
    /// </summary>
    public bool RepathAfterTile
    {
        get { return m_repathAfterTile; } set { m_repathAfterTile = value; } 
    }
    //----------------------------------------------------------------------------------------------

    #endregion //Properties

    #region "Monobehaviour"

    protected void Reset()
    {
        Transform childTransform = transform.FindChild(SEED_NAME);
        if (!childTransform)
            Debug.LogError("The floodfill seed game object was not finded using the default name " +
                           "\'" + SEED_NAME + "\'");
        else
            m_seed = childTransform.gameObject;

        while (transform.childCount > 1)
            if (m_seed.transform != transform.GetChild(0))
                DestroyImmediate(transform.GetChild(0).gameObject);

        m_areas.Clear();
        var polygon = GetComponent<PolygonCollider2D>();
        polygon.CreatePrimitive(4);
        polygon.isTrigger = true;
        Resolution = KDPathFinder.MAX_SIZE;
        m_seed.transform.position = transform.position;
    }
    //----------------------------------------------------------------------------------------------

    protected void Start()
    {
        PolygonCollider2D[] polygons = GetComponents<PolygonCollider2D>();

        if (m_tileLayerIndex == gameObject.layer)
            throw new Exception("Tile layer and Navigator 2D layer must be different");

        if (polygons.Length > 1)
            throw new Exception("Only one component of PolygonCollider2D can be attached to " +
                                "" + gameObject.name + " game object");

        SetLayers();
        polygons.First().isTrigger = polygons.First().enabled = true;
        m_pathFinder = new KDPathFinder
                        (
                            m_seed,
                            m_tileLayerIndex,
                            m_resolution,
                            m_separationFactor,
                            polygons[0]
                        );

        if (m_pathFinder.Density == 0)
            throw new Exception("You must to Bake the flood area " +
                                                               "first on KDNavigator 2D Inspector");
        m_agents = new List<KDNav2DAgent>();
        StartCoroutine(GeneratePaths());
    }
    //----------------------------------------------------------------------------------------------

    /// <summary>
    /// Set all areas inside the Physic layer
    /// </summary>
    protected void SetLayers()
    {
        m_areas.ForEach(a_area => a_area.layer = gameObject.layer);
        m_seed.layer = m_tileLayerIndex;
    }
    //----------------------------------------------------------------------------------------------

    protected void OnDrawGizmos()
    {
        Vector2[] points = GetComponent<PolygonCollider2D>().points;
        Vector2 position2D = transform.position;

        if (points != null)
        {
            Vector3 pointCurrent, pointNext;
            for (var i = 0; i < points.Length - 1; i++)
            {
                pointCurrent = points[i] + position2D;
                pointNext = points[i + 1] + position2D;
                Gizmos.DrawLine(pointCurrent, pointNext);
            }

            pointCurrent = points[points.Length - 1] + (Vector2)transform.position;
            pointNext = points[0] + (Vector2)transform.position;
            Gizmos.DrawLine(pointCurrent, pointNext);
        }
    }
    //----------------------------------------------------------------------------------------------
    #endregion //Monobehaviour

    #region "OnCode"

    public void Remove()
    {
        KDPathFinder.KillChilds(m_seed);
    }
    //----------------------------------------------------------------------------------------------

    public int Bake()
    {
        PolygonCollider2D[] polygons = GetComponents<PolygonCollider2D>();

        if (m_tileLayerIndex == gameObject.layer)
            throw new Exception("Tile layer and Navigator 2D layer must be different");

        if (polygons.Length > 1)
            throw new Exception("Only one component of PolygonCollider2D can be attached " +
                                "to " + gameObject.name + " game object");
        
        return KDPathFinder.Bake
        (
            m_tileLayerIndex,
            m_resolution,
            m_separationFactor,
            m_seed,
            polygons[0]
        );
    }
    //----------------------------------------------------------------------------------------------

    /// <summary>
    /// Dispatch routes of the agents on the list by FIFO
    /// </summary>
    /// <returns></returns>
    private IEnumerator GeneratePaths()
    {
        while (true)
        {
            if (m_agents.Count > 0)
            {
                KDNav2DAgent agent = m_agents.First();
                List<KD2DTile> route = m_pathFinder.FindPath(agent);
                agent.SetRoute(route);
                m_agents.RemoveAt(0);
            }
            yield return new WaitForEndOfFrame();
        }
    }
    //----------------------------------------------------------------------------------------------

    /// <summary>
    /// Creates a request to obtain a route for find the way to reach the goal tile
    /// </summary>
    /// <param name="a_agent">Agent requesting a route</param>
    public void FindWay(KDNav2DAgent a_agent)
    {
        m_agents.Remove(a_agent);
        m_agents.Add(a_agent);
    }
    //----------------------------------------------------------------------------------------------

    /// <summary>
    /// /// Adds a new Zone 2D to Area list
    /// </summary>
    /// <typeparam name="T">type of Are that we can create, must inherit from KD2DArea</typeparam>
    /// <param name="p_areaName">name of the new Area</param>
    public void AddArea<T>(string p_areaName) where T : KD2DArea
    {
        if (gameObject.layer == m_tileLayerIndex)
            throw new ArgumentException("Navigator physic layer must be different of tile physic layer");

        var area = new GameObject();
#if UNITY_EDITOR
        Undo.RegisterCreatedObjectUndo(area, "Created Area");
#endif
        var polygonCollider2D = area.AddComponent<PolygonCollider2D>();
        polygonCollider2D.isTrigger = true;
        polygonCollider2D.CreatePrimitive(4);
        area.AddComponent<T>();
        area.transform.position = transform.position;
        area.transform.rotation = Quaternion.identity;
        area.transform.parent = transform;
        area.name = p_areaName;
        area.layer = gameObject.layer;
        m_areas.Add(area);
        m_areas.TrimExcess();
    }
    //----------------------------------------------------------------------------------------------

    public KD2DTile NearestNodeToPosition(Vector2 a_position)
    {
        KD2DTile nearestTile = m_pathFinder.NearestTile(a_position);
        return nearestTile;
    }
    //----------------------------------------------------------------------------------------------
    #endregion
}