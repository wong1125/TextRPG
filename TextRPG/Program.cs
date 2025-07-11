using System.Runtime.CompilerServices;
using static TextRPG.Program;

namespace TextRPG
{
    
    internal class Program
    {
        static Stat stat;
        static List<Item> itemList;


        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();
            Market market = new Market();
            Rest rest = new Rest();
            Dungeon dungeon = new Dungeon();
            BlackJack blackJack = new BlackJack();
            SaveManager saveManager = new SaveManager();


            if (File.Exists("save.json"))
            {
                bool saveQuestionEnd = false;
                while (!saveQuestionEnd)
                {
                    Console.Clear();
                    Console.WriteLine("이전 세이브가 발견되었습니다. \n이어하시겠습니까?");
                    Console.WriteLine("\n1. 네 \n2. 아니오");
                    int inputNum = InputToNum();
                    if (inputNum == 1)
                    {
                        SaveManager loadedData = SaveManager.LoadSave();
                        stat = loadedData.PlayerStat;
                        itemList = loadedData.ItemList;
                        saveQuestionEnd = true;

                    }
                    else if (inputNum == 2)
                    {
                        NewGame();
                        saveQuestionEnd = true ;
                    }
                    else
                    {
                        WrongInput();
                    }

                }               

            }
            else
            {
                NewGame();
            }


                bool loopEnd = false;
            string gamble = "??";
            
            //주의! 아직 루프 탈출 방법 없음
            while (!loopEnd)
            {
                Console.Clear();

                if (stat.enterGamble) gamble = "도박장";
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine($"\n1. 상태보기 \n2. 인벤토리 \n3. 상점 \n4. 던전 입장 \n5. 휴식하기 \n6. {gamble} \n7. 저장하기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Stat.StatView(stat);
                        break;
                    case "2":
                        inventory.InvenView(stat, itemList);
                        break;
                    case "3":
                        market.MarketEnter(stat, itemList);
                        break;
                    case "4":
                        dungeon.DungeonEnter(stat);
                        break;
                    case "5":
                        rest.RestEnter(stat);
                        break;
                    case "6":
                        blackJack.GameStart(stat);
                        break;
                    case "7":
                        saveManager.SaveGame(stat, itemList);
                        Thread.Sleep(1000);
                        break;
                    case "Show Me The Money":
                        Console.WriteLine("치트: 10000G 추가!");
                        stat.Gold += 10000;
                        Thread.Sleep(1000);
                        break;
                    default:
                        WrongInput();
                        break;
                }
            }
            
        }

        public static void WrongInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n잘못된 입력입니다\n");
            Thread.Sleep(1000);
            Console.ResetColor();
        }

        public static void GameOver()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("사망하셨습니다\n");
            Console.WriteLine("게임이 종료됩니다\n");
            Environment.Exit(0);
        }

        public static int InputToNum()
        {
            string input = Console.ReadLine();
            bool isInputNum;
            isInputNum = int.TryParse(input, out int inputNum);
            if (!isInputNum) inputNum = -1;
            return inputNum;
        }
        
        public static void NewGame()
        {
            stat = new Stat();
            itemList = Item.GetDefaultItem();
            Console.Clear();
            Console.WriteLine("...당신의 이름은?");
            Console.Write(">>");
            string name = Console.ReadLine();
            stat.Name = name;
        }
        

        


    }

}
