//-------------------------------------------------------------------------------------------------
//  ©Copyright KDToons & Games 2014
//-------------------------------------------------------------------------------------------------

using System;
using UnityEngine;

/// <summary>
/// KDNavigationObject is an abstract class allows to create different elements inside of 
/// the navigation flood filled grill. Areas or zones (for example)
/// </summary>
[Serializable]
[RequireComponent(typeof(PolygonCollider2D))]
public abstract class KD2DArea : MonoBehaviour
{
    #region "Fields"
    [SerializeField]
    private GameObject m_linkedObject;
    private PolygonCollider2D m_pCollider;
    //---------------------------------------------------------------------------------------------
    #endregion //Fields

    #region "Properties"

    /// <summary>
    /// Is the (generally the sprite) gameobject that this area overlaps
    /// </summary>
    public GameObject LinkedObject
    {
        get { return m_linkedObject; }
        set { m_linkedObject = value; }
    }
    //---------------------------------------------------------------------------------------------
    #endregion //Properties

    #region "MonoBehaviour"
    protected virtual void Start()
    {
        GetComponent<PolygonCollider2D>().isTrigger = true;

        if (GetComponent<Rigidbody2D>() == null)
            gameObject.AddComponent<Rigidbody2D>();

        if (GetComponents<KD2DArea>().Length > 1)
            throw new Exception("Only one Component of KD2DArea can be attached");
    }
    //---------------------------------------------------------------------------------------------

    protected virtual void Update()
    {
        Follow();
    }
    //---------------------------------------------------------------------------------------------

    protected virtual void OnDrawGizmos()
    {
        Follow();

        if (!m_pCollider)
            m_pCollider = GetComponent<PolygonCollider2D>();

        Vector2[] points = m_pCollider.points;
        Vector2 p2D = transform.position;

        if (points == null) return;

        Vector3 pointCurrent, pointNext;
        Vector3 angles = transform.rotation.eulerAngles;

        for (var i = 0; i < points.Length - 1; i++)
        {
            pointCurrent = KDTools.Adjust(points[i], p2D, angles, transform.localScale);
            pointNext = KDTools.Adjust(points[i + 1], p2D, angles, transform.localScale);
            Gizmos.DrawLine(pointCurrent, pointNext);
        }

        pointCurrent = KDTools.Adjust
                       (
                            points[points.Length - 1], 
                            p2D, 
                            angles,
                            transform.localScale
                        );

        pointNext = KDTools.Adjust(points[0], p2D, angles, transform.localScale);

        Gizmos.DrawLine(pointCurrent, pointNext);
    }
    //---------------------------------------------------------------------------------------------
    #endregion //MonoBehaviour

    #region "OnCode"
    /// <summary>
    /// Put the "polygon guizmo" exactly on the same place that the linked object is 
    /// (This object can be null)
    /// WARNING!!: If the scale change in all frames, OnTriggerEnter2D Will not work.
    /// </summary>
    protected virtual void Follow()
    {
        if (m_linkedObject)
        {
            transform.position = m_linkedObject.transform.position;
            transform.rotation = m_linkedObject.transform.rotation;
            if (transform.localScale != m_linkedObject.transform.localScale) 
                transform.localScale = m_linkedObject.transform.localScale;
        }
    }
    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// Say if the Navigator Object Block a route
    /// </summary>
    /// <returns></returns>
    public abstract bool CanBlock();
    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// Return the value of the A* Manhatan cost of the Navigation Object
    /// bigger values indicates more difficults to walk over this objects
    /// </summary>
    /// <returns>AreaPenalty value of the tiles inside of the navigation Object</returns>
    public abstract float AreaPenalty();
    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// The interaction between the tiles inside of the navigation Area when the are enter on the p_tile
    /// </summary>
    /// <param name="a_tile"></param>
    public abstract void OnTileEnter(KD2DTile a_tile);
    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// The interaction between the tiles inside of the navigation Area when the area leaves the p_tile
    /// </summary>
    /// <param name="a_tile"></param>
    public abstract void OnTileExit(KD2DTile a_tile);
    //---------------------------------------------------------------------------------------------
    #endregion //OnCode
}
