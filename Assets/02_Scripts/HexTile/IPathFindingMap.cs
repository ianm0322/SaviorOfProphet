using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathFindingMap<TNode> where TNode : IComparable<TNode>
{
    int MapSize { get; }

    int EvaluateGCost(TNode from, TNode to);
    int EvaluateHuristicCost(TNode from, TNode to);
    bool CheckGoal(TNode a, TNode b);
    int NodeToIndex(TNode node);
    TNode IndexToNode(int index);
    void GetNeighbor(List<TNode> neighbors, TNode node);
    bool IsValidNode(TNode node);
}
