using MailKit.Security;
using MimeKit;

namespace Service.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendEmail(string Email, string Subject, string Html)
        {
                return true;
        }
    }
}
