using System;


namespace Async
{
    // async / await
    // async 비동기 프로그래밍 
    // 게임서버) 비동기 = 멀티쓰레드? -> 꼭 그렇지는 않음
    // 유니티) Coroutine = 일종의 비동기 but 싱글쓰레드  좀 비슷한 느낌



    class Program
    {
        static Task Test()
        {
            Console.WriteLine("Start Test");
            Task t = Task.Delay(3000);
            return t;
        }


        // 아메리카노를 제조중 (1분...)
        // 주문 대기
        static async Task<int> TestAsync()
        {
            Console.WriteLine("Start TestAsync");
            await Task.Delay(3000); // 복잡한 작업( DB나 파일 작업)

            Console.WriteLine("End TestAsync");

            return 1;
        }

        static async Task Main(string[] args)
        {
            Task<int> t = TestAsync();

            // 다른 일을 할 수 있음
            Console.WriteLine("while start");

            int ret = await t;
            Console.WriteLine(ret);
            while (true)
            {
                
            }
        }
    }
}