//-------------------------------------------------------------------------------------------------
//  ©Copyright KDToons & Games 2014
//-------------------------------------------------------------------------------------------------

using UnityEngine;

public class KDObstacleMover : MonoBehaviour
{
    [Range(1, 150)]
    public float RotationSpeed;
    public Transform _rotationPivot;

    protected void Update()
    {
        transform.RotateAround(_rotationPivot.position, Vector3.forward, Time.deltaTime * RotationSpeed);
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
    }
}