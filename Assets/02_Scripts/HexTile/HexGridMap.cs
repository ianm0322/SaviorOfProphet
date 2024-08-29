using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Glorynuts.HexGridMap
{
    public class HexGridMapNode
    {
        public HexPosition gridPosition;
        public Vector3 worldPosition;

        public HexPosition GridPosition
        {
            get => gridPosition;
            set => gridPosition = value;
        }

        public HexGridMapNode(int x, int y)
        {
            this.GridPosition = new HexPosition(x, y);
        }
    }

    public class HexGridMap : MonoBehaviour
    {
        [SerializeField]
        int xSize, ySize;

        [SerializeField]
        GameObject tilePrefab;


        #region [ Private Field ]
        HexGridMapNode[,] gridMap;
        #endregion
        private void Awake()
        {
            // Setup grid map 2d array.
            gridMap = new HexGridMapNode[xSize, ySize];
            for (int _x = 0; _x < xSize; ++_x)
            {
                for (int _y = 0; _y < ySize; _y++)
                {
                    gridMap[_x, _y] = new HexGridMapNode(_x, _y);
                }
            }
        }

        // Returns pos is inside of grid map bounds.
        public bool IsValidPosition(HexPosition pos)
        {
            return pos.x >= 0 && pos.x < xSize && pos.y >= 0 && pos.y < ySize;
        }

        // Returns node at pos of grid map
        public Vector3? GetWorldPosition(HexPosition pos)
        {
            if (IsValidPosition(pos))
            {
                return gridMap[pos.x, pos.y].worldPosition;
            }
            else
            {
                return null;
            }
        }
    }
}