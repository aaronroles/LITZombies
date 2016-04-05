//-------------------------------------------------------------------------------------------------
//  ©Copyright KDToons & Games 2014
//-------------------------------------------------------------------------------------------------

using System;
using UnityEngine;

/// <summary>
/// A tile is the base of the navigation system, represents the division of the navigation area
/// on walkable squares. A tile can be blocked or can have a penalty 
/// (fire zone, small river, full of enemies...)
/// </summary>
[ExecuteInEditMode]
public class KD2DTile : MonoBehaviour
{
    #region "Fields"
    private float m_penalty;
    #endregion //Fields

    #region "Properties"
    /// <summary>
    /// Indicates if this Tile is occupied by one obstacle
    /// </summary>
    public bool Occupied { get; set; }
    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// Indicates the flood fill level of the tile, this property is used only on the first 
    /// phase of path finder generation
    /// </summary>
    public int Level { get; set; }
    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// Total displacement cost of this tiles
    /// </summary>
    public float F { get; private set; }
    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// Cost of moving from the father of this tile to this tile
    /// </summary>
    public float G { get; set; }
    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// Euristic cost of this tile from the start point
    /// </summary>
    public float H { private get; set; }
    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// If this tile is inside of a zone, (Enemy or semiobstacle) what penalty has
    /// </summary>
    public float Penalty { get { return m_penalty; } set { m_penalty = value < 0 ? 0 : value; } }
    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// Indicates if this tile is watched yet by the 2D Pathfinder
    /// </summary>
    public bool IsWrited { get; set; }
    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// Indicates if this tile is inside of the final way to find the goal
    /// </summary>
    public bool IsBloked { get; set; }
    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// The tile that conects to the start point 
    /// (running all fathers we can go from the goal to the start)
    /// </summary>
    public GameObject Father { get; set; }
    //---------------------------------------------------------------------------------------------
    
    #endregion //Properties

    #region "Monobehaviour"
    protected void Start()
    {
        if (GetComponents<KD2DTile>().Length > 1)
            throw new Exception("Only one Component of KD2DTile can be attached");
    }
    //---------------------------------------------------------------------------------------------

    protected virtual void OnDrawGizmos()
    {
        var path = "aStarTile.png";
        if (m_penalty > 0) path = "aStarTileP.png";
        else if (Occupied) path = "aStarTileO.png";
        Gizmos.DrawIcon(transform.position, path, false);
    }
    //---------------------------------------------------------------------------------------------

    protected void OnTriggerEnter2D(Collider2D a_collider2D)
    {
        var area2D = a_collider2D.GetComponent<KD2DArea>();
        if (area2D)
            area2D.OnTileEnter(this);
    }
    //---------------------------------------------------------------------------------------------

    protected void OnTriggerExit2D(Collider2D a_collider2D)
    {
        var area2D = a_collider2D.GetComponent<KD2DArea>();
        if (area2D)
            area2D.OnTileExit(this);
    }
    //---------------------------------------------------------------------------------------------
    #endregion //Monobehaviour

    #region "OnCode"
    /// <summary>
    /// Set the value of F (Path method)
    /// </summary>
    public void SetF()
    {
        float auxF = H + G;
        F = auxF + (auxF / 100f) * Penalty;
    }
    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// Restart the pathfinder main parameters to start another path process with this tile
    /// </summary>
    public void Restart()
    {
        IsWrited = IsBloked = false;
        F = G = H = 0;
        Father = null;
    }
    //---------------------------------------------------------------------------------------------
    #endregion
}