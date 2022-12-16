using MemoryGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace CardLibrary
{
    public class Board
    {
        private List<Card> _matchList = new List<Card>();
        private int _moves = 0;

        public Card[][] Squares { get; }
        public readonly int BoardHeight;
        public readonly int BoardWidth;
        public int Moves { get { return _moves; } set { _moves = value; } }

        public List<Card> MatchList { get { return _matchList; } }

        public void CompareMatchList()
        {
            //check if cards match
            if (MatchList.Count == 2)
            {
                //reprint board to preserve info
                Print();

                if (MatchList[0].MatchId == MatchList[1].MatchId)
                    foreach (Card card in MatchList)
                        card.Matched = true;

                foreach (Card card in MatchList)
                    card.Flipped = false;

                Moves++;
                
                //clear list
                MatchList.Clear();
            }
        }
        
            //Simplify print:
                //Board skal være blindt for kortene (print card.icon)
                //Kortene skal manage deres egen state (flipped, match, icon)
                //Bevar info på move 2: print board med vist matchid. Print igen med matchede kort removed
        public void Print()
        {
            foreach (Card[] row in Squares)
            {
                foreach (Card card in row)
                    Console.Write($"|{card.Icon}|".PadRight(3));

                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void Flip(int x,int y)
        {
            Squares[y][x].Flip();
        }

        public Board(List<Card> cards)
        {
            //BoardWidth = inner array max length
            BoardWidth = (int)Math.Ceiling(Math.Sqrt((double)cards.Count));

            Squares = cards.Chunk(BoardWidth).ToArray();

            //BoardHeight = outer array length
            BoardHeight = Squares.Length;

        }
    }
}
