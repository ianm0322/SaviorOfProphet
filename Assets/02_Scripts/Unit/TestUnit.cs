using Glorynuts.HexGridSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Glorynuts.ProphetsArc
{
    public class TestUnit : MonoBehaviour
    {
        [SerializeField]
        public HexPosition location;

        private HexGridMap gridSys;

        private void Start()
        {
            gridSys = FindObjectOfType<HexGridMap>();
            this.transform.position = gridSys[location.x, location.y].transform.position;
        }

        // 대상 포지션까지 일정속도로 이동한다.
        public void MoveTo(HexPosition nextPosition)
        {
            StartCoroutine(C_MoveTo(nextPosition));
        }

        private IEnumerator C_MoveTo(HexPosition nextPosition)
        {
            float speed = 5.0f;
            yield return null;

            Vector3 goalPosition = gridSys[nextPosition.x, nextPosition.y].transform.position;
            Vector3 pointy = goalPosition - this.transform.position;
            Vector3 pointyNormal = pointy.normalized;
            while (Vector3.SqrMagnitude(this.transform.position - goalPosition) > 0.01f)
            {
                this.transform.position = this.transform.position + pointyNormal * speed * Time.deltaTime;

                yield return null;
            }

            this.transform.position = goalPosition;
        }
    }
}