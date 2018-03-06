using System;
using System.Collections.Generic;

namespace Maratona.Bots.Microsoft.Books.Models
{
    [Serializable]
    public class AnalyzeResult
    {
        public Adult adult { get; set; }
        public List<Category> categories { get; set; }
        public Description description { get; set; }
        public List<object> faces { get; set; }
        public string requestId { get; set; }
        public List<Tag> tags { get; set; }
    }
}