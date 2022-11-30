
using CardLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace MemoryGame
{
    public class CardGame
    {
        private int _num_cards;
        
        private List<Card> _cards = new List<Card>();
        
        public int NumCards { get { return _num_cards; } }
        
        public List<Card> Cards { get { return _cards; } }
        public Board Board;

        public struct GameData 
        {
            public int NumCards;
            public string PlayerName;
            public int Moves;

            public GameData(int cards, string name, int moves) 
            {
                NumCards = cards;
                PlayerName = name;
                Moves = moves;
            }
        }

        private bool Init(int num_cards)
        {
            if (num_cards > 0 && num_cards % 2 == 0 && num_cards <= 36)
            {
                //generate num_cards new cards
                for (int i = 0; i < num_cards; i++)
                {
                    Cards.Add(new Card(i));
                }
                Shuffle(Cards);
                Board = new Board(Cards);
                Console.WriteLine("Here is your board");
                Board.Print();

            }
            else
            {
                Console.Error.WriteLine("Enter an even number between 2 and 36 (inclusive)");
                return false;
            }
            return true;
        }
        public void Run()
        {
            //Initialize
            if (!Init(NumCards))
                return;
            //monitor
            while (true) 
            {
                Console.WriteLine("Please enter card to flip");
                
                int x;
                int y;
                //(Input valid? , (x, y))
                (bool, (int?, int?)) input = VerifyInput(Console.ReadLine());
                
                // if input is invalid, restart loop
                if (!input.Item1)
                    continue;
                else
                {                    
                    x = input.Item2.Item1 ?? -1;
                    y = input.Item2.Item2 ?? -1;
                }
                
                try 
                { 
                    Board.Flip(x, y);
                }
                catch(IndexOutOfRangeException e) 
                {
                    //Console.Error.WriteLine(e.ToString());
                    Console.Error.WriteLine("Index out of range. Please try again");
                    Console.WriteLine();
                    continue;
                }
                
                Board.MatchList.Add(Board.Squares[y][x]);
                Board.CompareMatchList();
                Board.Print();


                //Has game ended?
                    //if all cards are matched -> end game, show score
                if (Board.Squares.All
                        ( row => row.All
                            ( card => card.Matched ) 
                        )
                    )
                {
                    Console.WriteLine("You won!");
                    Console.WriteLine($"You spent {Board.Moves} moves!");
                    break;
                }
            }
            End();
        }
        public void End()
        {
            //Info from game: num_moves, num_cards, player name (console input)
            //Info from file: As above, but for all games ever

            Console.WriteLine("Do you want to save your score? (press n for no)");
            var ans = Console.ReadKey();
            if (ans.KeyChar == 'n')
                return;


            Console.WriteLine("Please enter your name");
            string? playerName = Console.ReadLine();


            GameData data = new GameData(NumCards, playerName, Board.Moves);
            DataBase dataBase = new DataBase();
            dataBase.Insert(data);

            //Console.WriteLine("End method ended");

            return;
        }

        private (Boolean, (int?, int?)) VerifyInput(String input)
        {
            //string? input = Console.ReadLine();
            //string s = input;
            string[] strings = input.Split(" ");
            if (strings.Length != 2)
            {
                Console.Error.WriteLine("Enter two coordinates and separate with space");
                Console.WriteLine();
                return (false, (null, null));
            }

            int x;
            int y;
            bool xCheck = int.TryParse(strings[0], out x);
            bool yCheck = int.TryParse(strings[1], out y);

            if (!(xCheck && yCheck))
            {
                Console.Error.WriteLine("Input must be integers");
                Console.WriteLine();
                return (false, (null, null));
            }

            return (true, (x, y));
        }

        private void Shuffle(List<Card> Cards)
        {
            //shuffle card list
            {
                int n = Cards.Count;
                while (n > 1)
                {
                    n--;
                    int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                    Card value = Cards[k];
                    Cards[k] = Cards[n];
                    Cards[n] = value;
                }
            }
            return;
        }

        public static class ThreadSafeRandom
        {
            [ThreadStatic] private static Random Local;

            public static Random ThisThreadsRandom
            {
                get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
            }
        }


        private void Monitor()
        {
            return;
        }

        public CardGame (int num_cards) 
        {
            _num_cards = num_cards;

        }
    }
}
