﻿using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Stack_P
{

    //스택 : 후입선출 (LIFO)
    //큐 : 선입선출 (FIFO)
    //트리 = 재귀함수

    class PriorityQueue<T> where T : IComparable<T>
    {

        List<T> _heap = new List<T>();

        //0logN
        public void Push(T data)
        {
            //힙의 맨 끝에 새로운 데이터 삽입
            _heap.Add(data);

            int now = _heap.Count - 1;

            //도장깨기...
            while (now > 0)
            {

                int next = (now - 1) / 2;
                //now가 더 작을 경우 종료
                if (_heap[now].CompareTo(_heap[next]) < 0)
                    break;

                //값 교체 (now가 더 클 경우
                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                //다음 도장으로 이동
                now = next;
            }
        }

        //0logN
        public T Pop()
        {
            //반환할 데이터를 따로 저장
            T ret = _heap[0];

            //마지막 데이터를 루트로 이동
            int lastindex = _heap.Count - 1;
            _heap[0] = _heap[lastindex];
            _heap.RemoveAt(lastindex);
            lastindex--;

            int now = 0;
            while (true)
            {
                int left = 2 * now + 1;
                int right = 2 * now + 2;

                int next = now;
                //왼쪽값이 현재값보다 크면, 왼쪽으로 이동
                if (left <= lastindex && _heap[next].CompareTo(_heap[left]) < 0) 
                    next = left;

                //오른쪽값이 현재값보다 크면, 오른쪽으로 이동
                if (right <= lastindex && _heap[next].CompareTo(_heap[right]) < 0)
                    next = right;

                //왼 오 모두 현재값보다 작으면 종료
                if (next == now)
                    break;

                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                now = next;

            }

            return ret;
        }

        public int Count()
        {
            return _heap.Count;
        }
    }

    class Knight : IComparable<Knight>
    {
        public int id { get; set; }

        public int CompareTo(Knight? other)
        {
            if (id == other.id)
                return 0;
            return id > other.id ? 1 : -1;

        }
    }


    class Graph
    {

        class FileName
        {


            static void Main(string[] args)
            {

                PriorityQueue<Knight> q = new PriorityQueue<Knight>();
                q.Push(new Knight() { id = 20 });
                q.Push(new Knight() { id = 30 });
                q.Push(new Knight() { id = 40 });
                q.Push(new Knight() { id = 60 });
                q.Push(new Knight() { id = 10 });


                while (q.Count() > 0)
                {
                    Console.WriteLine(q.Pop().id);
                }

            }
        }
    }
}

