using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Controllers
{
   // [Route("api/stripe")]
    public class StripePaymentController : Controller
    {
        [Route("api/stripe")]
        [HttpPost]
        [Obsolete]
        public ActionResult Post([FromBody] StripePaymentRequest paymentRequest)
        {
            StripeConfiguration.SetApiKey("sk_test_51LIH8CBTYy59yxHKoFAztHkuBuE8GV8yX4M4bX39lWY05T14fLSVXfeNrEuEnpJXSCafSuHJblE3gFWiY7E17BW700hpBFUr8p");

            var myCharge = new ChargeCreateOptions();
            myCharge.Source= paymentRequest.tokenId;
            myCharge.Amount = paymentRequest.kolicina;
            myCharge.Currency = "gbp";
            myCharge.Description = paymentRequest.order;
            myCharge.Metadata = new Dictionary<string, string>();
            myCharge.Metadata["OurRef"] = "OurRef-" + Guid.NewGuid().ToString();

            var chargeService = new ChargeService();
            Charge stripeCharge = chargeService.Create(myCharge);

            return Json(stripeCharge);
        }

        public class StripePaymentRequest
        {
            public string tokenId { get; set; }
            public int kolicina { get; set; }
            public string order { get; set; }
        }
    }
}
