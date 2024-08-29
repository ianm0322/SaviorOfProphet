using System;
using System.Collections.Generic;

namespace Glorynuts
{
    public class HeapTree<T>
    {
        public delegate int Comparer(T data1, T data2);

        private T[] array;
        private int rear = 0, capacity;

        private Comparer comparer;

        public int Count => rear;
        public int Capacity => capacity;

        public HeapTree(int capacity, Comparer comparer)
        {
            array = new T[capacity];
            this.comparer = comparer;
            this.capacity = capacity;
        }

        public HeapTree(int capacity, IComparer<T> icomparer) : this(capacity, icomparer.Compare)
        {
        }

        public void Push(T data)
        {
            // queue가 꽉 차면 exception.
            if (rear == array.Length - 1)
            {
                throw new System.IndexOutOfRangeException();
            }

            // 마지막 index 증가.
            rear++;

            // iterator = 마지막 칸부터 시작.
            int parent, iterator = rear;

            while (iterator > 1)
            {
                parent = GetParent(iterator);

                // 만약 iterator가 parent보다 우선순위가 높으면 iterator 위치에 parent data 저장하고 iterator를 parent로 이동.
                if (comparer(data, array[parent]) > 0)
                {
                    array[iterator] = array[parent];
                    iterator = parent;
                }
                else
                {
                    break;
                }
            }

            // data가 parent보다 작을 때, parent의 child에 data 삽입.
            array[iterator] = data;
        }

        public T Pop()
        {
            // queue에 데이터가 없으면 exception.
            if (rear == 0)
            {
                throw new System.IndexOutOfRangeException();
            }

            // queue의 첫 데이터(반환값)을 저장.
            T result = array[1];
            int child_left, child_right, next, iterator = 1;

            // iterator가 children보다 작을 때까지 반복.
            while (iterator < rear)
            {
                // 만약 iterator의 chlid가 없다면 루프 종료.
                child_left = GetChildLeft(iterator);
                if (child_left > rear)
                    break;

                // 만약 iterator의 left child가 있고 right child가 없다면 next child = left child.
                child_right = GetChildRight(iterator);
                if (child_right > rear)
                {
                    next = child_left;
                }
                // child가 모두 있으면 left와 right 중 우선순위가 더 높은 값을 next로 정함.
                else
                {
                    next = comparer(array[child_left], array[child_right]) > 0 ? child_left : child_right;
                }

                // 마지막 데이터(rear)가 next보다 작은지 검사. 만약 작다면 현재 위치에 array[next]를 옮기고 iterator를 next로 이동.
                if (comparer(array[rear], array[next]) > 0)
                {
                    break;
                }
                else
                {
                    array[iterator] = array[next];
                    iterator = next;
                }
            }

            // 마지막 데이터(rear)를 iterator 위치에 저장.
            array[iterator] = array[rear];

            // 기존 rear 위치 삭제.
            array[rear] = default;
            rear--;

            return result;
        }

        public T Peek()
        {
            if (rear == 0)
            {
                throw new System.IndexOutOfRangeException();
            }

            return array[1];
        }

        public void Clear()
        {
            for (int i = 1; i <= rear; i++)
            {
                array[i] = default;
            }
            rear = 0;
        }

        public T[] Values => array;

        public T[] ToArray()
        {
            T[] result = new T[Count];
            for (int i = 0; i < rear; i++)
            {
                result[i] = array[i + 1];
            }
            return result;
        }

        private int GetParent(int child)
        {
            return child / 2;
        }

        private int GetChildLeft(int parent)
        {
            return parent * 2;
        }

        private int GetChildRight(int parent)
        {
            return parent * 2 + 1;
        }
    }
}