using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Async
{
    public enum ClassType
    {
        Knight,
        Mage,
        Archer,

    }

    public class Player
    {
        public ClassType ClassType { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public List<int> Items { get; set; } = new List<int>();

    }
    class LINQ
    {
        static List<Player> _players = new List<Player>();

        static void Main(string[] args)
        {
            Random rand = new Random();

            for (int i = 0; i < 100; i++)
            {
                ClassType type = ClassType.Knight;
                switch (rand.Next(0, 3))
                {
                    case 0:
                        type = ClassType.Knight;
                        break;
                    case 1:
                        type = ClassType.Mage;
                        break;
                    case 2:
                        type = ClassType.Archer;
                        break;
                }


                Player player = new Player()
                {
                    ClassType = type,
                    Level = rand.Next(1, 10),
                    Attack = rand.Next(5, 50),
                    Hp = rand.Next(100, 1000)
                };

                for (int j = 0; j < 5; j++)
                    player.Items.Add(rand.Next(1, 100));


                _players.Add(player);
            }


            // Q) 레벨이 50이상인 knight만 추려내서, 레벨을 낮음 -> 높은 순서로 정렬
            {
                // 일반 버전
                List<Player> players = GetHighLevelKnight();
                foreach (Player player in players)
                {
                    Console.WriteLine($"{player.Level} {player.Hp}");
                }
            }

            // LINQ 버전
            {
                // from     foreach
                // where    조건문
                // orderby  sort (오름차순 ascending / 내림차순 descending)
                // select   최종 결과를 추출 -> 가공해서 추출도 가능 ex)p.Hp
                var players =
                    from p in _players
                    where p.ClassType == ClassType.Knight && p.Level >= 50
                    orderby p.Level
                    select p;

                foreach (Player p in players)
                {
                    Console.WriteLine($"{p.Level} {p.Hp}");
                }
            }

            // 중첩 from
            // ex) 모든 아이템 목록을 추출 (아이템id < 30)
            {
                var playerItem =
                    from p in _players
                    from i in p.Items
                    where i < 30
                    select new { p, i };

                var li = playerItem.ToList();

            }

            // grouping
            {
                var playersByLevel = from p in _players
                                     group p by p.Level into g
                                     orderby g.Key
                                     select new { g.Key, Players = g };
            }

            // join (내부 조인)
            // outer join (외부 조인)
            {
                List<int> levels = new List<int>() { 1, 5, 9 };


                var playerjoin = from p in _players
                                 join l in levels
                                 on p.Level equals l
                                 select p;
                                 
            }


            // LINQ 표준 연산자
            {
                var players =
                   from p in _players
                   where p.ClassType == ClassType.Knight && p.Level >= 50
                   orderby p.Level
                   select p;


                var player2 = _players
                        .Where(p => p.ClassType == ClassType.Knight && p.Level <= 50)
                        .OrderBy(p => p.Level)
                        .Select(p => p);

            }


        }

        static public List<Player> GetHighLevelKnight()
        {
            List<Player> players = new List<Player>();


            foreach (Player player in _players)
            {
                if (player.ClassType != ClassType.Knight)
                    continue;
                if (player.Level < 50)
                    continue;

                players.Add(player);
            }

            players.Sort ((lhs, rhs) => { return lhs.Level - rhs.Level; });

            return players;
        }
    }
}
