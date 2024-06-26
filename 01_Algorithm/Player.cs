﻿using _01_Algorithm;
using System;
using System.Collections.Generic;
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
        }

        void BFS()
        {
            int[] deltaY = new int[] {-1, 0, 1, 0 };
            int[] deltaX = new int[] {0, -1, 0, 1 };


            bool[,] found = new bool[_board.Size, _board.Size];
            Pos[,] parent = new Pos[_board.Size, _board.Size];


            
            Queue<Pos> q = new Queue<Pos>();
            q.Enqueue(new Pos(PosX, PosY));
            found[PosX, PosY] = true;
            parent[PosX, PosY] = new Pos(PosY, PosX);

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

        const int MOVE_TICK = 100; //0.1s
        int _sumTick = 0;
        int _lastIndex = 0;
        public void Update(int deltaTick)
        {

            if (_lastIndex >= _points.Count)
                return;

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
