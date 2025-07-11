using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace TextRPG
{
    public class SaveManager
    {
        public Stat PlayerStat { get; set; }
        public List<Item> ItemList { get; set; }

        public void SaveGame(Stat stat, List<Item> itemList)
        {
            SaveManager saveData = new SaveManager();
            saveData.PlayerStat = stat;
            saveData.ItemList = itemList;

            string json = JsonSerializer.Serialize(saveData, new  JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText("save.json",  json);
            Console.WriteLine("저장 완료!");
        }

        public static SaveManager LoadSave()
        {
            string json = File.ReadAllText("save.json");
            SaveManager loadData = JsonSerializer.Deserialize<SaveManager>(json);
            return loadData;
        }

    }
}
