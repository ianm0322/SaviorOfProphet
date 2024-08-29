using Glorynuts.HexGridSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Glorynuts.ProphetsArc
{
    public class TestUnit : MonoBehaviour
    {
        [SerializeField]
        public HexPosition location;

        [SerializeField]
        float moveSpeed = 5.0f;

        private HexGridMap gridSys;

        private void Start()
        {
            gridSys = FindObjectOfType<HexGridMap>();
            this.transform.position = gridSys[location.x, location.y].transform.position;
        }

        public void InputEvent(InputAction a)
        {
        }

        public void MoveRight()
        {
            MoveTo(location + HexPosition.RightMid);
        }

        public void MoveLeft()
        {
            MoveTo(location + HexPosition.LeftMid);
        }

        // 대상 포지션까지 일정속도로 이동한다.
        public void MoveTo(HexPosition nextPosition)
        {
            StartCoroutine(C_MoveTo(nextPosition));
        }

        private IEnumerator C_MoveTo(HexPosition nextPosition)
        {
            yield return null;

            Vector3 goalPosition = gridSys[nextPosition.x, nextPosition.y].transform.position;
            Vector3 pointy = goalPosition - this.transform.position;
            Vector3 pointyNormal = pointy.normalized;
            while (Vector3.SqrMagnitude(this.transform.position - goalPosition) > 0.01f)
            {
                this.transform.position = this.transform.position + pointyNormal * moveSpeed * Time.deltaTime;

                yield return null;
            }

            this.transform.position = goalPosition;
            location = nextPosition;
        }
    }
}