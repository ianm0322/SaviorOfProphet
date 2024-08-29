using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPathFinding<TNode> where TNode : IComparable<TNode>
{
    private class PFNode : IComparable<PFNode>
    {
        public readonly int index;
        public readonly TNode node;
        public int parentIndex;
        public int gCost;
        public int hCost;

        public int fCost => gCost + hCost;

        public PFNode(int index, TNode node, int parentIndex, int gCost, int hCost)
        {
            this.index = index;
            this.node = node;
            this.parentIndex = parentIndex;
            this.gCost = gCost;
            this.hCost = hCost;
        }

        public int CompareTo(PFNode other)
        {
            return fCost.CompareTo(other.fCost);
        }
    }

    IPathFindingMap<TNode> map;
    PFNode[] nodes;

    List<TNode> _nodeList = new List<TNode>();

    public GenericPathFinding(IPathFindingMap<TNode> map)
    {
        this.map = map;
        int size = map.MapSize;
        nodes = new PFNode[size];

        for (int i = 0; i < size; ++i)
        {
            nodes[i] = new PFNode(i, map.IndexToNode(i), -1, 0, 0);
        }
    }

    public TNode[] PathFinding(TNode start, TNode goal)
    {
        if (map.IsValidNode(start) && map.IsValidNode(goal))
        {
            return new TNode[0];
        }

        Heap<PFNode> openSet = new Heap<PFNode>(nodes.Length);
        HashSet<PFNode> closeSet = new HashSet<PFNode>();

        var startNode = nodes[map.NodeToIndex(start)];
        startNode.hCost = map.EvaluateHuristicCost(start, goal);
        openSet.Push(startNode);

        while (openSet.Count > 0)
        {
            // start loop process.
            PFNode currentNode = openSet.Pop();
            closeSet.Add(currentNode);
            if (map.CheckGoal(currentNode.node, goal))
            {
                return RetraceNodes(goal);
            }

            // add neighbor to open set.
            _nodeList.Clear();
            map.GetNeighbor(_nodeList, currentNode.node);

            foreach (var neighbor in _nodeList)
            {
                PFNode neighborNode = nodes[map.NodeToIndex(neighbor)];
                int neighborGCost = map.EvaluateGCost(currentNode.node, neighbor);

                if (closeSet.Contains(neighborNode))
                {
                    continue;
                }

                // open set contains neighbor
                if (openSet.HeapArray.Contains(neighborNode))
                {
                    if (neighborNode.gCost.CompareTo(neighborGCost) > 0)
                    {
                        neighborNode.gCost = neighborGCost;
                        neighborNode.parentIndex = currentNode.index;
                        openSet.Update(neighborNode);
                    }
                }
                else
                {
                    neighborNode.parentIndex = currentNode.index;
                    neighborNode.gCost = neighborGCost;
                    neighborNode.hCost = map.EvaluateHuristicCost(neighbor, goal);
                    openSet.Push(neighborNode);
                }
            }
        }
        return new TNode[0];
    }

    private TNode[] RetraceNodes(TNode goalIndex, bool containStartNode = true)
    {
        _nodeList.Clear();

        int currentIndex = map.NodeToIndex(goalIndex);

        while (nodes[currentIndex].parentIndex != -1)
        {
            _nodeList.Add(nodes[currentIndex].node);
            currentIndex = nodes[currentIndex].parentIndex;
        }

        if(containStartNode)
        {
            _nodeList.Add(nodes[currentIndex].node);
        }

        _nodeList.Reverse();

        return _nodeList.ToArray();
    }
}
