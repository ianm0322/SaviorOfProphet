using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EHexDirection
{
    RightUp,
    RightMid,
    RightDown,
    LeftDown,
    LeftMid,
    LeftUp,
}

public class HexNode
{
    // Hex ��ǥ ��ġ.
    public Vector2Int HexPosition;

    // ���� ��ġ.
    public Vector3 WorldPosition;

    public bool IsWalkable;

    public HexNode(Vector2Int hexPos, Vector3 worldPos, bool isWalkable)
    {
        HexPosition = hexPos;
        WorldPosition = worldPos;
        IsWalkable = isWalkable;
    }
}
