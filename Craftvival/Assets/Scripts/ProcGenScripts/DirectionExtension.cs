using System;
using UnityEngine;

public static class DirectionExtension
{
    public static Vector3Int GetVector(this Direction direction)
    {
        return direction switch
        {
            Direction.foreward => Vector3Int.forward,
            Direction.backwards => Vector3Int.back,
            Direction.right => Vector3Int.right,
            Direction.left => Vector3Int.left,
            Direction.up => Vector3Int.up,
            Direction.down => Vector3Int.down,
            _ => throw new Exception("Invalid Direction")
        };
    }
}
