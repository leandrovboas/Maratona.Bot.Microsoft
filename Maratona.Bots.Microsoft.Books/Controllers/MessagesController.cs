using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using Microsoft.Bot.Builder.Luis;
using System.Configuration;
using System.Linq;
using Maratona.Bots.Microsoft.Books.Dialogs;

namespace Maratona.Bots.Microsoft.Books
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            using (var connector = new ConnectorClient(new Uri(activity.ServiceUrl)))
            {

                var attributes = new LuisModelAttribute(ConfigurationManager.AppSettings["LuisId"], ConfigurationManager.AppSettings["LuisSubscriptionKey"]);
                var service = new LuisService(attributes);

                switch (activity.Type)
                {
                    case ActivityTypes.Message:
                        await Conversation.SendAsync(activity, () => new RootDialog(service));
                        break;
                    case ActivityTypes.ConversationUpdate:
                        if (activity.MembersAdded.Any(o => o.Id == activity.Recipient.Id))
                        {
                            var reply = activity.CreateReply();
                            reply.Text = "Olá, eu sou o **Bot Inteligentão**. Curte ai o que eu posso fazer:\n" +
                                         "* **Descrever a capa de um livro**\n" +
                                         "* **Traduzir textos**\n" +
                                         "* **Recomendar liveos que eu gosto**\n" +
                                         "* **Recomendar liveos por categoria**\n";

                            await connector.Conversations.ReplyToActivityAsync(reply);
                        }
                        break;
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}