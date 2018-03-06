using System;

namespace Maratona.Bots.Microsoft.Books.Models
{
    [Serializable]
    public class Category
    {
        public string name { get; set; }
        public double score { get; set; }
    }
}