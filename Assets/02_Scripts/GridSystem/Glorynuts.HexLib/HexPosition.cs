using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EHexAxis
{
    X = 0,  // axis of (1, 0)
    Y,  // axis of (0, 1)
    Z   // axis of (1, -1)
}

[Serializable]
public struct HexPosition
{
    public static HexPosition RightMid = new HexPosition(1, 0);
    public static HexPosition RightUp = new HexPosition(0, 1);
    public static HexPosition RightDown = new HexPosition(1, -1);
    public static HexPosition LeftMid = new HexPosition(-1, 0);
    public static HexPosition LeftDown = new HexPosition(0, -1);
    public static HexPosition LeftUp = new HexPosition(-1, 1);
    public static HexPosition AxisX = RightMid;
    public static HexPosition AxisY = RightUp;
    public static HexPosition AxisZ = RightDown;
    public static HexPosition Zero = new HexPosition(0, 0);

    [SerializeField]
    public int x, y;

    public HexPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void Set(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static HexPosition operator +(HexPosition lhs, HexPosition rhs)
    {
        return new HexPosition(lhs.x + rhs.x, lhs.y + rhs.y);
    }

    public static HexPosition operator -(HexPosition lhs, HexPosition rhs)
    {
        return new HexPosition(lhs.x - rhs.x, lhs.y - rhs.y);
    }

    public static HexPosition operator *(HexPosition lhs, int scalar)
    {
        return new HexPosition(lhs.x * scalar, lhs.y * scalar);
    }

    public static HexPosition operator *(int scalar, HexPosition lhs)
    {
        return new HexPosition(lhs.x * scalar, lhs.y * scalar);
    }

    public static HexPosition operator /(HexPosition lhs, int scalar)
    {
        return new HexPosition(lhs.x / scalar, lhs.y / scalar);
    }

    public static int HexDistance(HexPosition v)
    {
        return v.x * v.y > 0 ? Mathf.Abs(v.x + v.y) : Mathf.Max(Mathf.Abs(v.x), Mathf.Abs(v.y));
    }

    public static int HexDistance(HexPosition a, HexPosition b)
    {
        var v = b - a;
        return v.x * v.y > 0 ? Mathf.Abs(v.x + v.y) : Mathf.Max(Mathf.Abs(v.x), Mathf.Abs(v.y));
    }

    public static (int x, int y, int z) SplitHexAxis(HexPosition hex)
    {
        // if hex contains axis-z.
        if (hex.x * hex.y < 0)
        {
            int _z = hex.x + hex.y;
            hex -= _z * AxisZ;
            return (hex.x, hex.y, _z);
        }
        // no axis-z.
        else
        {
            return (hex.x, hex.y, 0);
        }
    }

    public override string ToString()
    {
        return $"({x}, {y})";
    }
}
