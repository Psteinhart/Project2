using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chatbot.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatbot.DataService.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    [EnableCors("AllowSpecificOrigin")]
    public class UserController : Controller
    {
        private static SpotDBContext _db = new SpotDBContext();// = new SpotDBContext();
        // GET: api/User
        [HttpGet]
       // [ValidateAntiForgeryToken]
        public IEnumerable<UserInfo> GetAll()
        {
            if (ModelState.IsValid)
            {
                var item = _db.UserInfo.ToList();
                if (item == null)
                {
                    return null;
                }
                return item;
            }
            return null;

        }

        // GET: api/User/5
        [HttpGet("{Email}")]
        //[ValidateAntiForgeryToken]
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
        [EnableCors("AllowSpecificOrigin")]
        //[ValidateAntiForgeryToken]
        public IActionResult Post([FromBody]UserInfo item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest();
                }
                item.ModifiedDate = DateTime.Now;
                item.Active = true;
                item.UserInterest = null;


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
