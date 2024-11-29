using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Exceptions;
using Microsoft.Extensions.Configuration;

namespace CarRentalSystem.Services
{
    public class SmsService
    {

        private readonly IConfiguration _configuration;
        public SmsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendSms(string toPhoneNumber, string userName, string carMake, int rentDays, decimal totalPrice)
        {
            try
            {
                var accountSid = _configuration["Twilio:AccountSID"];
                var authToken = _configuration["Twilio:AuthToken"];
                var fromPhoneNumber = _configuration["Twilio:FromPhoneNumber"];

                TwilioClient.Init(accountSid, authToken);

                var message = $"Hello {userName},\n Your car {carMake} booking is confired for {rentDays} days.\nTotal Price:${totalPrice}\nThank you for choosing our service!";

                var messageResponse = await MessageResource.CreateAsync(body: message,
                   from: new Twilio.Types.PhoneNumber(fromPhoneNumber),
                   to: new Twilio.Types.PhoneNumber("+91" + toPhoneNumber)
                 );

                //log the response
                Console.WriteLine($"Sms {message} sent with SID:{messageResponse.Sid}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message not sent" + ex.Message);
                return false;

            }
        }

    }
}
