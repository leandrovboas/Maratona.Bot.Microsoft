using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maratona.Bots.Microsoft.Books
{
    public class SevicoLinguagem
    {
        private readonly string _translateApiKey = ConfigurationManager.AppSettings["TranslateApiKey"];
        private readonly string _translateUri = ConfigurationManager.AppSettings["TranslateUri"];

        public async Task<string> TraducaoDeTextoAsync(string texto)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _translateApiKey);

                var uri = _translateUri + "?to=pt-br" +
                          "&text=" + System.Net.WebUtility.UrlEncode(texto);

                var response = await client.GetAsync(uri);
                var result = await response.Content.ReadAsStringAsync();
                var content = XElement.Parse(result).Value;

                return $"Texto original: **{ texto }**\nTradução: **{ content }**";
            }
        }
    }
}