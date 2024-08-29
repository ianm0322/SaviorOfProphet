using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Glorynuts.HexGridSystem
{
    public enum EGridAxis
    {
        XY,
        XZ
    }

    public class HexGridMap : MonoBehaviour
    {
        [SerializeField]
        int xSize, ySize;

        [SerializeField]
        float xDistance, yDistance;

        [SerializeField]
        GameObject tilePrefab;

        [SerializeField]
        EGridAxis gridAxis;

        public GameObject this[int x, int y]
        {
            get
            {
                return IsValidPosition(new HexPosition(x, y)) ? gridMap[x, y] : null;
            }
        }

        #region [ Private Field ]
        GameObject[,] gridMap;
        #endregion
        private void Awake()
        {
            // Setup grid map 2d array.
            gridMap = new GameObject[xSize, ySize];
            for (int _x = 0; _x < xSize; ++_x)
            {
                for (int _y = 0; _y < ySize; _y++)
                {
                    GameObject instance = Instantiate(tilePrefab);
                    gridMap[_x, _y] = instance;
                    gridMap[_x, _y].transform.position = TransformCoord(_x, _y);
                    instance.transform.SetParent(this.transform);
                }
            }
        }

        public Vector3 TransformCoord(int x, int y)
        {
            Vector3 ret = this.transform.position;

            switch (gridAxis)
            {
                case EGridAxis.XY:
                    ret.x -= xDistance * (xSize - 1 + ySize * 0.5f) * 0.5f;
                    ret.x += xDistance * (x + y * 0.5f);
                    ret.y -= yDistance * (ySize - 1) * 0.5f;
                    ret.y += yDistance * y;
                    break;
                case EGridAxis.XZ:
                    ret.x -= xDistance * (xSize - 1 + ySize * 0.5f) * 0.5f;
                    ret.x += xDistance * (x + y * 0.5f);
                    ret.z -= yDistance * (ySize - 1) * 0.5f;
                    ret.z += yDistance * y;
                    break;
            }

            return ret;
        }

        // Returns pos is inside of grid map bounds.
        public bool IsValidPosition(HexPosition pos)
        {
            return pos.x >= 0 && pos.x < xSize && pos.y >= 0 && pos.y < ySize;
        }

        // Returns node at pos of grid map
        public GameObject GetWorldPosition(HexPosition pos)
        {
            if (IsValidPosition(pos))
            {
                return gridMap[pos.x, pos.y];
            }
            else
            {
                return null;
            }
        }
    }
}