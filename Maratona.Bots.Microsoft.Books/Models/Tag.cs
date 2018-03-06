using System;

namespace Maratona.Bots.Microsoft.Books.Models
{
    [Serializable]
    public class Tag
    {
        public double confidence { get; set; }
        public string name { get; set; }
    }
}