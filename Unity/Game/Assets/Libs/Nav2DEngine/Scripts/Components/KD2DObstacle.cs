//-------------------------------------------------------------------------------------------------
//  ©Copyright KDToons & Games 2014
//-------------------------------------------------------------------------------------------------

using System;
using UnityEngine;

/// <summary>
/// Areas can be avoided for the navigation agent. 
/// navigable static obstacles stop the flood fill tilling method 
/// (for performace set an obstacle as "navigation static" if the obstacle is not going to move on 
/// the scene) Non navigable
/// </summary>
[Serializable]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class KD2DObstacle : KD2DArea
{
    #region "Fields"
    [SerializeField] private bool m_isDynamic;
    //---------------------------------------------------------------------------------------------
    #endregion //Fields

    #region "Properties"
    /// <summary>
    /// Determines if the obstacle can move or not, if we move an obstacle marked as Dynamic,
    /// all tiles inside of the objects will react to the area invasion
    /// of the dynamic obstacle, else tiles will not react to the invasion
    /// </summary>
    public bool IsDynamic { get { return m_isDynamic; } set { m_isDynamic = value; } }
    //---------------------------------------------------------------------------------------------
    #endregion //Properties

    #region "OnCode"
    public override float AreaPenalty()
    {
        return 0; //Areas has not penalty
    }
    //---------------------------------------------------------------------------------------------

    public override bool CanBlock()
    {
        return true;
    }
    //---------------------------------------------------------------------------------------------

    public override void OnTileEnter(KD2DTile a_tile)
    {
        if (m_isDynamic)
            a_tile.Occupied = true;
    }
    //---------------------------------------------------------------------------------------------

    public override void OnTileExit(KD2DTile a_tile)
    {
        if (m_isDynamic)
            a_tile.Occupied = false;
    }
    //---------------------------------------------------------------------------------------------
    #endregion //"OnCode"
}