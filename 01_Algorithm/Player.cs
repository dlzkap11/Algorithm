using _01_Algorithm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Algorithm
{
    class Pos
    {
        public Pos(int y, int x) { Y = y; X = x; }
        public int Y;
        public int X;
    }

    class Player
    {
        public int PosX { get; set; }
        public int PosY { get; set; }

        Random _random = new Random();
        Board _board;

        enum Dir
        {
            Up = 0,
            Left = 1,
            Down = 2,
            Right = 3
        }
        int _dir = (int)Dir.Up;    
        List<Pos> _points = new List<Pos>();

        public void Initalize(int posY, int posX, Board board)
        {
            PosX = posX;
            PosY = posY;
            _board = board;

            //RightHand();

            //BFS();

            AStar();

        }

        struct PQNode : IComparable<PQNode>
        {
            public int F;
            public int G;
            public int X;
            public int Y;
            

            public int CompareTo(PQNode other)
            {
                if (F == other.F)
                    return 0;
                return F < other.F ? 1 : -1;
            }
        }


        void AStar()
        {
            //대각선 U L D R UL DL DR UR
            int[] deltaY = new int[] { -1, 0, 1, 0, -1, 1, 1, -1 };
            int[] deltaX = new int[] { 0, -1, 0, 1, -1, -1, 1, 1 };
            int[] cost = new int[] { 10, 10, 10, 10, 14, 14, 14, 14 };

            /*
            int[] deltaY = new int[] { -1, 0, 1, 0 };
            int[] deltaX = new int[] { 0, -1, 0, 1 };
            int[] cost = new int[] { 10, 10, 10, 10};
            */

            //점수 매기기
            //F = G + H
            //F = 최종 점수 (작을수록 좋음, 경로에 따라 달라짐)
            //G = 시작점에서 해당 좌표까지 이동하는데 드는 비용 (작을수록 좋음, 경로에 따라 달라짐)
            //H = 목적지에서 얼마나 가까운지 (작을수록 좋음, 고정)


            //(y, x) 이미 방문했는지 여부 (방문 = closed 상태)
            bool[,] closed = new bool[_board.Size, _board.Size]; //CloseList

            //(y, x) 가는 길을 한 번이라도 발견했는지
            // 발견 X => MaxValue
            // 발견 O => F = G + H
            int[,] open = new int[_board.Size, _board.Size]; //OpenList

            

            //초기화
            for (int y = 0; y < _board.Size; y++)
                for (int x = 0; x < _board.Size; x++)
                    open[y, x] = Int32.MaxValue;

            Pos[,] parent = new Pos[_board.Size, _board.Size];

            //오픈리스트에 있는 정보들 중에서, 가장 좋은 후보를 빠르게 뽑아오기 위한 도구
            PriorityQueue<PQNode> pq = new PriorityQueue<PQNode>();



            //시작점 발견 (예약진행) Abs = 절대값
            open[PosY, PosX] = 10 * (Math.Abs(_board.DestY - PosY) + Math.Abs(_board.DestX - PosX));
            pq.Push(new PQNode() { F = 10 * (Math.Abs(_board.DestY - PosY) + Math.Abs(_board.DestX - PosX)), G = 0, Y = PosY, X = PosX });
            parent[PosY, PosX] = new Pos(PosY, PosX);


            while (pq.Count > 0)
            {
                //제일 좋은 후보를 찾는다
                PQNode node = pq.Pop();

                //동일한 좌표를 여러 경로로 찾아서 ,더 빠른 경로로 인해서 이미 방문된 경우 스킵
                if (closed[node.Y, node.X])
                    continue;

                //방문
                closed[node.Y, node.X] = true;

                //목적지 도착시 바로 종료
                if (node.Y == _board.DestY && node.X == _board.DestX)
                    break;

                // 상하좌우 등 이동할 수 있는 좌표인지 확인 후 예약
                for(int i = 0; i< deltaY.Length; i++)
                {
                    int nextY = node.Y + deltaY[i];
                    int nextX = node.X + deltaX[i];

                    //유효범위를 벗어났으면 스킵
                    if (nextX < 0 || nextX >= _board.Size || nextY < 0 || nextY >= _board.Size) //보드사이즈보다 크거나 작은 값에 대한 예외처리
                        continue;
                    //벽으로 막혀서 갈 수 없으면 스킵
                    if (_board.Tile[nextY, nextX] == Board.TileType.Wall)
                        continue;
                    //이미 방문한 곳이면 스킵
                    if (closed[nextY, nextX])
                        continue;


                    //비용계산
                    int g = node.G + cost[i];
                    int h = 10 *(Math.Abs(_board.DestY - nextY) + Math.Abs(_board.DestX - nextX));

                    // 다른 경로에서 더 빠른 길을 이미 찾았으면 스킵
                    if (open[nextY, nextX] < g + h)
                        continue;

                    open[nextY, nextX] = g + h;
                    pq.Push(new PQNode() { F = g + h, G = g, Y = nextY, X =  nextX });
                    parent[nextY, nextX] = new Pos(node.Y, node.X);
                }
            }

            CalcPathFromParent(parent);

        }

        void BFS()
        {
            int[] deltaY = new int[] {-1, 0, 1, 0 };
            int[] deltaX = new int[] {0, -1, 0, 1 };


            bool[,] found = new bool[_board.Size, _board.Size];
            Pos[,] parent = new Pos[_board.Size, _board.Size];


            
            Queue<Pos> q = new Queue<Pos>();
            q.Enqueue(new Pos(PosY, PosX));
            found[PosY, PosX] = true;
            parent[PosY, PosX] = new Pos(PosY, PosX);

            while(q.Count > 0)
            {
                Pos pos = q.Dequeue();
                int nowY = pos.Y;
                int nowX = pos.X;

                for (int i = 0; i < 4; i++)
                {
                    int nextY = nowY + deltaY[i];
                    int nextX = nowX + deltaX[i];


                    if(nextX < 0 || nextX >= _board.Size || nextY < 0 || nextY >= _board.Size) //보드사이즈보다 크거나 작은 값에 대한 예외처리
                        continue;   
                    if (_board.Tile[nextY, nextX] == Board.TileType.Wall)
                        continue;
                    if (found[nextY, nextX])
                        continue;

                    q.Enqueue(new Pos(nextY, nextX));
                    found[nextY, nextX] = true;
                    parent[nextY, nextX] = new Pos(nowY, nowX);
                }
            }

            CalcPathFromParent(parent);
        }

        void CalcPathFromParent(Pos[,] parent)
        {
            int y = _board.DestY;
            int x = _board.DestX;

            while (parent[y, x].Y != y || parent[y, x].X != x)
            {
                _points.Add(new Pos(y, x));
                Pos pos = parent[y, x];
                y = pos.Y;
                x = pos.X;
            }
            _points.Add(new Pos(y, x)); //시작점 기입
            _points.Reverse();
        }

        void RightHand()
        {
            //현재 바라보고 있는 방향을 기준으로 좌표 변화를 나타낸다.
            int[] frontY = new int[] { -1, 0, 1, 0 };
            int[] frontX = new int[] { 0, -1, 0, 1 };
            int[] rightY = new int[] { 0, -1, 0, 1 };
            int[] rightX = new int[] { 1, 0, -1, 0 };

            _points.Add(new Pos(PosY, PosX));

            //목적지 도착전까지 계속 실행
            while (PosX != _board.DestX || PosY != _board.DestY)
            {
                //1. 현재 바라보는 방향을 기준으로 오른쪽으로 갈 수 있는지 확인
                if (_board.Tile[PosY + rightY[_dir], PosX + rightX[_dir]] == Board.TileType.Empty)
                {
                    //오른쪽 방향으로 90도 회전
                    _dir = (_dir - 1 + 4) % 4; //+4는 -1로 인한 음수에 대응하기 위해

                    //앞으로 1보 전진
                    PosX = PosX + frontX[_dir];
                    PosY = PosY + frontY[_dir];
                    _points.Add(new Pos(PosY, PosX));

                }

                //2. 현재 바라보는 방향을 기준으로 전진할 수 있는지 확인
                else if (_board.Tile[PosY + frontY[_dir], PosX + frontX[_dir]] == Board.TileType.Empty)
                {
                    //앞으로 1보 전진
                    PosX = PosX + frontX[_dir];
                    PosY = PosY + frontY[_dir];
                    _points.Add(new Pos(PosY, PosX));
                }
                else
                {
                    //왼쪽 방향으로 90도 회전
                    _dir = (_dir + 1 + 4) % 4; //+4 구지?긴함
                }
            }
        }

        const int MOVE_TICK = 50; //0.1s
        int _sumTick = 0;
        int _lastIndex = 0;
        public void Update(int deltaTick)
        {

            if (_lastIndex >= _points.Count)
            {
                _lastIndex = 0;
                _points.Clear();
                _board.Initialize(_board.Size, this);
                Initalize(1, 1, _board);
            }

            _sumTick += deltaTick; 

            if (_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                PosY = _points[_lastIndex].Y;
                PosX = _points[_lastIndex].X;
                _lastIndex++;

            }
        }
    }
}
