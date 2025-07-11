using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public enum ItemType
    {
        DefUp, AtkUp
    }

    public enum ItemSlot
    {
        Armor, Weapon
    }

    public class Item()
    {
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public ItemSlot Slot { get; set; }
        public int StatPower { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public bool IsBought { get; set; } = false;
        public bool IsEquipped { get; set; } = false;


        public static List<Item> GetDefaultItem()
        {
            List<Item> itemList = new List<Item>
            {
                new Item{Name = "수련자 갑옷        ", Type = ItemType.DefUp, StatPower = 5, Price = 1000, Description = "수련에 도움을 주는 갑옷입니다.                   ", Slot = ItemSlot.Armor},
                new Item{Name = "무쇠갑옷           ", Type = ItemType.DefUp, StatPower = 9, Price = 2000, Description = "무쇠로 만들어져 튼튼한 갑옷입니다.               ", Slot = ItemSlot.Armor},
                new Item{Name = "스파르타의 갑옷", Type = ItemType.DefUp, StatPower = 15, Price = 3500, Description = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", Slot = ItemSlot.Armor},
                new Item{Name = "낡은 검           ", Type = ItemType.AtkUp, StatPower = 2, Price = 600, Description = "쉽게 볼 수 있는 낡은 검 입니다.                    ", Slot = ItemSlot.Weapon},
                new Item{Name = "청동 도끼          ", Type = ItemType.AtkUp, StatPower = 5, Price = 1500, Description = "어디선가 사용됐던거 같은 도끼입니다.              ", Slot = ItemSlot.Weapon},
                new Item{Name = "스파르타의 창     ", Type = ItemType.AtkUp, StatPower = 7, Price = 2500, Description = "스파르타의 전사들이 사용했다는 전설의 창입니다.", Slot = ItemSlot.Weapon}

            };

            return itemList;
        }

        
    }
}

