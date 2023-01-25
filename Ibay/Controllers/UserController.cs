using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using ClassIbay;
using IbayApi.Services;

namespace Ibay.Controllers
{
    public class UserController : ControllerBase

    {
        private IbayContext _userContext;
        public UserController(IbayContext context)
        {
            _userContext = context;
        }

        // GET: user
        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            return Ok(_userContext.Users);
        }

        // GET: user/id
        [HttpGet]
        [Route("/id")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: user
        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            _userContext.Users.Add(user);
            _userContext.SaveChanges();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id}, user);
        }

        // PUT: user
        [HttpPut]
        public ActionResult UpdateUser(int id, User user)
        {
            if(id != user.Id)
                return BadRequest();
            var userExist = _userContext.Users.Where(u => u.Id == id).FirstOrDefault();
            if (userExist is null) return NotFound(id);

            try
            {
                _userContext.Users.Update(user);
                _userContext.SaveChanges();
                return NoContent();
            }
            catch 
            {
                return BadRequest();
            }
        }

        // DELETE: user
        [HttpDelete] 
        public ActionResult DeleteUser(int id) 
        { 
            var userExist = _userContext.Users.Where(u => u.Id == id).FirstOrDefault();
            if(userExist is null) return NotFound();

            try
            {
                _userContext.Users.Remove(userExist);
                return NoContent();
            }
            catch 
            {
                return BadRequest();
            }
        }
    } 
}