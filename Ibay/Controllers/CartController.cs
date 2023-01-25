using ClassIbay;
using IbayApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ibay.Controllers
{
    public class CartController : Controller
    {
        private IbayContext _cartContext;
        public CartController(IbayContext context)
        {
            _cartContext = context;
        }

        // GET: cart
        [HttpGet]
        public ActionResult<List<Cart>> GetAllCarts()
        {
            return Ok(_cartContext.Carts
                .Include(c => c.User)
                .Include(c => c.Products)
                .OrderBy(c => c.Id));
        }

        // GET: cart/id
        [HttpGet]
        [Route("/id")]
        public ActionResult<Cart> GetCart(int id)
        {
            var cart = _cartContext.Carts
                .Include(c => c.User)
                .Include(c => c.Products)
                .FirstOrDefault();
            if (cart is null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        // POST: cart
        [HttpPost]
        public ActionResult CreateCart(Cart cart)
        {
            _cartContext.Carts.Add(cart);
            _cartContext.SaveChanges();
            return CreatedAtAction(nameof(GetCart), new { id = cart.Id }, cart);
        }

        // PUT: cart
        [HttpPut]
        public ActionResult UpdateCart(int id, Cart cart)
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

        // DELETE: cart
        [HttpDelete]
        public ActionResult DeleteCart(int id)
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
