namespace Stack_P
{

    //스택 : 후입선출 (LIFO)
    //큐 : 선입선출 (FIFO)


    class Graph
    {

        int[,] adj = new int[6, 6]
        {
            {0, 1, 0, 1, 0, 0},
            {1, 0, 1, 1, 0, 0},
            {0, 1, 0, 0, 0, 0},
            {1, 1, 0, 0, 1, 0},
            {0, 0, 0, 1, 0, 1},
            {0, 0, 0, 0, 1, 0},
        };

        List<int>[] adj2 = new List<int>[]
        {
            new List<int>() { 1, 3 },
            new List<int>() { 0, 2, 3 },
            new List<int>() { 1 },
            new List<int>() { 0, 1, 4 },
            new List<int>() { 3, 5 },
            new List<int>() { 4 },
        };


        bool[] visited = new bool[6]; //처음에는 false값을 가짐


        // 1. 우선 now부터 방문
        // 2. now와 연결된 정점들을 하나씩 확인하며 {아직 미발견(미방문} 상태라면 방문

        public void DFS(int now)
        {
            Console.WriteLine(now);
            visited[now] = true; // 1. 우선 now부터 방문



            for (int next = 0; next < 6; next++)
            {
                if (adj[now, next] == 0) //연결되어있지않으면 continue
                    continue;

                if (visited[next])  //이미 방문했으면 continue
                    continue;

                DFS(next);

            }
        }


        public void DFS2(int now)
        {
            Console.WriteLine(now);
            visited[now] = true;

            foreach (int next in adj2[now])
            {
                if (visited[next])
                    continue;
                DFS2(next);
            }
        }

        public void SearchAll()
        {
            visited = new bool[6];
            for (int now = 0; now < 6; now++)
                if (visited[now] == false)
                    DFS(now);
        }

        public void SearchAll2()
        {
            visited = new bool[6];
            for (int now = 0; now < 6; now++)
                if (visited[now] == false)
                    DFS2(now);
        }


        // 1. 우선 now부터 방문
        // 2. 예약된 곳으로 방문
        public void  BFS(int start)
        {
           
            bool[] found = new bool[6];
            int[] parent = new int[6];      //어디서 찾아왔는가
            int[] distance = new int[6];    //처음에서의 거리

            Queue<int> queue = new Queue<int>();

            queue.Enqueue(start);
            found[start] = true;

            parent[start] = start;
            distance[start] = 0;


            while(queue.Count > 0)
            {
                int now = queue.Dequeue();
                Console.WriteLine(now);

                for(int next = 0; next < 6; next++)
                {
                    if (adj[now, next] == 0) //인접하지 않으면 스킵
                        continue;

                    if (found[next])    //이미 발견한 곳이면 스킵
                        continue;
                    queue.Enqueue(next);
                    found[next] = true;
                    parent[next] = now;
                    distance[next] = distance[now] + 1;
                }
            }
        }
    }

    class FileName
    {
        static void Main(string[] args)
        {
            //DFS (Depth First Search 깊이 우선 탐색)
            //BFS (Breadth First Search 너비 우선 탐색) --> 최단거리에서 많이 사용

            Graph graph = new Graph();

            //graph.DFS(3);
            //graph.DFS2(3);
            //graph.SearchAll();

            graph.BFS(0);

        }
    }
}
