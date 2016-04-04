//-------------------------------------------------------------------------------------------------
//  ©Copyright KDToons & Games 2014
//-------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class KDBinaryTile : ISerializable
{
    public KDBinaryTile() { }

    #region "Properties"
    /// <summary>
    /// Indicates if this Tile is occupied by one obstacle
    /// </summary>
    public bool Occupied { get; set; }
    public KDVector2 TilePosition { get; set; }
    //----------------------------------------------------------------------------------------------
    #endregion //Properties

    public void GetObjectData(SerializationInfo a_info, StreamingContext a_context)
    {
        a_info.AddValue("Occupied", Occupied, typeof(bool));
        a_info.AddValue("TilePosition", TilePosition, typeof(Vector2));
    }
    //----------------------------------------------------------------------------------------------

    // The special constructor is used to deserialize values. 
    public KDBinaryTile(SerializationInfo a_info, StreamingContext a_context)
    {
        // Reset the property value using the GetValue method.
        Occupied = (bool)a_info.GetValue("Occupied", typeof(bool));
        TilePosition = (KDVector2)a_info.GetValue("TilePosition", typeof(KDVector2));
    }
    //----------------------------------------------------------------------------------------------
}

[Serializable]
public class KDVector2 : ISerializable
{
    public KDVector2() { }

    #region "Properties"
    /// <summary>
    /// Indicates if this Tile is occupied by one obstacle
    /// </summary>
    public float X { get; set; }
    public float Y { get; set; }
    //----------------------------------------------------------------------------------------------
    #endregion //Properties

    public void GetObjectData(SerializationInfo a_info, StreamingContext a_context)
    {
        a_info.AddValue("X", X, typeof(float));
        a_info.AddValue("Y", Y, typeof(float));
    }
    //----------------------------------------------------------------------------------------------

    // The special constructor is used to deserialize values. 
    public KDVector2(SerializationInfo a_info, StreamingContext a_context)
    {
        // Reset the property value using the GetValue method.
        X = (float)a_info.GetValue("X", typeof(float));
        Y = (float)a_info.GetValue("Y", typeof(float));
    }
    //----------------------------------------------------------------------------------------------
}
