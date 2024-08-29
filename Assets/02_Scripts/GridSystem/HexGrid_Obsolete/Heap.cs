using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Heap<T> where T : IComparable<T>
{
    List<T> heap;

    int count = 0;

    public int Count => count;

    public List<T> HeapArray => heap;

    public Heap(int capacity)
    {
        heap = new List<T>(capacity);
        heap.AddRange(new T[capacity]);
    }

    public void Push(T item)
    {
        if (count == heap.Count)
            return;

        heap[count] = item;
        SortUp(item, count);
        ++count;
    }

    public T Pop()
    {
        if (count == 0)
            return default(T);

        T result = heap[0];
        --count;
        T item = heap[0] = heap[count];
        SortDown(item, 0);
        return result;
    }

    public T Peek()
    {
        if (count == 0)
            return default(T);

        return heap[0];
    }

    public void Update(T item)
    {
        int i = heap.IndexOf(item);

        if (i == -1) return;

        SortUp(item, heap.IndexOf(item));
        SortDown(item, heap.IndexOf(item));
    }

    private void SortUp(T item, int index)
    {
        int currentIdx = index;

        // compare to up
        while (currentIdx > 0)
        {
            int parent = Parent(currentIdx);

            if (item.CompareTo(heap[parent]) >= 0)
            {
                break;
            }

            heap[currentIdx] = heap[parent];
            currentIdx = parent;
        }

        heap[currentIdx] = item;
    }

    private void SortDown(T item, int index)
    {
        int currentIdx = index;
        while (currentIdx < count)
        {
            // if left is out, break.
            // if right is out
            int left = LeftChild(currentIdx);
            int right = RightChild(currentIdx);
            int compareIdx;
            if (left >= count)
                break;

            compareIdx = right >= count || heap[left].CompareTo(heap[right]) < 0 ? left : right;

            if (item.CompareTo(heap[compareIdx]) <= 0)
            {
                break;
            }

            heap[currentIdx] = heap[compareIdx];
            currentIdx = compareIdx;
        }

        heap[currentIdx] = item;
    }

    private int Parent(int i) => (i - 1) / 2;

    private int LeftChild(int i) => i * 2 + 1;

    private int RightChild(int i) => i * 2 + 2;
}
