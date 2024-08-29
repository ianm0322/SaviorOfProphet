//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UIElements;

//public class HexGridObject : MonoBehaviour
//{
//    Vector2Int hexPos;
//    HexGrid grid;

//    public Vector2Int HexPosition
//    {
//        get
//        {
//            return hexPos;
//        }
//        set
//        {
//            hexPos = value;
//            if (grid != null)
//            {
//                HexNode currNode = grid.GetNode(hexPos);
//                if (currNode != null)
//                {
//                    this.transform.position = currNode.WorldPosition;
//                }
//            }
//        }
//    }

//    private void Start()
//    {
//        grid = FindObjectOfType<HexGrid>();

//        if (grid != null)
//        {
//            HexNode currNode = grid.GetNode(hexPos);
//            if (currNode != null)
//            {
//                this.transform.position = currNode.WorldPosition;
//            }
//        }

//        StartCoroutine(C_Move());
//    }

//    bool isRight = true;

//    private IEnumerator C_Move()
//    {
//        while (true)
//        {
//            yield return new WaitForSeconds(1.0f);

//            Vector2Int newPos = HexPosition;

//            if (isRight)
//            {
//                grid.MoveOnHex(ref newPos, EHexDirection.RightMid, 1);
//                if (newPos.x == grid.gridSizeX - 1)
//                    isRight = false;
//            }
//            else
//            {
//                grid.MoveOnHex(ref newPos, EHexDirection.RightMid, -1);
//                if (newPos.x == 0)
//                    isRight = true;
//            }

//            HexPosition = newPos;
//        }
//    }
//}
