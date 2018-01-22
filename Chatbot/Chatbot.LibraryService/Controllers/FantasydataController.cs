using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Chatbot.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatbot.LibraryService.Controllers
{
    [Produces("application/json")]
    [Route("api/Fantasydata")]
    [EnableCors("AllowSpecificOrigin")]
    public class FantasydataController : Controller
    {
        private static SpotDBContext _db = new SpotDBContext();// = new SpotDBContext();
        // GET: api/Fantasydata
        [HttpGet]
        [EnableCors("AllowSpecificOrigin")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Fantasydata/5
        [EnableCors("AllowSpecificOrigin")]
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Fantasydata
        [HttpPost]
        [EnableCors("AllowSpecificOrigin")]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Fantasydata/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        //get by team
        static async void MakeRequest()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            var team = "nyg";
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "{eef29af8e8ac402ea3f7f65c5ca7771c}");

            var uri = "https://api.fantasydata.net/v3/nfl/scores/Json/NewsByTeam/{team}?" + queryString;

            var response = await client.GetAsync(uri);
        }
    }
}
