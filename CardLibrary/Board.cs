using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CardLibrary
{
    public class Board
    {
        //private Card[][] _squares;

        public Card[][] Squares { get; }
        public readonly int BoardHeight;
        public readonly int BoardWidth;
        public readonly int BlankSquares;

        public Board(List<Card> cards)
        {
            //BoardWidth = inner array max length
            BoardWidth = (int)Math.Ceiling(Math.Sqrt((double)cards.Count));
            //BoardHeight = outer array length
            BoardHeight = cards.Count / BoardWidth;
            BlankSquares = BoardWidth - (cards.Count % BoardWidth);

            //Squares[X][] length = BoardHeight
            Squares = new Card[BoardHeight][];
            
            //Squares[][X] lengths = loop through Squares[X][]
                //lengths = BoardWidth up until second to last instance
            for (int i=0; i < BoardHeight - 1; i++) 
            {
                Squares[i] = new Card[BoardWidth];
            }
                //last length = BoardWidth minus number of blank fields
            Squares[BoardHeight - 1] = new Card[BoardWidth - BlankSquares];
        }
    }
}
