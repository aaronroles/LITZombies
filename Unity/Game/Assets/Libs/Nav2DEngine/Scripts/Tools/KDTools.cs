//-------------------------------------------------------------------------------------------------
//  ©Copyright KDToons & Games 2014
//-------------------------------------------------------------------------------------------------
using UnityEngine;

public static class KDTools
{
    /// <summary>
    /// Adjust one point to new scale and rotation
    /// </summary>
    /// <param name="p_point">Polygon Points</param>
    /// <param name="p_position">Current p_position</param>
    /// <param name="p_eulerAngles">rotation Angles</param>
    /// <param name="p_localScale">The local scale of the object</param>
    /// <returns></returns>
    public static Vector3 Adjust(Vector3 p_point, Vector3 p_position, Vector3 p_eulerAngles, Vector3 p_localScale)
    {
        var next = new Vector3(p_point.x * p_localScale.x, p_point.y * p_localScale.y, p_point.z * p_localScale.z);
        next = p_position + next;
        next = RotatePointAroundPivot(next, p_position, p_eulerAngles);
        return next;
    }

    /// <summary>
    /// Rotatate one p_point respect one transform rotation.
    /// </summary>
    /// <param name="p_point">p_point to rate</param>
    /// <param name="p_pivot">p_point of rotation reference</param>
    /// <param name="p_angles">Euler p_angles of rotation</param>
    /// <returns>p_point after rotation</returns>
    public static Vector3 RotatePointAroundPivot(Vector3 p_point, Vector3 p_pivot, Vector3 p_angles)
    {
        Vector3 dir = p_point - p_pivot;            // get p_point direction relative to p_pivot
        dir = Quaternion.Euler(p_angles) * dir;     // rotate it
        p_point = dir + p_pivot;                    // calculate rotated p_point
        return p_point;                             // return it
    }
}