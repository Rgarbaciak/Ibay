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

        /// <summary>
        /// Récupère la liste de tous les utilisateurs
        /// </summary>
        [HttpGet]
        [Route("/user")]
        public ActionResult<List<User>> GetAllUsers()
        {
            var users = _userContext.Users;
            if (users.Count() == 0)
            {
                return NotFound("No users found in the database.");
            }
            return Ok(users);
        }
        /// <summary>
        /// Récupère un utilisateur en fonction de l'ID
        /// </summary>
        // GET: user/id
        [HttpGet]
        [Route("/user/id")]
        public async Task<ActionResult<User>> GetUser([FromQuery] int id)
        {
            var user = await _userContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User with id " + id + " not found");
            }
            return Ok(user);
        }
        /// <summary>
        /// Ajoute un utilisateur 
        /// </summary>
        // POST: user
        [HttpPost]
        [Route("/user/insert")]
        public ActionResult CreateUser([FromBody] User user)
        {
            try
            {
                _userContext.Users.Add(user);
                _userContext.SaveChanges();
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch
            {
                return BadRequest("Failed to create user");
            }
        }
        /// <summary>
        /// Update un Utilisateur en fonction de l'ID
        /// </summary>
        // PUT: user
        [HttpPut]
        [Route("/user/update")]
        public ActionResult UpdateUser([FromQuery]int id, [FromBody]User user)
        {

            if(id != user.Id)
            {
                return BadRequest("ID in request and user object are different");
            }
            var userExist = _userContext.Users.Where(u => u.Id == id).FirstOrDefault();
            if (userExist is null) return NotFound("User with id " + id + " not found");

            try
            {
                _userContext.Users.Update(user);
                _userContext.SaveChanges();
                return NoContent();
            }
            catch 
            {
                return BadRequest("Failed to update user");
            }
        }
        /// <summary>
        /// Supprime un utilisateur en fonction de l'ID
        /// </summary>
        // DELETE: user
        [HttpDelete]
        [Route("/user/delete/id")]
        public ActionResult DeleteUser([FromQuery] int id) 
        { 
            var userExist = _userContext.Users.Where(u => u.Id == id).FirstOrDefault();
            if (userExist is null) return NotFound("User with id " + id + " not found");

            try
            {
                _userContext.Users.Remove(userExist);
                return NoContent();
            }
            catch 
            {
                return BadRequest("Failed to delete user");
            }
        }
    } 
}