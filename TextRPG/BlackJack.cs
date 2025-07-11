using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class BlackJack
    {
        Stat playerStat;
        string[] cardPack = new string[52];
        int bettingGold;

        List<string> playerCardList = new List<string>();
        List<string> dealerCardList = new List<string>();
        string dealerHiddenCard1;
        string dealerHiddenCard2;
        string playerSum ="";
        string dealerSum ="";
        bool isPlayerHasA;
        bool isDealerHasA;
        bool isPlayerBlackjack;
        bool isDealerBlackjack;
        bool isPlayerBust;
        bool isDealerBust;
        int cardIndex;

        public void GameStart(Stat stat)
        {
            playerStat = stat;
            if (!playerStat.enterGamble) FirstMessage();
            bool cazinoEnd = false;

            while (!cazinoEnd)
            {

                Console.Clear();
                CardReset();
                if (playerStat.Gold <= 0)
                {
                    Kick();
                    break;
                }
                else Betting();
                CardShuffle();
                CardDeal();

                bool gameEnd = false;
                while (!gameEnd) 
                {
                    CazinoInterface();
                    Console.WriteLine("\n1.힛 \n2. 스테이");

                    Console.WriteLine("");
                    Console.Write(">>");

                    int inputNum2 = Program.InputToNum();
                    if (inputNum2 == 1) PlayerHit();
                    else if (inputNum2 == 2)
                    {
                        PlayerTurnEnd();
                        gameEnd = true;
                    }
                    else Program.WrongInput();
                    if (isPlayerBust)
                    {
                        PlayerTurnEnd();
                        gameEnd = true;
                    }

                }

                Console.WriteLine("\n한 판 더?");
                Console.WriteLine("\n1.예 \n2. 아니오");

                Console.WriteLine("");
                Console.Write(">>");

                int inputNum = Program.InputToNum();
                if (inputNum == 1) CardReset();
                else if (inputNum == 2) cazinoEnd = true;
                else Program.WrongInput();

            }

        }

        void FirstMessage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("이 곳이 어딘줄 알고 함부로 들어오는 건가?\n");
            Thread.Sleep(1000);
            Console.WriteLine("...하지만 난 자네같은 겁 없는 사람을 좋아하지");
            Thread.Sleep(1000);
            Console.ResetColor();
            playerStat.enterGamble = true;
        }


        void Betting()
        {
            bool bettingEnd = false;

            while (!bettingEnd)
            {
                CazinoInterface();
                Console.WriteLine($"\n얼마를 베팅하시겠습니까? (보유 골드 : {playerStat.Gold} G)");
                Console.Write(">>");
                int inputNum = Program.InputToNum();
                if (inputNum > playerStat.Gold)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("베팅 금액이 가진 돈을 초과합니다!");
                    Thread.Sleep(1000);
                    Console.ResetColor();
                }
                else if (inputNum == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("후후, 들어오는건 맘대로지만 나가는건 아니지");
                    Thread.Sleep(1000);
                    Console.ResetColor();
                }
                else if (inputNum < 0)
                {
                    Program.WrongInput();
                }
                
                else
                {
                    bettingGold = inputNum;
                    bettingEnd = true;
                }


            }
      

        }

        void Kick()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("뭐야 거지잖아, 썩 꺼져!!");
            Thread.Sleep(1000);
            Console.ResetColor();
        }

        void CardReset()
        {
            playerCardList.Clear();
            dealerCardList.Clear();
            playerSum = "";
            dealerSum = "";
            isPlayerHasA = false;
            isDealerHasA = false;
            isPlayerBlackjack = false;
            isDealerBlackjack = false;
            isPlayerBust = false;
            isDealerBust = false;
            cardIndex = 0;
        }
        
        
        
        
        void CardShuffle()
        {


       
            for (int i = 0; i < 52; i++)
            {
                StringBuilder sb = new StringBuilder();
                if (i < 13)
                {
                    sb.Append("♠ ");
                }
                else if (i < 26)
                {
                    sb.Append("♣ ");
                }
                else if (i < 39)
                {
                    sb.Append("◆ ");
                }
                else
                {
                    sb.Append("♥ ");
                }


                if (i % 13 == 12)
                {
                    sb.Append("K");
                }
                else if (i % 13 == 11)
                {
                    sb.Append("Q");
                }
                else if (i % 13 == 10)
                {
                    sb.Append("J");
                }
                else if (i % 13 == 0)
                {
                    sb.Append("A");
                }
                else
                {
                    int cardNum = i % 13 + 1;
                    sb.Append(cardNum);
                }
                cardPack[i] = sb.ToString();

            }

            Random random = new Random();
            cardPack = cardPack.OrderBy(x => random.Next(0, 53)).ToArray();
        }
        
        
        
        void CardDeal()
        {
            playerCardList.Add(cardPack[0]);
            CazinoInterface();
            dealerHiddenCard1 = cardPack[1];
            dealerCardList.Add("??");
            CazinoInterface();
            playerCardList.Add(cardPack[2]);
            PlayerCalculator();
            CazinoInterface();
            dealerHiddenCard2 = cardPack[3];
            dealerCardList.Add("??");
            CazinoInterface();
            dealerCardList[1] = dealerHiddenCard2;
            CazinoInterface();
            cardIndex = 4;
        }

        void CazinoInterface()
        {
            Console.Clear();
            Console.WriteLine("도박장");
            Console.Write($"\n딜러 카드\t");
            if (isDealerBust)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(dealerSum);
                Console.ResetColor();
            }
            else if (isDealerBlackjack)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(dealerSum);
                Console.ResetColor();
            }
            else Console.WriteLine(dealerSum);
            Console.WriteLine();
            foreach (string card in dealerCardList)
            {
                Console.Write($"\t{card}");
            }
            Console.WriteLine();
            Console.Write($"\n플레이어 카드\t");
            if (isPlayerBust)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(playerSum);
                Console.ResetColor();
            }
            else if (isPlayerBlackjack)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(playerSum);
                Console.ResetColor();
            }
            else Console.WriteLine(playerSum);
            Console.WriteLine();
            foreach (string card in playerCardList)
            {
                Console.Write($"\t{card}");
            }
            Console.WriteLine();
            Thread.Sleep(1000);
        }
        
        void PlayerCalculator()
        {
            int intplayerSum = 0;
            
            foreach (string card in playerCardList)
            {
                string[] splited = card.Split(" ");
                if (splited[1] == "A")
                {
                    intplayerSum += 11;
                    isPlayerHasA = true;
                }
                else if (splited[1] == "J" | splited[1] == "Q" | splited[1] == "K")
                {
                    intplayerSum += 10;
                }
                else
                {
                    intplayerSum += int.Parse(splited[1]);
                }
            }
            
            if (intplayerSum == 21 && playerCardList.Count == 2) 
            {
                isPlayerBlackjack = true;
                playerSum = "Blackjack!";
                return;
            }
      
            if (isPlayerHasA && intplayerSum > 21)
            {
                intplayerSum -= 10;
                isPlayerHasA = false;
            }

            if (intplayerSum > 21)
            {
                isPlayerBust = true;
                playerSum = "Bust!!";
                return;
            }

            playerSum = intplayerSum.ToString();
 
        }


        void DealerCalculator()
        {
            int intDealerSum = 0;

            foreach (string card in dealerCardList)
            {
                string[] splited = card.Split(" ");
                if (splited[1] == "A")
                {
                    intDealerSum += 11;
                    isDealerHasA = true;
                }
                else if (splited[1] == "J" | splited[1] == "Q" | splited[1] == "K")
                {
                    intDealerSum += 10;
                }
                else
                {
                    intDealerSum += int.Parse(splited[1]);
                }
            }

            if (intDealerSum == 21 && dealerCardList.Count == 2)
            {
                isDealerBlackjack = true;
                dealerSum = "Blackjack!";
                return;
            }

            if (isDealerHasA && intDealerSum > 21)
            {
                intDealerSum -= 10;
                isDealerHasA = false;
            }

            if (intDealerSum > 21)
            {
                isDealerBust = true;
                dealerSum = "Bust!!";
                return;
            }

            dealerSum = intDealerSum.ToString();
        }


        void PlayerHit()
        {
            playerCardList.Add(cardPack[cardIndex]);
            cardIndex++;
            PlayerCalculator();
            CazinoInterface();
        }

        void PlayerTurnEnd()
        {
            dealerCardList[0] = dealerHiddenCard1;
            DealerCalculator();
            CazinoInterface();
            if (!isDealerBlackjack && !isDealerBust)
            {
                while( int.Parse(dealerSum) < 17)
                {
                    dealerCardList.Add(cardPack[cardIndex]);
                    cardIndex++;
                    DealerCalculator();
                    CazinoInterface();
                    if (!int.TryParse(dealerSum, out _)) break;

                }
            }


            Judge();
        }

        void Judge()
        {
            if (isPlayerBust)
            {
                playerLose();
            }
            else if (isDealerBust)
            {
                playerWin();
            }
            else
            {
                int intPlayerSum;
                int intDealerSum;

                if (isPlayerBlackjack) intPlayerSum = 22;
                else intPlayerSum = int.Parse(playerSum);
                if (isDealerBlackjack) intDealerSum = 22;
                else intDealerSum = int.Parse(dealerSum);

                if (intPlayerSum > intDealerSum) playerWin();
                else if (intPlayerSum < intDealerSum) playerLose();
                else Draw();


            }





        }

        void playerWin()
        {
            if (isPlayerBlackjack) bettingGold *= 3 / 2;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n돈을 땄습니다!\n");
            Console.WriteLine($"+ {bettingGold} G \n");
            Console.ResetColor();
            playerStat.Gold += bettingGold;
            Console.WriteLine($"(보유 골드 : {playerStat.Gold} G)");

        }
        
        void Draw()
        {
            Console.WriteLine("\n비겼습니다");
        }

        void playerLose()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n돈을 잃었습니다!\n");
            Console.WriteLine($"- {bettingGold} G\n");
            Console.ResetColor();
            playerStat.Gold -= bettingGold;
            Console.WriteLine($"(보유 골드 : {playerStat.Gold} G)");


        }


    }

}
