using AdaptiveCards;
using Bot.Tools;
using Microsoft.Bot.Connector;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI.WebControls;

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
                var reply = activity.CreateReply();
                if (string.IsNullOrEmpty(activity.Text) && activity.Value != null)
                {
                    var reviewData = (JObject)activity.Value;
                    var bookId = reviewData.GetValue("guid").Value<string>();
                    var bookGuid = Guid.Parse(bookId);
                    var rate = reviewData.GetValue("rate").Value<string>();
                    var rateAsInt = int.Parse(rate);
                    var reviewText = reviewData.GetValue("review").Value<string>() ?? string.Empty;
                    var reader = reviewData.GetValue("reader").Value<string>() ?? string.Empty;
                    var tagsString = reviewData.GetValue("tags").Value<string>() ?? string.Empty;
                    var tags = tagsString.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    _app.Do(CommandHelper.CreateReview(bookGuid, rateAsInt, reviewText, reader, tags));
                    reply.Text = "Review added!";
                }
                else
                {
                    var tokens = activity.Text.ToTokens();
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
                            var someList = _app.Books(new string[0]);
                            var card = new HeroCard
                            {
                                Title = "Books list",
                            };
                            var books = someList.Select(x => new CardAction
                            {
                                Type = PostBackName,
                                Title = x.Name,
                                Value = ShowBookDetailsCommandName + " " + x.Id.ToString("N")
                            }).ToList();
                            card.Buttons = books;
                            reply.Attachments.Add(card.ToAttachment());
                            break;

                        case ShowBookDetailsCommandName:
                            var bookId = Guid.Parse(tokens[1]);
                            var bookInfo = _app.Books(new string[0]).FirstOrDefault(x => x.Id == bookId);
                            if (bookInfo == null)
                            {
                                reply.Text = "Can't get book details";
                                break;
                            }
                            var bookDetails = bookInfo.Name;
                            var book = new HeroCard
                            {
                                Title = "Book details:",
                                Subtitle = bookDetails,
                                Text = "Book id: " + bookId.ToString("N")
                            };
                            var actions = new List<CardAction>
                        {
                            new CardAction
                            {
                                Title = "Read",
                                Type = PostBackName,
                                Value = "start " + bookId.ToString("N")
                            },
                            new CardAction
                            {
                                Title = "Finish",
                                Type = PostBackName,
                                Value = "finish " + bookId.ToString("N")
                            },
                            new CardAction
                            {
                                Title = "Review",
                                Type = PostBackName,
                                Value = "review " + bookId.ToString("N")
                            }
                        };

                            book.Buttons = actions;
                            reply.Attachments.Add(book.ToAttachment());
                            break;

                        case "review":
                            var id = Guid.Parse(tokens[1]);
                            var bookInf = _app.Books(new string[0]).FirstOrDefault(b => b.Id == id);
                            if (bookInf == null)
                            {
                                reply.Text = "Can't get book details";
                                break;
                            }
                            var ca = new AdaptiveCard
                            {
                                Title = "Review Book:",
                                Body = new List<CardElement>
                            {
                                new TextBlock
                                {
                                    Text = "Book:",
                                    Size = TextSize.ExtraLarge
                                },

                                new TextBlock
                                {
                                    Text = bookInf.Name,
                                    Size = TextSize.Large
                                },
                                new TextBlock
                                {
                                    Text = "Your name:",
                                },
                                new TextInput
                                {
                                    Id = "reader",
                                },
                                new TextBlock
                                {
                                    Text = "Rate book:"
                                },
                                new ChoiceSet
                                {
                                    Choices = new List<Choice>
                                    {
                                        new Choice {Title = "5 (Good)", Value = "5", IsSelected = true},
                                        new Choice {Title = "4", Value = "4"},
                                        new Choice {Title = "3", Value = "3"},
                                        new Choice {Title = "2", Value = "2"},
                                        new Choice {Title = "1 (Bad)", Value = "1"},
                                    },
                                    Style = ChoiceInputStyle.Expanded,
                                    Id = "rate",
                                    IsRequired = true
                                },
                                new TextBlock
                                {
                                    Text = "Review:"
                                },
                                new Input
                                {
                                    Id = "review",
                                    Type = "Input.Text",
                                },

                                new TextBlock
                                {
                                    Text = "Tags (comma-separated):"
                                },
                                new TextInput
                                {
                                    Id = "tags"
                                },
                                new TextBlock
                                {
                                    Text = "Book id:",
                                },
                                new TextInput
                                {
                                    Id = "guid",
                                    Value = bookInf.Id.ToString("N"),
                                }
                            },
                                Actions = new List<ActionBase>
                            {
                                new SubmitAction
                                {
                                    Title = "Submit",
                                    Type = "Action.Submit",
                                }
                            }
                            };
                            reply.Attachments.Add(new Attachment
                            {
                                ContentType = AdaptiveCard.ContentType,
                                Content = ca
                            });

                            break;
                    }
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