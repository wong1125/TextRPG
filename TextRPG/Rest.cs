using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Rest
    {
        Stat playerStat;
        public void RestEnter(Stat stat)
        {
            playerStat = stat;
            
            bool restEnd = false;
            while (!restEnd)
            {
                Console.Clear();

                Console.WriteLine($"휴식하기 \n500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {playerStat.Gold} G) \n");

                Console.WriteLine("\n1.휴식하기 \n0.나가기\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                string input = Console.ReadLine();
                
                if (input == "1")
                {
                    if (playerStat.Hp >= playerStat.MaxHp)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n이미 최대 체력입니다\n");
                        Thread.Sleep(1000);
                        Console.ResetColor();
                    }
                    else if (playerStat.Gold < 500)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nGold가 부족합니다\n");
                        Thread.Sleep(1000);
                        Console.ResetColor();
                    }
                    else
                    {
                        playerStat.Hp = playerStat.MaxHp;
                        playerStat.Gold -= 500;
                        Console.WriteLine($"휴식했습니다 (현재 체력: {playerStat.Hp})");
                        Thread.Sleep(1000);
                    }

                }
                else if (input == "0")
                {
                    Console.Clear();
                    restEnd = true;
                }
                else
                {
                    Program.WrongInput();
                }
            }
            




        } 


    }
}
