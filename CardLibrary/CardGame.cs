
using CardLibrary;
using System.Collections.Generic;

namespace MemoryGame
{
    public class CardGame
    {
        private int _num_cards;
        private List<Card> _cards = new List<Card>();
        

        public int NumCards { get { return _num_cards; } }
        public List<Card> Cards { get { return _cards; } }
        

        private bool Init(int num_cards)
        {
            if (num_cards > 0 && num_cards % 2 == 0 && num_cards <= 36)
            {
                Console.WriteLine("Card IDs:");

                //generate num_cards new cards
                //each card has id i
                for (int i = 0; i < num_cards; i++)
                {
                    Cards.Add(new Card(i));
                    Console.WriteLine(Cards[i].Id);
                }
                Board GameBoard = new Board(Cards);
                Console.WriteLine("Shuffle");
                Shuffle(Cards);
                foreach (Card card in Cards)
                    Console.WriteLine(card.Id);
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
            Init(NumCards);

            //shuffle()
            //printBoard()
            //monitor()

            return;
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

        private bool PrintBoard()
        {
            //load card list into 2D array

            //foreach in array:
            // print card to console
            // print flipped state along with card
            return true;
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
