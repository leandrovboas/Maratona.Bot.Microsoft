using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Builder.Luis;
using System.Linq;

namespace Maratona.Bots.Microsoft.Books.Dialogs
{
    [Serializable]
    public class RootDialog : LuisDialog<object>
    {
        public RootDialog(ILuisService service) : base(service) { }

        /// <summary>
        /// Quando não houve intenção reconhecida.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [LuisIntent("")]
        public async Task IntencaoNaoReconhecidaAsync(IDialogContext context, LuisResult result)
        {
            context.Wait(MessageReceivedAsync);

            await context.PostAsync("**( ͡° ͜ʖ ͡°)** - Desculpe, mas não entendi o que você quis dizer.\n" +
                                    "Lembre-se que sou um bot e meu conhecimento é limitado.");
        }

        /// <summary>
        /// Caso a intenção não seja reconhecida.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [LuisIntent("None")]
        public async Task NoneAsync(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Desculpe, eu não entendi...\n" +
                                    "Lembre-se que sou um bot e meu conhecimento é limitado.");
            context.Done<string>(null);
        }

        /// <summary>
        /// Quando a intenção for por consciencia.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [LuisIntent("consciencia")]
        public async Task ConscienciaAsync(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("**(▀̿Ĺ̯▀̿ ̿)** - Eu sou famoso **Bot Inteligentão**\nFalo vários idiomas e reconheço padrões...");
            context.Done<string>(null);
        }

        /// <summary>
        /// Quando a intenção for por ajuda.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [LuisIntent("ajudar")]
        public async Task AjudarAsync(IDialogContext context, LuisResult result)
        {
            const string response = "Não se esqueça que eu sou um **Bot** e minha conversação é limitada. Olha ai o que eu consigo fazer:\n" +
                             "* **Descrever a capa de um livro**\n" +
                             "* **Traduzir textos**\n" +
                             "* **Recomendar liveos por categoria**\n" +
                             "* **Recomendar liveos que o boot gosta**\n";
            await context.PostAsync(response);
            context.Done<string>(null);
        }

        /// <summary>
        /// Quando a intenção for uma saudação.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [LuisIntent("saudar")]
        public async Task SaudarAsync(IDialogContext context, LuisResult result)
        {
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")).TimeOfDay;
            string saudacao;

            saudacao = now < TimeSpan.FromHours(12) ? "Bom dia" : now < TimeSpan.FromHours(18) ? "Boa tarde" : "Boa noite";

            await context.PostAsync($"{saudacao}! Em que posso ajudar?");
            context.Done<string>(null);
        }

        /// <summary>
        /// Tradução de texto
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [LuisIntent("traducao")]
        public async Task TraducaoAsync(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("**(▀̿Ĺ̯▀̿ ̿)** - Ok, me fala o texto então...");
            context.Wait(TraduzirPtBrAsync);
        }

        [LuisIntent("descrever-imagem")]
        public async Task DescreverImagenAsync(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Beleza, me passa a url da imagem que eu descrevo o que tem nela.");
            context.Wait((c, a) => ProcessarImagemAsync(c, a));
        }

        #region [Métodos internos]
        private async Task TraduzirPtBrAsync(IDialogContext context, IAwaitable<IMessageActivity> value)
        {
            var message = await value;

            var text = message.Text;

            var response = await new SevicoLinguagem().TraducaoDeTextoAsync(text);

            await context.PostAsync($"Texto original: **{ text }**\nTradução: **{ response }**");
            context.Wait(MessageReceived);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            var length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            await context.PostAsync($"You sent {activity.Text} which was {length} characters");

            context.Wait(MessageReceivedAsync);
        }

        private async Task ProcessarImagemAsync(IDialogContext contexto,
            IAwaitable<IMessageActivity> argument)
        {
            var activity = await argument;

            var uri = activity.Attachments?.Any() == true ?
                new Uri(activity.Attachments[0].ContentUrl) :
                new Uri(activity.Text);

            try
            {
                var reply = await new ServicoVisaoComputacional().AnaliseDetalhadaAsync(uri);
                await contexto.PostAsync(reply);
            }
            catch (Exception ex)
            {
                await contexto.PostAsync("Ops! Deu algo errado na hora de analisar sua imagem!");
            }

            contexto.Wait(MessageReceived);
        }

        #endregion
    }
}