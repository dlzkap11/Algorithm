using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Algorithm
{

    class Board
    {
        const char CIRCLE = '\u25cf';
        public TileType[,] Tile { get; private set; }
        public int Size { get; private set; }
        

        public int DestX { get; private set; }
        public int DestY { get; private set; }

        Player _player;

        public enum TileType
        {
            Empty,
            Wall,
        }


        public void Initialize(int size, Player player)
        {
            //size가 짝수일 경우 리턴(외곽이 벽이 되어야하므로)
            if (size % 2 == 0)
                return;

            _player = player;

            Tile = new TileType[size, size];
            Size = size;

            DestX = size - 2;
            DestY = size - 2;

            //이진트리 미로
            //GenerateByBinaryTree();
            GenrateBySideWinder();
        }
        void GenrateBySideWinder()
        {
            //일단 길을 다 막는 작업
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        Tile[y, x] = TileType.Wall;
                    else
                        Tile[y, x] = TileType.Empty;

                }
            }

            Random rand = new Random();
            

            for (int y = 0; y < Size; y++)
            {
                int count = 1;
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (y == Size - 2 && x == Size - 2)
                        continue;

                    if (y == Size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    if (x == Size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    //우측 혹은 아래로 1/2확률로 길을 뚫음
                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        count++;
                    }
                    else
                    {
                        int randomindex = rand.Next(0, count);
                        Tile[y + 1, x - randomindex * 2] = TileType.Empty;
                        count = 1;
                    }

                }
            }
        }

        void GenerateByBinaryTree()
        {
            //일단 길을 다 막는 작업
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        Tile[y, x] = TileType.Wall;
                    else
                        Tile[y, x] = TileType.Empty;

                }
            }

            Random rand = new Random();

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (y == Size - 2 && x == Size - 2)
                        continue;

                    if (y == Size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    if (x == Size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    //우측 혹은 아래로 1/2확률로 길을 뚫음
                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                    }
                    else
                    {
                        Tile[y + 1, x] = TileType.Empty;                   
                    }

                }
            }
        }
        
        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            for (int y = 0; y < Size; y++) 
            {
                for (int x = 0; x < Size; x++) 
                {
                    //플레이어 좌표를 갖고 와서, 그 좌표랑 현재 y,x 가 일치하면 플레이어 색상으로 표시
                    if (y == _player.PosY && x == _player.PosX)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else if (y == DestY && x == DestX)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = GetTileColor(Tile[y, x]);

                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = prevColor;
        }

        ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Empty:
                    return ConsoleColor.Green;

                case TileType.Wall:
                    return ConsoleColor.Red;

                default:
                    return ConsoleColor.Green;
            }
        }
    }
}
