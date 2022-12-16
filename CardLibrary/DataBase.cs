using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static MemoryGame.CardGame;

namespace MemoryGame
{
    public class DataBase
    {
        //Data to contain: Game size (key), player name, number of moves
        //Format: Json  { GameKey : [{player_name, #moves}, {player_name, #moves}...] }

        private string GameKey;
        private string PathDB { get; set; }

        public JObject dataBase;

        //test json
        //private string JsonTest = "{ '2' : [{ 'hello' : 2 }, { 'hello' : 3 }] }";


        private class HScore
        {
            public string name { get; set; }
            public int score { get; set; }

            public HScore(string name, int score) 
            { 
                this.name = name; 
                this.score = score; 
            }
        };


        private bool LoadDataBase()
        {
            //read data from file, using try/catch to emulate the possibility of failing a load from an imaginary server
            string json;
            try
            {
                json = File.ReadAllText(PathDB);
            }
            catch
            {
                Console.WriteLine("Failed to load database");
                return false;
            }

            //verify json format
            if (!json.Contains('{') || !json.Contains('}'))
                dataBase = new JObject();
            else
                dataBase = JObject.Parse(json);

            return true;
        }


        public void Insert(GameData gamedata)
        {
            GameKey = gamedata.NumCards.ToString();

            //number of cards in game is added as key in the database, unless it already exists
            //      add the current game's stats to the array of game stats
            if (!dataBase.ContainsKey(GameKey))
            {
                dataBase.Add(
                    new JProperty(
                        GameKey, 
                        new JArray(
                            new JObject 
                            {
                                { gamedata.PlayerName, gamedata.Moves }
                            }
                        )
                    )
                );
            }
            else
            {
                JArray TempArr = (JArray)dataBase[GameKey];
                TempArr.Add(new JObject { { gamedata.PlayerName, gamedata.Moves } });
                dataBase[GameKey] = TempArr;
            }

        }

        //extract the relevant array for this game's number of cards and sort it by game moves in ascending order
        public void Sort()
        {
            JArray SortArr = (JArray)dataBase[GameKey];
            List<HScore> SortList = new List<HScore>();

            //extract JProps from JObjects
            foreach (JObject item in SortArr)
            {
                HScore PropHolder = new HScore("", -1);
                foreach (JProperty prop in item.Properties())
                {
                    PropHolder.name = prop.Name;
                    PropHolder.score = (int)prop.Value;
                }
                SortList.Add(PropHolder);
            }

            //sort by extracted prop value
            var DoneSorting = SortList.OrderBy(x => x.score);

            //recompress props to key:value pairs
            JArray SortedArr = new JArray();
            foreach (HScore item in DoneSorting)
            {
                SortedArr.Add(new JObject{ { item.name, item.score } } );
            }

            dataBase[GameKey] = SortedArr;
        }

        public bool Save()
        {
            //write db to file - ensure that save succeeds

            try
            {
                //save data
                File.WriteAllText(PathDB, dataBase.ToString());
            }
            catch
            {
                Console.WriteLine("Failed to save data");
                return false;
            }

            return true;
        }

        public void Print()
        {
            Console.WriteLine("Look how well you did!");
            Console.WriteLine($"Cards in game: {GameKey}" );
            Console.WriteLine();

            foreach (JObject score in dataBase[GameKey])
                Console.WriteLine(score.First.ToString().PadRight(12) );
        }

        public DataBase()
        { 
            string fileName = "Highscores.json";
            string directory = @"Data\";
            Directory.CreateDirectory(directory);
            PathDB = Path.Combine(Environment.CurrentDirectory, directory, fileName);

            LoadDataBase();
        }
    }
}
