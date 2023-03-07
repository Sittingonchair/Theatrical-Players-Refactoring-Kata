namespace TheatricalPlayersRefactoringKata.Models
{
    public class Performance
    {
        private string _playID;
        private int _audience;

        public string PlayID { get => _playID; set => _playID = value; }
        public int Audience { get => _audience; set => _audience = value; }

        public Performance(string playID, int audience)
        {
            _playID = playID;
            _audience = audience;
        }

    }
}
