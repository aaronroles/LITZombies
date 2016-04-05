//-------------------------------------------------------------------------------------------------
//  ©Copyright KDToons & Games 2014
//-------------------------------------------------------------------------------------------------

using System;
using UnityEngine;

/// <summary>
/// The seed of the flood fill Tiles. Move the object on the Editor to place the Seed in the best 
/// place to obtain the best distribution of your tiles
/// </summary>
[Serializable]
public class KDNav2DSeed : MonoBehaviour
{
    #region "Fields"
    [SerializeField] private bool m_init;
    [SerializeField] private Vector3? m_lastPosition;
    [SerializeField] private PolygonCollider2D m_navigationArea;
    [SerializeField] private KDNav2D m_navigator;
    #endregion

    #region "MonoBehaviour"

    protected void Start()
    {
        Init();
    }
    //----------------------------------------------------------------------------------------------

    protected void OnDrawGizmos()
    {
        Init();

        Vector3 position = transform.position;
        if (m_lastPosition.HasValue && m_lastPosition.Value != position)
        {
            m_navigator.Remove();
            if (!m_navigationArea.OverlapPoint(position))
                transform.position = m_navigationArea.transform.position;
        }

        Gizmos.DrawIcon(position, "FloodFillSeed.png", true);
        m_lastPosition = position;
    }
    //----------------------------------------------------------------------------------------------

    private void Init()
    {
        m_navigationArea = m_navigationArea ?? transform.parent.GetComponent<PolygonCollider2D>();
        m_navigator = m_navigator ?? transform.parent.GetComponent<KDNav2D>();
    }
    //----------------------------------------------------------------------------------------------
    #endregion //MonoBehaviour
}