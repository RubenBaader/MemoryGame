
namespace CardLibrary
{
    public class Card
    {
        //private int _id;
        //private int _matchId;
        //private string _img;
        private bool _flipped = false;

        public readonly int Id;
        public readonly int MatchId;

        public readonly string BackImg;
        public readonly string Img;
        public bool Flipped 
        {
            get
            {
                return _flipped;
            }
        }

        public bool Flip()
        {
            _flipped = !_flipped;
            return true;
        }
        public Card(int id) 
        {
            Id = id;
            MatchId = id / 2;
            Img = $"Card_Img{MatchId}";
        }
    }
}