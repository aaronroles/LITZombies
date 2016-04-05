//-------------------------------------------------------------------------------------------------
//  ©Copyright KDToons & Games 2014
//-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Zones2D represents areas inside of the Main navigation polygon that can be walkable but has some penalty for walk over ther (sand, fire zone, small river, zones with a lot fo enemies)
/// </summary>
[Serializable]
[RequireComponent(typeof(PolygonCollider2D))]
public class KD2DZone : KD2DArea
{
    #region "Field"
    [SerializeField]
    protected float m_penaltyPercentage;
    protected List<KD2DTile> m_overlapedTiles;
    //---------------------------------------------------------------------------------------------
    #endregion //Fields

    #region "Properties

    /// <summary>
    /// Indicates the percentage of penalty that this zone has over the euristic cost of all 
    /// tiles inside of this area.
    /// </summary>
    public float PenaltyPercentage
    {
        get { return m_penaltyPercentage; } set { m_penaltyPercentage = value; }
    }
    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// Collection of all overlaped tiles of this area
    /// </summary>
    protected List<KD2DTile> OverlapedTiles
    {
        get { return m_overlapedTiles ?? (m_overlapedTiles = new List<KD2DTile>()); } 
        set { m_overlapedTiles = value; }
    }
    //---------------------------------------------------------------------------------------------
    #endregion //Properties

    #region "Monobehaviour"
    protected override void Start()
    {
        base.Start();
        if (Math.Abs(m_penaltyPercentage) < float.Epsilon)
            Debug.LogWarning("One navigator 2D Zone has no penalty");
    }
    //---------------------------------------------------------------------------------------------
    #endregion///"Monobehaviour"

    #region "OnCode"

    public override bool CanBlock()
    {
        return false;
    }
    //---------------------------------------------------------------------------------------------

    public override float AreaPenalty()
    {
        return m_penaltyPercentage;
    }
    //---------------------------------------------------------------------------------------------

    public override void OnTileExit(KD2DTile a_tile)
    {
        a_tile.Penalty -= m_penaltyPercentage;
        OverlapedTiles.Remove(a_tile);
    }
    //---------------------------------------------------------------------------------------------

    public override void OnTileEnter(KD2DTile a_tile)
    {
        if (!OverlapedTiles.Contains(a_tile))
        {
            a_tile.Penalty += m_penaltyPercentage;
            OverlapedTiles.Add(a_tile);
        }
    }
    //---------------------------------------------------------------------------------------------
    #endregion
}