using ClassIbay;
using IbayApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ibay.Controllers
{
    public class PaymentController : Controller
    {
        private IbayContext _paymentContext;
        public PaymentController(IbayContext context)
        {
            _paymentContext = context;
        }

        // GET: payment
        [HttpGet]
        public ActionResult<List<Payment>> GetAllPayments()
        {
            return Ok(_paymentContext.Payments
                .Include(p => p.Cart)
                .OrderBy(p => p.Id));
        }

        // GET: payment/id
        [HttpGet]
        [Route("/id")]
        public ActionResult<Payment> GetPayment(int id)
        {
            var payment = _paymentContext.Payments
                .Include(p => p.Cart)
                .FirstOrDefault();
            if (payment is null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        // POST: payment
        [HttpPost]
        public ActionResult CreatePayment(Payment payment)
        {
            _paymentContext.Payments.Add(payment);
            _paymentContext.SaveChanges();
            return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, payment);
        }

        // PUT: payment
        [HttpPut]
        public ActionResult UpdatePayment(int id, Payment payment)
        {
            if (id != payment.Id)
                return BadRequest();
            var paymentExist = _paymentContext.Payments.Where(p => p.Id == id).FirstOrDefault();
            if (paymentExist is null) return NotFound(id);

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

        // DELETE: payment
        [HttpDelete]
        public ActionResult DeletePayment(int id)
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
