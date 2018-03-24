using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maratona.Bots.Microsoft.Books.Models
{
    public class Livro
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("autor")]
        public string Autor { get; set; }
        [JsonProperty("editora")]
        public string Editora { get; set; }
        [JsonProperty("qtdPaginas")]
        public int QtdPaginas { get; set; }
        [JsonProperty("urlImagem")]
        public string UrlImagem { get; set; }
    }
}