using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using Chatbot.Bot.Models;

namespace Chatbot.Bot.Dialogs
{
    //Add a comment
    [Serializable]
    [LuisModel("652ee539-f05a-4076-b790-06ab8d8b0cf3", "4ac32110c73144c09bb3f5ee4398e79d")]
    public class TestLuisDialog : LuisDialog<Task>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }


        [LuisIntent("TeamSearch")]
        public async Task TeamSearch(IDialogContext context, LuisResult result)
        {
            string teamName = result.Entities.FirstOrDefault(e => e.Type == "Team Name").Entity;
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            List<string> title = new List<string>();
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "eef29af8e8ac402ea3f7f65c5ca7771c");

            var uri = "https://api.fantasydata.net/v3/nfl/scores/Json/NewsByTeam/"+teamName+"?" + queryString;

            var response = await client.GetAsync(uri);
            var contents = await response.Content.ReadAsStringAsync();

            //deserilabalfdkj json
            JsonConvert.DeserializeObject(contents);
            var myobjList = JsonConvert.DeserializeObject<List<Article>>(contents);
            var myObj = myobjList[0];

            string replyMessage = string.Empty;
            replyMessage += "*" + myObj.Title + '\n';


            await context.PostAsync(replyMessage);
            context.Wait(MessageReceived);
        }



        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I'm sorry. I didn't understand you.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("AboutMe")]
        public async Task AboutMe(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(@"Phil is a super cool dude");
            await context.PostAsync(@"He is a technology enthusiast and loves to dig in emerging technologies. Most of his working hours are spent on creating architecture, evaluating upcoming products and developing frameworks.");
            context.Wait(MessageReceived);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as IMessageActivity;

            // TODO: Put logic for handling user message here

            context.Wait(MessageReceivedAsync);
        }
    }
}