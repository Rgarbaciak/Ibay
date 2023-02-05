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
            return Ok(_cartContext.Carts
                .Include(c => c.User)
                .Include(c => c.CartProducts)
                 .ThenInclude(cp => cp.Product)
                 .Select(cp => new { cp.Id, cp.CartProducts, cp.User })
                .OrderBy(c => c.Id));
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
                return NotFound();
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
            cart.User = user;
            _cartContext.Carts.Add(cart);
            _cartContext.SaveChanges();
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
                return BadRequest();
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
                return BadRequest();
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
            if (cartExist is null) return NotFound();

            try
            {
                _cartContext.Carts.Remove(cartExist);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
