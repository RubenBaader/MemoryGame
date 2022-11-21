
using CardLibrary;
using System.Collections.Generic;
using System.Linq;

namespace MemoryGame
{
    public class CardGame
    {
        private int _num_cards;
        
        private List<Card> _cards = new List<Card>();
        
        public int NumCards { get { return _num_cards; } }
        
        public List<Card> Cards { get { return _cards; } }
        public Board Board;

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
                //Console.Error.WriteLine("Enter an even number between 2 and 36 (inclusive)");
                throw new ArgumentException(String.Format("Number of cards must be an even integer between 2 and 26"));
            }
            return true;
        }
        public void Run()
        {
            //Initialize
            Init(NumCards);

            //monitor
            while (true) 
            {

                // change this to input method
                // if !inputValid restart input method
                Console.WriteLine("Please enter card to flip");
                string? input = Console.ReadLine();
                string[] strings = input.Split(" ");
                if (strings.Length != 2)
                {
                    Console.Error.WriteLine("Lolno");
                    return;
                }

                int x;
                int y;
                bool xCheck = int.TryParse(strings[0], out x);
                bool yCheck = int.TryParse(strings[1], out y);

                if (!(xCheck && yCheck))
                {
                    Console.Error.WriteLine("Input must be integers");
                    return;
                }


                Board.Flip(x, y);
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
                    return;
                }
            }
        }
        private void End()
        {
            return;
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

            //create board
            // board columns = num_cards^1/2
            // board row = 
        }
    }
}
