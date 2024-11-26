
using System.Net.Mail;
using System.Net;

namespace Notifications.API.Services
{
    public class EmailService : IEmailService
    {
        public readonly SmtpSettings settings;
        private readonly ILogger<EmailService> logger;

        public EmailService(ILogger<EmailService> logger, IConfiguration configuration)
        {
            configuration.GetSection("Smtp").Bind(settings);
            logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> SendEmail(string email, string subject, string message)
        {
            // Create a new MailMessage object
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(settings.FromAddress);
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.Body = message;

            // Configure the SMTP client
            SmtpClient smtpClient = new SmtpClient(settings.Server);
            smtpClient.Port = settings.Port;
            smtpClient.Credentials = new NetworkCredential(settings.FromAddress, settings.Password);
            smtpClient.EnableSsl = true;

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogWarning("Error: " + ex.Message);
                return false;
            }
        }
    }
}
