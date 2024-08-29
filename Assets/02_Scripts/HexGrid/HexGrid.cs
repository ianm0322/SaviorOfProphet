using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    //    (-1, +1)    (0, +1)
    //  (-1, 0)     o     (+1, 0)
    //    (0, -1)     (+1, -1)
    static readonly int[] HexMoveX = { 0, -1, -1, 0, 1, 1 };
    static readonly int[] HexMoveY = { -1, 0, 1, 1, 0, -1 };

    HexNode[,] grid;

    [SerializeField]
    public int gridSizeX;

    [SerializeField]
    public int gridSizeY;



    [SerializeField]
    public float cellSizeX;

    [SerializeField]
    public float cellSizeY;


    private Vector2Int player;

    private void Awake()
    {
        int x = gridSizeX, y = gridSizeY;
        grid = new HexNode[x, y];

        gridSizeX = x;
        gridSizeY = y;

        for (int _x = 0; _x < x; ++_x)
        {
            for (int _y = 0; _y < y; ++_y)
            {
                Vector3 worldPos = Vector3.zero;

                worldPos.x = _x * cellSizeX + _y * cellSizeX * 0.5f;
                worldPos.z = _y * cellSizeY;

                grid[_x, _y] = new HexNode(new Vector2Int(_x, _y), worldPos, /*(Random.Range(0, 5) != 0)*/true);
            }
        }
    }

    public void MoveOnHex(ref Vector2Int pos, EHexDirection dir, int distance)
    {
        pos.x += HexMoveX[(int)dir] * distance;
        pos.y += HexMoveY[(int)dir] * distance;
    }

    public HexNode GetNode(Vector2Int pos)
    {
        if (grid == null)
            return null;

        if (pos.x < 0 || pos.x >= gridSizeX || pos.y < 0 || pos.y >= gridSizeY)
            return null;

        return grid[pos.x, pos.y];
    }

    public Vector3 GetWorldPosition(Vector2Int pos)
    {
        if (IsValidPosition(pos) == false)
            throw new System.IndexOutOfRangeException();

        return grid[pos.x, pos.y].WorldPosition;
    }

    public bool IsValidPosition(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < gridSizeX && pos.y >= 0 && pos.y < gridSizeY && grid[pos.x, pos.y].IsWalkable == true;
    }

    public static int GetDistance(Vector2Int v)
    {
        return v.x * v.y > 0 ? Mathf.Abs(v.x + v.y) : Mathf.Max(Mathf.Abs(v.x), Mathf.Abs(v.y));
    }

    private void OnDrawGizmos()
    {
        if (grid != null)
        {
            foreach (HexNode node in grid)
            {
                Gizmos.color = node.IsWalkable ? Color.white : Color.red;
                if (node.HexPosition == player)
                {
                    Gizmos.color = Color.green;
                }
                Gizmos.DrawSphere(node.WorldPosition, 0.1f);
            }
        }
    }
}
