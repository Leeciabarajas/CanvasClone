using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using SeeSharpLMS.Models.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SeeSharpLMS.Models;
using Microsoft.AspNetCore.Identity;
using SeeSharpLMS.Data;

namespace SeeSharpLMS
{
    public class MakePaymentModel : PageModel
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MakePaymentModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }
        public void OnGet()
        {

        }

        //Class to bind and store the entered payment information
        [BindProperty]
        public PaymentInfo PaymentInfo { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            //values of the card to be used for payment
            var cardValues = new Dictionary<string, string>
            {
                { "card[number]", PaymentInfo.CardNumber },
                { "card[exp_month]", PaymentInfo.ExpMonth},
                { "card[exp_year]", PaymentInfo.ExpYear },
                { "card[cvc]", PaymentInfo.CVC}
            };

            //assign post request authorization headers
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "sk_test_1ZLIjXbcdEj6uuj8EzrIcFwH00527VvVTq");

            //create the encoded url content from the post data
            var cardContent = new FormUrlEncodedContent(cardValues);

            //send the post request to get the purchase care todken
            var cardResponse = await client.PostAsync("https://api.stripe.com/v1/tokens", cardContent);

            //store the card token response object
            var cardResponseString = await cardResponse.Content.ReadAsStringAsync();

            //convert the card token response back into JSON
            JObject cardToken = JObject.Parse(cardResponseString);

            //values of the charged payment with the charge token id
            var chargeValues = new Dictionary<string, string>
            {
                { "amount", (PaymentInfo.Amount*100).ToString() },
                { "currency", "usd"},
                { "description", "Test Charge Through LMS" },
                { "source", (string)cardToken.SelectToken("id") }//the token id from the charge
            };

            //create the encoded url content from the post data
            var chargeContent = new FormUrlEncodedContent(chargeValues);

            //send the post request with the payment info to be charged
            var chargeResponse = await client.PostAsync("https://api.stripe.com/v1/charges", chargeContent);

            if(chargeResponse.IsSuccessStatusCode)
            {
                Charge charge = new Charge
                {
                    Amount = -1*(PaymentInfo.Amount),
                    Date = DateTime.Now,
                    Reason = "Student Account Payment",
                    StudentId = _userManager.GetUserId(User)
                };

                _context.Charge.Add(charge);
                _context.SaveChanges();
                return RedirectToPage("PaymentConfirmation", new {success = true });
            }
            else
            {
                return RedirectToPage("PaymentConfirmation", new { success = false });

            }

        }
    }
}



