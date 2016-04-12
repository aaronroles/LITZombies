//-------------------------------------------------------------------------------------------------
//  ©Copyright KDToons & Games 2014
//-------------------------------------------------------------------------------------------------

using System;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The Agent give the capacity to demand routes to the KDNavigator2D
/// and move the object using "State Machine" coroutines.
/// </summary>
[Serializable]
public class KDNav2DAgent : MonoBehaviour
{
    #region "Fields"
#if UNITY_EDITOR
    private Color _color;
#endif
    [SerializeField] private float m_speed;
    [SerializeField] private Vector2 m_destination;
    [SerializeField] private bool m_repathThisAgent;

    //---------------------------------------------------------------------------------------------
    private static KDNav2D m_nav2D;
    private delegate IEnumerator StateMachine(List<KD2DTile> a_tiles);
    private StateMachine m_currentState;
    //---------------------------------------------------------------------------------------------

    public KD2DTile NearestTile { get; private set; }
    //---------------------------------------------------------------------------------------------
    #endregion //Fields

    #region "Events"
    #endregion //Events

    #region "Properties"
	
    /// <summary>
    /// Current KD Navigation Agent state machine
    /// </summary>
    public KDAgentStates States { get; private set; }
    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// Displacement speed of the agent
    /// </summary>
    public float Speed { get { return m_speed; } set { m_speed = value; } }
    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// Final point of the route
    /// </summary>
    public Vector2 Destination { get { return m_destination; } }
    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// Indicates if the agent can repath or not (If KDNavigator2D repath
    ///  settings is set to false "RepathThisAgent" prevails over KDNavigator2D repath)
    /// </summary>
    public bool RepathThisAgent { get { return m_repathThisAgent; } set { m_repathThisAgent = value; } }
    //---------------------------------------------------------------------------------------------

    #endregion //Properties

    #region "Monobehaviour"

    private void Start()
    {
#if UNITY_EDITOR
        _color = Color.red;
#endif
        m_nav2D = FindObjectsOfType<KDNav2D>().First();
        StartCurrentState(Wait, KDAgentStates.Waiting, null);
        NearestTile = m_nav2D.NearestNodeToPosition(transform.position);
    }
    //---------------------------------------------------------------------------------------------
    #endregion

    #region "OnCode"

    private void StartCurrentState
    (
        StateMachine a_nextState, 
        KDAgentStates a_agentState, 
        List<KD2DTile> a_steps
    )
    {
        StopAllCoroutines();
        m_currentState = a_nextState;
        States = a_agentState;
        StartCoroutine(m_currentState(a_steps));
    }
    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// Implement Wait Function
    /// </summary>
    /// <returns></returns>
    private IEnumerator Wait(object a_param)
    {
        States = KDAgentStates.Waiting;
        yield break;
    }
    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// Semi-implement Turn Function
    /// </summary>
    /// <param name="a_steps"></param>
    /// <returns></returns>
    private IEnumerator Turn(List<KD2DTile> a_steps)
    {
        StartCurrentState(Walk, KDAgentStates.Walking, a_steps);
        yield break;
    }
    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// Move over all p_tiles of the route and verify every step if the route has suffered some alterations
    /// </summary>
    /// <param name="a_tiles">full route for going from start to goal</param>
    /// <returns></returns>
    private IEnumerator Walk(List<KD2DTile> a_tiles)
    {
        if (a_tiles.Count > 0)
        {
            KD2DTile currentTile2DTile = a_tiles.Last();
            Vector2 currentStep = currentTile2DTile.transform.position;
            bool repathForObstacle;
            do
            {
#if UNITY_EDITOR
                var routePoints = a_tiles.Select
                (
                    a_tile => new Vector2(a_tile.transform.position.x, a_tile.transform.position.y)
                ).ToList();

                DrawRouteLines(routePoints, _color);
#endif
                transform.Translate
                (
                    new Vector2
                    (
                        currentStep.x - transform.position.x, 
                        currentStep.y - transform.position.y
                    ).normalized * Time.deltaTime * Speed
                );

                yield return null;
                repathForObstacle = a_tiles.Exists(a_tile => a_tile.Occupied);
            }
            while 
            (
                Vector2.Distance(transform.position, currentStep) > m_speed * Time.deltaTime && 
                !repathForObstacle
            );

            if (repathForObstacle || RepathThisAgent || m_nav2D.RepathAfterTile)
            {
                Vector3 destiation = a_tiles.First().transform.position;
                NearestTile = currentTile2DTile;
                SetDestination(destiation);
            }
            else
            {
                a_tiles.Remove(currentTile2DTile);
                
                KDAgentStates state;
                StateMachine stateCoroutine;

                if(a_tiles.Count == 0)
                {
                    stateCoroutine=Wait;
                    state = KDAgentStates.Waiting;                  
                }
                else
                {
                    stateCoroutine=Turn;
                    state=KDAgentStates.Turning;
                }
                NearestTile=currentTile2DTile;
                StartCurrentState(stateCoroutine, state, a_tiles);
            }
        }
        else { StartCurrentState(Wait, KDAgentStates.Waiting, a_tiles); }
    }
    //---------------------------------------------------------------------------------------------

#if UNITY_EDITOR
    private void DrawRouteLines(IList<Vector2> a_steps, Color a_color)
    {
        int i = 0;
        Debug.DrawLine(transform.position, a_steps.Last(), a_color);
        for (; i < a_steps.Count - 1; i++)
            Debug.DrawLine(a_steps[i], a_steps[i + 1], a_color);
    }
    //---------------------------------------------------------------------------------------------
#endif

    /// <summary>
    /// Set destination activate the agent for moving to the 2D point passed as a parameter
    /// </summary>
    /// <param name="a_destination">2D point passesd to tell the Agent must move to this point</param>
    public void SetDestination(Vector2 a_destination)
    {
        States = KDAgentStates.Demanding;
        m_destination = a_destination;
        m_nav2D.FindWay(this);
    }
    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// Recibe a list of tiles and estart to walk
    /// </summary>
    /// <param name="a_steps">A list of tiles of the flood filled grid that the agent must walk 
    /// for arrive to the destination point</param>
    public void SetRoute(List<KD2DTile> a_steps)
    {
        StartCurrentState(Turn, KDAgentStates.Turning, a_steps);
    }
    //---------------------------------------------------------------------------------------------
    #endregion //OnCode
}