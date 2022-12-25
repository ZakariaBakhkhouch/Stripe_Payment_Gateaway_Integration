using IntegratingStripePaymentGateaway.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace IntegratingStripePaymentGateaway.Controllers
{
    public class PaymentController : Controller
    {
        [HttpGet]
        public IActionResult Payment()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreatecCheckOutSession(Payment model)
        {
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = Convert.ToInt32(model.Amount) * 100,
                            Currency = model.Currency,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = model.ProductName,
                            },
                            
                        },
                        
                        Quantity = model.Quantity,
                    }
                },
                Mode = "payment",
                SuccessUrl = "https://localhost:7037/Payment/Success",
                CancelUrl = "https://localhost:7037/Payment/Cancel",
                CustomerEmail = "zakabakho@gmail.com"
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);

            return StatusCode(303);
        }


        [HttpPost]
        public IActionResult GetPayment()
        {
            var service = new PaymentIntentService();
            var options = new PaymentIntentCreateOptions
            {
                Amount = 1099,
                SetupFutureUsage = "off_session",
                Currency = "inr",
                PaymentMethod = "pm_card_visa",
            };

            var paymentIntent = service.Create(options);

            return Json(paymentIntent);
        }


        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
        
        
        [HttpGet]
        public IActionResult Cancel()
        {
            return View();
        }
    }
}
