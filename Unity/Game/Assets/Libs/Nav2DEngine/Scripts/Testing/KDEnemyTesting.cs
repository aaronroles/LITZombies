//-------------------------------------------------------------------------------------------------
//  ©Copyright KDToons & Games 2014
//-------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// Testing Script to generate several IAS and shows the performace of KD2D Path Engine (For testing)
/// </summary>
public class KDEnemyTesting : MonoBehaviour
{
    #region "Fields"
    private KDNav2DAgent _agent;
    #endregion //Fields

    #region "Properties"
    /// <summary>
    /// Navigation Agent of the enemy
    /// </summary>
    public KDNav2DAgent Agent { get { return _agent ?? (_agent = GetComponent<KDNav2DAgent>()); } }
    #endregion //Properties

    protected void Start()
    {
        StartCoroutine(Patrol());
    }

    /// <summary>
    /// Patrol over time changing the destination every random interval.
    /// </summary>
    /// <returns></returns>
    public IEnumerator Patrol()
    {
        while (true)
        {
            if (Agent.States == KDAgentStates.Waiting)
            {
                var cameraPoint = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 5f);
                Vector2 spacePosition = Camera.main.ScreenToWorldPoint(cameraPoint);
                Agent.SetDestination(spacePosition);
            }
            yield return new WaitForSeconds(Random.Range(1, 5));
        }
    }
}
