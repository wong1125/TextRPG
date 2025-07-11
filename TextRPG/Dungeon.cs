using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Dungeon
    {
        Stat playerStat;
        int pastGold;
        int pastHp;
        int pastLv;


        public void DungeonEnter(Stat stat)
        {
            playerStat = stat;
            bool dungeonEnterEnd = false;
            while (!dungeonEnterEnd)
            {
                if(playerStat.Hp <= 0) Program.GameOver();
                
                Console.Clear();

                Console.WriteLine("던전입장 \n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

                Console.WriteLine("\n1. 쉬운 던전\t | 방어력 5 이상 권장");
                Console.WriteLine("2. 일반 던전\t | 방어력 11 이상 권장");
                Console.WriteLine("3. 어려운 던전\t | 방어력 17 이상 권장");

                Console.WriteLine("0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        DungeonCalculator(1);
                        break;
                    case "2":
                        DungeonCalculator(2);
                        break;
                    case "3":
                        DungeonCalculator(3);
                        break;
                    case "0":
                        Console.Clear();
                        dungeonEnterEnd = true;
                        break;
                    default:
                        Program.WrongInput();
                        break;

                }

            }
        }

        void DungeonCalculator(int difficulty)
        {
            pastGold = playerStat.Gold;
            pastHp = playerStat.Hp;
            pastLv = playerStat.Lv;
            int doungeondefense = 5 * difficulty + (difficulty - 1);
            int totalDef = playerStat.Def + playerStat.DefBonus;
            float totalAtk = playerStat.Atk + playerStat.AtkBonus;

            Random random = new Random();
            int successValue = random.Next(0, 101);

            if (totalDef > doungeondefense | successValue > 40)
            {
                int defDifference = totalDef - doungeondefense;
                int hpReduce = random.Next(20 -  defDifference, 36 - defDifference);
                int defaultGold = 1000 + 700*(difficulty - 1);
                if (difficulty ==3) defaultGold += 100;
                int additionalGold = (random.Next((int)totalAtk, (int)(totalAtk * 2))*defaultGold/100);

                playerStat.Hp -= hpReduce;
                playerStat.Gold += defaultGold + additionalGold;
                playerStat.Lv++;
                playerStat.Def++;
                playerStat.Atk += 0.5f;
           
                DungeonClear(difficulty);
            }
            else
            {
                playerStat.Hp -= 50;
                DungeonFail(difficulty);
            }


        }

        void DungeonClear(int difficulty)
        {
            bool dungeonClearEnd = false;

            string diffString = "";
            switch (difficulty)
            {
                case 1:
                    diffString = "쉬운";
                    break;
                case 2:
                    diffString = "일반";
                    break;
                case 3:
                    diffString = "어려운";
                    break;
                default:
                    diffString = "미지의";
                    break;
            }

            while (!dungeonClearEnd)
            {
                Console.Clear();
                Console.WriteLine($"던전 클리어 \n\n축하합니다!! \n{diffString} 던전을 클리어 하였습니다");

                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"체력 {pastHp} -> {playerStat.Hp}");
                Console.WriteLine($"Gold {pastGold} -> {playerStat.Gold}");
                Console.WriteLine($"Lv {pastLv} -> {playerStat.Lv}");

                Console.WriteLine("\n 0.나가기\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                string input = Console.ReadLine();
                if (input == "0")
                {
                    Console.Clear();
                    dungeonClearEnd = true;
                }
                else
                {
                    Program.WrongInput();
                }


            }
        }

        void DungeonFail(int difficulty)
        {
            bool dungeonFailEnd = false;

            string diffString = "";
            switch (difficulty)
            {
                case 1:
                    diffString = "쉬운";
                    break;
                case 2:
                    diffString = "일반";
                    break;
                case 3:
                    diffString = "어려운";
                    break;
                default:
                    diffString = "미지의";
                    break;
            }

            while (!dungeonFailEnd)
            {
                Console.Clear();
                Console.WriteLine($"던전 실패 \n\n실패했습니다!! \n{diffString} 던전을 클리어 하지 못했습니다");

                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"체력 {pastHp} -> {playerStat.Hp}");
                Console.WriteLine($"Gold {pastGold} -> {playerStat.Gold}");

                Console.WriteLine("\n 0.나가기\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                string input = Console.ReadLine();
                if (input == "0")
                {
                    Console.Clear();
                    dungeonFailEnd = true;
                }
                else
                {
                    Program.WrongInput();
                }

            }

            
        }

           
    }
}