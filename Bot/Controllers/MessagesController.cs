using Bot.Tools;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Bot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private const string ShowBookDetailsCommandName = "show_book_details";
        private const string BooksCommandName = "books";
        private const string AddCommandName = "add";
        private const string PostBackName = "postBack";
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
                switch (tokens[0].ToLower())
                {
                    case AddCommandName:
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

                    case BooksCommandName:
                        var someList = new List<Tuple<string, Guid>>
                        {
                            new Tuple<string, Guid> ("книга 1", Guid.NewGuid() ),
                            new Tuple<string, Guid> ("книга 2", Guid.NewGuid() ),
                            new Tuple<string, Guid> ("книга 3", Guid.NewGuid() ),
                        };
                        var card = new HeroCard
                        {
                            Title = "Books list",
                        };
                        var books = someList.Select(x => new CardAction
                        {
                            Type = PostBackName,
                            Title = x.Item1,
                            Value = ShowBookDetailsCommandName+" "+ x.Item2.ToString("N")
                        }).ToList();
                        card.Buttons = books;
                        reply.Attachments.Add(card.ToAttachment());
                        break;
                    case ShowBookDetailsCommandName:
                        var bookId = Guid.Parse(tokens[1]);
                        var bookDetails = "НАЗЫАГие";
                        var book = new HeroCard
                        {
                            Title = "Book details:",
                            Subtitle = bookDetails,
                            Text = "Book id: "+bookId.ToString("N")
                        };
                        var actions = new List<CardAction>
                        {
                            new CardAction
                            {
                                Title = "Read",
                                Type = PostBackName,
                                Value = "start "+bookId.ToString("N")
                            },
                            new CardAction
                            {
                                Title = "Finish",
                                Type = PostBackName,
                                Value = "finish "+bookId.ToString("N")
                            },
                        };

                        book.Buttons = actions;
                        reply.Attachments.Add(book.ToAttachment());
                        break;
                }
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