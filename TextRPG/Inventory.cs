using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Inventory
    {
        Stat playerStat;
        List<Item> playerItemList;
        
        public void InvenView(Stat stat, List<Item> itemList)
        {
            playerStat = stat;
            playerItemList = itemList;


            bool invenViewEnd = false;

            while (!invenViewEnd)
            {
                Console.Clear();

                Console.WriteLine("인벤토리 \n보유 중인 아이템을 관리할 수 있습니다.\n");

                Console.WriteLine("[ 아이템 목록 ]");

                foreach (Item item in playerItemList)
                {
                    if (item.IsBought == true)
                    {
                        string statType;
                        if (item.Type == ItemType.DefUp) statType = "방어력";
                        else statType = "공격력";
                        Console.Write("- ");
                        if (item.IsEquipped) Console.Write("[E]");
                        Console.WriteLine($"{item.Name}\t| {statType} +{item.StatPower}\t| {item.Description}");
                    }
                }

                Console.WriteLine("\n1. 장착 관리");
                Console.WriteLine("0. 나가기\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    EquipManage();
                }
                else if (input == "0")
                {
                    Console.Clear();
                    invenViewEnd = true;
                }
                else
                {
                    Program.WrongInput();
                }

            }

        }

        void EquipManage()
        {

            bool equipManageEnd = false;
            while (!equipManageEnd)
            {
                Console.Clear();

                Console.WriteLine("인벤토리 - 장착 관리 \n보유 중인 아이템을 관리할 수 있습니다.\n");

                Console.WriteLine("[ 아이템 목록 ]");

                List<Item> boughtItemList = LoadBoughtItem(playerItemList, false);

                Console.WriteLine("\n 0.나가기\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                string input = Console.ReadLine();
                int inputNum;
                bool isInputNum;
                isInputNum = int.TryParse(input, out inputNum);
                if (0 < inputNum && inputNum < (boughtItemList.Count + 1))
                {
                    EquipCalculator(playerStat, boughtItemList[inputNum - 1], playerItemList);
                }
                else if (inputNum == 0 && isInputNum)
                {
                    Console.Clear();
                    equipManageEnd = true;
                }
                else
                {
                    Program.WrongInput();
                }

            }
        }

        public static List<Item> LoadBoughtItem(List<Item> itemList, bool forMarket = false)
        {

            List<Item> boughtItemList = new List<Item>();

            foreach (Item item in itemList)
            {
                if (item.IsBought == true)
                {
                    boughtItemList.Add(item);
                }
            }
            int index = 1;
            foreach (Item item in boughtItemList)
            {
                string statType;
                if (item.Type == ItemType.DefUp) statType = "방어력";
                else statType = "공격력";
                Console.Write("- "); Console.Write($"{index} ");
                if (item.IsEquipped) Console.Write("[E]");
                Console.Write($"{item.Name}\t| {statType} +{item.StatPower}\t| {item.Description}");
                if (forMarket) Console.WriteLine($"\t| {item.Price*0.85f} G");
                else Console.WriteLine();

                index++;
            }

            return boughtItemList;
        }

        static void EquipCalculator(Stat stat, Item item, List<Item> itemList)
        {
            if (!item.IsEquipped)
            {
                Item equippedItem = itemList.FirstOrDefault(x => x.IsEquipped && x.Slot == item.Slot);
                if (equippedItem != null)
                {
                    EquipCalculator(stat, equippedItem, itemList);
                }

                if (item.Type == ItemType.DefUp)
                {
                    stat.DefBonus += item.StatPower;
                }
                else
                {
                    stat.AtkBonus += item.StatPower;
                }

                item.IsEquipped = true;
            }
            else
            {
                if (item.Type == ItemType.DefUp)
                {
                    stat.DefBonus -= item.StatPower;
                }
                else
                {
                    stat.AtkBonus -= item.StatPower;
                }

                item.IsEquipped = false;
            }

        }

        



    }
}
