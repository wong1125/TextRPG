using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Stat()
    {
        public int Lv { get; set; } = 1;
        public string Name { get; set; }
        public string Job { get; set; } = "전사";
        public float Atk { get; set; } = 10;
        public int AtkBonus { get; set; }
        public int Def { get; set; } = 5;
        public int DefBonus { get; set; }
        public int Hp { get; set; } = 100;
        public int MaxHp { get; set; } = 100;

        public int Gold { get; set; } = 1500;

        public bool enterGamble { get; set; } = false;

        public static void StatView(Stat s)
        {
            bool statViewEnd = false;
            while (!statViewEnd)
            {
                Console.Clear();

                Console.WriteLine("상태 보기 \n캐릭터의 정보가 표시됩니다.\n");
                Console.WriteLine($"Lv. {s.Lv}");
                Console.WriteLine($"{s.Name} ({s.Job})");
                if (s.AtkBonus != 0)
                {
                    Console.WriteLine($"공격력: {s.Atk + s.AtkBonus} (+{s.AtkBonus})");
                }
                else
                {
                    Console.WriteLine($"공격력: {s.Atk}");
                }
                if (s.DefBonus != 0)
                {
                    Console.WriteLine($"방어력: {s.Def + s.DefBonus} (+{s.DefBonus})");
                }
                else
                {
                    Console.WriteLine($"방어력: {s.Def}");
                }
                Console.WriteLine($"체 력: {s.Hp}");
                Console.WriteLine($"Gold: {s.Gold}");

                Console.WriteLine("\n 0.나가기\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                string input = Console.ReadLine();
                if (input == "0")
                {
                    Console.Clear();
                    statViewEnd = true;
                }
                else
                {
                    Program.WrongInput();
                }
            }

        }



    }
}
