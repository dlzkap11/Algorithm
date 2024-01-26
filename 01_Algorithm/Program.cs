using System;

namespace Algorithm
{
    class Progrmas
    {
        static void Main(string[] args)
        {

            Console.CursorVisible = false;


            const int WAIT_TICK = 1000 / 30;
            const char CIRCLE = '\u25cf';

            int lastTick = 0;


            while (true)
            {

                #region 프레임 관리
                int currenTick = System.Environment.TickCount;  //시스템이 시작된 이후 시간 ms
                //if 경과한 시간이 1/30초 보다 작으면

                if (currenTick - lastTick < WAIT_TICK)
                    continue;
                lastTick = currenTick;

                #endregion


                // FPS -> 프레임 퍼 세컨드 (60프레임... 30프레임 ㄷㄷㄷ)

                // 입력

                //로직

                //렌더링


                Console.SetCursorPosition(0, 0);

                for (int i = 0; i < 25; i++)
                {
                    for(int j= 0; j < 25; j++)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(CIRCLE);
                    }
                    Console.WriteLine();
                }
                
            }
            
        }
    }
}