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
            var myObj2 = myobjList[1];


            string replyMessage = string.Empty;
            replyMessage += $"Here's what I found on {teamName}:\n\n";
            replyMessage += $"*{myObj.Title} \n\n";
            replyMessage += $"*{myObj.Url} \n\n";
            replyMessage += $"*{myObj2.Title} \n\n";
            replyMessage += $"*" + myObj2.Url + '\n';


            await context.PostAsync(replyMessage);
            context.Wait(MessageReceived);
        }


        [LuisIntent("StatsTeamYear")]
        public async Task StatsTeamYear(IDialogContext context, LuisResult result)
        {
            string nameYear = result.Entities.FirstOrDefault(e => e.Type == "StatsTeamYear").Entity;
            //just get team name
            nameYear = nameYear.ToUpper();
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            List<string> title = new List<string>();
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "eef29af8e8ac402ea3f7f65c5ca7771c");
            //for stats
            var uri = "https://api.fantasydata.net/v3/nfl/scores/JSON/TeamSeasonStats/2017?" + queryString;
            //https://api.fantasydata.net/v3/nfl/scores/JSON/Standings/2017
            var response = await client.GetAsync(uri);
            var contents = await response.Content.ReadAsStringAsync();
            //deserilabalfdkj json
            JsonConvert.DeserializeObject(contents);
            var myobjList = JsonConvert.DeserializeObject<List<Stats>>(contents);

            //standings
            var uri1 = "https://api.fantasydata.net/v3/nfl/scores/JSON/Standings/2017?" + queryString;
            //
            var response1 = await client.GetAsync(uri1);
            var contents1 = await response1.Content.ReadAsStringAsync();
            //deserilabalfdkj json
            JsonConvert.DeserializeObject(contents1);
            var myobjList1 = JsonConvert.DeserializeObject<List<Standing>>(contents1);

            //get stats for specific name
            Stats temp = new Stats();
            //get team name by nameyear
            for (int i = 0; i < myobjList.Count; i++)
            {
                if (myobjList[i].Team == nameYear)
                {
                    temp = myobjList[i];
                    break;
                }
            }

            //get standing specific name
            Standing temp1 = new Standing();
            //get team name by nameyear
            for (int i = 0; i < myobjList1.Count; i++)
            {
                if (myobjList1[i].Team == nameYear)
                {
                    temp1 = myobjList1[i];
                    break;
                }
            }

            string replyMessage = string.Empty;
            replyMessage += $"Here are the {nameYear} 2017 season statistics:\n\n";
            replyMessage += $"*Wins: {temp1.Wins} \n\n";
            replyMessage += $"*Losses: {temp1.Losses} \n\n";
            replyMessage += $"*Points Scored: {temp.Score} \n\n";
            replyMessage += $"*Points Allowed: " + temp.OpponentScore + '\n';


            await context.PostAsync(replyMessage);
            context.Wait(MessageReceived);
        }

        [LuisIntent("ScoreSearch")]
        public async Task ScoreSearch(IDialogContext context, LuisResult result)
        {
            string week = result.Entities.FirstOrDefault(e => e.Type == "Week").Entity;
            //just get team name
            string team = result.Entities.FirstOrDefault(e => e.Type == "Team Name").Entity;
            team = team.ToUpper();

            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            List<string> title = new List<string>();
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "eef29af8e8ac402ea3f7f65c5ca7771c");
            //for stats
            var uri = "https://api.fantasydata.net/v3/nfl/scores/JSON/ScoresByWeek/2017/" + week + queryString;
      
            var response = await client.GetAsync(uri);
            var contents = await response.Content.ReadAsStringAsync();
            //deserilabalfdkj json
            JsonConvert.DeserializeObject(contents);
            var myobjList = JsonConvert.DeserializeObject<List<Scores>>(contents);

            //get stats for specific name
            Scores temp = new Scores();
            //get team name by nameyear
            for (int i = 0; i < myobjList.Count; i++)
            {
                if (myobjList[i].AwayTeam.Equals(team) || myobjList[i].HomeTeam.Equals(team)) 
                {
                    temp = myobjList[i];
                }
                
            }


            string replyMessage = string.Empty;
            replyMessage += $"Here is the week {week} score for {team} this season:\n\n";
            replyMessage += $"*{temp.HomeTeam}:";
            replyMessage += $"{temp.HomeScore} \n\n";
            replyMessage += $"*{temp.AwayTeam}:";
            replyMessage += $"{temp.AwayScore}\n";


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