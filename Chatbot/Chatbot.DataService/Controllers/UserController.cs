using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chatbot.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatbot.DataService.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private static SpotDBContext _db = new SpotDBContext();// = new SpotDBContext();
        // GET: api/User
        [HttpGet]
        public IEnumerable<UserInfo> GetAll()
        {
            return _db.UserInfo.ToList();
        }
        // GET: api/User/5
        [HttpGet("{Email}")]
        public IActionResult GetById(string Email)
        {
            var item = _db.UserInfo.FirstOrDefault(t => t.Email == Email);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        // POST: api/User
        [HttpPost]
        public IActionResult Post([FromBody]UserInfo item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest();
                }

                _db.UserInfo.Add(item);
                _db.SaveChanges();

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
