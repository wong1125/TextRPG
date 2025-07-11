using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Market
    {
        Stat playerStat;
        List<Item> playerItemList;

        public void MarketEnter(Stat stat, List<Item> itemList)
        {
            playerStat = stat;
            playerItemList = itemList;
            
            bool marketEnd = false;
            while (!marketEnd)
            {
                Console.Clear();

                Console.WriteLine("상점 \n필요한 아이템을 얻을 수 있는 상점입니다.\n");

                Console.WriteLine("[ 보유 골드 ]");
                Console.WriteLine($"{playerStat.Gold} G \n");

                Console.WriteLine("[ 아이템 목록 ]");

                foreach (Item item in playerItemList)
                {
                    string statType;
                    if (item.Type == ItemType.DefUp) statType = "방어력";
                    else statType = "공격력";
                    Console.Write("- ");
                    Console.Write($"{item.Name}\t| {statType} +{item.StatPower}\t| {item.Description}\t| ");
                    if (item.IsBought) Console.WriteLine("구매완료");
                    else Console.WriteLine($"{item.Price} G");
                }

                Console.WriteLine("\n1. 아이템 구매 \n2. 아이템 판매");
                Console.WriteLine("\n0.나가기\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    MarketBuy();
                }
                else if (input == "2")
                {
                    MarketSell();
                }
                else if (input == "0")
                {
                    Console.Clear();
                    marketEnd = true;
                }
                else
                {
                    Program.WrongInput();
                }



            }

            void MarketBuy()
            {
                bool marketBuyEnd = false;
                while (!marketBuyEnd)
                {
                    Console.Clear();

                    Console.WriteLine("상점 - 아이템 구매 \n필요한 아이템을 얻을 수 있는 상점입니다.\n");

                    Console.WriteLine("[ 보유 골드 ]");
                    Console.WriteLine($"{playerStat.Gold} G \n");

                    Console.WriteLine("[ 아이템 목록 ]");

                    int index = 1;
                    foreach (Item item in playerItemList)
                    {
                        string statType;
                        if (item.Type == ItemType.DefUp) statType = "방어력";
                        else statType = "공격력";
                        Console.Write("- "); Console.Write($"{index} ");
                        Console.Write($"{item.Name}\t| {statType} +{item.StatPower}\t| {item.Description}\t| ");
                        if (item.IsBought) Console.WriteLine("구매완료");
                        else Console.WriteLine($"{item.Price} G");
                        index++;
                    }

                    Console.WriteLine("\n0.나가기\n");

                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    Console.Write(">>");
                    string input = Console.ReadLine();
                    int inputNum;
                    bool isInputNum;
                    isInputNum = int.TryParse(input, out inputNum);
                    if (0 < inputNum && inputNum < index)
                    {
                        MarketBuyCalculator(playerStat, playerItemList[inputNum - 1]);
                    }
                    else if (inputNum == 0 && isInputNum)
                    {
                        Console.Clear();
                        marketBuyEnd = true;
                    }
                    else
                    {
                        Program.WrongInput();
                    }
                }
            }
            void MarketBuyCalculator(Stat playerStat, Item playerItemList)
            {
                if (playerItemList.IsBought)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n이미 구매한 아이템입니다\n");
                    Thread.Sleep(1000);
                    Console.ResetColor();
                }
                else if (playerItemList.Price > playerStat.Gold)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nGold가 부족합니다\n");
                    Thread.Sleep(1000);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("\n구매를 완료했습니다\n");
                    Thread.Sleep(1000);
                    playerStat.Gold -= playerItemList.Price;
                    playerItemList.IsBought = true;

                }

            }

            void MarketSell()
            {
                bool marketSellEnd = false;
                while (!marketSellEnd)
                {
                    Console.Clear();

                    Console.WriteLine("상점 - 아이템 구매 \n필요한 아이템을 얻을 수 있는 상점입니다.\n");

                    Console.WriteLine("[ 보유 골드 ]");
                    Console.WriteLine($"{playerStat.Gold} G \n");

                    Console.WriteLine("[ 아이템 목록 ]");

                    List<Item> boughtItemList = Inventory.LoadBoughtItem(playerItemList, true);

                    Console.WriteLine("\n0.나가기\n");

                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    Console.Write(">>");
                    string input = Console.ReadLine();
                    int inputNum;
                    bool isInputNum;
                    isInputNum = int.TryParse(input, out inputNum);
                    if (0 < inputNum && inputNum < (boughtItemList.Count + 1))
                    {
                        MarketSellCalculator(playerStat, boughtItemList[inputNum - 1]);
                        Console.WriteLine("\n판매를 완료했습니다\n");
                        Thread.Sleep(1000);

                    }
                    else if (inputNum == 0 && isInputNum)
                    {
                        Console.Clear();
                        marketSellEnd = true;
                    }
                    else
                    {
                        Program.WrongInput();
                    }

                }


            }

            void MarketSellCalculator(Stat stat, Item item)
            {
                stat.Gold += (int)(item.Price * 0.85f);
                item.IsBought = false;
                item.IsEquipped = false;

            }


        }



    }
}
