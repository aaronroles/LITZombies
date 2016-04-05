//-------------------------------------------------------------------------------------------------
//  ©Copyright KDToons & Games 2014
//-------------------------------------------------------------------------------------------------

using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

/// <summary>
/// Testing Script to generate several IAS and shows the performace of KD2D Path Engine (For testing)
/// </summary>
[Serializable]
public class KDEnemyTestingPingPong : MonoBehaviour
{
    #region "Fields"
    [SerializeField]
    private Transform[] _patrolPoints;
    private int _index;
    #endregion //Fields

    protected void Start()
    {
        _index = 1;
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
            Vector2 spacePosition = _patrolPoints[_index].position;

            if (Vector2.Distance(spacePosition, transform.position) < 0.1f)
                _index = _index + 1 == _patrolPoints.Length ? 0 : _index + 1;

            Vector2 direction = (spacePosition - (Vector2)transform.position).normalized;
            transform.Translate(direction * Time.deltaTime);

            yield return null; // new WaitForSeconds(Random.Range(1, 5));
        }
    }
}
