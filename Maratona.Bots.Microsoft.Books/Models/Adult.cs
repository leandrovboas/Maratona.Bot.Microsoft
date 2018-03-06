using System;

namespace Maratona.Bots.Microsoft.Books.Models
{
    [Serializable]
    public class Adult
    {
        public double adultScore { get; set; }
        public bool isAdultContent { get; set; }
        public bool isRacyContent { get; set; }
        public double racyScore { get; set; }
    }
}