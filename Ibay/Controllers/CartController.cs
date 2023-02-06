using ClassIbay;
using IbayApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Ibay.Controllers
{
    public class CartController : Controller
    {
        private IbayContext _cartContext;
        public CartController(IbayContext context)
        {
            _cartContext = context;
        }
        /// <summary>
        /// Récupère la liste de tous les paniers
        /// </summary>
        // GET: cart
        [HttpGet]
        [Route("/cart")]
        public ActionResult<List<Cart>> GetAllCarts() 
        {
            var carts = _cartContext.Carts
                .Include(c => c.User)
                .Include(c => c.CartProducts)
                 .ThenInclude(cp => cp.Product)
                 .Select(cp => new { cp.Id, cp.CartProducts, cp.User })
                .OrderBy(c => c.Id);

            if (carts.Count() == 0)
            {
                return NotFound("No carts found in the database.");
            }
            return Ok(carts);
        }
        /// <summary>
        /// Récupère un panier en fonction de l'ID
        /// </summary>
        // GET: cart/id
        [HttpGet]
        [Route("/cart/id")]
        public ActionResult<Cart> GetCart([FromQuery] int id)
        {
            var cart = _cartContext.Carts
              .Include(c => c.User)
                .Include(c => c.CartProducts)
                 .ThenInclude(cp => cp.Product)
                 .Select(cp => new { cp.Id, cp.CartProducts, cp.User })
                .FirstOrDefault(cp => cp.Id == id);
            if (cart is null)
            {
                return NotFound("No cart found in the database.");
            }
            return Ok(cart);
        }
        /// <summary>
        /// Ajoute un panier
        /// </summary>
        // POST: cart
        [HttpPost]
        [Route("/cart/insert/")]

        public ActionResult CreateCart([FromBody] Cart cart, [FromQuery] int userId)
        {
            var user = _cartContext.Users.Find(userId);
            if (user is null) return BadRequest("User not found");
            try
            {
                cart.User = user;
            _cartContext.Carts.Add(cart);
            _cartContext.SaveChanges();
            }
            catch
            {
                return BadRequest("Failed to create Cart");
            }
            return CreatedAtAction(nameof(GetCart), new { id = cart.Id }, cart);
        }
        /// <summary>
        /// Update un panier en fonction de l'ID
        /// </summary>
        // PUT: cart
        [HttpPut]
        [Route("/cart/update")]
        public ActionResult UpdateCart([FromQuery] int id, [FromBody] Cart cart)
        {
            if (id != cart.Id)
            {
                return BadRequest("ID in request and Cart object are different");
            }
            var cartExist = _cartContext.Carts.Where(c => c.Id == id).FirstOrDefault();
            if (cartExist is null) return NotFound(id);

            try
            {
                _cartContext.Carts.Update(cart);
                _cartContext.SaveChanges();
                return NoContent();
            }
            catch
            {
                return BadRequest("Failed to update Cart");
            }
        }
        /// <summary>
        /// Supprime un panier en fonction de l'ID
        /// </summary>
        // DELETE: cart
        [HttpDelete]
        [Route("/cart/delete/")]
        public ActionResult DeleteCart([FromQuery] int id)
        {
            var cartExist = _cartContext.Carts.Where(c => c.Id == id).FirstOrDefault();
            if (cartExist is null) return NotFound("Cart with id " + id + " not found");

            try
            {
                _cartContext.Carts.Remove(cartExist);
                return NoContent();
            }
            catch
            {
                return BadRequest("Failed to delete Cart");
            }
        }
    }
}
