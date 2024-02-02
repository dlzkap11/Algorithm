using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Algorithm
{

    internal class Board
    {
        public TileType[,] _tile;
        public int _size;

        const char CIRCLE = '\u25cf';

        public enum TileType
        {
            Empty,
            Wall,
        }


        public void Initialize(int size)
        {
            //size가 짝수일 경우 리턴(외곽이 벽이 되어야하므로)
            if (size % 2 == 0)
                return;

            _tile = new TileType[size, size];
            _size = size;

            //이진트리 미로
            GenerateByBinaryTree();
        }



        void GenerateByBinaryTree()
        {
            //일단 길을 다 막는 작업
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        _tile[y, x] = TileType.Wall;
                    else
                        _tile[y, x] = TileType.Empty;

                }
            }

            Random rand = new Random();

            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {

                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (y == _size - 2 && x == _size - 2)
                        continue;

                    if (y == _size - 2)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    if (x == _size - 2)
                    {
                        _tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    //우측 혹은 아래로 1/2확률로 길을 뚫음
                    if (rand.Next(0, 2) == 0)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                    }
                    else
                    {
                        _tile[y + 1, x] = TileType.Empty;                   
                    }

                }
            }
        }

        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            for (int y = 0; y < _size; y++) 
            {
                for (int x = 0; x < _size; x++) 
                {
                    Console.ForegroundColor = GetTileColor(_tile[y, x]);
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
