using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/payments")]
[ApiController]
public class PaymentsController : ControllerBase
{
    [HttpPost("create-payment-intent")]
    public async Task<IActionResult> CreatePaymentIntent([FromBody] PaymentRequest request)
    {
        try
        {
            // Ensure Stripe API key is set
            StripeConfiguration.ApiKey = "keyhere"; // Replace with your test secret key

            var options = new PaymentIntentCreateOptions
            {
                Amount = request.Amount * 100, // Convert LKR to cents
                Currency = "usd",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            };

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent = await service.CreateAsync(options);

            return Ok(new { clientSecret = paymentIntent.ClientSecret });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}

public class PaymentRequest
{
    public int Amount { get; set; } // Amount in LKR
}