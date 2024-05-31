using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_P
{

    //스택 : 후입선출 (LIFO)
    //큐 : 선입선출 (FIFO)


    class FileName
    {
        static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            int data = stack.Pop();

            stack.Peek();

            
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(101);
            queue.Enqueue(102);
            queue.Enqueue(103);
            queue.Enqueue(104);

            int data2 = queue.Dequeue();
            int data3 = queue.Peek();

            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(101);
            list.AddLast(102);


            //FIFO
            int value1 = list.First.Value;
            list.RemoveFirst();


            //LIFO
            int value2 = list.Last.Value;
            list.RemoveLast();

            List<int> list2 = new List<int>();



            
        }
    }
}
