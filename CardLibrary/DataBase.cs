using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;
using static MemoryGame.CardGame;

namespace MemoryGame
{
    public class DataBase
    {
        //Data to contain: Username, num_moves - and how many cards the game had
        //Format: JSON
        //{key: value} = 
        // {cards_in_game : [{player_name, #moves}, {player_name, #moves}...]}
        public int NumCards { get; set; }
        public string Name { get; set; }
        public int Moves { get; set; }
        public string PathDB { get; set; }

        private JsonObject dataBase;

        // db.Insert(struct GameData);
            //          GameData contains num_cards, num_moves, player_name
            // db.PrintHighScores()

        private bool LoadDataBase()
        {
            //read data from file
            string testData = File.ReadAllText(PathDB);
            //convert to json
            dataBase = JsonObject.Parse(testData).AsObject();

            return true;
        }

        public void Insert(GameData gamedata)
        {
            string NumOfCards = gamedata.NumCards.ToString();
            
            if (!dataBase.ContainsKey(NumOfCards))
                dataBase.Add(NumOfCards, new JsonObject() { { gamedata.PlayerName, gamedata.Moves } });

            
            else if (dataBase[NumOfCards].AsObject().ContainsKey(gamedata.PlayerName))
                dataBase[NumOfCards].AsObject()[gamedata.PlayerName] = gamedata.Moves < dataBase[NumOfCards].AsObject()[gamedata.PlayerName].GetValue<int>() ? gamedata.Moves : dataBase[NumOfCards].AsObject()[gamedata.PlayerName];
            else
                dataBase[NumOfCards].AsObject().Add(gamedata.PlayerName, gamedata.Moves);

            File.WriteAllText(PathDB, dataBase.ToString());
            Console.WriteLine(dataBase.ToString());
        }

        public void Print()
        {
            Console.WriteLine("Look how well you did!");
            Console.WriteLine();

            // Outcome of my desires:
            //  DB written to list and sorted by lowest #moves
            // Logic
            //  Foreach element in #Numcards key: add Json node to a list
            //  Sort list by #Moves
            //  Return list and print it to console
            // "Key" challenge: (get it?)
            //  How to sort by the #moves value, when key and value are the same list item?

        }

        public DataBase()
        { 
            string fileName = "yaboi.json";
            string directory = @"Data\";
            Directory.CreateDirectory(directory);
            PathDB = Path.Combine(Environment.CurrentDirectory, directory, fileName);

            //Console.WriteLine("This is the DB path: " + PathDB);
            LoadDataBase();
        }
    }
}
