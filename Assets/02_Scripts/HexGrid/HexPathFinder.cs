using Glorynuts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class HexPathFinder : MonoBehaviour
{
    public class HexPathNode : IComparable<HexPathNode>
    {
        public HexPathNode parent;

        public int g, h;

        public Vector2Int hexPos;

        public int F => g + h;

        public HexPathNode(Vector2Int pos, HexPathNode parent, int g, int h)
        {
            hexPos = pos;
            this.parent = parent;
            this.g = g;
            this.h = h;
        }

        public int CompareTo(HexPathNode other)
        {
            int comp = this.F.CompareTo(other.F);

            return this.F.CompareTo(other.F);
        }
    }

    [SerializeField]
    private HexGrid grid;
    private Vector2Int start;
    private Vector2Int goal;

    private HexPathNode[,] Nodes;

    private void Start()
    {
        Nodes = new HexPathNode[grid.gridSizeX, grid.gridSizeY];
        for (int _x = 0; _x < grid.gridSizeX; ++_x)
        {
            for (int _y = 0; _y < grid.gridSizeY; ++_y)
            {
                Nodes[_x, _y] = new HexPathNode(new Vector2Int(_x, _y), null, 0, 0);
            }
        }
    }

    public Vector2Int[] PathFinding(Vector2Int start, Vector2Int goal, HexGrid grid)
    {
        this.grid = grid;
        this.start = start;
        this.goal = goal;

        Heap<HexPathNode> openSet = new(grid.gridSizeX * grid.gridSizeY);
        HashSet<HexPathNode> closeSet = new();

        openSet.Push(new HexPathNode(start, null, 0, GetHuristic(start)));
        while (openSet.Count > 0)
        {
            HexPathNode currentNode = openSet.Pop();
            closeSet.Add(currentNode);
            if (currentNode.hexPos == goal)
            {
                // ∏Æ∆Æ∑π¿ÃΩÃ
                Vector2Int[] path = RetracePath(currentNode);
                return path;
            }

            List<Vector2Int> neighbors = GetNeighbors(currentNode.hexPos);

            // Add neighbors.
            // if neighbor is in closeSet, pass it.
            // if neighbor is in openSet AND neighbor's f cost is lower than already node, update node in openSet.
            foreach (var neighbor in neighbors)
            {
                HexPathNode neighborNode = Nodes[neighbor.x, neighbor.y];
                int newGCost = currentNode.g + 10;

                if (closeSet.Contains(neighborNode))
                {
                    continue;
                }

                if (openSet.HeapArray.Contains(neighborNode))
                {
                    if (neighborNode.g.CompareTo(newGCost) > 0)
                    {
                        neighborNode.g = newGCost;
                        neighborNode.parent = currentNode;
                        openSet.Update(neighborNode);
                    }
                }
                else
                {
                    neighborNode.g = newGCost;
                    neighborNode.h = HexGrid.GetDistance(goal - neighbor) * 10;
                    //neighborNode.h = (int)(Vector3.Distance(grid.GetNode(neighbor).WorldPosition, grid.GetNode(goal).WorldPosition) * 10);    // Huristic oriented of world position.
                    neighborNode.parent = currentNode;
                    openSet.Push(neighborNode);
                }
            }
        }

        return null;
    }

    public Vector2Int[] RetracePath(HexPathNode node)
    {
        List<Vector2Int> path = new();
        while (node.parent != null)
        {
            path.Add(node.hexPos);
            node = node.parent;
        }
        path.Reverse();
        return path.ToArray();
    }

    private int GetHuristic(Vector2Int v)
    {
        return HexGrid.GetDistance(goal - v);
    }

    // Returns valid position of neighbors
    private List<Vector2Int> GetNeighbors(Vector2Int pos)
    {
        Vector2Int v;
        List<Vector2Int> results = new();

        for (int i = 0; i < 6; ++i)
        {
            v = pos;
            grid.MoveOnHex(ref v, (EHexDirection)i, 1);
            if (grid.IsValidPosition(v))
            {
                results.Add(v);
            }
        }

        return results;
    }
}
