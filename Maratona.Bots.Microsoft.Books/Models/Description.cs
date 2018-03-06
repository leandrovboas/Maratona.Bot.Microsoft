using System;
using System.Collections.Generic;

namespace Maratona.Bots.Microsoft.Books.Models
{
    [Serializable]
    public class Description
    {
        public List<Caption> captions { get; set; }
        public List<string> tags { get; set; }
    }
}