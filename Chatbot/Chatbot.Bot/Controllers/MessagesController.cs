﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Chatbot.Bot.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Chatbot.Bot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        /// 
        ///
        
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new TestLuisDialog());
            }
            else
            {
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                var reply = HandleSystemMessage(activity);
                if (reply != null)
                    await connector.Conversations.ReplyToActivityAsync(reply);
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
                string replyMessage = string.Empty;
                replyMessage += $"Hi there\n\n";
                replyMessage += $"I am Spot. Designed to answer questions about Sports.  \n";
                replyMessage += $"Currently I have following features  \n";
                replyMessage += $"* Ask question about the author of this App: Try 'Who is Phil'\n\n";
                replyMessage += $"I will get more intelligent in future.";
                return message.CreateReply(replyMessage);
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