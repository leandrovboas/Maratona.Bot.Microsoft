using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Builder.Luis;

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
            var response = "Não se esqueça que eu sou um **Bot** e minha conversação é limitada. Olha ai o que eu consigo fazer:\n" +
                       "* **Falar que nem gente**\n" +
                       "* **Descrever imagens**\n" +
                       "* **Reconhecer emoções**\n" +
                       "* **Classificar objetos**\n" +
                       "* **Traduzir textos**\n" +
                       "* **Recomendar liveos por categoria**\n" +
                       "* **Recomendar livros por livros lidos**";
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

            if (now < TimeSpan.FromHours(12)) saudacao = "Bom dia";
            else if (now < TimeSpan.FromHours(18)) saudacao = "Boa tarde";
            else saudacao = "Boa noite";

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
            context.Wait(TraduzirPtBr);
        }

        #region [Métodos internos]
        private async Task TraduzirPtBr(IDialogContext context, IAwaitable<IMessageActivity> value)
        {
            var message = await value;

            var text = message.Text;

            var response = await new SevicoLinguagem().TraducaoDeTextoAsync(text);

            await context.PostAsync(response);
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

        #endregion
    }
}