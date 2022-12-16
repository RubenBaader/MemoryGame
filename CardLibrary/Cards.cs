
namespace CardLibrary
{
    public class Card
    {
        //private int _id;
        //private int _matchId;
        //private string _img;
        private bool _flipped = false;
        private bool _matched = false;
        private string _icon;

        public readonly int Id;
        public readonly int MatchId;

        public readonly string BackImg;
        public readonly string Img;
        public string Icon 
        { 
            get 
            {
                //Console.WriteLine(_flipped.ToString() + " " + _matched.ToString() + " " + _icon + " " + BackImg);
                if (_flipped)
                    _icon = MatchId.ToString();
                else
                    _icon = BackImg;
                if (_matched)
                    _icon = " ";
                return _icon; 
            } 
            set { _icon = value.ToString(); } 
        }
        public bool Flipped { get { return _flipped; } set { _flipped = value; } }
        public bool Matched { get { return _matched; } set { _matched = value; } }

        public void Flip() { _flipped = !_flipped; }

        public Card(int id) 
        {
            Id = id;
            MatchId = id / 2;
            BackImg = $"#";
            Img = $"Card_Img{MatchId}";
            _icon = BackImg;
        }
    }
}