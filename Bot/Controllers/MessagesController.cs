using Bot.Tools;
using Microsoft.Bot.Connector;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Bot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private readonly ReadOne.Application.ReadOne _app;

        public MessagesController(ReadOne.Application.ReadOne app)
        {
            _app = app;
        }

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                var connectorClient = new ConnectorClient(new Uri(activity.ServiceUrl));
                var tokens = activity.Text.ToTokens();
                var reply = activity.CreateReply();
                switch (tokens[0])
                {
                    case "add":
                        if (tokens.Length < 2)
                        {
                            reply.Text = "Using: add \"Books name\"";
                        }
                        else
                        {
                            _app.Do(CommandHelper.CreateAdd(tokens[1]));
                            reply.Text = "Book was added";
                        }
                        break;

                    case "books":
                        //todo
                        break;
                }
                var header = string.Empty;

                /*              var heroCard = new HeroCard
                              {
                                  Title = header,
                                  Buttons = new List<CardAction>
                                  {
                                      new CardAction {Type = "postBack", Title = "Select 1 fdshfsdjkhsdfjk", Value = "1"},
                                      new CardAction {Type = "postBack", Title = "Select 1 fdshfsdjkhsdfjkfdshfsdjkhsdfjk", Value = "1"},
                                      new CardAction {Type = "postBack", Title = "Select 1", Value = "1"},
                                      new CardAction {Type = "postBack", Title = "Select 1", Value = "1"},
                                      new CardAction {Type = "postBack", Title = "Select 1", Value = "1"},
                                      new CardAction {Type = "postBack", Title = "Select 1", Value = "1"},
                                      new CardAction {Type = "postBack", Title = "Select 1fdshfsdjkhsdfjkfdshfsdjkhsdfjkfdshfsdjkhsdfjk", Value = "1"},
                                      new CardAction {Type = "postBack", Title = "Select 1", Value = "1"},
                                      new CardAction {Type = "postBack", Title = "Select 1", Value = "1"},
                                      new CardAction {Type = "postBack", Title = "Select 1fdshfsdjkhsdfjkfdshfsdjkhsdfjkfdshfsdjkhsdfjkfdshfsdjkhsdfjk", Value = "1"},
                                      new CardAction {Type = "postBack", Title = "Select 1", Value = "1"},
                                  },
                                  Tap = new CardAction
                                  {
                                      Value = "44"
                                  }
                              };
                              reply.Attachments.Add(heroCard.ToAttachment());
              */
                await connectorClient.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}