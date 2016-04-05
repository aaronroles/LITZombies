//-------------------------------------------------------------------------------------------------
//  ©Copyright KDToons & Games 2014
//-------------------------------------------------------------------------------------------------

using UnityEngine;

/// <summary>
/// Use the KDMouseClickAnge for sending orders of displacement to KDNavigationAgent2D
/// </summary>
[RequireComponent(typeof(KDNav2DAgent))]
public class KDMouseClickAgent : MonoBehaviour
{
    #region "Fields"
    private KDNav2DAgent m_agent;
    //---------------------------------------------------------------------------------------------
    #endregion //Fields

    #region "Properties"

    /// <summary>
    /// Navigation Agent to set destination of this object
    /// </summary>
    public KDNav2DAgent Agent
    {
        get { return m_agent ?? (m_agent = GetComponent<KDNav2DAgent>()); }
    }
    //---------------------------------------------------------------------------------------------
    #endregion //Properties

    #region "Monobehaviour"
    protected void Update()
    {
        if (Input.GetMouseButtonDown(0) && Camera.main)
        {
            var mouseSpace = new Vector3
            (
                Input.mousePosition.x, 
                Input.mousePosition.y, 
                Camera.main.transform.position.z
            );

            Vector2 planePosition = Camera.main.ScreenToWorldPoint(mouseSpace);
            Agent.SetDestination(planePosition);
        }
    }
    //---------------------------------------------------------------------------------------------
    #endregion //Monobehaviour
}
