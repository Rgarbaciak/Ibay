using ClassIbay;
using IbayApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Helpers;

namespace Ibay.Controllers
{
    public class PaymentController : Controller
    {
        private IbayContext _paymentContext;
        public PaymentController(IbayContext context)
        {
            _paymentContext = context;
        }
        /// <summary>
        /// Récupère la liste de tous les payments 
        /// </summary>
        // GET: payment
        [HttpGet]
        [Route("/payment")]
        public ActionResult<List<Payment>> GetAllPayments()
        {
            var payments = _paymentContext.Payments
                .Include(p => p.Cart)
                .ThenInclude(u => u.User)
                .Include(p => p.Cart)
                .ThenInclude(c => c.CartProducts)
                .ThenInclude(cp => cp.Product)
                .Select(p => new { p.Id, p.Amount, p.Cart })
                .OrderBy(p => p.Id);
            return Ok(payments);

        }
        /// <summary>
        /// Récupère un payment en fonction de l'id 
        /// </summary>
        // GET: payment/id
        [HttpGet]
        [Route("/payment/id")]
        public ActionResult<Payment> GetPayment([FromQuery] int id)
        {
            var payment = _paymentContext.Payments
                .Include(p => p.Cart)
                .ThenInclude(u => u.User)
                .Include(p => p.Cart)
                .ThenInclude(c => c.CartProducts)
                .ThenInclude(cp => cp.Product)
                .Select(p => new { p.Id, p.Amount, p.Cart })
                .FirstOrDefault(p => p.Id == id);

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);


        }
        /// <summary>
        /// Ajoute un payment
        /// </summary>

        // POST: payment
        [HttpPost]
        [Route("/payment/insert/")]
        public ActionResult CreatePayment([FromBody] Payment payment, [FromQuery] int cartId)
        {
            var cart = _paymentContext.Carts.Find(cartId);
            if (cart is null) return BadRequest("Cart not found");
            payment.Cart = cart;
            _paymentContext.Payments.Add(payment);
            _paymentContext.SaveChanges();
            return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, payment);
        }
        /// <summary>
        /// Update un payment en fonction de l'id 
        /// </summary>
        // PUT: payment
        [HttpPut]
        [Route("/payment/update/")]
        public ActionResult UpdatePayment([FromQuery]int id, [FromBody] Payment payment)
        {
            if (id != payment.Id)
            return BadRequest();
            var paymentExist = _paymentContext.Payments.Where(p => p.Id == id).FirstOrDefault();
            if (paymentExist is null)
            return NotFound(id);

            try
            {
                _paymentContext.Payments.Update(payment);
                _paymentContext.SaveChanges();
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Supprime un payment en fonction de l'id 
        /// </summary>
        // DELETE: payment
        [HttpDelete]
        [Route("/payment/delete/")]
        public ActionResult DeletePayment([FromQuery] int id)
        {
            var paymentExist = _paymentContext.Payments.Where(p => p.Id == id).FirstOrDefault();
            if (paymentExist is null) return NotFound();

            try
            {
                _paymentContext.Payments.Remove(paymentExist);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
