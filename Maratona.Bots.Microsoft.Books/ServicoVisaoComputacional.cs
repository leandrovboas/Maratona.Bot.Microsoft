using Maratona.Bots.Microsoft.Books.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Maratona.Bots.Microsoft.Books
{
    public class ServicoVisaoComputacional
    {

        private readonly string _computerVisionApiKey = ConfigurationManager.AppSettings["ComputerVisionApiKey"];
        private readonly string _computerVisionUri = ConfigurationManager.AppSettings["ComputerVisionUri"];

        public async Task<string> AnaliseDetalhadaAsync(Uri query)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _computerVisionApiKey);

                HttpResponseMessage response = null;

                var queryString = HttpUtility.ParseQueryString(string.Empty);
                queryString["visualFeatures"] = "Categories,Tags,Description,Faces,ImageType,Color,Adult";

                var byteData = Encoding.UTF8.GetBytes("{ 'url': '" + query + "' }");

                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    response = await client.PostAsync($"{_computerVisionUri}?{queryString}",
                        content).ConfigureAwait(false);
                }

                var responseString = await response.Content.ReadAsStringAsync();

                var analise = JsonConvert.DeserializeObject<AnalyzeResult>(responseString);

                var descricao = await new SevicoLinguagem().TraducaoDeTextoAsync(analise.description.captions.FirstOrDefault()?.text);
                var temConteudoAdulto = analise.adult.isAdultContent? "Não": "Sim";
                var temConteudoRacista = analise.adult.isRacyContent? "Não" : "Sim";
                var quantasPessoasForamIdentificadas = analise.faces.Count== 0? "Nenhuma": analise.faces.Count.ToString();

                var builder = new StringBuilder("Eu identifiquei os seguintes objetos na imagem: ");
                for (var i = 0; i < analise.tags.Count; i++)
                {
                    if (i == analise.tags.Count - 1) builder.Append(" & ");
                    else if (i != 0) builder.Append(", ");

                    builder.Append(analise.tags[i].name);
                }

                return $"Descrição: **{descricao}**\n" +
                       $"Tags: **{builder}**\n" +
                       $"Tem conteúdo adulto: **{temConteudoAdulto}**\n" +
                       $"Tem conteúdo racista: **{temConteudoRacista}**\n" +
                       $"Tem alguma pessoa na foto: **{quantasPessoasForamIdentificadas}**";
            }
        }
    }
}