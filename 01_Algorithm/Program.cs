using _01_Algorithm;
using System;

namespace Algorithm
{
    class Progrmas
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            board.Initialize(25);



            Console.CursorVisible = false;


            const int WAIT_TICK = 1000 / 30;
            

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

                
                Console.SetCursorPosition(0, 0);
                //렌더링
                board.Render();


            }
            
        }
    }
}