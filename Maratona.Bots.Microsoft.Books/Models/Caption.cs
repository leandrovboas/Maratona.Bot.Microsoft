using System;

namespace Maratona.Bots.Microsoft.Books.Models
{
    [Serializable]
    public class Caption
    {
        public double confidence { get; set; }
        public string text { get; set; }
    }
}